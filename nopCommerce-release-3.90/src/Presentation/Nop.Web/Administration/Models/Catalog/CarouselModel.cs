using FluentValidation.Attributes;
using Nop.Admin.Validators.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Models.Catalog;

namespace Nop.Admin.Models.Catalog
{
    [Validator(typeof(CarouselValidator))]
    public partial class CarouselModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.Id")]
        public override int Id { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.Link")]
        [AllowHtml]
        public string Link { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.Path")]
        [AllowHtml]
        public string Path { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.DisplayOrder")]
        [AllowHtml]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.IsActive")]
        [AllowHtml]
        public bool IsActive { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.StartDate")]
        [AllowHtml]
        public DateTime StartDate { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.FinishDate")]
        [AllowHtml]
        public DateTime FinishDate { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Fields.AdditionDate")]
        [AllowHtml]
        public DateTime AdditionDate { get; set; }
    }
}