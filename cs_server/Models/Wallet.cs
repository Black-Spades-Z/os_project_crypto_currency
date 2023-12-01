using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

[Table("Wallets")]
public class Wallet 
{
    [Key]
    public int WalletId { get; set; }

    public string WalletAddress { get; set; }

    public int UserId { get; set; }

    public decimal Balance { get; set; }
    
    public bool InsertWallet(out string message)
    {
        try
        {
            using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext class
            {
                Console.WriteLine("Adding wallet!");
                context.Wallets.Add(this);
                Console.WriteLine("Wallet added!");
                context.SaveChanges();
                message = "Success";
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
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

