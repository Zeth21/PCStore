using PCStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{


    public class CommentVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentVoteId { get; set; }
        public VoteType CommentVoteValue { get; set; }
        public string? CommentVoteUserId { get; set; }
        public int CommentVoteCommentId { get; set; }

        [ForeignKey("CommentVoteUserId")]
        public User? User { get; set; }

        [ForeignKey("CommentVoteCommentId")]
        public Comment? Comment { get; set; }
    }
}
