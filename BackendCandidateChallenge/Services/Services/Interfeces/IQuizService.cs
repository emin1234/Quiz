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
        
        /// TODO: All other methods(CRUD and others) related to quizzes should be implemented here.
        /// We should also create service layer for all other entities like Answers, Questions etc in order to implement business logic.
        Task<IEnumerable<QuizDto>> GetAllAsync();

        /// <summary>
        /// Returns quiz by id.
        /// </summary>
        /// <param name="id">Id of quiz.</param>
        /// <returns><see cref="QuizDto">async.</returns>
        Task<QuizDto> GetByIdAsync(int id);
    }
}
