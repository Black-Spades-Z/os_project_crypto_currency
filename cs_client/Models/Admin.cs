using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Admin")]
public class Admin
{
	[Key]
	public int id {get; set; }

	public string Email { get; set; }
    	public string PasswordHash { get; set; }


	 public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static Admin Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Admin>(json);
    }

	public static List<Admin> WaitForAdminList(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[2097152];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); // Exit the client if the server closes the connection
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
		List<Admin> usersList = JsonConvert.DeserializeObject<List<Admin>>(response);
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
