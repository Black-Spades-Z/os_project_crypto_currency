using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

public class UserOffer
{
    [NotMapped]
    public string ObjectType => "UserOffer";
    [NotMapped]
    public string Purpose {get; set;}
    
    public int OfferId { get; set;  }
    public string FromAddress { get; set;  }
    public decimal CryptoValue { get; set;  }
    public decimal CashValue { get; set;  }
    public string CryptocurrencyName { get; set;  }
    public DateTime DateTime { get; set; }
    public bool Available { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static UserOffer Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<UserOffer>(json);
    }
    
    public static List<UserOffer>  WaitForUserOffers(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[65536];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); // Exit the client if the server closes the connection
			return null;
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
		List<UserOffer> userOffers = JsonConvert.DeserializeObject<List<UserOffer>>(response);
		Console.WriteLine(userOffers.Count);

		return userOffers;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}
    
    public static UserOffer GetUserOffer()
	{
	    Console.WriteLine("Enter User Offer details:");

	    Console.Write("From Address: ");
	    string fromAddress = Console.ReadLine();

	    Console.Write("Cash Value: ");
	    decimal cashValue;
	    while (!decimal.TryParse(Console.ReadLine(), out cashValue))
	    {
		Console.Clear(); // Clear the console to ensure a clean input prompt
		Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		Console.Write("Value: ");
	    }
	    
	    Console.Write("Crypto Value: ");
	    decimal cryptoValue;
	    while (!decimal.TryParse(Console.ReadLine(), out cryptoValue))
	    {
		Console.Clear(); // Clear the console to ensure a clean input prompt
		Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		Console.Write("Value: ");
	    }

	    Console.Write("Cryptocurrency (Bitcoin, Ethereum, Litecoin, etc.): ");
	    string cryptocurrency = Console.ReadLine();
	    
	    return new UserOffer
	    {
		FromAddress = fromAddress,
		CashValue = cashValue,
		CryptoValue = cryptoValue,
		CryptocurrencyName = cryptocurrency
	    };
	}
	public static UserOffer setUserOffer(string fromAddress, decimal cashValue, decimal cryptoValue, string cryptoCurrencyName)
	{

	    return new UserOffer
	    {
		FromAddress = fromAddress,
		CashValue = cashValue,
		CryptoValue = cryptoValue,
		CryptocurrencyName = cryptoCurrencyName
	    };
	}
}
