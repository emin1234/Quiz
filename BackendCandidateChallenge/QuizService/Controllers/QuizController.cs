using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using QuizService.Model;
using System;
using System.Threading.Tasks;
using Services.Services.Interfeces;
using Service.Dto;

namespace QuizService.Controllers;

[Route("api/quizzes")]
public class QuizController : Controller
{
    private readonly IDbConnection _connection;
    private readonly IQuizService _quizService;

    public QuizController(IDbConnection connection, IQuizService quizService)
    {
        _connection = connection;
        _quizService = quizService;
    }

    // GET api/quizzes
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var quizzesDto = await this._quizService.GetAllAsync();

        return Ok(quizzesDto);
    }

    // GET api/quizzes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var quizDto = await this._quizService.GetByIdAsync(id);
        if (quizDto == null)
        {
            return NotFound();
        }

        return Ok(quizDto);
    }

    // POST api/quizzes
    [HttpPost]
    public IActionResult Post([FromBody] QuizDto value)
    {
        var sql = $"INSERT INTO Quiz (Title) VALUES('{value.Title}'); SELECT LAST_INSERT_ROWID();";
        var id = _connection.ExecuteScalar(sql);
        return Created($"/api/quizzes/{id}", id);
    }

    // PUT api/quizzes/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]QuizUpdateModel value)
    {
        const string sql = "UPDATE Quiz SET Title = @Title WHERE Id = @Id";
        int rowsUpdated = _connection.Execute(sql, new {Id = id, Title = value.Title});
        if (rowsUpdated == 0)
            return NotFound();
        return NoContent();
    }

    // DELETE api/quizzes/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        const string sql = "DELETE FROM Quiz WHERE Id = @Id";
        int rowsDeleted = _connection.Execute(sql, new {Id = id});
        if (rowsDeleted == 0)
            return NotFound();
        return NoContent();
    }

    // POST api/quizzes/5/questions
    [HttpPost]
    [Route("{id}/questions")]
    public IActionResult PostQuestion(int id, [FromBody]QuestionCreateModel value)
    {
        try
        {
            const string sql = "INSERT INTO Question (Text, QuizId) VALUES(@Text, @QuizId); SELECT LAST_INSERT_ROWID();";
            var questionId = _connection.ExecuteScalar(sql, new { Text = value.Text, QuizId = id });
            return Created($"/api/quizzes/{id}/questions/{questionId}", questionId);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    // PUT api/quizzes/5/questions/6
    [HttpPut("{id}/questions/{qid}")]
    public IActionResult PutQuestion(int id, int qid, [FromBody]QuestionUpdateModel value)
    {
        const string sql = "UPDATE Question SET Text = @Text, CorrectAnswerId = @CorrectAnswerId WHERE Id = @QuestionId";
        int rowsUpdated = _connection.Execute(sql, new {QuestionId = qid, Text = value.Text, CorrectAnswerId = value.CorrectAnswerId});
        if (rowsUpdated == 0)
            return NotFound();
        return NoContent();
    }

    // DELETE api/quizzes/5/questions/6
    [HttpDelete]
    [Route("{id}/questions/{qid}")]
    public IActionResult DeleteQuestion(int id, int qid)
    {
        const string sql = "DELETE FROM Question WHERE Id = @QuestionId";
        _connection.ExecuteScalar(sql, new {QuestionId = qid});
        return NoContent();
    }

    // POST api/quizzes/5/questions/6/answers
    [HttpPost]
    [Route("{id}/questions/{qid}/answers")]
    public IActionResult PostAnswer(int id, int qid, [FromBody] AnswerDto value)
    {
        const string sql = "INSERT INTO Answer (Text, QuestionId, IsCorrectAnswer) VALUES(@Text, @QuestionId, @IsCorrectAnswer); SELECT LAST_INSERT_ROWID();";
        var answerId = _connection.ExecuteScalar(sql, new { Text = value.Text, QuestionId = qid, IsCorrectAnswer = value.IsCorrectAnswer });
        return Created($"/api/quizzes/{id}/questions/{qid}/answers/{answerId}", null);
    }

    // PUT api/quizzes/5/questions/6/answers/7
    [HttpPut("{id}/questions/{qid}/answers/{aid}")]
    public IActionResult PutAnswer(int id, int qid, int aid, [FromBody]AnswerUpdateModel value)
    {
        const string sql = "UPDATE Answer SET Text = @Text WHERE Id = @AnswerId";
        int rowsUpdated = _connection.Execute(sql, new {AnswerId = qid, Text = value.Text});
        if (rowsUpdated == 0)
            return NotFound();
        return NoContent();
    }

    // DELETE api/quizzes/5/questions/6/answers/7
    [HttpDelete]
    [Route("{id}/questions/{qid}/answers/{aid}")]
    public IActionResult DeleteAnswer(int id, int qid, int aid)
    {
        const string sql = "DELETE FROM Answer WHERE Id = @AnswerId";
        _connection.ExecuteScalar(sql, new {AnswerId = aid});
        return NoContent();
    }
}