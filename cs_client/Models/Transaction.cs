using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaction
{
    [NotMapped]
    public string ObjectType => "Transaction";
    [NotMapped]
    public string Purpose {get; set;}
    [NotMapped]
    public int MinerId {get; set;}
    [NotMapped]
    public decimal TransactionFee {get; set;}
    
    public int TransactionId { get; init; }
    public string FromAddress { get; init; }
    public string ToAddress { get; init; }
    public int CryptoValue { get; init; }
    public int CashValue { get; init; }
    public string CryptocurrencyName { get; init; }
    public DateTime DateTime { get; init; }
    public string TransactionsHash { get; set; }
    public string ValidationStatus { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static Transaction Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Transaction>(json);
    }
    
    public static bool HandleReceivedTransaction(string data)
	{
	    // Deserialize the Transaction object
	    Transaction receivedTransaction = JsonConvert.DeserializeObject<Transaction>(data);

	    // Process the received transaction object as needed
	    Console.WriteLine($"Deserialized Transaction: {receivedTransaction.FromAddress}, {receivedTransaction.ToAddress}, {receivedTransaction.CashValue}");
	    return true;
	}

    public static Transaction GetTransaction()
	{
	    Console.WriteLine("Enter transaction details:");

	    Console.Write("From Address: ");
	    //string fromAddress = Console.ReadLine();
	    string fromAddress = "F99E527860ECB668DEA0F2E8840335F9648600D1E0937E9F047C1714A27505D0";

	    Console.Write("To Address: ");
	    //string toAddress = Console.ReadLine();
	    string toAddress = "0EBABFC7C06FC828AA01D82979AEE3A21236D41F228FB56EDF8F378BA18A849E";

	    Console.Write("Cash Value: ");
	    int cashValue = 1;
	    //while (!int.TryParse(Console.ReadLine(), out cashValue))
	    //{
		//Console.Clear(); // Clear the console to ensure a clean input prompt
		//Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		//Console.Write("Value: ");
	    //}
	    
	    Console.Write("Value: ");
	    int cryptoValue = 1;
	    //while (!int.TryParse(Console.ReadLine(), out cryptoValue))
	    //{
		//Console.Clear(); // Clear the console to ensure a clean input prompt
		//Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		//Console.Write("Value: ");
	   // }

	    Console.Write("Cryptocurrency (Bitcoin, Ethereum, Litecoin, etc.): ");
	    //string cryptocurrency = Console.ReadLine();
	    string cryptocurrency = "Bitcoin";
	    
	    return new Transaction
	    {
		FromAddress = fromAddress,
		ToAddress = toAddress,
		CashValue = cashValue,
		CryptoValue = cryptoValue,
		CryptocurrencyName = cryptocurrency
	    };
	}
	
	public static void WaitForUserTransactionList(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[16384];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); // Exit the client if the server closes the connection
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
		List<Transaction> userTransactions = JsonConvert.DeserializeObject<List<Transaction>>(response);
		Console.WriteLine(userTransactions.Count);
		
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	}
}
