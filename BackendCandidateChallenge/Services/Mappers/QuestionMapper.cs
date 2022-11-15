using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;

namespace Service.Mappers
{
    public static class QuestionMapper
    {
        internal static IMapper Mapper { get; }
        static QuestionMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<QuestionMapperProfile>()).CreateMapper();
        }

        public static QuestionDto ToDto(this Question question)
        {
            return Mapper.Map<QuestionDto>(question);
        }

        public static IEnumerable<QuestionDto> ToDto(this IEnumerable<Question> questions)
        {
            return Mapper.Map<IEnumerable<QuestionDto>>(questions);
        }
    }
}
