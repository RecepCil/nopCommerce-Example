using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class CopyCarouselModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Catalog.Carousels.Copy.Link")]
        [AllowHtml]
        public string Link { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Copy.Path")]
        public bool Path { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Carousels.Copy.IsActive")]
        public bool IsActive { get; set; }
    }
}