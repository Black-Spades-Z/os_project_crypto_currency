using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


[Table("Miners")]
public class Miner {
	[Key]
	public int UserId { get; set; }
	public DateTime JoinDate { get; set; }
	

public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static User Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<User>(json);
    }

    public static List<Miner> WaitForMiningList(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[2097192];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); // Exit the client if the server closes the connection
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
		List<Miner> usersList = JsonConvert.DeserializeObject<List<Miner>>(response);
		Console.WriteLine(usersList.Count);
		return usersList;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}

}
