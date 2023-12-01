using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

[Table("UserPortfolio")]
public class UserPortfolio
{
    [Key]
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
    
    public static bool HandleUserPortfolioRequest(string data, out string message)
	{ 
            User user = JsonConvert.DeserializeObject<User>(data);
	    try
	    {
	    	using (var context = new AppDbContext())
		{
			var userPortfolio = context.AccPortfolio.FirstOrDefault(p => p.UserId == user.UserId);
			message = JsonConvert.SerializeObject(userPortfolio);
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
