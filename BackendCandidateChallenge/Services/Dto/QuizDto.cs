namespace Service.Dto
{
    public class QuizDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
