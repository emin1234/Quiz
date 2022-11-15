using Microsoft.EntityFrameworkCore;
using Repository.Model.Domain;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class QuizRepository : RepositoryBase<Quiz, RepositoryContext>, IQuizRepository
    {
        public QuizRepository(RepositoryContext dbContext)
            : base(dbContext)
        {}

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            var quizzes = await FindAll(q => q.Include(qq => qq.Questions).ThenInclude(a => a.Answers)).ToListAsync();
            return quizzes;
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            var quiz = await FindByCondition(q => q.Id.Equals(id), q => q.Include(q => q.Questions).ThenInclude(a => a.Answers)).FirstOrDefaultAsync();
            return quiz;
        }
    }
}
