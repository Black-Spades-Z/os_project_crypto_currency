using Newtonsoft.Json;

public class User
{
    public string ObjectType => "User";
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
    
    public static User GetUserDetails()
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
        
        Console.Write("Join Date (YYYY-MM-DD): ");
        DateTime joinDate;
        DateTime.TryParse(Console.ReadLine(), out joinDate);

        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            Address = address,
            PhoneNumber = phoneNumber,
            Nationality = nationality,
            JoinDate = joinDate
        };
    }
}
