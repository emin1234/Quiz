using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;

namespace Service.Mappers
{
    public static class AnswerMapper
    {
        internal static IMapper Mapper { get; }
        static AnswerMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AnswerMapperProfile>()).CreateMapper();
        }

        public static AnswerDto ToDto(this Answer answer)
        {
            return Mapper.Map<AnswerDto>(answer);
        }

        public static IEnumerable<AnswerDto> ToDto(this IEnumerable<Answer> answers)
        {
            return Mapper.Map<IEnumerable<AnswerDto>>(answers);
        }
    }
}
