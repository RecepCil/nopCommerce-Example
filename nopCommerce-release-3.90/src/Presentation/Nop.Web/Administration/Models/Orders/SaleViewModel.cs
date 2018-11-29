using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Orders
{
    public class SaleViewModel : BaseNopEntityModel
    {
        public SaleViewModel()
        {
            this.transactions = new List<TransactionModel>();
        }

        public SaleViewModel(DateTime? StartDate, DateTime? EndDate)
        {
            this.StartDate = StartDate;
            this.EndDate = EndDate;

        }

        [NopResourceDisplayName("Admin.Orders.Sales.ID")]
        public override int Id { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.TransactionNumber")]
        public int TransactionNumber { get; set; }   //SalesNumber

        [NopResourceDisplayName("Admin.Orders.Sales.TotalSales")]
        public string TotalSales { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.TotalCost")]
        public string TotalCost { get; set; }

        [NopResourceDisplayName("Admin.Orders.Sales.Profit")]
        public string Profit { get; set; }

        public List<TransactionModel> transactions;

        #region nested TransactionModel

        public partial class TransactionModel : BaseNopEntityModel
        {
            public TransactionModel()
            {
                this.Items = new List<TransactionItemModel>();
            }

            [NopResourceDisplayName("Admin.Orders.Sales.ID")]
            public override int Id { get; set; }

            [NopResourceDisplayName("Admin.Orders.Sales.PaidDate")]
            [UIHint("DateNullable")]
            public DateTime? PaidDate { get; set; }

            [NopResourceDisplayName("Admin.Orders.Sales.List.TotalSales")]
            public decimal TotalSales { get; set; }

            [NopResourceDisplayName("Admin.Orders.Sales.List.TotalCost")]
            public decimal TotalCost { get; set; }

            [NopResourceDisplayName("Admin.Orders.Sales.List.Profit")]
            public decimal Profit { get; set; }

            public List<TransactionItemModel> Items { get; set; }

            #region Nested classes

            public partial class TransactionItemModel : BaseNopEntityModel
            {
                public TransactionItemModel(int productId, string productName, int quantity, decimal originalProductCost)
                {
                    this.ProductId = productId;
                    this.ProductName = productName;
                    this.Quantity = quantity;
                    this.OriginalProductCost = originalProductCost;
                }

                [NopResourceDisplayName("Admin.Orders.Shipments.Products.ProductName")]
                public int ProductId { get; set; }
                [NopResourceDisplayName("Admin.Orders.Shipments.Products.ProductName")]
                public string ProductName { get; set; }
                [NopResourceDisplayName("Admin.Orders.Shipments.Products.ProductName")]
                public int Quantity { get; set; }
                [NopResourceDisplayName("Admin.Orders.Shipments.Products.ProductName")]
                public decimal OriginalProductCost { get; set; }
            }

            #endregion

        }

        #endregion
    }
}