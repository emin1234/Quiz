using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model.Domain;

public class Answer
{
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [MaxLength(256)]
    public string Text { get; set; }

    public bool IsCorrectAnswer { get; set; } = false;

    [Required]
    [ForeignKey("Question")]
    public int QuestionId { get; set; }

    public Question Question { get; set; }
}