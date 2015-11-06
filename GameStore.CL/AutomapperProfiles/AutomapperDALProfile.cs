using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Northwind;
using GameStore.Domain.Entities;
using GameStore.Domain.Static;
using GameStoreOrder = GameStore.Domain.Entities.Order;
using NorthwindOrder = GameStore.DAL.Northwind.Order;

using GameStoreOrderDetails = GameStore.Domain.Entities.OrderDetails;
using NorthwindOrderDetails = GameStore.DAL.Northwind.Order_Detail;

namespace GameStore.CL.AutomapperProfiles
{
    public class AutomapperDALProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Game, Game>().ForMember(m => m.Genres, opt => opt.Ignore())
                .ForMember(m => m.PlatformTypes, opt => opt.Ignore()).ForMember(m => m.Publisher, opt => opt.Ignore());

            Mapper.CreateMap<Product, Game>()
                .ForMember(m => m.GameId, opt => opt.MapFrom(m => KeyManager.Encode(m.ProductID, DatabaseType.Northwind)))
                .ForMember(m => m.GameKey, opt => opt.MapFrom(m => "Northwind " + m.ProductID))
                .ForMember(m => m.GameName, opt => opt.MapFrom(m => m.ProductName))
                .ForMember(m => m.Description, opt => opt.MapFrom(m => "Unit from northwind database"))
                .ForMember(m => m.Price, opt => opt.MapFrom(m => m.UnitPrice))
                .ForMember(m => m.UnitsInStock, opt => opt.MapFrom(m => m.UnitsInStock))
                .ForMember(m => m.Discontinued, opt => opt.MapFrom(m => m.Discontinued))
                .ForMember(m => m.PublicationDate, opt => opt.MapFrom(m => new DateTime(1990,1,1)))
                .ForMember(m => m.ReceiptDate, opt => opt.MapFrom(m => new DateTime(1990, 1, 1)))
                .ForMember(m => m.PublisherId, opt => opt.MapFrom(m => KeyManager.Encode(m.SupplierID.Value, DatabaseType.Northwind)))
                .ForMember(m => m.Genres, opt => opt.MapFrom(m => new[] {m.Category}))
                .ForMember(m => m.Publisher, opt => opt.MapFrom(m => m.Supplier))
                .ForMember(m => m.PlatformTypes, opt => opt.MapFrom(m => new List<PlatformType>()));


            Mapper.CreateMap<Category, Genre>()
                .ForMember(m => m.GenreId, opt => opt.MapFrom(m => KeyManager.Encode(m.CategoryID, DatabaseType.Northwind)))
                .ForMember(m => m.GenreName, opt => opt.MapFrom(m => m.CategoryName))
                .ForMember(m => m.ChildGenres, opt => opt.Ignore())
                .ForMember(m => m.ParentGenre, opt => opt.Ignore())
                .ForMember(m => m.Games, opt => opt.MapFrom(m => m.Products));

            
            Mapper.CreateMap<Supplier, Publisher>()
                .ForMember(m => m.PublisherId,
                    opt => opt.MapFrom(m => KeyManager.Encode(m.SupplierID, DatabaseType.Northwind)))
                .ForMember(m => m.CompanyName, opt => opt.MapFrom(m => m.CompanyName))
                .ForMember(m => m.Description, opt => opt.MapFrom(m =>
                    "Country: " + m.Country + "\n" +
                    "Region: " + m.Region + "\n" +
                    "City: " + m.City + "\n" +
                    "Address: " + m.Address + "\n" +
                    "Phone: " + m.Phone + "\n" +
                    "Contact name: " + m.ContactName + "\n"))
                .ForMember(m => m.HomePage, opt => opt.MapFrom(m => m.HomePage))
                .ForMember(m => m.Games, opt => opt.MapFrom(m => m.Products));

            Mapper.CreateMap<NorthwindOrder, GameStoreOrder>()
                .ForMember(m => m.OrderId, opt => opt.MapFrom(m => KeyManager.Encode(m.OrderID, DatabaseType.Northwind)))
                .ForMember(m => m.Date, opt => opt.MapFrom(m => m.OrderDate))
                .ForMember(m => m.OrderState, opt => opt.MapFrom(m => OrderState.Complete))
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(m => m.CustomerID))
                .ForMember(m => m.OrderDetailses, opt => opt.MapFrom(m => m.Order_Details));


            Mapper.CreateMap<NorthwindOrderDetails, GameStoreOrderDetails>()
                .ForMember(m => m.OrderId, opt => opt.MapFrom(m => KeyManager.Encode(m.OrderID, DatabaseType.Northwind)))
                .ForMember(m => m.ProductId,
                    opt => opt.MapFrom(m => KeyManager.Encode(m.ProductID, DatabaseType.Northwind)))
                .ForMember(m => m.Quantity, opt => opt.MapFrom(m => m.Quantity))
                .ForMember(m => m.Discount, opt => opt.MapFrom(m => m.Discount))
                .ForMember(m => m.Order, opt => opt.MapFrom(m => m.Order))
                .ForMember(m => m.Product, opt => opt.MapFrom(m => m.Product));
        }
    }
}
