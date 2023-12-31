using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

public class Wallet 
{
    [NotMapped]
    public string ObjectType => "Wallet";
    [NotMapped]
    public string Purpose {get; set;}

    public int WalletId { get; set; }

    public string WalletAddress { get; set; }

    public int UserId { get; set; }

    public decimal Balance { get; set; }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public static Wallet Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Wallet>(json);
    }
    
    public static Wallet WaitForWallet(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[16384];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); 
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
		Wallet wallet = JsonConvert.DeserializeObject<Wallet>(response);
		return wallet;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}
}

