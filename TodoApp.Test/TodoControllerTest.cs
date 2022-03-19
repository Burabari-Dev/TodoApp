using FakeItEasy;
using System.Linq;
using TodoApp.Models;
using TodoApp.Data;
using Xunit;
using System;
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

        [Fact]
        public void AddTodo_Should_Return_201_Created_StatusCode()
        {
            //GIVEN
            int id = 1;
            var fakeDbUser = A.Fake<User>(x => x.WithArgumentsForConstructor(
                () => new User(1, "Jack", "jack@email.com", "123")));
            var fakeTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo("First Action", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var fakeDbTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo(id, "First Action", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var dataStore = A.Fake<ITodoDBContext>();
            var controller = new TodosController(dataStore);
            
            //WHEN
            var actionResult = controller.AddTodo(id, fakeTodo);

            //THEN
            var result = actionResult.Result.Result as CreatedResult;
            var addedTodo = result?.Value as Todo;
            Assert.True(result?.StatusCode == 201);
            //Assert.True(addedTodo.Id == 0);   //TODO: Can we depend on test to have actual values from DB???
        }

        [Fact]
        public void GetTodo_Should_Return_200_Status_And_Todo_With_Requested_Id()
        {
            //GIVEN
            int id = 3;
            var fakeDBTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo(id, "Task 1", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var dbContext = A.Fake<ITodoDBContext>();
            A.CallTo(() => dbContext.GetTodo(id)).Returns(Task.FromResult(fakeDBTodo));
            var controller = new TodosController(dbContext);

            //WHEN
            var actionResult = controller.GetTodo(id);

            //THEN
            var result = actionResult.Result as OkObjectResult;
            var dbTodo = result?.Value as Todo;
            Assert.True(result?.StatusCode == 200);
        }

        [Fact]
        public void UpdateTodo_Should_Return_204_NoContent_When_Successful()
        {
            //GIVEN
            int id = 2;
            var fakeTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo(id, "Task 1", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var dbContext = A.Fake<ITodoDBContext>();
            A.CallTo(() => dbContext.UpdateTodo(id, fakeTodo));
            var controller = new TodosController(dbContext);

            //WHEN
            var actionResult = controller.UpdateTodo(id, fakeTodo);

            //THEN
            var result = actionResult.Result as NoContentResult;
            Assert.True(result?.StatusCode == 204);
        }


        [Fact]
        public void UpdateTodo_Should_Return_404_NotFound_When_Failed()
        {
            //GIVEN
            int id = 2;
            var fakeTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo(id, "Task 1", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var notFakeTodo = A.Fake<Todo>(x => x.WithArgumentsForConstructor(
                () => new Todo(4, "Task 2", DateTime.Today.AddHours(2), DateTime.Today.AddHours(4), false)));
            var dbContext = A.Fake<ITodoDBContext>();
            A.CallTo(() => dbContext.UpdateTodo(id, notFakeTodo)).Throws(new Exception());
            var controller = new TodosController(dbContext);

            //WHEN
            var actionResult = controller.UpdateTodo(id, notFakeTodo);

            //THEN
            var result = actionResult.Result as NotFoundResult;
            Assert.True(result?.StatusCode == 404);
        }
    }
}