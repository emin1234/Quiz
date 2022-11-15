namespace Service.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; } 
        public string Text { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
