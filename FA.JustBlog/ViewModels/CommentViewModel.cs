using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.ViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "{0} must be required.")]
        [StringLength(50, ErrorMessage = "{0} only a maximum of {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must be required.")]
        [StringLength(100, ErrorMessage = "{0} only a maximum of {1} characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Header must be required.")]
        [StringLength(100, ErrorMessage = "The Header only a maximum of {1} characters")]
        public string CommentHeader { get; set; }

        [Required(ErrorMessage = "Content must be required.")]
        [StringLength(1000, ErrorMessage = "Content only a maximum of {1} characters")]
        public string CommentText { get; set; }

        public long PostId { get; set; }
    }
}