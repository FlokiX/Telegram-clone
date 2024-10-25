namespace TelegramClone.Core.Models;
public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Username { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public DateTime DateOfRegistration { get; }

    // Новые поля для чата
    public string IPAddress { get; private set; } = "0.0.0.0";
    public int Port { get; private set; } = 0;
    public bool IsOnline { get; private set; } = false;

    public List<Contact> Contacts { get; set; } = new();

    public void AddContact(Contact contact)
    {
        if (contact == null)
        {
            throw new ArgumentNullException(nameof(contact), "Контакт не может быть null.");
        }

  
        if (Contacts.Any(c => c.ContactUsername == contact.ContactUsername))
        {
            throw new InvalidOperationException("Контакт с таким именем уже существует.");
        }

        Contacts.Add(contact); 
    }


    private User(Guid Id, string username, string email, string password, DateTime dateOfRegistration, string ipAddress, int port, bool isOnline)
    {
        Username = username;
        Email = email;
        PasswordHash = password;
        DateOfRegistration = dateOfRegistration;
        IPAddress = ipAddress;
        Port = port;
        IsOnline = isOnline;
    }

    // Существующий метод создания
    public static (User user, string error) Create(Guid Id, string username, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length > 20)
        {
            return (null, "Username cannot be null or empty and must be less than or equal to 20 characters.");
        }
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            return (null, "Email must be valid and end with 'gmail.com'.");
        }

        string finalPasswordHash;

        // Проверяем, был ли передан хэшированный пароль
        if (!string.IsNullOrEmpty(password) && IsHashedPassword(password))
        {
            finalPasswordHash = password;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return (null, "Password must be at least 8 characters long.");
            }
            finalPasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        var newUser = new User(Id, username, email, finalPasswordHash, DateTime.UtcNow, "0.0.0.0", 0, false);
        return (newUser, null);
    }

    // Новый метод создания пользователя с полями для чата
    public static (User user, string error) CreateWithChatInfo(Guid Id, string username, string email, string password, string ipAddress, int port, bool isOnline)
    {
  
        var (user, error) = Create(Id, username, email, password);

        if (user == null)
        {
            return (null, error); 
        }

        // Проверка IP-адреса
        if (!IsValidIPAddress(ipAddress))
        {
            return (null, "Invalid IP address.");
        }

        // Проверка порта
        if (port < 1024 || port > 65535)
        {
            return (null, "Port must be between 1024 and 65535.");
        }

       
        user.IPAddress = ipAddress;
        user.Port = port;
        user.IsOnline = isOnline;

        return (user, null); 
    }

    // Проверка правильности IP-адрес
    private static bool IsValidIPAddress(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            return false;
        }

       
        return System.Net.IPAddress.TryParse(ipAddress, out _);
    }



    private static bool IsHashedPassword(string passwordHash)
    {
        return !string.IsNullOrWhiteSpace(passwordHash) && passwordHash.Length == 60;
    }

    public bool VerifyPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));
        }

        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    private static bool IsValidEmail(string email)
    {
        return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
    }
}
