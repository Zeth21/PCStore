namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands
{
    public class CreateCategoryCommand
    {
        public required string CategoryPath { get; set; }
        public required string CategoryName { get; set; }
    }
}
