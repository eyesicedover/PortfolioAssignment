using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAssignment.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime PostDate { get; set; }
        public string Content { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
} 
