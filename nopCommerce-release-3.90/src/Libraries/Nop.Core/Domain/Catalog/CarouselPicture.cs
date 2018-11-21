using Nop.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public partial class CarouselPicture : BaseEntity
    {
        public int CarouselId { get; set; }
        public int PictureId { get; set; }
        public int DisplayOrder { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual Carousel Carousel { get; set; }
    }
}
