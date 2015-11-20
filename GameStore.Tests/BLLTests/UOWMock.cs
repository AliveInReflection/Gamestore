using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.Tests.BLLTests
{
    public class UOWMock
    {
        private Mock<IUnitOfWork> mock;
        private TestCollections collections;

        public UOWMock(TestCollections collections)
        {
            mock = new Mock<IUnitOfWork>();
            this.collections = collections;
            Initialize();
        }

        public IUnitOfWork UnitOfWork { get { return mock.Object; }}

        private void Initialize()
        {

            //Games
            mock.Setup(x => x.Games.GetAll()).Returns(collections.Games);
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => collections.Games.Where(predicate.Compile()).First());
            mock.Setup(x => x.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => collections.Games.Where(predicate.Compile()));
            mock.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback((Game game) => collections.Games.Add(game));
            mock.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(
                (Game game) =>
                {
                    var entry = collections.Games.First(m => m.GameId.Equals(game.GameId));
                    entry.GameKey = game.GameKey;
                    entry.GameName = game.GameName;
                    entry.Description = game.Description;
                });
            mock.Setup(x => x.Games.Delete(It.IsAny<int>())).Callback(
                (int id) =>
                {
                    collections.Games.Remove(collections.Games.First(m => m.GameId.Equals(id)));
                });
            mock.Setup(x => x.Games.Count()).Returns(collections.Games.Count);


            //Comments
            mock.Setup(x => x.Comments.GetAll()).Returns(collections.Comments);
            mock.Setup(x => x.Comments.Get(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => collections.Comments.Where(predicate.Compile()).First());
            mock.Setup(x => x.Comments.GetMany(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => collections.Comments.Where(predicate.Compile()));
            mock.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback((Comment comment) => collections.Comments.Add(comment));
            mock.Setup(x => x.Comments.Update(It.IsAny<Comment>())).Callback((Comment comment) =>
            {
                var entry = collections.Comments.First(m => m.CommentId.Equals(comment.CommentId));
                entry.Content = comment.Content;
                entry.Quote = comment.Quote;
            });

            //Genres
            mock.Setup(x => x.Genres.GetAll()).Returns(collections.Genres);
            mock.Setup(x => x.Genres.Get(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => collections.Genres.Where(predicate.Compile()).First());
            mock.Setup(x => x.Genres.GetMany(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => collections.Genres.Where(predicate.Compile()));
            mock.Setup(x => x.Genres.Create(It.IsAny<Genre>())).Callback((Genre genre) => collections.Genres.Add(genre));
            mock.Setup(x => x.Genres.Update(It.IsAny<Genre>())).Callback((Genre genre) =>
            {
                var entry = collections.Genres.First(m => m.GenreId.Equals(genre.GenreId));
                entry.GenreName = genre.GenreName;
            });
            mock.Setup(x => x.Genres.Delete(It.IsAny<int>()))
                .Callback((int id) => collections.Genres.Remove(collections.Genres.First(m => m.GenreId.Equals(id))));


            //PlatformTypes
            mock.Setup(x => x.PlatformTypes.GetAll()).Returns(collections.PlatformTypes);
            mock.Setup(x => x.PlatformTypes.Get(It.IsAny<Expression<Func<PlatformType, bool>>>())).Returns((Expression<Func<PlatformType, bool>> predicate) => collections.PlatformTypes.Where(predicate.Compile()).First());
            mock.Setup(x => x.PlatformTypes.GetMany(It.IsAny<Expression<Func<PlatformType, bool>>>())).Returns((Expression<Func<PlatformType, bool>> predicate) => collections.PlatformTypes.Where(predicate.Compile()));
            mock.Setup(x => x.PlatformTypes.Create(It.IsAny<PlatformType>())).Callback((PlatformType platformType) => collections.PlatformTypes.Add(platformType));
            mock.Setup(x => x.PlatformTypes.Update(It.IsAny<PlatformType>())).Callback((PlatformType platformType) =>
            {
                var entry = collections.PlatformTypes.First(m => m.PlatformTypeId.Equals(platformType.PlatformTypeId));
                entry.PlatformTypeName = platformType.PlatformTypeName;
            });
            mock.Setup(x => x.PlatformTypes.Delete(It.IsAny<int>()))
                .Callback((int id) => collections.PlatformTypes.Remove(collections.PlatformTypes.First(m => m.PlatformTypeId.Equals(id))));


            //Publishers
            mock.Setup(x => x.Publishers.GetAll()).Returns(collections.Publishers);
            mock.Setup(x => x.Publishers.Get(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => collections.Publishers.Where(predicate.Compile()).First());
            mock.Setup(x => x.Publishers.GetMany(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => collections.Publishers.Where(predicate.Compile()));
            mock.Setup(x => x.Publishers.Create(It.IsAny<Publisher>())).Callback((Publisher publisher) => collections.Publishers.Add(publisher));
            mock.Setup(x => x.Publishers.Update(It.IsAny<Publisher>())).Callback((Publisher publisher) =>
            {
                var entry = collections.Publishers.First(m => m.PublisherId.Equals(publisher.PublisherId));
                entry.CompanyName = publisher.CompanyName;
                entry.HomePage = publisher.HomePage;
                entry.Description = publisher.Description;
            });
            mock.Setup(x => x.Publishers.Delete(It.IsAny<int>()))
                .Callback((int id) => collections.Publishers.Remove(collections.Publishers.First(m => m.PublisherId.Equals(id))));



            //Orders
            mock.Setup(x => x.Orders.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                 .Returns(collections.Orders[0]);

            mock.Setup(x => x.Orders.Create(It.IsAny<Order>()))
                .Callback((Order order) => collections.Orders.Add(order));
            mock.Setup(x => x.OrderDetailses.Create(It.IsAny<OrderDetails>()))
                .Callback((OrderDetails orderDetailse) => collections.OrderDetailses.Add(orderDetailse));
            mock.Setup(x => x.OrderDetailses.GetMany(It.IsAny<Expression<Func<OrderDetails, bool>>>()))
                .Returns(collections.Orders[0].OrderDetailses);

            //Users
            mock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(collections.Users[0]);

            mock.Setup(x => x.Users.Create(It.IsAny<User>()))
                .Callback((User user) => collections.Users.Add(user));

            mock.Setup(x => x.Users.GetAll()).Returns(collections.Users);
            
            mock.Setup(x => x.Users.GetMany(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((Expression<Func<User, bool>> predicate) => collections.Users.Where(predicate.Compile()));
           
            mock.Setup(x => x.Users.Update(It.IsAny<User>())).Callback(
                (User user) =>
                {
                    var entry = collections.Users.First(m => m.UserId.Equals(user.UserId));
                    entry.UserName = user.UserName;
                    entry.BanExpirationDate = user.BanExpirationDate;
                    entry.Claims = user.Claims;
                    entry.Roles = user.Roles;
                    entry.Password = user.Password;
                    entry.DateOfBirth = user.DateOfBirth;
                    entry.Country = user.Country;
                });

            mock.Setup(x => x.Users.Delete(It.IsAny<int>())).Callback(
                (int id) =>
                {
                    collections.Users.Remove(collections.Users.First(m => m.UserId.Equals(id)));
                });


            //Roles

            mock.Setup(x => x.Roles.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(collections.Roles[0]);

            mock.Setup(x => x.Roles.Create(It.IsAny<Role>()))
                .Callback((Role role) => collections.Roles.Add(role));

            mock.Setup(x => x.Roles.GetAll()).Returns(collections.Roles);

            mock.Setup(x => x.Roles.GetMany(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns((Expression<Func<Role, bool>> predicate) => collections.Roles.Where(predicate.Compile()));

            mock.Setup(x => x.Roles.Update(It.IsAny<Role>())).Callback(
                (Role role) =>
                {
                    var entry = collections.Roles.First(m => m.RoleId.Equals(role.RoleId));
                    entry.RoleName = role.RoleName;
                    entry.Claims = role.Claims;
                });

            mock.Setup(x => x.Roles.Delete(It.IsAny<int>())).Callback(
                (int id) =>
                {
                    collections.Roles.Remove(collections.Roles.First(m => m.RoleId.Equals(id)));
                });

            
            
        }
    }
}
