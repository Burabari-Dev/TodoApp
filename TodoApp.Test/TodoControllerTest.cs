using FakeItEasy;
using System.Linq;
using TodoApp.Models;
using TodoApp.Data;
using Xunit;
using System.Threading.Tasks;
using TodoApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoApp.Test
{
    public class TodoControllerTest
    {
        [Fact]
        public void GetAll_Should_Get_10_Todos_From_DB()
        {
            //GIVEN
            int total = 10;
            var fakeTodos = A.CollectionOfDummy<Todo>(total).AsEnumerable();
            var dataStore = A.Fake<ITodoDBContext>();
            A.CallTo(() => dataStore.GetAll()).Returns(Task.FromResult(fakeTodos));
            //A.CallTo(async () => dataStore.GetAll()).Returns(fakeTodos);
            var controller = new TodosController(dataStore);

            //WHEN
            var actionResult = controller.GetAll();

            //THEN
            var result = actionResult.Result?.Result as OkObjectResult;
            var returnTodos = result?.Value as IEnumerable<Todo>;
            Assert.NotNull(returnTodos);
            Assert.True(returnTodos?.Any());
            Assert.Equal(total, returnTodos?.Count());
        }
    }
}