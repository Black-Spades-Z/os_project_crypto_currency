using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [NotMapped]
    public string ObjectType => "User";
    [NotMapped]
    public string Purpose {get; set;}
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Nationality { get; set; }
    public DateTime JoinDate { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public static User Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<User>(json);
    }
    
    public static void HandleReceivedUser(string data)
    {
    	// Deserialize the User object
    	User receivedUser = JsonConvert.DeserializeObject<User>(data);

    	// Process the received user object as needed
    	Console.WriteLine($"Deserialized User: {receivedUser.Email}, {receivedUser.PasswordHash}, {receivedUser.Address}, {receivedUser.FullName}, {receivedUser.PhoneNumber}, {receivedUser.Nationality}");
    }
    
    public static User GetUserRegsitrationDetails()
    {
        Console.WriteLine("Enter user details:");

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password Hash: ");
        string passwordHash = Console.ReadLine();

        Console.Write("Full Name: ");
        string fullName = Console.ReadLine();

        Console.Write("Date of Birth (YYYY-MM-DD): ");
        DateTime dateOfBirth;
        DateTime.TryParse(Console.ReadLine(), out dateOfBirth);

        Console.Write("Address: ");
        string address = Console.ReadLine();

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Nationality: ");
        string nationality = Console.ReadLine();

        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            Address = address,
            PhoneNumber = phoneNumber,
            Nationality = nationality
        };
    }
    
    public static User GetUserPortfolioDetails()
    {
        Console.WriteLine("Enter user details:");

        Console.Write("UserId : ");
        int userId = Convert.ToInt32(Console.ReadLine());

        return new User
        {
            UserId = userId
        };
    }
    
    public static User GetUserLoginDetails()
    {
        Console.WriteLine("Enter user details:");

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string passwordHash = Console.ReadLine();

        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
        };
    }
}
