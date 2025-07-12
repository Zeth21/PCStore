namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands
{
    public class RemoveOrderStatusCommand
    {
        public int StatusId { get; set; }
        public RemoveOrderStatusCommand(int statusId)
        {
            StatusId = statusId;
        }
    }
}
