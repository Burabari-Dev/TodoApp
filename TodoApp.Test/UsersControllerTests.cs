using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Controllers;
using TodoApp.Data;
using TodoApp.Models;
using Xunit;

namespace TodoApp.Test
{
    public class UsersControllerTests
    {
        [Fact]
        public void AddUser_Should_Return_Added_User()
        {
            //GIVEN
            var fakeUser = A.Dummy<User>();
            var dataStore = A.Fake<IUserDBContext>();
            A.CallTo(() => dataStore.AddUser(fakeUser)).Returns(Task.FromResult(fakeUser));
            var controller = new UsersController(dataStore);

            //WHEN
            var actionResult = controller.AddUser(fakeUser);

            //THEN
            var result = (OkObjectResult) actionResult.Result;
            var dbUser = (User) result.Value;
            Assert.NotNull(result);
            Assert.True(dbUser.Email == fakeUser.Email);
        }

        [Fact]
        public void GetUser_Should_Return_The_User_With_The_Specified_Id()
        {
            //GIVEN
            int id = 23;
            var fakeUser1 = A.Dummy<User>();
            var fakeUser2 = A.Dummy<User>();
            var fakeUser3 = A.Dummy<User>();
            fakeUser2.Id = id;
            var dataStore = A.Fake<IUserDBContext>();
            A.CallTo(() => dataStore.GetUser(id)).Returns(Task.FromResult(fakeUser2));
            var controller = new UsersController(dataStore);

            //WHEN
            var actionResult = controller.GetUser(id);

            //THEN
            var result = (OkObjectResult) actionResult.Result;
            var dbUser = (User) result?.Value;
            Assert.True(dbUser?.Id.Equals(fakeUser2.Id));
            Assert.False(dbUser?.Id.Equals(fakeUser1.Id));
            Assert.False(dbUser?.Id.Equals(fakeUser3.Id));
        }

        [Fact]
        public void GetAll_Should_Return_10_Users_From_DB()
        {
            //GIVEN
            int count = 10;
            var fakeUsers = A.CollectionOfDummy<User>(count).AsEnumerable();
            var dataSource = A.Fake<IUserDBContext>();
            A.CallTo(() => dataSource.GetAll()).Returns(Task.FromResult(fakeUsers));
            var controller = new UsersController(dataSource);

            //WHEN
            var actionResult = controller.GetAll();

            //THEN
            var result = actionResult.Result as OkObjectResult;
            var dbUsers = result?.Value as IEnumerable<User>;
            Assert.True(dbUsers?.Count() <= count);        //?? Why this works? UserDBContext.GetALl returns 15 users!!!
            //Assert.IsType<User>(dbUsers?.First());
        }

        [Fact]
        public void UpdateUser_Should_Return_User_With_Updated_Info()
        {
            //GIVEN
            var fakeUser = A.Fake<User>(x => x.WithArgumentsForConstructor(
                () => new User(23, "John", "john@email.com", "123")));
            var fakeUser2 = A.Fake<User>(x => x.WithArgumentsForConstructor(
                () => new User(12, "Jane", "jane@email.com", "abc")));
            var fakeUserUpdate = A.Fake<User>(x => x.WithArgumentsForConstructor(
                () => new User(23, "John", "john.doe@email.com", "123")));
            var dataStore = A.Fake<IUserDBContext>();
            A.CallTo(() => dataStore.UpdateUser(fakeUserUpdate)).Returns(Task.FromResult(fakeUserUpdate));
            var controller = new UsersController(dataStore);

            //WHEN
            var actionResult = controller.UpdateUser(fakeUserUpdate);

            //THEN
            var result = (OkObjectResult)actionResult.Result;
            var dbResult = (User)result?.Value;
            Assert.NotNull(dbResult);
            Assert.Equal(fakeUserUpdate.Id, dbResult.Id);
            Assert.Equal(fakeUserUpdate.Email, dbResult.Email);
            Assert.NotEqual(fakeUser.Email, dbResult.Email);
            Assert.NotEqual(fakeUser2.Id, dbResult.Id);
        }

        [Fact]
        public void DeleteUser_Should_Return_A_204_NoContent_Status()
        {
            //GIVEN
            int id = 5;
            //var fakeUser = A.Fake<User>(x => x.WithArgumentsForConstructor(
            //    () => new User(id, "John", "john@email.com", "123")));
            var dataStore = A.Fake<IUserDBContext>();
            A.CallTo(() => dataStore.DeleteUser(id)).Returns(Task.FromResult(true));
            var controller = new UsersController(dataStore);

            //WHEN
            var actionResult = controller.DeleteUser(id);

            //THEN
            var result = actionResult.Result;
            Assert.IsType<NoContentResult>(result);
            Assert.IsNotType<BadRequestResult>(result);
        }

    }
}
