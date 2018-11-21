using Nop.Admin.Extensions;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Infrastructure.Mapper;
using Nop.Web.Models.Common;

namespace Nop.Web.Extensions
{
    public static class MappingExtensions
    {
        //AutoMapper Çalışmadı
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var x = AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
            return x;
        }
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
        
        #region Carousel

        //Carousel
        public static CarouselModel ToModel(this Carousel entity)
        {
            //return entity.MapTo<Carousel, CarouselModel>();

            if (entity == null)
                return null;

            var model = new CarouselModel();
            return ToModel(entity, model);
        }
        public static CarouselModel ToModel(this Carousel entity, CarouselModel destination)
        {
            //return entity.MapTo(destination);

            if (entity == null)
                return destination;

            if (entity.Description != null)
                entity.Description = entity.Description.Trim();
            if (entity.Path != null)
                entity.Path = entity.Path.Trim();
            if (entity.Link != null)
                entity.Link = entity.Link.Trim();

            destination.Id = entity.Id;
            destination.Link = entity.Link;
            destination.Path = entity.Path;
            destination.Description = entity.Description;
            destination.DisplayOrder = entity.DisplayOrder;
            destination.IsActive = entity.IsActive;
            destination.StartDate = entity.StartDate;
            destination.FinishDate = entity.FinishDate;
            destination.AdditionDate = entity.AdditionDate;

            return destination;
        }

        public static Carousel ToEntity(this CarouselModel model)
        {
            //return model.MapTo<CarouselModel, Carousel>();

            if (model == null)
                return null;

            var entity = new Carousel();
            return ToEntity(model, entity);
        }
        public static Carousel ToEntity(this CarouselModel model, Carousel destination)
        {
            //return model.MapTo(destination);

            if (model == null)
                return destination;

            if (model.Description != null)
                model.Description = model.Description.Trim();
            if (model.Path != null)
                model.Path = model.Path.Trim();
            if (model.Link != null)
                model.Link = model.Link.Trim();

            destination.Id = model.Id;
            destination.Link = model.Link;
            destination.Path = model.Path;
            destination.Description = model.Description;
            destination.DisplayOrder = model.DisplayOrder;
            destination.IsActive = model.IsActive;
            destination.StartDate = model.StartDate;
            destination.FinishDate = model.FinishDate;
            destination.AdditionDate = model.AdditionDate;

            return destination;
        }

        #endregion

        //address
        public static Address ToEntity(this AddressModel model, bool trimFields = true)
        {
            if (model == null)
                return null;

            var entity = new Address();
            return ToEntity(model, entity, trimFields);
        }
        public static Address ToEntity(this AddressModel model, Address destination, bool trimFields = true)
        {
            if (model == null)
                return destination;

            if (trimFields)
            {
                if (model.FirstName != null)
                    model.FirstName = model.FirstName.Trim();
                if (model.LastName != null)
                    model.LastName = model.LastName.Trim();
                if (model.Email != null)
                    model.Email = model.Email.Trim();
                if (model.Company != null)
                    model.Company = model.Company.Trim();
                if (model.City != null)
                    model.City = model.City.Trim();
                if (model.Address1 != null)
                    model.Address1 = model.Address1.Trim();
                if (model.Address2 != null)
                    model.Address2 = model.Address2.Trim();
                if (model.ZipPostalCode != null)
                    model.ZipPostalCode = model.ZipPostalCode.Trim();
                if (model.PhoneNumber != null)
                    model.PhoneNumber = model.PhoneNumber.Trim();
                if (model.FaxNumber != null)
                    model.FaxNumber = model.FaxNumber.Trim();
            }
            destination.Id = model.Id;
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = model.CountryId;
            destination.StateProvinceId = model.StateProvinceId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }
    }
}