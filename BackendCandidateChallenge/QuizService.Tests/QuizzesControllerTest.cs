using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizService.Model;
using QuizService.Tests.Base;
using Service.Dto;
using Xunit;

namespace QuizService.Tests;

public class QuizzesControllerTest : IClassFixture<TestServerFixture>
{
    const string QuizApiEndPoint = "/api/quizzes/";
    
    private readonly TestServerFixture _serverFixture;

    public QuizzesControllerTest(TestServerFixture serverFixture)
    {
        _serverFixture = serverFixture;
    }

    [Fact]
    public async Task Created_ShouldBeReturned_OnPostQuiz()
    {
        //arrange
        var quiz = new QuizDto();
        quiz.Title = "Test title";

        //act
        var response = await _serverFixture.Client.PostAsync(QuizApiEndPoint, ToHttpContent(quiz));

        //assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Quiz_ShouldBeReturned_WhenQuizExists_OnGetQuiz()
    {
        //arrange
        const long quizId = 1;

        //act
        var response = await _serverFixture.Client.GetAsync($"{QuizApiEndPoint}{quizId}");

        //assert
        var quiz = JsonConvert.DeserializeObject<QuizDto>(await response.Content.ReadAsStringAsync());
        Assert.Equal(quizId, quiz.Id);
    }

    [Fact]
    public async Task AQuizDoesNotExistGetFails()
    {
        const long quizId = 999;
        var response = await _serverFixture.Client.GetAsync($"{QuizApiEndPoint}{quizId}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AQuizDoesNotExists_WhenPostingAQuestion_ReturnsNotFound()
    {
        const string QuizApiEndPoint = "/api/quizzes/999/questions";

        const long quizId = 999;
        var question = new QuestionCreateModel("The answer to everything is what?");
        var content = new StringContent(JsonConvert.SerializeObject(question));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _serverFixture.Client.PostAsync($"{QuizApiEndPoint}{quizId}", content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static HttpContent ToHttpContent(object obj)
    {
        HttpContent content = new StringContent(ObjectToJson(obj), Encoding.UTF8, "application/json");
        return content;
    }

    private static string ObjectToJson(object obj)
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var convertedObj = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        return convertedObj;
    }
}