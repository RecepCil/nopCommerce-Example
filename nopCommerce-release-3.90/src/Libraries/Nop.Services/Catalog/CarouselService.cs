using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Localization;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Carousel service
    /// </summary>
    public partial class CarouselService : ICarouselService
    {
        #region Fields

        private readonly IRepository<Carousel> _carouselRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly ILanguageService _languageService;
        private readonly CommonSettings _commonSettings;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="carouselRepository">Carousel repository</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="languageService">Language service</param>
        /// <param name="commonSettings">Common settings</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">Database context</param>
        public CarouselService(ICacheManager cacheManager,
            IRepository<Carousel> carouselRepository,
            IEventPublisher eventPublisher,
            ILanguageService languageService,
            CommonSettings commonSettings,
            IDataProvider dataProvider,
            IDbContext dbContext)
        {
            this._cacheManager = cacheManager;
            this._carouselRepository = carouselRepository;
            this._eventPublisher = eventPublisher;
            this._languageService = languageService;
            this._commonSettings = commonSettings;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        public virtual void DeleteCarousel(Carousel carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException("carousel");

            _carouselRepository.Delete(carousel);
            
            //event notification
            _eventPublisher.EntityDeleted(carousel);
        }

        /// <summary>
        /// Delete carousels
        /// </summary>
        /// <param name="carousels">Carousels</param>
        public virtual void DeleteCarousels(IList<Carousel> carousels)
        {
            if (carousels == null)
                throw new ArgumentNullException("carousels");

            foreach (var carousel in carousels)
            {
                _carouselRepository.Delete(carousel);

                //event notification
                _eventPublisher.EntityDeleted(carousel);
            }
        }

        /// <summary>
        /// Gets block carousels
        /// </summary>
        /// <returns>Carousels</returns>
        public virtual IList<Carousel> GetBlockCarousels()
        {
            var query = from p in _carouselRepository.Table
                        where p.IsActive == true
                        orderby p.DisplayOrder descending
                        select p;

            var carousels = query.ToList();

            return carousels;
        }

        /// <summary>
        /// Gets carousel
        /// </summary>
        /// <param name="carouselId">Carousel identifier</param>
        /// <returns>Carousel</returns>
        public Carousel GetCarouselById(int carouselId)
        {
            if (carouselId == 0)
                return null;

            return _carouselRepository.GetById(carouselId);
        }

        /// <summary>
        /// Gets carousel
        /// </summary>
        /// <param name="searchDate">Carousel identifier</param>
        /// /// <param name="searchOnlyActiveOnes">Carousel identifier</param>
        /// <returns>Carousel</returns>
        public IList<Carousel> SearchCarousel(DateTime searchDate=default(DateTime), bool searchOnlyActiveOnes=false)
        {
            var query = _carouselRepository.Table;

            if (searchDate != new DateTime())
            {
                query = query.Where(x => x.StartDate <= searchDate && x.FinishDate >= searchDate);
            }

            if (searchOnlyActiveOnes)
                query = query.Where(x => x.IsActive == searchOnlyActiveOnes);

            return query.ToList();
        }

        /// <summary>
        /// Get carousels by identifiers
        /// </summary>
        /// <param name="carouselIds">Carousel identifiers</param>
        /// <returns>Carousels</returns>
        public virtual IList<Carousel> GetCarouselsByIds(int[] carouselIds)
        {
            if (carouselIds == null)
                return new List<Carousel>();

            var query = from p in _carouselRepository.Table
                        where carouselIds.Contains(p.Id)
                        select p;
            var carousels = query.ToList();
            //sort by passed identifiers
            var sortedCarouless = new List<Carousel>();
            foreach (int id in carouselIds)
            {
                var carousel = carousels.Find(x => x.Id == id);
                if (carousel != null)
                    sortedCarouless.Add(carousel);
            }
            return sortedCarouless;
        }

        /// <summary>
        /// Inserts a carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        public virtual void InsertCarousel(Carousel carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException("carousel");

            _carouselRepository.Insert(carousel);

            //event notification
            _eventPublisher.EntityInserted(carousel);
        }

        /// <summary>
        /// Updates the carousel
        /// </summary>
        /// <param name="carousel">Carousel</param>
        public void UpdateCarousel(Carousel carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException("carousel");

            _carouselRepository.Update(carousel);

            //event notification
            _eventPublisher.EntityUpdated(carousel);
        }

        #endregion

    }
}


