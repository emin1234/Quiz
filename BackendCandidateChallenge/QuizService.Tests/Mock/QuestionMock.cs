using Bogus;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService.Tests.Mock
{
    public class QuestionMock
    {
        public static QuestionDto GenerateRandomQuestionDto()
        {
            var questionFaker = QuestionFakerDto();

            var generatedQuestion = questionFaker.Generate();

            return generatedQuestion;
        }

        public static List<QuestionDto> GenerateRandomQuestionsDto(int count = 10)
        {
            var questionFaker = QuestionFakerDto();

            var generatedQuestions = questionFaker.Generate(count).ToList();

            return generatedQuestions;
        }

        public static Faker<QuestionDto> QuestionFakerDto()
        {
            var questionFaker = new Faker<QuestionDto>()
            .StrictMode(false)
            .RuleFor(q => q.Text, f => f.Random.Words(f.Random.Number(1, 3)));

           return questionFaker;
        }
    }
}
