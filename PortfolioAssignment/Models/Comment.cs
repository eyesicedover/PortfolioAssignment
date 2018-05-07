using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAssignment.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public virtual Post Post { get; set; }

        public Comment()
        { }

        public Comment(int postId, string author, string content)
        {
            PostId = postId;
            Author = author;
            Content = content;
        }
    }
}
