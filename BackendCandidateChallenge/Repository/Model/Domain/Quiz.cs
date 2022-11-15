using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Domain;

public class Quiz
{
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string Title { get; set; }

    public ICollection<Question> Questions { get; set; }    
}