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
        /// Search carousels
        /// </summary>
        /// <param name="searchDate">Carousel identifier</param>
        /// <returns>Carousel</returns>
        IList<Carousel> SearchCarousel(DateTime? searchDate, bool isActive);

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
        /// Gets block carousels
        /// </summary>
        /// <returns>Carousels</returns>
        IList<Carousel> GetBlockCarousels();

    }
}
