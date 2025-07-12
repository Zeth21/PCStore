using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults
{
    public class UpdateCommentResult
    {
        public int CommentId { get; set; }
        public required string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
