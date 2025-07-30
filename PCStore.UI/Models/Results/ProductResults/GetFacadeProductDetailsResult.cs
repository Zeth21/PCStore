namespace PCStore.UI.Models.Results.ProductResults
{
    public class GetFacadeProductDetailsResult
    {
        public string? BrandName { get; set; }
        public GetProductInformationsResult? Informations { get; set; }
        public List<GetProductAttributesResult>? Attributes { get; set; }
        public List<GetQuestionsByProductIdResult>? Questions { get; set; }
        public List<GetCommentsByProductIdResult>? Comments { get; set; }
        public List<GetPhotosByProductIdResult>? Photos { get; set; }
    }
}
