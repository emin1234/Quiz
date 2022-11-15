using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;

namespace Service.Mappers
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile()
        {
            this.CreateMap<Question, QuestionDto>();
        }
    }
}
