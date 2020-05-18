using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Post : AuditableEntity<long>
    {
        public Post()
        {
            Tags = new HashSet<Tag>();
        }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Title only a maximum of 500 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Short Description only a maximum of 500 characters")]
        [Column("Short Description")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(50000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        public string PosterImg { get; set; }

        public bool Published { get; set; }

        [Column("Posted On")]
        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }

        public bool Modified { get; set; }

        public DateTime ModifiedDate { get; set; }

        [DefaultValue(0)]
        public int ViewCount { get; set; }

        [DefaultValue(0)]
        public int RateCount { get; set; }

        [DefaultValue(0)]
        public int TotalRate { get; set; }


        private decimal _rate;
        [DefaultValue(0)]
        [Display(Name = "Rate")]
        [NotMapped]
        public decimal Rate
        {
            get => _rate;
            set => _rate = RateCount > 0 ? TotalRate / RateCount : 0;
        }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public void UpdateView()
        {
            ViewCount++;
        }
    }
}
