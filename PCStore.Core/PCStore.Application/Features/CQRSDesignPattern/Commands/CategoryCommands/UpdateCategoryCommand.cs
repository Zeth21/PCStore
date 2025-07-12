namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands
{
    public class UpdateCategoryCommand
    {
        public int CategoryId { get; set; }
        public required string CategoryPath { get; set; }
        public required string CategoryName { get; set; }
    }
}
