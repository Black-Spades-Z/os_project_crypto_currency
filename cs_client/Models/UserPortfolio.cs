using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

[Table("UserPortfolio")]
public class UserPortfolio
{
    public string ObjectType => "UserPortfolio";

    public int UserId { get; set; }
    public decimal Bitcoin { get; set; }
    public decimal Ethereum { get; set; }
    public decimal Ripple { get; set; }
    public decimal Litecoin { get; set; }
    public decimal Cardano { get; set; }
    public decimal Polkadot { get; set; }
    public decimal BinanceCoin { get; set; }
    public decimal Chainlink { get; set; }
    public decimal Stellar { get; set; }
    public decimal BitcoinCash { get; set; }
    public decimal Dogecoin { get; set; }
    public decimal USD_Coin { get; set; }
    public decimal Aave { get; set; }
    public decimal Cosmos { get; set; }
    public decimal Monero { get; set; }
    public decimal Neo { get; set; }
    public decimal Tezos { get; set; }
    public decimal Maker { get; set; }
    public decimal EOS { get; set; }
    public decimal TRON { get; set; }
    public decimal VeChain { get; set; }
    public decimal Solana { get; set; }
    public decimal Theta { get; set; }
    public decimal Dash { get; set; }
    public decimal Uniswap { get; set; }
    public decimal Compound { get; set; }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public static UserPortfolio Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<UserPortfolio>(json);
    }
    
    public static UserPortfolio WaitForAccountPortfolio(NetworkStream stream)
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
		UserPortfolio userPortfolio = JsonConvert.DeserializeObject<UserPortfolio>(response);
		return userPortfolio;
	    }
	    catch (Exception e)
	    {
		Console.WriteLine($"Error in waiting for response: {e}");
	    }
	    return null;
	}
 
}
