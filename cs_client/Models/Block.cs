using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

[Table("Block")]
public class Block
{
    [NotMapped]
    public string ObjectType => "Block";
    
    [NotMapped]
    public string Purpose {get; set;}
    
    [NotMapped]
    public List<Transaction> BlockTransactions = new();


    [Key]
    public int BlockNumber { get; set; }

    public string RootHash { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime Timestamp { get; set; }

    public int TotalTransactions { get; set; }

    public string PreviousHash { get; set; }

    public string Hash { get; set; }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static Block Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Block>(json);
    }
    private static  void writeBlockChainJsonFile(string response){
        string filePath = "User/blockChain.json"; // Replace with your file path

        try
        {
            // Serialize object to JSON
            string jsonString = JsonConvert.SerializeObject(response);

            // Write JSON string to file
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine("JSON data saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving JSON data to file: " + ex.Message);
        }
    }
    
    public static List<Block> WaitForBLockchain(NetworkStream stream)
	{
	    try
	    {
		byte[] buffer = new byte[65536];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);

		if (bytesRead <= 0)
		{
		    Console.WriteLine("Connection closed by server.");
		    Environment.Exit(0); // Exit the client if the server closes the connection
		}

		string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        writeBlockChainJsonFile(response);
		List<Block> blockchain = JsonConvert.DeserializeObject<List<Block>>(response);
		Console.WriteLine(blockchain.Count);
		return blockchain;
		
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}
}

