using Service.Dto;

namespace Services.Services.Interfeces
{
    /// <summary>
    /// Quiz service.
    /// </summary>
    public interface IQuizService
    {
        /// <summary>
        /// Returns all quizzes.
        /// </summary>
        /// <returns>List of <see cref="QuizDto"> async.</returns>
        Task<IEnumerable<QuizDto>> GetAllAsync();

        /// <summary>
        /// Returns quiz by id.
        /// </summary>
        /// <param name="id">Id of quiz.</param>
        /// <returns><see cref="QuizDto">async.</returns>
        Task<QuizDto> GetByIdAsync(int id);
    }
}
