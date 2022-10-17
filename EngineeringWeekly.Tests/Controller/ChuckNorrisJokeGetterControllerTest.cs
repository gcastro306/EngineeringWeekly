using AutoBogus;
using CodeTest.Controllers;
using EngineeringWeekly.API.Componets.Interfaces;
using EngineeringWeekly.DTOS;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EngineeringWeekly.Tests.Controller
{
    public class ChuckNorrisJokeGetterControllerTest
    {
        Mock<ILogger<ChuckNorrisJokeGetterController>> Logger { get; }
        Mock<IChuckNorrisJokeGetter> ChuckNorrisJokeGetter { get; }
        ChuckNorrisJokeGetterController Controller { get; }
        ServiceConfigOptions ServiceConfigOptions { get; }
        public ChuckNorrisJokeGetterControllerTest()
        {
            Logger = new Mock<ILogger<ChuckNorrisJokeGetterController>>();
            ChuckNorrisJokeGetter = new Mock<IChuckNorrisJokeGetter>();
            ServiceConfigOptions = new AutoFaker<ServiceConfigOptions>().Generate();
            Controller = new ChuckNorrisJokeGetterController(Logger.Object, Options.Create(ServiceConfigOptions), ChuckNorrisJokeGetter.Object);
        }

        [Fact]
        public Task ChuckNorrisJokeGetterController_Health_ReturnOk()
        {
            //Arrange

            //Act
            var result = Controller.Health();

            //Validate
            ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Never);
            ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Never);

            //Assert
            var value = Assert.IsType<OkObjectResult>(result).Value as ServiceConfigOptions;
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<ServiceConfigOptions>();
            value!.OS.Should().Be(ServiceConfigOptions.OS);
            value!.ServiceName.Should().Be(ServiceConfigOptions.ServiceName);
            value!.ServiceVersion.Should().Be(ServiceConfigOptions.ServiceVersion);
            value!.Environment.Should().Be(ServiceConfigOptions.Environment);

            return Task.CompletedTask;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("type01")]
        public async Task ChuckNorrisJokeGetterController_GetJoke_ReturnOk(string category)
        {
            //Arrange
            var validationData = new List<string>() { null, "", "type01", "type02", "type03" };
            var mockResult = "Chuk Norris Joke";
            ChuckNorrisJokeGetter.Setup(s => s.GetJoke(It.Is<string>(s => validationData.Contains(s)))).ReturnsAsync(Result.Ok(mockResult));


            //Act
            var result = await Controller.GetJoke(category);

            //Validate
            ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Once);
            ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Never);

            //Assert
            var value = Assert.IsType<OkObjectResult>(result).Value as string;
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<string>();
            value.Should().NotBeNull();
            value.Should().Be(mockResult);
        }

        [Fact]
        public async Task ChuckNorrisJokeGetterController_GetJoke_Return501()
        {
            //Arrange
            var errorMessage = "Chuk Norris API Fail";
            var category = "category";
            ChuckNorrisJokeGetter.Setup(s => s.GetJoke(It.IsAny<string>())).ReturnsAsync(Result.Fail(errorMessage));


            //Act
            var result = await Controller.GetJoke(category);

            //Validate
            ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Once);
            ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Never);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            result.Should().BeOfType<ObjectResult>();
            objectResult.Value.Should().BeOfType<string>();
            objectResult.Value.Should().Be(errorMessage);
            objectResult.StatusCode.Should().Be(501);
        }

        [Fact]
        public async Task ChuckNorrisJokeGetterController_GetJokeCategories_ReturnOk()
        {
            //Arrange
            var categories = new List<string>() { "Category 01", "Category 02", "Category 03" };
            ChuckNorrisJokeGetter.Setup(s => s.GetJokeCategories()).ReturnsAsync(Result.Ok(categories));


            //Act
            var result = await Controller.GetJokeCategories();

            //Validate
            ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Never);
            ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Once);


            //Assert
            var value = Assert.IsType<OkObjectResult>(result).Value as List<string>;
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<List<string>>();
            value.Should().NotBeNull();
            value.Should().HaveCount(categories.Count);
        }

        [Fact]
        public async Task ChuckNorrisJokeGetterController_GetJokeCategories_Return501()
        {
            //Arrange
            var errorMessage = "Chuk Norris API Fail";
            ChuckNorrisJokeGetter.Setup(s => s.GetJokeCategories()).ReturnsAsync(Result.Fail(errorMessage));


            //Act
            var result = await Controller.GetJokeCategories();

            //Validate
            ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Never);
            ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Once);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            result.Should().BeOfType<ObjectResult>();
            objectResult.Value.Should().BeOfType<string>();
            objectResult.Value.Should().Be(errorMessage);
            objectResult.StatusCode.Should().Be(501);
        }
    }
}
