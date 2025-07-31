using System.ComponentModel.DataAnnotations;

namespace PCStore.UI.Models.Commands.UserCommands
{
    public class RegisterCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
