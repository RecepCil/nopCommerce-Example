using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Copy carousel service
    /// </summary>
    public partial interface ICopyCarouselService
    {
        /// <summary>
        /// Create a copy of carousel with all depended data
        /// </summary>
        Carousel CopyCarousel(Carousel carousel);
    }
}
