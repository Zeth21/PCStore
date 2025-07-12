using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class Answer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        public required string AnswerText { get; set; }
        public DateTime AnswerDate { get; set; } = DateTime.Now;
        public required string AnswerUserId { get; set; }
        public int AnswerCommentId { get; set; }
        public int AnswerUpVoteCount { get; set; } = 0;
        public int AnswerDownVoteCount { get; set; } = 0;



        [ForeignKey("AnswerUserId")]
        public User? User { get; set; }

        [ForeignKey("AnswerCommentId")]
        public Comment? Comment { get; set; }

        public ICollection<AnswerVote>? AnswerVotes { get; set; }
    }
}
