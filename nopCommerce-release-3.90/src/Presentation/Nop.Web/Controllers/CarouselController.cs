using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Nop.Services.Common;
using Nop.Core;

namespace Nop.Web.Controllers
{
    public class CarouselController : BaseController
    {
        #region Fields

        private readonly ICarouselService _carouselService;
        private readonly ICopyCarouselService _copyCarouselService;
        private readonly IPdfService _pdfService;

        #endregion

        #region Constructors

        public CarouselController(ICarouselService carouselService, ICopyCarouselService copyCarouselService, IPdfService pdfService)
        {
            this._carouselService = carouselService;
            this._copyCarouselService = copyCarouselService;
            this._pdfService = pdfService;
        }

        #endregion

        #region Utilities

        public static string SaveImage(HttpPostedFileBase Image, HttpContextBase ctx)
        {
            //new name of image (prevention of getting same name for different images)
            string newName = Path.GetFileNameWithoutExtension(Image.FileName) + "-" + Guid.NewGuid() + Path.GetExtension(Image.FileName);

            //get original image and turn it to bitmap
            Image orjImg = System.Drawing.Image.FromStream(Image.InputStream);
            Bitmap Img = new Bitmap(orjImg);

            //path+name
            string path = "/Content/Images/Carousel/" + newName;

            // Finding format of image
            ImageFormat format;
            switch (Image.ContentType)
            {
                case "image/png":
                    format = ImageFormat.Png;
                    break;
                case "image/gif":
                    format = ImageFormat.Gif;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }

            //save bitmap
            Img.Save(ctx.Server.MapPath("~/Content/Images/Carousel/") + newName, format);

            return path;
        }

        #endregion

        #region Methods

        #region Carousel list / create / edit / delete / copy

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new CarouselListModel();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult CarouselList(DataSourceRequest command, CarouselListModel model)
        {
             var carousels = _carouselService.GetAllCarousels();
            //var carousels = _carouselService.SearchCarousels(
            //    Link: "JerrySmith",
            //    IsActive: true,
            //    SearchDate: DateTime.UtcNow
            //      );

            var gridModel = new DataSourceResult();

            gridModel.Data = carousels.Select(x =>
            {
                var carouselModel = x.ToModel();
                return carouselModel;
            });

            gridModel.Total = carousels.Count();

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            var model = new CarouselModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CarouselModel model, HttpPostedFileBase Image)
        {
            if (Image != null) // If carousel has animage
            {
                var path = SaveImage(Image, HttpContext);
                model.Path = path;
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                _carouselService.InsertCarousel(entity);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var entity = _carouselService.GetCarouselById(id);
            var model = entity.ToModel();
            return View(model);
        }

        //Buttonun name'i save-continue ise, continueEditing True oluyor
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CarouselModel model, HttpPostedFileBase Image, bool continueEditing)
        {
            var entity = _carouselService.GetCarouselById(model.Id);

            if (entity == null) //No product found with the specified id

                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                if (Image != null)  // If we get a new image
                {
                    model.Path = SaveImage(Image, HttpContext);
                }
                else    //  Continue to use old image
                {
                    model.Path = entity.Path;
                }

                entity = model.ToEntity(entity);

                _carouselService.UpdateCarousel(entity);

                if (!continueEditing)

                    return RedirectToAction("List");

            }
            return View(model);

        }

        public virtual ActionResult Delete(int id)
        {
            var entity = _carouselService.GetCarouselById(id);

            if (entity == null)    //No carousel found with the specified id

                return RedirectToAction("List");

            _carouselService.DeleteCarousel(entity);

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (selectedIds != null)
            {
                _carouselService.DeleteCarousels(_carouselService.GetCarouselsByIds(selectedIds.ToArray()).ToList());
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public virtual ActionResult CopyCarousel(CarouselModel model)
        {
            try
            {
                var entity = _carouselService.GetCarouselById(model.Id);

                var newCarousel = _copyCarouselService.CopyCarousel(entity);

                return RedirectToAction("Edit", new { id = newCarousel.Id });
            }
            catch 
            {
                return RedirectToAction("Edit", new { id = model.Id });
            }
        }

        #endregion

        #region Export / Import

        [HttpPost, ActionName("List")]
        [FormValueRequired("download-catalog-pdf")]
        public virtual ActionResult DownloadCatalogAsPdf(CarouselModel model)
        {
            var entities = _carouselService.GetAllCarousels();

            try
            {
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    _pdfService.PrintCarouselsToPdf(stream, entities);
                    bytes = stream.ToArray();
                }
                return File(bytes, MimeTypes.ApplicationPdf, "pdfcatalog.pdf");
            }
            catch 
            {
                return RedirectToAction("List");
            }
        }

        #endregion
        [HttpPost, ActionName("List")]
        [FormValueRequired("go-to-product-by-sku")]
        public virtual ActionResult GoToSku(CarouselListModel model)
        {
            // İsmini değiştr-tüm  columnları kapsaacak bir şey olsun
            // Gelen tek bir property'e göre carouselleri döndür
            // 3'ü için de yap
            // Sonra bunları birleştir (Join falan)
            // Sonucu döndür

            string sku = "asd";



            //not found
            return RedirectToAction("CarouselList");
        }
        #endregion
    }
}