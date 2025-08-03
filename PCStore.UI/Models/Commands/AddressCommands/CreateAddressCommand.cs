namespace PCStore.UI.Models.Commands.AddressCommands
{
    public class CreateAddressCommand
    {
        public required string AddressName { get; set; }
        public required string Description { get; set; }
    }
}
