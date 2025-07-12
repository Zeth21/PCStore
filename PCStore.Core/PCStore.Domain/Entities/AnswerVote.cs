using PCStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCStore.Domain.Entities
{
    public class AnswerVote
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerVoteId { get; set; }
        public VoteType AnswerVoteValue { get; set; }
        public string? AnswerVoteUserId { get; set; }
        public int AnswerVoteAnswerId { get; set; }


        [ForeignKey("AnswerVoteUserId")]
        public User? User { get; set; }

        [ForeignKey("AnswerVoteAnswerId")]
        public Answer? Answer { get; set; }
    }
}
