using Bogus;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService.Tests.Mock
{
    public class QuizMock
    {
        public static QuizDto GenerateRandomQuizDto()
        {
            var quizFaker = QuizFakerDto();

            var generatedQuiz = quizFaker.Generate();
            return generatedQuiz;
        }

        public static Faker<QuizDto> QuizFakerDto()
        {
            var quizFaker = new Faker<QuizDto>()
            .StrictMode(false)
            .RuleFor(q => q.Title, f => f.Random.Words(f.Random.Number(1, 3)));

            return quizFaker;
        }
    }
}
