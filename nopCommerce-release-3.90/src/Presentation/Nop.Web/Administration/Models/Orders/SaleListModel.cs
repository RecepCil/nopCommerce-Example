using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Orders
{
    public class SaleListModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Orders.Sales.List.ID")]
        public override int Id { get; set; }


        [NopResourceDisplayName("Admin.Orders.Sales.List.SalesNumber")]
        public int SalesNumber { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.List.TotalSales")]
        public decimal TotalSales { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.List.TotalCost")]
        public decimal TotalCost { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.List.Profit")]
        public decimal Profit { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.List.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.List.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

    }
}