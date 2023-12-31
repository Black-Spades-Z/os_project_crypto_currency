using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

public class Cryptocurrency
{
	[NotMapped]
    	public string ObjectType => "Crypto";
	[NotMapped]
	public string Purpose { get; set; }

	[Key]
	public string Name { get; set; }
	public decimal Price { get; set; }
	public decimal Fee { get; set; }
	public decimal Amount { get; set; }
	
	public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static Cryptocurrency Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Cryptocurrency>(json);
    }
	
	public static List<Cryptocurrency> WaitForServerAssets(NetworkStream stream)
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
		List<Cryptocurrency> serverAssets = JsonConvert.DeserializeObject<List<Cryptocurrency>>(response);
		Console.WriteLine(serverAssets.Count);
		return serverAssets;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}
}
