using Gtk;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using static Transaction;
using static User;
using static Cryptocurrency;
using static UserPortfolio;
using static UserOffer;
using static Wallet;
using static Block;

using static ServerAssetsWindow;
using static TransactionsWindow;
using static P2PWindow;
using static UserWindow;

public class UpdateServerAssetsWindow : ServerAssetsWindow
{
	public static Window Edit_window;
        public static Button update_price_button_edit_window;
        public static Button update_fee_button_edit_window;
        public static Button update_amount_button_edit_window;
        public static Entry price_entry_edit_window;
        public static Entry fee_entry_edit_window;
        public static Entry amount_entry_edit_window;
        
        static UpdateServerAssetsWindow()
    	{
    		Edit_window = (Window)builder.GetObject("Edit_window");
    		
    		update_price_button_edit_window = (Button)builder.GetObject("update_price_button_edit_window");
        	update_fee_button_edit_window = (Button)builder.GetObject("update_fee_button_edit_window");
        	update_amount_button_edit_window = (Button)builder.GetObject("update_amount_button_edit_window");
        	
        	price_entry_edit_window = (Entry)builder.GetObject("price_entry_edit_window");
        	fee_entry_edit_window = (Entry)builder.GetObject("fee_entry_edit_window");
        	amount_entry_edit_window = (Entry)builder.GetObject("amount_entry_edit_window");
        	
        	update_price_button_edit_window.Clicked += OnUpdatePriceButtonClicked;
            	update_fee_button_edit_window.Clicked += OnUpdateFeeButtonClicked;
            	update_amount_button_edit_window.Clicked += OnUpdateAmountButtonClicked;
    	}
    	
    	public static void OnUpdatePriceButtonClicked(object sender, EventArgs e)
        {	
        	string priceEntryText = price_entry_edit_window.Text;
        	//string feeEntryText = fee_entry_edit_window.Text;
        	//string amountEntryText = amount_entry_edit_window.Text;
		Cryptocurrency crypto = new();
		crypto.Name = currencyName[editIndex];
		crypto.Price = ConvertToDecimal(priceEntryText);
		crypto.Amount = ConvertToDecimal(currencyVolume[editIndex]);
		crypto.Fee = ConvertToDecimal(currencyFee[editIndex]);
	
		try
    		{
    			crypto.Purpose = "UpdateInServerAssets";
			string serializedCrypto = crypto.Serialize();
			SendMessage(_stream, serializedCrypto);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	    	price_entry_edit_window.Text = string.Empty;	
	    	foreach (Widget child in server_assets_market_box.Children)
		{
		    server_assets_market_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveServerAssets();
		for(int i=0; i < 26;i++)
        	{
        		AddFrameToMarketValuesServerAssetsWindow(i);
        	}  	
	    	Edit_window.Hide();
		return;		
        }
        
        public static void OnUpdateFeeButtonClicked(object sender, EventArgs e)
        {
        	string feeEntryText = fee_entry_edit_window.Text;
        	
		Cryptocurrency crypto = new();
		crypto.Name = currencyName[editIndex];
		crypto.Price = ConvertToDecimal(currencyPrice[editIndex]);
		crypto.Amount = ConvertToDecimal(currencyVolume[editIndex]);
		crypto.Fee = ConvertToDecimal(feeEntryText);
	
		try
    		{
    			crypto.Purpose = "UpdateInServerAssets";
			string serializedCrypto = crypto.Serialize();
			SendMessage(_stream, serializedCrypto);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	    	fee_entry_edit_window.Text = string.Empty;
	    	foreach (Widget child in server_assets_market_box.Children)
		{
		    server_assets_market_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveServerAssets();
		for(int i=0; i < 26;i++)
        	{
        		AddFrameToMarketValuesServerAssetsWindow(i);
        	} 
	    	Edit_window.Hide();
		return;		
        }
        
        public static void OnUpdateAmountButtonClicked(object sender, EventArgs e)
        {
        	string amountEntryText = amount_entry_edit_window.Text;
        	
		Cryptocurrency crypto = new();
		crypto.Name = currencyName[editIndex];
		crypto.Price = ConvertToDecimal(currencyPrice[editIndex]);
		crypto.Amount = ConvertToDecimal(amountEntryText);
		crypto.Fee = ConvertToDecimal(currencyFee[editIndex]);
	
		try
    		{
    			crypto.Purpose = "UpdateInServerAssets";
			string serializedCrypto = crypto.Serialize();
			SendMessage(_stream, serializedCrypto);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	    	amount_entry_edit_window.Text = string.Empty;
	    	foreach (Widget child in server_assets_market_box.Children)
		{
		    server_assets_market_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveServerAssets();
		for(int i=0; i < 26;i++)
        	{
        		AddFrameToMarketValuesServerAssetsWindow(i);
        	} 
	    	Edit_window.Hide();
		return;		
        }
        
        public static decimal ConvertToDecimal(string input)
	{
	    if (decimal.TryParse(input, out decimal result))
	    {
		return result;
	    }
	    else
	    {
		// Handle the case where the conversion fails
		Console.WriteLine($"Invalid input: {input}. Unable to convert to decimal.");
		return 0; // You may want to return a default value or throw an exception instead
	    }
	}
	
}
