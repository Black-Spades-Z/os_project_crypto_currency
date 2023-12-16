using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Cryptography;

using static UserPortfolio;
using static Cryptocurrency;

public static class MinerUtil
{	
	public static bool WaitForValidationResponse(NetworkStream stream)
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
		Console.WriteLine($"Response from server: {response}");
		if(response == "Available"){
			return true;
		}
		
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return false;
	} 

	public static bool WaitForBlockValidationResponse(NetworkStream stream)
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
		Console.WriteLine($"Response from server: {response}");
		if(response == "Available"){
			return true;
		}
		
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return false;
	}
	
	public static Transaction WaitForTransactionForValidation(NetworkStream stream)
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
		Transaction userTransaction = JsonConvert.DeserializeObject<Transaction>(response);
		return userTransaction;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
		return null;
	    }
	    
	}

	public static Block WaitForBlockForValidation(NetworkStream stream)
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
		Block block = JsonConvert.DeserializeObject<Block>(response);
		return block;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
		return null;
	    }
	    
	}
	
	public static bool ValidateTransaction(Wallet seller, Wallet buyer, UserPortfolio pSeller, UserPortfolio pBuyer, Transaction transaction, out string message)
	{
		if(buyer.Balance < transaction.CashValue)
		{
			message = "Cash Balance of buyer is not sufficient ! ";
			return false;
		}
		
		Console.WriteLine($"Checking seller assets");
		decimal sellerCryptoAmount = FindCryptoCurrency(transaction.CryptocurrencyName, pSeller);
		decimal buyerCryptoAmount = FindCryptoCurrency(transaction.CryptocurrencyName, pBuyer);
		

		if(sellerCryptoAmount < transaction.CryptoValue)
		{
			message = "Crypto Balance of seller is not sufficient ! ";
			return false;
		}
		
		buyer.Balance -= transaction.CashValue;
		seller.Balance += transaction.CashValue;
		
		PropertyInfo property = typeof(UserPortfolio).GetProperty(transaction.CryptocurrencyName);
		property.SetValue(pSeller, sellerCryptoAmount - transaction.CryptoValue - transaction.TransactionFee);
		property.SetValue(pBuyer, buyerCryptoAmount + transaction.CryptoValue);
		
		message = "Valid";
		return true;
	}

	public static bool ValidateServerTransaction(List<Cryptocurrency> assets, Wallet buyer, UserPortfolio pBuyer, Transaction transaction, out Cryptocurrency crypto)
	{
		Cryptocurrency varCrypto = new();
	
		if(buyer.Balance < transaction.CashValue)
		{
			//message = "Cash Balance of buyer is not sufficient ! ";
			crypto = varCrypto;
			return false;
		}
		
		Console.WriteLine($"Checking seller assets");
		decimal buyerCryptoAmount = FindCryptoCurrency(transaction.CryptocurrencyName, pBuyer);	
		decimal selletCryptoAmount;
		
		foreach(Cryptocurrency c in assets)
		{
			if(c.Name == transaction.CryptocurrencyName)
			{
				varCrypto = c;
			}
		}
		
		
		if(varCrypto.Amount < transaction.CryptoValue)
		{
			//message = "Crypto Balance of server is not sufficient ! ";
			crypto = varCrypto;
			return false;
		}
		
		buyer.Balance -= transaction.CashValue;
		//seller.Balance += transaction.CashValue;
		
		varCrypto.Amount = varCrypto.Amount - transaction.CryptoValue - transaction.TransactionFee;
		
		
		PropertyInfo property = typeof(UserPortfolio).GetProperty(transaction.CryptocurrencyName);
		property.SetValue(pBuyer, buyerCryptoAmount + transaction.CryptoValue);
		
		//message = "Valid";
		crypto = varCrypto;
		return true;
	}
	
	public static decimal FindCryptoCurrency(string CryptoCurrencyName, UserPortfolio userPortfolio)
	{
		Type userPortfolioType = typeof(UserPortfolio);
		
		
		// Iterate over the properties of UserPortfolio
		foreach (PropertyInfo propertyInfo in userPortfolioType.GetProperties())
		{
		    // Check if the property name matches the CryptocurrencyName
		    if (propertyInfo.Name == CryptoCurrencyName)
		    {
		        // Get the value of the matched property
		        decimal cryptocurrencyAmount = (decimal)propertyInfo.GetValue(userPortfolio);
		        Console.WriteLine($"{CryptoCurrencyName} amount: {cryptocurrencyAmount}");
		        return cryptocurrencyAmount;
		    }
        	}
        	return 0;
	}

	public static void HashBlock(Block block)
	{
		block.TotalAmount = CalculateTotalAmount(block);
	
		List<string> transactionHashes = block.BlockTransactions.Select(t => t.TransactionsHash).ToList();

		while (transactionHashes.Count > 1)
		{
		    List<string> newHashes = new List<string>();

		    // Combine pairs of hashes and hash them together
		    for (int i = 0; i < transactionHashes.Count; i += 2)
		    {
		        string hashPair = transactionHashes[i] + (i + 1 < transactionHashes.Count ? transactionHashes[i + 1] : "");
		        newHashes.Add(Hash(hashPair));
		    }

		    transactionHashes = newHashes;
		}

        	block.RootHash = transactionHashes[0];
        	block.TotalTransactions = 20;
        	block.Timestamp = DateTime.Now;
        	block.Hash = HashBlockHeader(block);   
        	
        	Console.WriteLine($"{block.BlockNumber}{block.RootHash}{block.TotalAmount}{block.Timestamp}{block.TotalTransactions}{block.PreviousHash}{block.Hash}");   	
	}
	
	public static int CalculateTotalAmount(Block block)
	{
		int sum = 0;
		foreach(Transaction t in block.BlockTransactions)
		{
			sum += t.CashValue;
		}
		return sum;
	}
	
	public static string HashBlockHeader(Block block)
    	{
            // Concatenate the values of the properties
            var concatenatedString = $"{block.BlockNumber}{block.RootHash}{block.TotalAmount}{block.Timestamp}{block.TotalTransactions}{block.PreviousHash}";

            // Hash the concatenated string (you can use any hash function you prefer)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(concatenatedString));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
    	}
	
	private static string Hash(string input)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
		    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
		    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		}
	}
}
