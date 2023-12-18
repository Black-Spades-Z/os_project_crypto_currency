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

public class EditUserWindow : UserWindow
{
	public static Window edit_user_window;
        public static Button update_email_button_e_u_w;
        public static Button delete_email_button_e_u_w;
        public static Button update_password_button_e_u_w;
        public static Entry password_entry_e_u_w;
        public static Entry email_entry_e_u_w;
        
        static EditUserWindow()
    	{
    		edit_user_window = (Window)builder.GetObject("edit_user_window");
    		
    		update_email_button_e_u_w = (Button)builder.GetObject("update_email_button_e_u_w");
        	delete_email_button_e_u_w = (Button)builder.GetObject("delete_email_button_e_u_w");
        	update_password_button_e_u_w = (Button)builder.GetObject("update_password_button_e_u_w");
        	
        	password_entry_e_u_w = (Entry)builder.GetObject("password_entry_e_u_w");
        	email_entry_e_u_w = (Entry)builder.GetObject("email_entry_e_u_w");
        	
        	update_email_button_e_u_w.Clicked += OnUpdateUserEmailButtonClicked;
            	delete_email_button_e_u_w.Clicked += OnDeleteEmailButtonClicked;
            	update_password_button_e_u_w.Clicked += OnUpdatePasswordEditButtonClicked;
    	}
    	
    	public static void OnUpdateUserEmailButtonClicked(object sender, EventArgs e)
        {	
        	string emailEntryText = email_entry_e_u_w.Text;
		User user = new();
		user.Purpose = "UpdateUser";
		
		user.UserId = listUser[editIndex].UserId;
		user.Email = emailEntryText;
		user.PasswordHash = listUser[editIndex].PasswordHash;
		user.FullName = listUser[editIndex].FullName;
		user.DateOfBirth = listUser[editIndex].DateOfBirth;
		user.Address = listUser[editIndex].Address;
		user.PhoneNumber = listUser[editIndex].PhoneNumber;
		user.Nationality = listUser[editIndex].Nationality;
		user.JoinDate = listUser[editIndex].JoinDate;
		
		try
    		{
			string serializedUser = user.Serialize();
			SendMessage(_stream, serializedUser);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	    	email_entry_e_u_w.Text = string.Empty;	
	    	foreach (Widget child in user_admin_box.Children)
		{
		    user_admin_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveUserPList();
		for(int i=0; i < listUser.Count;i++)
        	{
        		AddFrameToMarketValuesUserWindow(i);
        	}  	
	    	edit_user_window.Hide();
		return;		
        }
        
        public static void OnUpdatePasswordEditButtonClicked(object sender, EventArgs e)
        {	
        	string passwordEntryText = password_entry_e_u_w.Text;
		User user = new();
		user.Purpose = "UpdateUser";
		
		user.UserId = listUser[editIndex].UserId;
		user.Email = listUser[editIndex].Email;
		user.PasswordHash = passwordEntryText;
		user.FullName = listUser[editIndex].FullName;
		user.DateOfBirth = listUser[editIndex].DateOfBirth;
		user.Address = listUser[editIndex].Address;
		user.PhoneNumber = listUser[editIndex].PhoneNumber;
		user.Nationality = listUser[editIndex].Nationality;
		user.JoinDate = listUser[editIndex].JoinDate;
		
		try
    		{
			string serializedUser = user.Serialize();
			SendMessage(_stream, serializedUser);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	    	
	    	password_entry_e_u_w.Text = string.Empty;	
	    	foreach (Widget child in user_admin_box.Children)
		{
		    user_admin_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveUserPList();
		for(int i=0; i < listUser.Count;i++)
        	{
        		AddFrameToMarketValuesUserWindow(i);
        	}  	
	    	edit_user_window.Hide();
		return;		
        }
        
        public static void OnDeleteEmailButtonClicked(object sender, EventArgs e)
        {		
		try
    		{
    			User user = new();
    			user.UserId = listUser[editIndex].UserId;
    			user.Purpose = "DeleteUser";
			string serializedUserOffer = user.Serialize();
			SendMessage(_stream, serializedUserOffer);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	
	    	foreach (Widget child in user_admin_box.Children)
		{
		    user_admin_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveUserPList();
		for(int i=0; i < listUser.Count;i++)
        	{
        		AddFrameToMarketValuesUserWindow(i);
        	}  	
	    	edit_user_window.Hide();
		return;		
        }
        
        
        
        
        
        /*public static void OnUpdateFeeButtonClicked(object sender, EventArgs e)
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
	}*/
	
}
