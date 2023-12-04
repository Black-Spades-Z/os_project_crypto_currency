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
	    string fromAddress = Console.ReadLine();

	    Console.Write("To Address: ");
	    string toAddress = Console.ReadLine();

	    Console.Write("Cash Value: ");
	    int cashValue;
	    while (!int.TryParse(Console.ReadLine(), out cashValue))
	    {
		Console.Clear(); // Clear the console to ensure a clean input prompt
		Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		Console.Write("Value: ");
	    }
	    
	    Console.Write("Value: ");
	    int cryptoValue;
	    while (!int.TryParse(Console.ReadLine(), out cryptoValue))
	    {
		Console.Clear(); // Clear the console to ensure a clean input prompt
		Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
		Console.Write("Value: ");
	    }

	    Console.Write("Cryptocurrency (Bitcoin, Ethereum, Litecoin, etc.): ");
	    string cryptocurrency = Console.ReadLine();

	    Console.Write("Date and Time (YYYY-MM-DD HH:mm:ss): ");
	    DateTime dateTime;
	    DateTime.TryParse(Console.ReadLine(), out dateTime);
	    
	    return new Transaction
	    {
		FromAddress = fromAddress,
		ToAddress = toAddress,
		CashValue = cashValue,
		CryptoValue = cryptoValue,
		CryptocurrencyName = cryptocurrency,
		DateTime = dateTime
	    };
	}
	
	
	public static void WaitForUserTransaction(NetworkStream stream)
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
