using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

[Table("Transactions")]
public class Transaction
{
    [NotMapped]
    public string ObjectType => "Transaction";
    [NotMapped]
    public string Purpose {get; set;}
    
    [Key]
    public int TransactionId { get; set; }
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }
    public int CryptoValue { get; set; }
    public int CashValue { get; set; }
    public string CryptocurrencyName { get; set; }
    public DateTime DateTime { get; set; }
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
    
    public static bool HandleReceivedTransaction(string data, out string message)
{
    // Deserialize the Transaction object
    Transaction receivedTransaction = JsonConvert.DeserializeObject<Transaction>(data);
    	if(!receivedTransaction.InsertTransaction(out message)){
    	return false;
    	}

    // Process the received transaction object as needed
    Console.WriteLine($"Deserialized Transaction: {receivedTransaction.FromAddress}, {receivedTransaction.ToAddress}, {receivedTransaction.CashValue}, {receivedTransaction.CryptoValue}, {receivedTransaction.CryptocurrencyName}, {receivedTransaction.DateTime}");
    return true;
}

   public bool InsertTransaction(out string message)
    {
        try
        {
            using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext class
            {
                this.TransactionsHash = HashTransaction();
                this.ValidationStatus = "Not validated";
                Console.WriteLine($"Deserialized Transaction: {this.FromAddress}, {this.ToAddress}, {this.CashValue}, {this.CryptoValue}, {this.CryptocurrencyName}, {this.DateTime}, {this.ValidationStatus}, {this.TransactionsHash}");
                context.Transactions.Add(this);
                Console.WriteLine("Transaction added!");
                context.SaveChanges();
                message = "Success";
            }
        }
        catch (Exception ex)
        {
            message = "Error: {ex.Message}";
            Console.WriteLine(ex.Message);
            return false;          
        }
        return true;
    }
    
    public string HashTransaction()
    {
            // Concatenate the values of the properties
            var concatenatedString = $"{FromAddress}{ToAddress}{CryptoValue}{CashValue}{CryptocurrencyName}{DateTime}";

            // Hash the concatenated string (you can use any hash function you prefer)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatenatedString));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
    }
    
    
}
