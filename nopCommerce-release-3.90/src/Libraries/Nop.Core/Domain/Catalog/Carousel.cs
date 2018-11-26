using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a carousel
    /// </summary>
    public partial class Carousel : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        /// <summary>
        /// Gets or sets the link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the path of the image of the carousel
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets that is carousel active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the starting date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the finishing date
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// Gets or sets the addition date
        /// </summary>
        public DateTime AdditionDate { get; set; }
    }
}
