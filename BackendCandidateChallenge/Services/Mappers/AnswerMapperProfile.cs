using AutoMapper;
using Repository.Model.Domain;
using Service.Dto;


namespace Service.Mappers
{
    public class AnswerMapperProfile : Profile
    {
        public AnswerMapperProfile()
        {
            this.CreateMap<Answer, AnswerDto>();
        }
    }
}
