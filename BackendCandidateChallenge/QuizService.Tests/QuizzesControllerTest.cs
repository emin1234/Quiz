using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizService.Model;
using QuizService.Tests.Base;
using QuizService.Tests.Mock;
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
        //TODO: Create static generic method for deserializing responses
        var quiz = JsonConvert.DeserializeObject<QuizDto>(await response.Content.ReadAsStringAsync());
        Assert.Equal(quizId, quiz.Id);
    }

    [Fact]
    public async Task Quiz_ShouldBeReturned_WithNumberOfCorrectAnswers_OnGetQuizById()
    {
        //TODO: We can introduce UnitOfWork pattern in our application and then we can insert quiz, related questions and answers using only one
        //POST request for quiz insertion instead of using multiple POST request
        
        //arrange
        var quiz = QuizMock.GenerateRandomQuizDto();
        var postQuiz = await _serverFixture.Client.PostAsync(QuizApiEndPoint, ToHttpContent(quiz));
        var quizId = JsonConvert.DeserializeObject<int>(await postQuiz.Content.ReadAsStringAsync());

        List<QuestionDto> Questions = QuestionMock.GenerateRandomQuestionsDto(2);
        List<int> QuestionIds = new List<int>();
        foreach (var item in Questions)
        {
            var postQuestion = await _serverFixture.Client.PostAsync($"{QuizApiEndPoint}{quizId}/questions", ToHttpContent(item));
            var insertedQuestionId = JsonConvert.DeserializeObject<int>(await postQuestion.Content.ReadAsStringAsync());
            QuestionIds.Add(insertedQuestionId);
        }

        List<AnswerDto> Answers = AnswerMock.GenerateRandomAnswersDto(4);
        for (int i = 0; i < Answers.Count; i++)
        {
            int questionId = (i % 2 == 0) ? QuestionIds[0] : QuestionIds[1];
            //setting that one answer per question is correct
            Answers[i].IsCorrectAnswer = (i < 2) ? true : false;
            await _serverFixture.Client.PostAsync($"{QuizApiEndPoint}{quizId}/questions/{questionId}/answers", ToHttpContent(Answers[i]));
        }

        //act
        var response = await _serverFixture.Client.GetAsync($"{QuizApiEndPoint}{quizId}");
        var quizResponse = JsonConvert.DeserializeObject<QuizDto>(await response.Content.ReadAsStringAsync());

        //assert
        var numberOfCorrectAnswers = quizResponse.Questions.SelectMany(a => a.Answers.Where(a => a.IsCorrectAnswer == true));
        Assert.Equal(quizId, quizResponse.Id);
        Assert.Equal(2, numberOfCorrectAnswers.Count());
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