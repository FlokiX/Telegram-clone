namespace TelegramClone.API.Contracts
{
    public record UserRequest (
        string Username,
        string Email,
        string PasswordHash);
    
}
