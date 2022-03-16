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
            var result = actionResult.Result;
            Assert.NotNull(result);
        }


    }
}
