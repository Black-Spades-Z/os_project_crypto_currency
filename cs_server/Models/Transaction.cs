using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Transaction
{
    public string ObjectType => "Transaction";
    public int TransactionId { get; init; }
    public string FromAddress { get; init; }
    public string ToAddress { get; init; }
    public int CryptoValue { get; init; }
    public int CashValue { get; init; }
    public Cryptocurrency Cryptocurrency { get; init; }
    public DateTime DateTime { get; init; }
    public string TransactionsHash { get; set; }

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
}
