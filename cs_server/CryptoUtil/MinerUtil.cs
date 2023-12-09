using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;

using static Transaction;

public static class MinerUtil
{	
	public static bool HandleValidationStatus(string data, out string message)
	{ 
	    try
	    {
	    	using (var context = new AppDbContext())
		{
			var countOfInvalidTransactions = context.Transactions.Count(p => p.ValidationStatus == "Pending");
			if(countOfInvalidTransactions > 0){
				message = "Available";
			}else{
				message = "Failure. No validation needed.";
			}
		}
	    }
	    catch (Exception ex)
	    {
	    	Console.WriteLine(ex.Message);
		message = "Failure.";
		return false;
	    }
	    return true;
	}
	
	public static bool HandleTransactionForValidation(string data, out string message)
	{ 
	    try
	    {
	    	using (var context = new AppDbContext())
		{
			var pendingTransaction = context.Transactions.Where(p => p.ValidationStatus == "Pending").FirstOrDefault();
			
			
			decimal transactionFee = context.ServerAssets.Where(s => s.Name == pendingTransaction.CryptocurrencyName).Select(s => s.Fee).First();
			
			
			pendingTransaction.TransactionFee +=transactionFee;
			pendingTransaction.ValidationStatus = "...";
			context.SaveChanges();
			
			if(pendingTransaction is not null){
				message = JsonConvert.SerializeObject(pendingTransaction);
			}else{
				message = "Failure.";
			}
		}
	    }
	    catch (Exception ex)
	    {
	    	Console.WriteLine(ex.Message);
		message = "Failure.";
		return false;
	    }
	    return true;
	}
	
	public static bool HandleValidationOfTransaction(string data, out string message)
	{ 
	    try
	    {
	    	using (var context = new AppDbContext())
		{
			Transaction receivedTransaction = Deserialize(data);
			 
			var transaction = context.Transactions.FirstOrDefault(p => p.TransactionId == receivedTransaction.TransactionId);
			if(transaction is not null)
			{
				transaction.ValidationStatus = receivedTransaction.Purpose;
				context.SaveChanges();
				message = "Success";
				AddTransactionFee(receivedTransaction.MinerId, receivedTransaction.CryptocurrencyName,receivedTransaction.TransactionFee );
			}
			else
			{
				message = "Failure.";
			}
		}
	    }
	    catch (Exception ex)
	    {
	    	Console.WriteLine(ex.Message);
		message = "Failure.";
		return false;
	    }
	    return true;
	}
	
	public static void AddTransactionFee(int userId, string CryptocurrencyName, decimal transactionFee)
	{
		try
	    {
	    	using (var context = new AppDbContext())
		{			
			UserPortfolio userPort = context.AccPortfolio.Where(p => p.UserId == userId).First();
			
			PropertyInfo property = typeof(UserPortfolio).GetProperty(CryptocurrencyName);
			object currentValue = property.GetValue(userPort);

			    // Assuming the property type is numeric (adjust the type accordingly)
			    if (currentValue is decimal decimalValue)
			    {
				// Increase the value by transactionFee
				decimalValue += transactionFee;

				// Set the updated value back to the property
				property.SetValue(userPort, decimalValue);
			    }
			context.SaveChanges();
		}
	    }
	    catch (Exception ex)
	    {
	    	Console.WriteLine(ex.Message);
	    }
	}

}
