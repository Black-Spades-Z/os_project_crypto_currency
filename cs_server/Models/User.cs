using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class User
{
    public string ObjectType => "User";
    public int UserId {get; set;}
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
    
    public bool InsertUser(out string message)
    {
        try
        {
            using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext class
            {
            	string emailVar = context.Users
                	.Where(user => user.Email == this.Email)
                	.Select(user => user.Email)
                	.FirstOrDefault();
                	
                if(emailVar==null){
                	Console.WriteLine("Adding user!");
                context.Users.Add(this);
                Console.WriteLine("User added!");
                context.SaveChanges();
                message = "Success";
                }
                else{
                message = "Email is already used !";
                return false;
                }
            
                
            }
        }
        catch (Exception ex)
        {
            message = "Error: {ex.Message}";
            return false;          
        }
        return true;
    }
    
    public static bool HandleReceivedUser(string data, out string message)
    {
    	// Deserialize the User object
    	User receivedUser = JsonConvert.DeserializeObject<User>(data);
    	if(!receivedUser.InsertUser(out message)){
    	return false;
    	}
    	
    	Wallet wallet = new();
    	WalletUtil w = new();
    	wallet.WalletAddress = w.GenerateAddress();
    	wallet.UserId = GetUserIdFromEmail(receivedUser.Email);
    	if(!wallet.InsertWallet()){
    		message = "Failure";
    		return false;
    	}
	message = "Success";
    	return true;
    }
    
    public static int GetUserIdFromEmail(string userEmail)
    {
        try
        {
            using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext class
            {
            	int userId = context.Users
                	.Where(user => user.Email == userEmail)
                	.Select(user => user.UserId)
                	.FirstOrDefault();
                	
                 
            

            if (userId != 0)
            {
                return userId;
            }
            else
            {
                // Handle the case where the email is not associated with any user
                Console.WriteLine("User with the given email not found.");
                return -1; // Or some other value indicating that the email is not found
            }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Error: {ex.Message}");
            return -1; // Or some other value indicating an error
        }
    }
}
