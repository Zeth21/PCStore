using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults
{
    public class CreateCommentResult
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int CommentAnswerCount { get; set; }
        public int CommentUpVoteCount { get; set; }
        public int CommentDownVoteCount { get; set; }
    }
}
