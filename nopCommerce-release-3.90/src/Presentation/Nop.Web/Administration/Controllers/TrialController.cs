using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Controllers;
using Nop.Admin.Extensions;
using Nop.Admin.Helpers;
using Nop.Admin.Infrastructure.Cache;
using Nop.Admin.Models.Catalog;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Date;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Messages;
using Nop.Core;
using Nop.Core.Domain.Messages;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;

//Nop.Web.Administration.Controllers
namespace Nop.Admin.Controllers
{
    public partial class TrialController : BaseAdminController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICustomerService _customerService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IPictureService _pictureService;
        private readonly ITaxCategoryService _taxCategoryService;
        private readonly IProductTagService _productTagService;
        private readonly ICopyProductService _copyProductService;
        private readonly IPdfService _pdfService;
        private readonly IExportManager _exportManager;
        private readonly IImportManager _importManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;
        private readonly IAclService _aclService;
        private readonly IStoreService _storeService;
        private readonly IOrderService _orderService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IVendorService _vendorService;
        private readonly IDateRangeService _dateRangeService;
        private readonly IShippingService _shippingService;
        private readonly IShipmentService _shipmentService;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly IMeasureService _measureService;
        private readonly MeasureSettings _measureSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDiscountService _discountService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IDownloadService _downloadService;
        private readonly ISettingService _settingService;
        private readonly TaxSettings _taxSettings;
        private readonly VendorSettings _vendorSettings;

        #endregion
        #region Constructors

        public TrialController(IProductService productService,
            IProductTemplateService productTemplateService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            ISpecificationAttributeService specificationAttributeService,
            IPictureService pictureService,
            ITaxCategoryService taxCategoryService,
            IProductTagService productTagService,
            ICopyProductService copyProductService,
            IPdfService pdfService,
            IExportManager exportManager,
            IImportManager importManager,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IAclService aclService,
            IStoreService storeService,
            IOrderService orderService,
            IStoreMappingService storeMappingService,
            IVendorService vendorService,
            IDateRangeService dateRangeService,
            IShippingService shippingService,
            IShipmentService shipmentService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IMeasureService measureService,
            MeasureSettings measureSettings,
            ICacheManager cacheManager,
            IDateTimeHelper dateTimeHelper,
            IDiscountService discountService,
            IProductAttributeService productAttributeService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            IShoppingCartService shoppingCartService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IDownloadService downloadService,
            ISettingService settingService,
            TaxSettings taxSettings,
            VendorSettings vendorSettings)
        {
            this._productService = productService;
            this._productTemplateService = productTemplateService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._customerService = customerService;
            this._urlRecordService = urlRecordService;
            this._workContext = workContext;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._specificationAttributeService = specificationAttributeService;
            this._pictureService = pictureService;
            this._taxCategoryService = taxCategoryService;
            this._productTagService = productTagService;
            this._copyProductService = copyProductService;
            this._pdfService = pdfService;
            this._exportManager = exportManager;
            this._importManager = importManager;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
            this._aclService = aclService;
            this._storeService = storeService;
            this._orderService = orderService;
            this._storeMappingService = storeMappingService;
            this._vendorService = vendorService;
            this._dateRangeService = dateRangeService;
            this._shippingService = shippingService;
            this._shipmentService = shipmentService;
            this._currencyService = currencyService;
            this._currencySettings = currencySettings;
            this._measureService = measureService;
            this._measureSettings = measureSettings;
            this._cacheManager = cacheManager;
            this._dateTimeHelper = dateTimeHelper;
            this._discountService = discountService;
            this._productAttributeService = productAttributeService;
            this._backInStockSubscriptionService = backInStockSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._productAttributeFormatter = productAttributeFormatter;
            this._productAttributeParser = productAttributeParser;
            this._downloadService = downloadService;
            this._settingService = settingService;
            this._taxSettings = taxSettings;
            this._vendorSettings = vendorSettings;
        }

        #endregion       
        #region Utilities

        [NonAction]
        protected virtual void UpdateLocales(Product product, ProductModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.ShortDescription,
                                                               localized.ShortDescription,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.FullDescription,
                                                               localized.FullDescription,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.MetaKeywords,
                                                               localized.MetaKeywords,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.MetaDescription,
                                                               localized.MetaDescription,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                                                               x => x.MetaTitle,
                                                               localized.MetaTitle,
                                                               localized.LanguageId);

                //search engine name
                var seName = product.ValidateSeName(localized.SeName, localized.Name, false);
                _urlRecordService.SaveSlug(product, seName, localized.LanguageId);
            }
        }

        [NonAction]
        protected virtual void UpdateLocales(ProductTag productTag, ProductTagModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(productTag,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);
            }
        }

        [NonAction]
        protected virtual void UpdateLocales(ProductAttributeValue pav, ProductModel.ProductAttributeValueModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(pav,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);
            }
        }

        [NonAction]
        protected virtual void UpdatePictureSeoNames(Product product)
        {
            foreach (var pp in product.ProductPictures)
                _pictureService.SetSeoFilename(pp.PictureId, _pictureService.GetPictureSeName(product.Name));
        }

        [NonAction] //action methodlarının dışarıdan view ile görüntülenmesini istemiyorsak
        protected virtual void PrepareAclModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(product).ToList();

            var allRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var role in allRoles)
            {
                model.AvailableCustomerRoles.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                });
            }
        }

        [NonAction]
        protected virtual void SaveProductAcl(Product product, ProductModel model)
        {
            product.SubjectToAcl = model.SelectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(product);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(product, customerRole.Id);
                }
                else
                {
                    //remove role
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        _aclService.DeleteAclRecord(aclRecordToDelete);
                }
            }
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(product).ToList();

            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                model.AvailableStores.Add(new SelectListItem
                {
                    Text = store.Name,
                    Value = store.Id.ToString(),
                    Selected = model.SelectedStoreIds.Contains(store.Id)
                });
            }
        }

        [NonAction]
        protected virtual void SaveStoreMappings(Product product, ProductModel model)
        {
            product.LimitedToStores = model.SelectedStoreIds.Any();

            var existingStoreMappings = _storeMappingService.GetStoreMappings(product);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(product, store.Id);
                }
                else
                {
                    //remove store
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        _storeMappingService.DeleteStoreMapping(storeMappingToDelete);
                }
            }
        }

        [NonAction]
        protected virtual void PrepareCategoryMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedCategoryIds = _categoryService.GetProductCategoriesByProductId(product.Id, true).Select(c => c.CategoryId).ToList();

            var allCategories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var c in allCategories)
            {
                c.Selected = model.SelectedCategoryIds.Contains(int.Parse(c.Value));
                model.AvailableCategories.Add(c);
            }
        }

        [NonAction]
        protected virtual void SaveCategoryMappings(Product product, ProductModel model)
        {
            var existingProductCategories = _categoryService.GetProductCategoriesByProductId(product.Id, true);

            //delete categories
            foreach (var existingProductCategory in existingProductCategories)
                if (!model.SelectedCategoryIds.Contains(existingProductCategory.CategoryId))
                    _categoryService.DeleteProductCategory(existingProductCategory);

            //add categories
            foreach (var categoryId in model.SelectedCategoryIds)
                if (existingProductCategories.FindProductCategory(product.Id, categoryId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingCategoryMapping = _categoryService.GetProductCategoriesByCategoryId(categoryId, showHidden: true);
                    if (existingCategoryMapping.Any())
                        displayOrder = existingCategoryMapping.Max(x => x.DisplayOrder) + 1;
                    _categoryService.InsertProductCategory(new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                        DisplayOrder = displayOrder
                    });
                }
        }

        [NonAction]
        protected virtual void PrepareManufacturerMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedManufacturerIds = _manufacturerService.GetProductManufacturersByProductId(product.Id, true).Select(c => c.ManufacturerId).ToList();

            var allManufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, _cacheManager, true);
            foreach (var m in allManufacturers)
            {
                m.Selected = model.SelectedManufacturerIds.Contains(int.Parse(m.Value));
                model.AvailableManufacturers.Add(m);
            }
        }

        [NonAction]
        protected virtual void SaveManufacturerMappings(Product product, ProductModel model)
        {
            var existingProductManufacturers = _manufacturerService.GetProductManufacturersByProductId(product.Id, true);

            //delete manufacturers
            foreach (var existingProductManufacturer in existingProductManufacturers)
                if (!model.SelectedManufacturerIds.Contains(existingProductManufacturer.ManufacturerId))
                    _manufacturerService.DeleteProductManufacturer(existingProductManufacturer);

            //add manufacturers
            foreach (var manufacturerId in model.SelectedManufacturerIds)
                if (existingProductManufacturers.FindProductManufacturer(product.Id, manufacturerId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingManufacturerMapping = _manufacturerService.GetProductManufacturersByManufacturerId(manufacturerId, showHidden: true);
                    if (existingManufacturerMapping.Any())
                        displayOrder = existingManufacturerMapping.Max(x => x.DisplayOrder) + 1;
                    _manufacturerService.InsertProductManufacturer(new ProductManufacturer()
                    {
                        ProductId = product.Id,
                        ManufacturerId = manufacturerId,
                        DisplayOrder = displayOrder
                    });
                }
        }

        [NonAction]
        protected virtual void PrepareDiscountMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedDiscountIds = product.AppliedDiscounts.Select(d => d.Id).ToList();

            foreach (var discount in _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true))
            {
                model.AvailableDiscounts.Add(new SelectListItem
                {
                    Text = discount.Name,
                    Value = discount.Id.ToString(),
                    Selected = model.SelectedDiscountIds.Contains(discount.Id)
                });
            }
        }

        [NonAction]
        protected virtual void SaveDiscountMappings(Product product, ProductModel model)
        {
            var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);

            foreach (var discount in allDiscounts)
            {
                if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                {
                    //new discount
                    if (product.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                        product.AppliedDiscounts.Add(discount);
                }
                else
                {
                    //remove discount
                    if (product.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                        product.AppliedDiscounts.Remove(discount);
                }
            }

            _productService.UpdateProduct(product);
            _productService.UpdateHasDiscountsApplied(product);
        }

        [NonAction]
        protected virtual void PrepareAddProductAttributeCombinationModel(AddProductAttributeCombinationModel model, Product product)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (product == null)
                throw new ArgumentNullException("product");

            model.ProductId = product.Id;
            model.StockQuantity = 10000;
            model.NotifyAdminForQuantityBelow = 1;

            var attributes = _productAttributeService.GetProductAttributeMappingsByProductId(product.Id)
                //ignore non-combinable attributes for combinations
                .Where(x => !x.IsNonCombinable())
                .ToList();
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddProductAttributeCombinationModel.ProductAttributeModel
                {
                    Id = attribute.Id,
                    ProductAttributeId = attribute.ProductAttributeId,
                    Name = attribute.ProductAttribute.Name,
                    TextPrompt = attribute.TextPrompt,
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = _productAttributeService.GetProductAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new AddProductAttributeCombinationModel.ProductAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.Name,
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                model.ProductAttributes.Add(attributeModel);
            }
        }

        [NonAction]
        protected virtual string[] ParseProductTags(string productTags)
        {
            var result = new List<string>();
            if (!String.IsNullOrWhiteSpace(productTags))
            {
                string[] values = productTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string val1 in values)
                    if (!String.IsNullOrEmpty(val1.Trim()))
                        result.Add(val1.Trim());
            }
            return result.ToArray();
        }

        [NonAction]   // modeli hazırla. içine attributeleri vs ata. ilişkili olduğu tablolardan(productAttribute, spesificationAttribute vs) veri çek
        protected virtual void PrepareProductModel(ProductModel model, Product product, bool setPredefinedValues, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (product != null)    // db'den al, view'deki modele ver.
            {
                //ürünün ilişkili olduğu üst ürün var mı?
                var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
                if (parentGroupedProduct != null)
                {
                    model.AssociatedToProductId = product.ParentGroupedProductId;
                    model.AssociatedToProductName = parentGroupedProduct.Name;
                }

                // zamanın tipini Unspecified'den Utc'ye dönüştürdük
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;

            //little performance hack here
            //there's no need to load attributes when creating a new product
            //anyway they're not used (you need to save a product before you map them)
            if (product != null)
            {
                // ProductAttributes tablosunun içindeki tüm propertyleri modelin AvailableProductAttributes'lerine ata
                foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
                {
                    model.AvailableProductAttributes.Add(new SelectListItem
                    {
                        Text = productAttribute.Name,
                        Value = productAttribute.Id.ToString()
                    });
                }

                // içerdeki döngüden dönen değeri de model'in AddSpecificationAttributeModel.AvailableAttributes'ine ata
                model.AddSpecificationAttributeModel.AvailableAttributes = _cacheManager
                    .Get(ModelCacheEventConsumer.SPEC_ATTRIBUTES_MODEL_KEY, () =>
                    {
                        // SpecificationAttribute tablosunun içindeki tüm propertyleri availableSpecificationAttributes içine ata
                        var availableSpecificationAttributes = new List<SelectListItem>();
                        foreach (var sa in _specificationAttributeService.GetSpecificationAttributes())
                        {
                            availableSpecificationAttributes.Add(new SelectListItem
                            {
                                Text = sa.Name,
                                Value = sa.Id.ToString()
                            });
                        }
                        return availableSpecificationAttributes;
                    });

                //options of preselected specification attribute
                // dönen değerleri anlamlandıramadım
                //13.0          13.3         14.0
                if (model.AddSpecificationAttributeModel.AvailableAttributes.Any())
                {
                    var selectedAttributeId = int.Parse(model.AddSpecificationAttributeModel.AvailableAttributes.First().Value);
                    foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(selectedAttributeId))
                        model.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem
                        {
                            Text = sao.Name,
                            Value = sao.Id.ToString()
                        });
                }
                //default specs values
                model.AddSpecificationAttributeModel.ShowOnProductPage = true;
            }

            //copy product
            if (product != null)
            {
                model.CopyProductModel.Id = product.Id;
                model.CopyProductModel.Name = string.Format(_localizationService.GetResource("Admin.Catalog.Products.Copy.Name.New"), product.Name);
                model.CopyProductModel.Published = true;
                model.CopyProductModel.CopyImages = true;
            }

            //templates
            // return value: Simple Product-Grouped Product 
            var templates = _productTemplateService.GetAllProductTemplates();
            foreach (var template in templates)
            {
                model.AvailableProductTemplates.Add(new SelectListItem
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                });
            }

            //supported product types
            foreach (var productType in ProductType.SimpleProduct.ToSelectList(false).ToList())
            {
                var productTypeId = int.Parse(productType.Value);
                model.ProductsTypesSupportedByProductTemplates.Add(productTypeId, new List<SelectListItem>());
                foreach (var template in templates)
                {
                    if (String.IsNullOrEmpty(template.IgnoredProductTypes) ||
                        !((IList<int>)TypeDescriptor.GetConverter(typeof(List<int>)).ConvertFrom(template.IgnoredProductTypes)).Contains(productTypeId))
                    {
                        model.ProductsTypesSupportedByProductTemplates[productTypeId].Add(new SelectListItem
                        {
                            Text = template.Name,
                            Value = template.Id.ToString()
                        });
                    }
                }
            }

            //vendors
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AvailableVendors.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = SelectListHelper.GetVendorList(_vendorService, _cacheManager, true);
            foreach (var v in vendors)
                model.AvailableVendors.Add(v);

            //delivery dates(teslim tarihleri)
            model.AvailableDeliveryDates.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"),
                Value = "0"
            });
            var deliveryDates = _dateRangeService.GetAllDeliveryDates();
            foreach (var deliveryDate in deliveryDates)
            {
                model.AvailableDeliveryDates.Add(new SelectListItem
                {
                    Text = deliveryDate.Name,
                    Value = deliveryDate.Id.ToString()
                });
            }

            //product availability ranges
            model.AvailableProductAvailabilityRanges.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.ProductAvailabilityRange.None"),
                Value = "0"
            });
            foreach (var range in _dateRangeService.GetAllProductAvailabilityRanges())
            {
                model.AvailableProductAvailabilityRanges.Add(new SelectListItem
                {
                    Text = range.Name,
                    Value = range.Id.ToString()
                });
            }

            //warehouses
            var warehouses = _shippingService.GetAllWarehouses();
            model.AvailableWarehouses.Add(new SelectListItem    // bu kısım warehouses değişkenine eklenmiyor
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
                Value = "0"
            });
            foreach (var warehouse in warehouses)
            {
                model.AvailableWarehouses.Add(new SelectListItem
                {
                    Text = warehouse.Name,
                    Value = warehouse.Id.ToString()
                });
            }

            //multiple warehouses (warehouses'la aynı valuelar,localizationservicele gelen hariç)
            foreach (var warehouse in warehouses)
            {
                var pwiModel = new ProductModel.ProductWarehouseInventoryModel
                {
                    WarehouseId = warehouse.Id,
                    WarehouseName = warehouse.Name
                };
                if (product != null)
                {
                    var pwi = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                    if (pwi != null)
                    {
                        pwiModel.WarehouseUsed = true;
                        pwiModel.StockQuantity = pwi.StockQuantity;
                        pwiModel.ReservedQuantity = pwi.ReservedQuantity;
                        pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(product, pwi.WarehouseId, true, true);
                    }
                }
                model.ProductWarehouseInventoryModels.Add(pwiModel);
            }

            //product tags
            if (product != null)
            {
                var result = new StringBuilder();
                for (int i = 0; i < product.ProductTags.Count; i++)
                {
                    var pt = product.ProductTags.ToList()[i];
                    result.Append(pt.Name);
                    if (i != product.ProductTags.Count - 1)
                        result.Append(", ");
                }
                model.ProductTags = result.ToString();
            }

            //tax categories
            var taxCategories = _taxCategoryService.GetAllTaxCategories();
            model.AvailableTaxCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Configuration.Settings.Tax.TaxCategories.None"), Value = "0" });
            foreach (var tc in taxCategories)
                model.AvailableTaxCategories.Add(new SelectListItem { Text = tc.Name, Value = tc.Id.ToString(), Selected = product != null && !setPredefinedValues && tc.Id == product.TaxCategoryId });

            //baseprice units       HATA MI VAR? (baseUnits ile Units?)
            var measureWeights = _measureService.GetAllMeasureWeights();
            foreach (var mw in measureWeights)
                model.AvailableBasepriceUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceUnitId });
            foreach (var mw in measureWeights)
                model.AvailableBasepriceBaseUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceBaseUnitId });

            //last stock quantity
            if (product != null)
            {
                model.LastStockQuantity = product.StockQuantity;
            }

            //default values
            if (setPredefinedValues)
            {
                model.MaximumCustomerEnteredPrice = 1000;
                model.MaxNumberOfDownloads = 10;
                model.RecurringCycleLength = 100;
                model.RecurringTotalCycles = 10;
                model.RentalPriceLength = 1;
                model.StockQuantity = 10000;
                model.NotifyAdminForQuantityBelow = 1;
                model.OrderMinimumQuantity = 1;
                model.OrderMaximumQuantity = 10000;
                model.TaxCategoryId = _taxSettings.DefaultTaxCategoryId;
                model.UnlimitedDownloads = true;
                model.IsShipEnabled = true;
                model.AllowCustomerReviews = true;
                model.Published = true;
                model.VisibleIndividually = true;
            }

            //editor settings
            var productEditorSettings = _settingService.LoadSetting<ProductEditorSettings>();
            model.ProductEditorSettingsModel = productEditorSettings.ToModel();
        }

        [NonAction]     // parentCategory'e göre ÖZYİNELEMELİ olarak categoryleri getir
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            // Sanırım bu şekilde olmamasının sebebi farklı parentCategoryId'lere göre de istekte bulunulabilmesi
            //List<int> categoriesIds2 =(List<int>) _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);  HATALI

            var categoriesIds = new List<int>();
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }

        [NonAction]
        protected virtual void SaveProductWarehouseInventory(Product product, ProductModel model)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (model.ManageInventoryMethodId != (int)ManageInventoryMethod.ManageStock)
                return;

            if (!model.UseMultipleWarehouses)
                return;

            var warehouses = _shippingService.GetAllWarehouses();

            foreach (var warehouse in warehouses)
            {
                //parse stock quantity
                int stockQuantity = 0;
                foreach (string formKey in this.Request.Form.AllKeys)
                    if (formKey.Equals(string.Format("warehouse_qty_{0}", warehouse.Id), StringComparison.InvariantCultureIgnoreCase))
                    {
                        int.TryParse(this.Request.Form[formKey], out stockQuantity);
                        break;
                    }
                //parse reserved quantity
                int reservedQuantity = 0;
                foreach (string formKey in this.Request.Form.AllKeys)
                    if (formKey.Equals(string.Format("warehouse_reserved_{0}", warehouse.Id), StringComparison.InvariantCultureIgnoreCase))
                    {
                        int.TryParse(this.Request.Form[formKey], out reservedQuantity);
                        break;
                    }
                //parse "used" field
                bool used = false;
                foreach (string formKey in this.Request.Form.AllKeys)
                    if (formKey.Equals(string.Format("warehouse_used_{0}", warehouse.Id), StringComparison.InvariantCultureIgnoreCase))
                    {
                        int tmp;
                        int.TryParse(this.Request.Form[formKey], out tmp);
                        used = tmp == warehouse.Id;
                        break;
                    }

                //quantity change history message
                var message = string.Format("{0} {1}", _localizationService.GetResource("Admin.StockQuantityHistory.Messages.MultipleWarehouses"),
                        _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));

                var existingPwI = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                if (existingPwI != null)
                {
                    if (used)
                    {
                        var previousStockQuantity = existingPwI.StockQuantity;

                        //update existing record
                        existingPwI.StockQuantity = stockQuantity;
                        existingPwI.ReservedQuantity = reservedQuantity;
                        _productService.UpdateProduct(product);

                        //quantity change history
                        _productService.AddStockQuantityHistoryEntry(product, existingPwI.StockQuantity - previousStockQuantity, existingPwI.StockQuantity,
                            existingPwI.WarehouseId, message);
                    }
                    else
                    {
                        //delete. no need to store record for qty 0
                        _productService.DeleteProductWarehouseInventory(existingPwI);

                        //quantity change history
                        _productService.AddStockQuantityHistoryEntry(product, -existingPwI.StockQuantity, 0, existingPwI.WarehouseId, message);
                    }
                }
                else
                {
                    if (used)
                    {
                        //no need to insert a record for qty 0
                        existingPwI = new ProductWarehouseInventory
                        {
                            WarehouseId = warehouse.Id,
                            ProductId = product.Id,
                            StockQuantity = stockQuantity,
                            ReservedQuantity = reservedQuantity
                        };
                        product.ProductWarehouseInventory.Add(existingPwI);
                        _productService.UpdateProduct(product);

                        //quantity change history
                        _productService.AddStockQuantityHistoryEntry(product, existingPwI.StockQuantity, existingPwI.StockQuantity,
                            existingPwI.WarehouseId, message);
                    }
                }
            }
        }

        #endregion
        #region Crud

        public ActionResult Hata()
        {
            return AccessDeniedView();      //  extends from BaseAdminController        (/Security/AccessDenied)
        }

        public virtual ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))              // Yetkinlik kontrolü(Hangi yetkinlik-Admin Area. Manage Products)
                return AccessDeniedView();                                                             //erişim engellendi çıktısı				BaseAdminController -> SecurityController(AccessDenied)

            var model = new ProductListModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;                              // Giriş yapan satıcı mı?
            model.AllowVendorsToImportProducts = _vendorSettings.AllowVendorsToImportProducts;          // Satıcılar ürün ekleyebiliyor muydu?

            //categories
            model.AvailableCategories.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.All"),  //çalışma dili null'sa "" döner yoksa "All" döndü
                Value = "0"
            });                                                                //Önbellek
            var categories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);      //Get all categories and add them to model.AvailableCategories
            foreach (var c in categories)
                model.AvailableCategories.Add(c);

            //manufacturers(üretici firmalar)
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });    // categories ile aynı işlemler
            var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, _cacheManager, true);
            foreach (var m in manufacturers)
                model.AvailableManufacturers.Add(m);

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });         // categories ile aynı işlemler
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses(depolar)
            model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });         // categories ile aynı işlemler
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors(satıcı)
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });            // categories ile aynı işlemler
            var vendors = SelectListHelper.GetVendorList(_vendorService, _cacheManager, true);
            foreach (var v in vendors)
                model.AvailableVendors.Add(v);

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();       // Product Types(Default): Simple-5, Grouped-10
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });   // Product Types: All-0, Simple, Grouped

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });                            // AvailablePublishedOptions.Add(All)
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });                  // AvailablePublishedOptions.Add(PublishedOnly)
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });                // AvailablePublishedOptions.Add(UnpublishedOnly)

            return View(model);
        }

        //Command: Page ve PageSize(kaç adet product) bilgilerini içeriyor
        //model, bildiğimiz model ama içeriği null
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            /**/
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedKendoGridJson(); //BaseAdminController'dan geliyor

            //a vendor should have access only to his products
            //Satıcı varsa id'sini model.SearchVendorId'ye ata
            if (_workContext.CurrentVendor != null)
            {
                model.SearchVendorId = _workContext.CurrentVendor.Id;
            }

            var categoryIds = new List<int> { model.SearchCategoryId };     // 0
            //include subcategories
            if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
                //if(true)
                // AddRange: Toplu Ekleme
                categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            //ilgili paramtrelere göre productları getir
            var products = _productService.SearchProducts(
                categoryIds: categoryIds,
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                warehouseId: model.SearchWarehouseId,
                productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
            );

            // gridModel <- products
            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(x =>       //içinde gezemedim
            {
                var productModel = x.ToModel();
                //little performance optimization: ensure that "FullDescription" is not returned
                productModel.FullDescription = "";

                //picture
                var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                //product type
                productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                //friendly stock qantity
                //if a simple product AND "manage inventory" is "Track inventory", then display
                if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                    productModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();
                return productModel;
            });

            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }

        //edit product
        public virtual ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var product = _productService.GetProductById(id);       //  !!! Tekrar bak
            if (product == null || product.Deleted)
                //No product found with the specified id
                return RedirectToAction("List");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("List");

            var model = product.ToModel();

            PrepareProductModel(model, product, false, false);

            AddLocales(_languageService, model.Locales, (locale, languageId) =>         //  !!! Tekrar bak
            {
                locale.Name = product.GetLocalized(x => x.Name, languageId, false, false);
                locale.ShortDescription = product.GetLocalized(x => x.ShortDescription, languageId, false, false);
                locale.FullDescription = product.GetLocalized(x => x.FullDescription, languageId, false, false);
                locale.MetaKeywords = product.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = product.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = product.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = product.GetSeName(languageId, false, false);
            });

            PrepareAclModel(model, product, false);
            PrepareStoresMappingModel(model, product, false);
            PrepareCategoryMappingModel(model, product, false);
            PrepareManufacturerMappingModel(model, product, false);
            PrepareDiscountMappingModel(model, product, false);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue2", "continueEditing")]
        public virtual ActionResult Edit(ProductModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var product = _productService.GetProductById(model.Id);

            if (product == null || product.Deleted)
                //No product found with the specified id
                return RedirectToAction("List");

            //a vendor should have access only to his products
            //   vendor varsa                    ilgili ürünün vendor'u mu
            if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)        //ürünün sahibi kendi ürününü editleyemez mi???
                return RedirectToAction("List");

            //check if the product quantity has been changed while we were editing the product
            //and if it has been changed then we show error notification
            //and redirect on the editing page without data saving
            if (product.StockQuantity != model.LastStockQuantity)           //ürünün sayısı değiştirilemez mi??
            {
                ErrorNotification(_localizationService.GetResource("Admin.Catalog.Products.Fields.StockQuantity.ChangedWarning"));
                return RedirectToAction("Edit", new { id = product.Id });
            }

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (_workContext.CurrentVendor != null)
                {
                    model.VendorId = _workContext.CurrentVendor.Id;
                }

                //we do not validate maximum number of products per vendor when editing existing products (only during creation of new products)

                //vendors cannot edit "Show on home page" property
                if (_workContext.CurrentVendor != null && model.ShowOnHomePage != product.ShowOnHomePage)
                {
                    model.ShowOnHomePage = product.ShowOnHomePage;
                }
                //some previously used values
                var prevTotalStockQuantity = product.GetTotalStockQuantity();
                var prevDownloadId = product.DownloadId;
                var prevSampleDownloadId = product.SampleDownloadId;
                var previousStockQuantity = product.StockQuantity;
                var previousWarehouseId = product.WarehouseId;

                //product
                product = model.ToEntity(product);

                product.UpdatedOnUtc = DateTime.UtcNow;
                _productService.UpdateProduct(product);
                //search engine name
                model.SeName = product.ValidateSeName(model.SeName, product.Name, true);
                _urlRecordService.SaveSlug(product, model.SeName, 0);
                //locales
                UpdateLocales(product, model);
                //tags
                _productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));
                //warehouses
                SaveProductWarehouseInventory(product, model);
                //categories
                SaveCategoryMappings(product, model);
                //manufacturers
                SaveManufacturerMappings(product, model);
                //ACL (customer roles)
                SaveProductAcl(product, model);
                //stores
                SaveStoreMappings(product, model);
                //discounts
                SaveDiscountMappings(product, model);
                //picture seo names
                UpdatePictureSeoNames(product);

                //back in stock notifications
                if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                    product.BackorderMode == BackorderMode.NoBackorders &&
                    product.AllowBackInStockSubscriptions &&
                    product.GetTotalStockQuantity() > 0 &&
                    prevTotalStockQuantity <= 0 &&
                    product.Published &&
                    !product.Deleted)
                {  //Geri dön Stok abonelik Servisine
                    _backInStockSubscriptionService.SendNotificationsToSubscribers(product);
                }
                //delete an old "download" file (if deleted or updated)
                if (prevDownloadId > 0 && prevDownloadId != product.DownloadId)
                {
                    var prevDownload = _downloadService.GetDownloadById(prevDownloadId);
                    if (prevDownload != null)
                        _downloadService.DeleteDownload(prevDownload);
                }
                //delete an old "sample download" file (if deleted or updated)
                if (prevSampleDownloadId > 0 && prevSampleDownloadId != product.SampleDownloadId)
                {
                    var prevSampleDownload = _downloadService.GetDownloadById(prevSampleDownloadId);
                    if (prevSampleDownload != null)
                        _downloadService.DeleteDownload(prevSampleDownload);
                }

                //quantity change history
                //Depo değiştiyse
                if (previousWarehouseId != product.WarehouseId)
                {
                    //warehouse is changed 
                    //compose a message
                    var oldWarehouseMessage = string.Empty;
                    if (previousWarehouseId > 0)
                    {
                        var oldWarehouse = _shippingService.GetWarehouseById(previousWarehouseId);
                        if (oldWarehouse != null)
                            oldWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.Old"), oldWarehouse.Name);
                    }
                    var newWarehouseMessage = string.Empty;
                    if (product.WarehouseId > 0)
                    {
                        var newWarehouse = _shippingService.GetWarehouseById(product.WarehouseId);
                        if (newWarehouse != null)
                            newWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.New"), newWarehouse.Name);
                    }
                    var message = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse"), oldWarehouseMessage, newWarehouseMessage);

                    //record history
                    _productService.AddStockQuantityHistoryEntry(product, -previousStockQuantity, 0, previousWarehouseId, message);
                    _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId, message);

                }
                else
                {
                    _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity - previousStockQuantity, product.StockQuantity,
                        product.WarehouseId, _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));
                }

                //activity log
                _customerActivityService.InsertActivity("EditProduct", _localizationService.GetResource("ActivityLog.EditProduct"), product.Name);

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = product.Id });
                }
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, product, false, true);
            PrepareAclModel(model, product, true);
            PrepareStoresMappingModel(model, product, true);
            PrepareCategoryMappingModel(model, product, true);
            PrepareManufacturerMappingModel(model, product, true);
            PrepareDiscountMappingModel(model, product, true);

            return View(model);
        }

        //create product
        public virtual ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            // validate maximum number of products per vendor
            if (_vendorSettings.MaximumProductNumber > 0 &&             // ??? Şartlar olumlu değil mi neden error notification döndürüyor
                _workContext.CurrentVendor != null &&
                _productService.GetNumberOfProductsByVendorId(_workContext.CurrentVendor.Id) >= _vendorSettings.MaximumProductNumber)
            {
                string msg = _localizationService.GetResource("Admin.Catalog.Products.ExceededMaximumNumber");
                ErrorNotification(string.Format(msg, _vendorSettings.MaximumProductNumber));
                return RedirectToAction("List");
            }

            var model = new ProductModel();

            PrepareProductModel(model, null, true, true);
            AddLocales(_languageService, model.Locales);
            PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);
            PrepareCategoryMappingModel(model, null, false);
            PrepareManufacturerMappingModel(model, null, false);
            PrepareDiscountMappingModel(model, null, false);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue3", "continueEditing")]   // save-continue is name of the submit button  ???
        public virtual ActionResult Create(ProductModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //validate maximum number of products per vendor
            if (_vendorSettings.MaximumProductNumber > 0 &&
                _workContext.CurrentVendor != null &&
                _productService.GetNumberOfProductsByVendorId(_workContext.CurrentVendor.Id) >= _vendorSettings.MaximumProductNumber)
            {
                ErrorNotification(string.Format(_localizationService.GetResource("Admin.Catalog.Products.ExceededMaximumNumber"), _vendorSettings.MaximumProductNumber));
                return RedirectToAction("List");
            }

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (_workContext.CurrentVendor != null)
                {
                    model.VendorId = _workContext.CurrentVendor.Id;
                }

                //vendors cannot edit "Show on home page" property
                if (_workContext.CurrentVendor != null && model.ShowOnHomePage)
                {
                    model.ShowOnHomePage = false;
                }

                //product
                var product = model.ToEntity();
                product.CreatedOnUtc = DateTime.UtcNow;
                product.UpdatedOnUtc = DateTime.UtcNow;
                _productService.InsertProduct(product);     //Db'ye ekleme ???
                //search engine name
                model.SeName = product.ValidateSeName(model.SeName, product.Name, true);
                _urlRecordService.SaveSlug(product, model.SeName, 0);
                //locales
                UpdateLocales(product, model);
                //categories
                SaveCategoryMappings(product, model);
                //manufacturers
                SaveManufacturerMappings(product, model);
                //ACL (customer roles)
                SaveProductAcl(product, model);
                //stores
                SaveStoreMappings(product, model);
                //discounts
                SaveDiscountMappings(product, model);
                //tags
                _productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));
                //warehouses
                SaveProductWarehouseInventory(product, model);

                var message = _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit");  //"The stock quantity has been edited"

                //quantity change history
                //_stockQuantityHistoryRepository.Insert(historyEntry);    historyEntry = { message="The stock quantity has been edited",ProductId,StockQuantity }
                _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId,
                    message);

                //activity log
                _customerActivityService.InsertActivity("AddNewProduct", _localizationService.GetResource("ActivityLog.AddNewProduct"), product.Name);  // transaction log diyebiliriz

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Added"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = product.Id });
                }
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, null, false, true);
            PrepareAclModel(model, null, true);
            PrepareStoresMappingModel(model, null, true);
            PrepareCategoryMappingModel(model, null, true);
            PrepareManufacturerMappingModel(model, null, true);
            PrepareDiscountMappingModel(model, null, true);

            return View(model);
        }



        //delete product
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var product = _productService.GetProductById(id);
            if (product == null)
                //No product found with the specified id
                return RedirectToAction("List");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("List");

            _productService.DeleteProduct(product);

            //activity log
            _customerActivityService.InsertActivity("DeleteProduct", _localizationService.GetResource("ActivityLog.DeleteProduct"), product.Name);

            var x = _localizationService.GetResource("Admin.Catalog.Products.Deleted");

            SuccessNotification(x);
            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                _productService.DeleteProducts(_productService.GetProductsByIds(selectedIds.ToArray()).Where(p => _workContext.CurrentVendor == null || p.VendorId == _workContext.CurrentVendor.Id).ToList());
            }

            return Json(new { Result = true });
        }


        #endregion
    }
}