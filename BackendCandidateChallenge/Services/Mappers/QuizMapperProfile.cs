using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;

namespace Service.Mappers
{
    public class QuizMapperProfile : Profile
    {
        public QuizMapperProfile()
        {
            this.CreateMap<Quiz, QuizDto>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions.Select(q => new QuestionDto { Id = q.Id, Text = q.Text, Answers = q.Answers.Select(a => new AnswerDto { Id = a.Id, Text = a.Text, IsCorrectAnswer = a.IsCorrectAnswer }) })));
        }
    }
}
