using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public partial class CarouselMap : NopEntityTypeConfiguration<Carousel>
    {
        public CarouselMap()
        {
            this.ToTable("Carousel");

            this.HasKey(p => p.Id);

            this.Property(p => p.Link)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(p => p.Path)
                .IsRequired().
                HasMaxLength(255);

            this.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(p => p.DisplayOrder)
                .IsRequired();

            this.Property(p => p.IsActive)
                .IsRequired();

            this.Property(p => p.StartDate)
                .IsRequired();

            this.Property(p => p.FinishDate)
                .IsRequired();

            this.Property(p => p.AdditionDate)
                .IsRequired();        
        }
    }
}
