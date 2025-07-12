using Microsoft.AspNetCore.Identity;

namespace PCStore.Domain.Entities
{
    public class User : IdentityUser
    {
        public string ProfilePhoto { get; set; } = "default-profile.jpg";
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public ICollection<Order>? Orders { get; set; } = [];
        public ICollection<CouponUsage>? CouponUsages { get; set; } = [];
        public ICollection<ProductRate>? ProductRates { get; set; } = [];
        public ICollection<Comment>? ProductComments { get; set; } = [];
        public ICollection<CommentVote>? ProductCommentVotes { get; set; } = [];
        public ICollection<AnswerVote>? AnswerVotes { get; set; } = [];
        public ICollection<Notification>? Notifications { get; set; } = [];
        public ICollection<Address>? Addresses { get; set; } = [];
        public ICollection<FollowedProduct>? FollowedProducts { get; set; } = [];
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; } = [];


    }
}
