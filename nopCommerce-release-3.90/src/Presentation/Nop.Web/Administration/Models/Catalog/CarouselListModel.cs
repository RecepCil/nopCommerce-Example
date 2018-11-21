using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class CarouselListModel : BaseNopModel
    {
        [NopResourceDisplayName("Admin.Catalog.Carousels.List.SearchLink")]
        [AllowHtml]
        public string SearchLink { get; set; }
        [NopResourceDisplayName("Admin.Catalog.Carousels.List.IsActive")]
        public bool IsActive { get; set; }
        [NopResourceDisplayName("Admin.Catalog.Carousels.List.SearchDate")]
        public DateTime SearchDate { get; set; }

    }
}