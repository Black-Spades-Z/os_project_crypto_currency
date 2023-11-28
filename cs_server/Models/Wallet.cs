using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Wallet 
{
    public int WalletId { get; set; }

    public string WalletAddress { get; set; }

    public int UserId { get; set; }

    public decimal Balance { get; set; }
    
    public bool InsertWallet()
    {
        try
        {
            using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext class
            {
                Console.WriteLine("Adding wallet!");
                context.Wallets.Add(this);
                Console.WriteLine("Wallet added!");
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;          
        }
        return true;
    }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public static Wallet Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Wallet>(json);
    }
}

