using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;

namespace Service.Mappers
{
    public static class QuizMapper
    {
        internal static IMapper Mapper { get; }
        static QuizMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<QuizMapperProfile>()).CreateMapper();
        }

        public static QuizDto ToDto(this Quiz quiz)
        {
            return Mapper.Map<QuizDto>(quiz);
        }

        public static IEnumerable<QuizDto> ToDto(this IEnumerable<Quiz> quizzes)
        {
            return Mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }
    }
}
