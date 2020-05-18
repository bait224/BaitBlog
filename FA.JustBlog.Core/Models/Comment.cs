using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Comment : AuditableEntity<Guid>
    {
        [Required(ErrorMessage = "The Name must be required.")]
        [StringLength(50, ErrorMessage = "The Name only a maximum of 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email must be required.")]
        [StringLength(100, ErrorMessage = "The Email only a maximum of 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Comment Header must be required.")]
        [StringLength(100, ErrorMessage = "The Comment Header only a maximum of 100 characters")]
        public string CommentHeader { get; set; }

        [Required(ErrorMessage = "The Comment Content must be required.")]
        [StringLength(1000, ErrorMessage = "The Comment Content only a maximum of 1000 characters")]
        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }
        [ForeignKey("Post")]
        public long PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
