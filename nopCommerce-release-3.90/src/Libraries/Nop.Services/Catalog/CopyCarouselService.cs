using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Copy Product service
    /// </summary>
    public partial class CopyCarouselService : ICopyCarouselService
    {
        private readonly ICarouselService _carouselService;

        public CopyCarouselService(ICarouselService carouselService)
        {
            this._carouselService = carouselService;
        }

        public Carousel CopyCarousel(Carousel carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException("carousel");

            if (String.IsNullOrEmpty(carousel.Link))
                throw new ArgumentException("Carousel link is required");
            
            // carousel
            var carouselCopy = new Carousel
            {
                AdditionDate=DateTime.UtcNow,
                Description=carousel.Description,
                FinishDate=carousel.FinishDate,
                IsActive= carousel.IsActive,
                Link=carousel.Link,
                Path=carousel.Path,
                StartDate=carousel.StartDate
            };

            _carouselService.InsertCarousel(carouselCopy);

            return carouselCopy;
        }
    }
}
