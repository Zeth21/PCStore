namespace PCStore.Domain.Enum
{
    public enum NotifType
    {
        Campaign = 0,
        Order = 1,
        Warning = 2,
        Product = 3
    }

    public enum VoteType
    {
        Up = 1,
        Down = 0
    }

    public enum CouponTargetType 
    {
        AllProducts = 0,
        SpecificProducts = 1,
        Categories = 2,
        Brands = 3,
        ProductTypes = 4,
    }

    public enum DiscountTargetType
    {
        AllProducts = 0,
        SpecificProducts = 1,
        Categories = 2,
        Brands = 3,
        ProductTypes = 4,
    }
}
