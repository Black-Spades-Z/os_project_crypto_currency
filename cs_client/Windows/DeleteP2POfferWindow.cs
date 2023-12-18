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

public class DeleteP2POfferWindow : P2PWindow
{
	public static Window user_offer_confiramtion_window;
        public static Button delete_button_u_c_w;
        public static Button back_button_u_c_w;
        
        static DeleteP2POfferWindow()
    	{
    		user_offer_confiramtion_window = (Window)builder.GetObject("user_offer_confiramtion_window");
    		
        	delete_button_u_c_w = (Button)builder.GetObject("delete_button_u_c_w");
        	back_button_u_c_w = (Button)builder.GetObject("back_button_u_c_w");
        	
        	delete_button_u_c_w.Clicked += OnDeleteP2PButtonClicked;
            	back_button_u_c_w.Clicked += OnBackP2PButtonClicked;
    	}
    	
    	public static void OnDeleteP2PButtonClicked(object sender, EventArgs e)
        {		
		try
    		{
    			UserOffer uOffer = new();
    			uOffer.OfferId = listOffer[editIndex].OfferId;
    			uOffer.Purpose = "DeleteUserOffer";
			string serializedUserOffer = uOffer.Serialize();
			SendMessage(_stream, serializedUserOffer);
			WaitForResponse(_stream);
    		}
    		catch (Exception ex)
	    	{
			Console.WriteLine($"Error in messaging: {ex}");
	    	}
	
	    	foreach (Widget child in p2p_admin_box.Children)
		{
		    p2p_admin_box.Remove(child);
		    child.Destroy();
		}
		RequestAndSaveP2PList();
		for(int i=0; i < offerTimestamp.Count;i++)
        	{
        		AddFrameToMarketValuesUserOfferWindow(i);
        	}  	
	    	user_offer_confiramtion_window.Hide();
		return;		
        }
        
        public static void OnBackP2PButtonClicked(object sender, EventArgs e)
        {        	
	    	user_offer_confiramtion_window.Hide();
		return;		
        }
}
