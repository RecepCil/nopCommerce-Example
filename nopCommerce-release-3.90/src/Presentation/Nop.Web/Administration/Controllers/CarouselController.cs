using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Services.Security;
using Nop.Admin.Controllers;

namespace Nop.Admin.Controllers
{
    public class CarouselController : BaseAdminController
    {
        #region Fields

        private readonly ICarouselService _carouselService;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Constructors

        public CarouselController(ICarouselService carouselService, IPermissionService permissionService)
        {
            this._carouselService = carouselService;
            this._permissionService = permissionService;
        }

        #endregion

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var model = new CarouselModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CarouselModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //if (ModelState.IsValid)
            //{
            //    var carousel = model.ToEntity();
            //    _carouselService.InsertCarousel(carousel);
            //}
            return RedirectToAction("List");
        }

        //edit carousel
        public virtual ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var carousel = _carouselService.GetCarouselById(id);
            if (carousel == null)
                //No carousel found with the specified id
                return RedirectToAction("List");

            var model = carousel.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual ActionResult Edit(CarouselModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var carousel = _carouselService.GetCarouselById(model.Id);

            if (carousel == null)
                //No carousel found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                carousel = model.ToEntity(carousel);
                _carouselService.UpdateCarousel(carousel);
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var carousel = _carouselService.GetCarouselById(id);
            if (carousel == null)
                //No product found with the specified id
                return RedirectToAction("List");

            _carouselService.DeleteCarousel(carousel);

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                _carouselService.DeleteCarousels(_carouselService.GetCarouselsByIds(selectedIds.ToArray()).ToList());
            }

            return Json(new { Result = true });
        }


        public ActionResult Trial()
        {
            return View();
        }


    }
}