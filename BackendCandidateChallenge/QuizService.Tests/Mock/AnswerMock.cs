using Bogus;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService.Tests.Mock
{
    public class AnswerMock
    {
        public static AnswerDto GenerateRandomAnswerDto()
        {
            var answerFaker = AnswerFakerDto();

            var generatedAnswer = answerFaker.Generate();

            return generatedAnswer;
        }

        public static List<AnswerDto> GenerateRandomAnswersDto(int count = 10)
        {
            var answerFaker = AnswerFakerDto();

            var generatedAnswer = answerFaker.Generate(count);

            return generatedAnswer;
        }

        public static Faker<AnswerDto> AnswerFakerDto()
        {
            var answerFaker = new Faker<AnswerDto>()
            .StrictMode(false)
            .RuleFor(a => a.Text, f => f.Random.Words(f.Random.Number(1, 3)))
            .RuleFor(a => a.IsCorrectAnswer, f => false);

            return answerFaker;
        }
    }
}
