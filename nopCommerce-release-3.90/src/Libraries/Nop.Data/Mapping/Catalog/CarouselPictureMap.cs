using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public partial class CarouselPictureMap : NopEntityTypeConfiguration<CarouselPicture>
    {
        public CarouselPictureMap()
        {
            this.ToTable("Carousel_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.Picture)
                .WithMany()
                .HasForeignKey(pp => pp.PictureId);


            this.HasRequired(pp => pp.Carousel)
                .WithMany(p => p.CarouselPictures)
                .HasForeignKey(pp => pp.CarouselId);
        }
    }
}
