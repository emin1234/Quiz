namespace Service.Dto
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; } = false;
        public QuestionDto Question { get; set; }
    }
}
