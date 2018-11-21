using Nop.Core;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Carousel service
    /// </summary>
    public partial interface ICarouselService
    {
        /// <summary>
        /// Delete a carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        void DeleteCarousel(Carousel carousel);

        /// <summary>
        /// Delete carousels
        /// </summary>
        /// <param name="carousels">Carousels</param>
        void DeleteCarousels(IList<Carousel> carousels);

        /// <summary>
        /// Gets carousel
        /// </summary>
        /// <param name="carouselId">Carousel identifier</param>
        /// <returns>Carousel</returns>
        Carousel GetCarouselById(int carouselId);

        /// <summary>
        /// Gets carousels by identifier
        /// </summary>
        /// <param name="carouselIds">Carousel identifiers</param>
        /// <returns>Carousels</returns>
        IList<Carousel> GetCarouselsByIds(int[] carouselIds);

        /// <summary>
        /// Inserts a carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        void InsertCarousel(Carousel carousel);

        /// <summary>
        /// Updates the carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        void UpdateCarousel(Carousel carousel);

        /// <summary>
        /// Gets all carousels
        /// </summary>
        /// <returns>Carousels</returns>
        IList<Carousel> GetAllCarousels();





        /// <summary>
        /// Search carousels
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// 
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="markedAsNewOnly">A values indicating whether to load only products marked as "new"; "false" to load all records; "true" to load "marked as new" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// 
        /// <param name="keywords">Keywords</param>
      
            /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchManufacturerPartNumber">A value indicating whether to search by a specified "keyword" in manufacturer part number</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="overridePublished">
        /// </param>
        /// <returns>Carousels</returns>
        IPagedList<Carousel> SearchCarousels(string Link, bool IsActive, DateTime SearchDate,int pageIndex = 0,int pageSize = int.MaxValue);







    }
}
