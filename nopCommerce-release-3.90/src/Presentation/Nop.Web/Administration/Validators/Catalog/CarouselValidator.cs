using FluentValidation;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Validators.Catalog
{
    public partial class CarouselValidator : BaseNopValidator<CarouselModel>
    {
        public CarouselValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Link).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Carousels.Fields.Link.Required"));
            SetDatabaseValidationRules<Carousel>(dbContext);
        }
    }
}