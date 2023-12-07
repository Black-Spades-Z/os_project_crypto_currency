using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;


[Table("ServerAssets")]
public class Cryptocurrency
{
	[Key]
	public string Name { get; set; }
	
	public decimal Price { get; set; }
	public decimal Fee { get; set; }
	public decimal Amount { get; set; }
	
	public static bool HandleServerAssetsRequest(string data, out string message)
	{
	    try
	    {
	    	using (var context = new AppDbContext())
		{
			var serverAssets = context.ServerAssets.ToList();
			message = JsonConvert.SerializeObject(serverAssets);
		}
	    }
	    catch (Exception ex)
	    {
	    	Console.WriteLine(ex.Message);
		message = "Failure to send Server Assets";
		return false;
	    }
	    return true;
	}
}
