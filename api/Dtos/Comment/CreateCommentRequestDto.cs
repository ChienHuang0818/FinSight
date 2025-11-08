using System.ComponentModel.DataAnnotations;
public class CreateCommentRequestDto
{   [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    [MaxLength(280, ErrorMessage = "Title must be less than 280 characters long")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(10, ErrorMessage = "Comment must be at least 10 characters long")]
    [MaxLength(1000, ErrorMessage = "Content must be less than 1000 characters long")]
    public string Content { get; set; } = string.Empty;
} 