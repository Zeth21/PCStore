using System.Text.Json.Serialization;

namespace PCStore.UI.Models.Commands.ShopCartCommands
{
    public class CreateShopCartItemCommand
    {
        public int ProductId { get; set; }
    }
}
