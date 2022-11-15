using Repository.Model.Domain;

namespace Repository.Repositories.Interfaces
{
    /// <summary>
    /// Quiz repository.
    /// </summary>
    
    /// TODO: All other methods(CRUD and others) related to quizzes should be implemented here.
    /// We should also create repository layer for all other entities like Answers, Questions etc.
    public interface IQuizRepository
    {
        /// <summary>
        /// Returns all quizzes.
        /// </summary>
        /// <returns>List of <see cref="Quiz"> async.</returns>
        Task<IEnumerable<Quiz>> GetAllAsync();
        
        /// <summary>
        /// Returns quiz by id.
        /// </summary>
        /// <param name="id">Id of quiz.</param>
        /// <returns><see cref="Quiz"/> async.</returns>
        Task<Quiz> GetByIdAsync(int id);
    }
}
