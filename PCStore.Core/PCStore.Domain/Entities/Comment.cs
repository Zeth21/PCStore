using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PCStore.Domain.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public bool CommentIsQuestion { get; set; } = false;
        [JsonIgnore]
        public string? CommentUserId { get; set; } = "";
        public int CommentProductId { get; set; }
        [JsonIgnore]
        public int CommentAnswerCount { get; set; } = 0;
        [JsonIgnore]
        public int CommentUpVoteCount { get; set; } = 0;
        [JsonIgnore]
        public int CommentDownVoteCount { get; set; } = 0;

        [ForeignKey("CommentUserId")]
        public User? User { get; set; }

        [ForeignKey("CommentProductId")]
        public Product? Product { get; set; }

        public ICollection<Answer> Answers { get; set; } = [];
        public ICollection<CommentVote>? CommentVotes { get; set; }

    }
}
