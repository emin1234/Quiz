using Repository.Repositories.Interfaces;
using Service.Dto;
using Service.Mappers;
using Services.Services.Interfeces;

namespace Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<QuizDto>> GetAllAsync()
        {
            var quizes =  await this._quizRepository.GetAllAsync();
            var quizesDto = quizes.ToDto();
            return quizesDto;
        }

        public async Task<QuizDto> GetByIdAsync(int id)
        {
            var quiz = await this._quizRepository.GetByIdAsync(id);
            var quizDto = quiz.ToDto();
            return quizDto;
        }
    }
}
