using Repository.Model.Domain;

namespace Repository.Repositories.Interfaces
{
    /// <summary>
    /// Quiz repository.
    /// </summary>
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
