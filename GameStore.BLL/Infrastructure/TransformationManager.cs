using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Concrete;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.BLL.Concrete.ContentSorters;
using GameStore.BLL.ContentFilters;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Infrastructure
{
    public static class TransformationManager
    {
        public static IFilteringPipeline<Game> GetGameFilteringPipeline(GameFilteringMode filteringMode)
        {
            var pipeline = new GameFilterPipeline();

            if (filteringMode.GenreIds != null && filteringMode.GenreIds.Any())
            {
                pipeline.Add(new GameFilterByGenre(filteringMode.GenreIds));
            }

            if (filteringMode.PlatformTypeIds != null && filteringMode.PlatformTypeIds.Any())
            {
                pipeline.Add(new GameFilterByPlatformType(filteringMode.PlatformTypeIds));
            }

            if (filteringMode.PublisherIds != null && filteringMode.PublisherIds.Any())
            {
                pipeline.Add(new GameFilterByPublisher(filteringMode.PublisherIds));
            }

            if (filteringMode.MaxPrice > 0)
            {
                pipeline.Add(new GamePriceFilter(filteringMode.MinPrice, filteringMode.MaxPrice));
            }

            if (!String.IsNullOrEmpty(filteringMode.PartOfName))
            {
                pipeline.Add(new GameFilterByName(filteringMode.PartOfName));
            }

            if (filteringMode.PublishingDate.Days != 0)
            {
                pipeline.Add(new GameFilterByPublitingDate(filteringMode.PublishingDate));
            }


            return pipeline;
        }

        public static IContentSorter<Game> GetGameSorter(GamesSortingMode sortingMode)
        {
            switch (sortingMode)
            {
                case GamesSortingMode.MostPopular:
                    return new GameSorterByViews();
                case GamesSortingMode.MostCommented:
                    return new GameSorterByComments();
                case GamesSortingMode.AdditionDate:
                    return new GameSorterByDate();
                case GamesSortingMode.PriceAscending:
                    return new GameSorterByPrice(SortingDirection.Ascending);
                case GamesSortingMode.PriceDescending:
                    return new GameSorterByPrice(SortingDirection.Descending);
                default:
                    return null;
            }
        }
    }
}
