using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Controllers
{
    public class CarouselController : BaseAdminController
    {
        #region Fields

        private readonly ICarouselService _carouselService;
        private readonly ICopyCarouselService _copyCarouselService;
        private readonly IPdfService _pdfService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructors

        public CarouselController(ICarouselService carouselService, ICopyCarouselService copyCarouselService, IPdfService pdfService, 
            IPermissionService permissionService, ICustomerActivityService customerActivityService, ILocalizationService localizationService)
        {
            this._carouselService = carouselService;
            this._copyCarouselService = copyCarouselService;
            this._pdfService = pdfService;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
            this._localizationService = localizationService;
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

        //list carousels
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var model = new CarouselListModel();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult CarouselList(DataSourceRequest command, CarouselListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var carousels = _carouselService.SearchCarousel(model.SearchDate, model.IsActive);

            var gridModel = new DataSourceResult();

            gridModel.Data = carousels.Select(x =>
            {
                var carouselModel = x.ToModel();
                return carouselModel;
            });

            gridModel.Total = carousels.Count();

            return Json(gridModel);
        }

        //create product
        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var model = new CarouselModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CarouselModel model, bool continueEditing, HttpPostedFileBase Image)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            if (Image != null)
            {
                // If carousel has animage
                var path = SaveImage(Image, HttpContext);
                model.Path = path;
            }

            if (ModelState.IsValid)
            {
                model.AdditionDate = DateTime.Now;

                //carousel
                var entity = model.ToEntity();
                _carouselService.InsertCarousel(entity);

                //activity log
                _customerActivityService.InsertActivity("AddNewCarousel", _localizationService.GetResource("ActivityLog.AddNewCarousel"), model.Link);

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.Carousels.Added"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = entity.Id });
                }
            }

            return RedirectToAction("List");
        }

        //edit carousel
        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var entity = _carouselService.GetCarouselById(id);
            if (entity == null)
                //No carousel found with the specified id
                return RedirectToAction("List");

            var model = entity.ToModel();
            return View(model);
        }

        //Buttonun name'i save-continue ise, continueEditing True oluyor
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CarouselModel model, HttpPostedFileBase Image, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var entity = _carouselService.GetCarouselById(model.Id);

            if (entity == null) 
                //No product found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                if (Image != null)  
                {
                    //If we get a new image
                    model.Path = SaveImage(Image, HttpContext);
                }
                else    
                {
                    //Continue to use old image
                    model.Path = entity.Path;
                }

                //carousel
                entity = model.ToEntity(entity);

                _carouselService.UpdateCarousel(entity);
                
                //activity log
                _customerActivityService.InsertActivity("EditCarousel", _localizationService.GetResource("ActivityLog.EditCarousel"), entity.Id);

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.Carousels.Updated"));

                if (!continueEditing)

                    return RedirectToAction("List");

            }
            return View(model);

        }

        //delete carousel
        [HttpPost] // ???
        public virtual ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            var entity = _carouselService.GetCarouselById(id);

            if (entity == null)
                //No carousel found with the specified id
                return RedirectToAction("List");

            _carouselService.DeleteCarousel(entity);

            //activity log
            _customerActivityService.InsertActivity("DeleteCarousel", _localizationService.GetResource("ActivityLog.DeleteCarousel"), entity.Link);

            SuccessNotification(_localizationService.GetResource("Admin.Catalog.Carousels.Deleted"));

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                _carouselService.DeleteCarousels(_carouselService.GetCarouselsByIds(selectedIds.ToArray()).ToList());
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public virtual ActionResult CopyCarousel(CarouselModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCarousels))
                return AccessDeniedView();

            try
            {
                var entity = _carouselService.GetCarouselById(model.Id);

                var newCarousel = _copyCarouselService.CopyCarousel(entity);

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.Carousels.Copied"));
                return RedirectToAction("Edit", new { id = newCarousel.Id });
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = model.Id });
            }
        }

        #endregion

        #region Export / Import

        [HttpPost, ActionName("List")]
        [FormValueRequired("download-catalog-pdf")]
        public virtual ActionResult DownloadCatalogAsPdf(CarouselModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //Get all carousels
            var entities = _carouselService.SearchCarousel(default(DateTime),false);

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
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #endregion

        #region Show On The Page

        public ActionResult CarouselBlock()
        {
            var carousel = _carouselService.GetBlockCarousels();
            List<CarouselModel> carouselModel = new List<CarouselModel>();

            foreach (var item in carousel)
            {
                carouselModel.Add(item.ToModel());
            }

            return View(carouselModel);
        }

        #endregion

        #endregion

    }
}