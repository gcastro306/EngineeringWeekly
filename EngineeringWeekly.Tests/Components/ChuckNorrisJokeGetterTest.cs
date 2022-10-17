using AutoBogus;
using CodeTest.Controllers;
using EngineeringWeekly.API.Componets;
using EngineeringWeekly.API.Componets.Interfaces;
using EngineeringWeekly.DTOS;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EngineeringWeekly.Tests.Components
{
    public class ChuckNorrisJokeGetterTest
    {
        Mock<ILogger<IChuckNorrisJokeGetter>> Logger { get; }
        ExternalAPIUrs ExternalAPIUrs { get; }
        IChuckNorrisJokeGetter ChuckNorrisJokeGetter { get; }
        public ChuckNorrisJokeGetterTest()
        {
            Logger = new Mock<ILogger<IChuckNorrisJokeGetter>>();
            ExternalAPIUrs = new AutoFaker<ExternalAPIUrs>().Generate();
            ChuckNorrisJokeGetter = new ChuckNorrisJokeGetter(Logger.Object, Options.Create(ExternalAPIUrs));
        }

        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //public async Task ChuckNorrisJokeGetter_GetJokeNullAndEmptyCategory_ReturnOk(string category)
        //{
        //    //Arrange
        //    var validationData = new List<string>() { null, "", "type01", "type02", "type03" };
        //    var mockResult = "Chuk Norris Joke";
        //    ChuckNorrisJokeGetter.Setup(s => s.GetJoke(It.Is<string>(s => validationData.Contains(s)))).ReturnsAsync(Result.Ok(mockResult));


        //    //Act
        //    var result = await ChuckNorrisJokeGetter.GetJoke(category);

        //    //Validate
        //    ChuckNorrisJokeGetter.Verify(v => v.GetJoke(It.IsAny<string>()), Times.Once);
        //    ChuckNorrisJokeGetter.Verify(v => v.GetJokeCategories(), Times.Never);

        //    //Assert
        //    var value = Assert.IsType<OkObjectResult>(result).Value as string;
        //    result.Should().BeOfType<OkObjectResult>();
        //    value.Should().BeOfType<string>();
        //    value.Should().NotBeNull();
        //    value.Should().Be(mockResult);
        //}
    }
}
