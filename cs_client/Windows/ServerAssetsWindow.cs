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


using static AdminWindow;
using static ServerAssetsWindow;
using static TransactionsWindow;
using static P2PWindow;
using static UserWindow;
using static MinerWindow;
using static WalletWindow;
using static UserPortfolioWindow;
using static BlockWindow;
using static UserPortfolioWindow;
using static UpdateServerAssetsWindow;

public class ServerAssetsWindow : CCTPSApp
{
	
	
	public static Box server_assets_market_box;

	public static int i_counter = 1;
	public static string[] currencyName = new string[26];
 	public static string[] currencyPrice = new string[26];
  	public static string[] currencyVolume = new string[26];
  	public static string[] currencyRank = new string[26];
  	public static string[] currencyIcons = new string[26];
    	public static string[] currencyFee = new string[26];
    	
    	public static Window Admin_main_window;
    	
    	public static Button server_assets_button_admin_main_window;
	public static Button transactions_button_admin_main_window;
	public static Button p2p_button_admin_main_window;
	public static Button users_button_admin_main_window;
	public static Button user_portfolio_button_admin_main_window;
	public static Button wallets_button_admin_main_window;
	public static Button admin_button_admin_main_window;
	public static Button block_button_admin_main_window;
	public static Button miner_button_admin_main_window;
	public static Button logout_button_admin_main_window;
        
        public static int editIndex;
        
    	static ServerAssetsWindow()
    	{    		
    		Admin_main_window = (Window)builder.GetObject("Admin_main_window");
    		
    		server_assets_button_admin_main_window = (Button)builder.GetObject("server_assets_button_admin_main_window");
        	logout_button_admin_main_window = (Button)builder.GetObject("logout_button_admin_main_window");
        	p2p_button_admin_main_window = (Button)builder.GetObject("p2p_button_admin_main_window");
        	transactions_button_admin_main_window = (Button)builder.GetObject("transactions_button_admin_main_window");
        	users_button_admin_main_window = (Button)builder.GetObject("users_button_admin_main_window");
        	user_portfolio_button_admin_main_window = (Button)builder.GetObject("user_portfolio_button_admin_main_window");
        	miner_button_admin_main_window = (Button)builder.GetObject("miner_button_admin_main_window");
        	wallets_button_admin_main_window = (Button)builder.GetObject("wallets_button_admin_main_window");
        	admin_button_admin_main_window = (Button)builder.GetObject("admin_button_admin_main_window");
        	block_button_admin_main_window = (Button)builder.GetObject("block_button_admin_main_window");
        	
        	server_assets_market_box = (Box)builder.GetObject("server_assets_market_box");
        	
        	RequestAndSaveServerAssets();
        	for(int i=0; i < 26;i++)
        	{
        		AddFrameToMarketValuesServerAssetsWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the admin main window
        server_assets_button_admin_main_window.Clicked += OnServerAssetsButtonClickedInAdminMainWindow;
        transactions_button_admin_main_window.Clicked += OnTransactionsButtonClickedInAdminMainWindow;
        p2p_button_admin_main_window.Clicked += OnP2PButtonInAdminClickedInAdminMainWindow;
        users_button_admin_main_window.Clicked += OnUsersButtonClickedInAdminMainWindow;
        user_portfolio_button_admin_main_window.Clicked += OnUserPortfolioButtonClickedInAdminMainWindow;
        wallets_button_admin_main_window.Clicked += OnWalletsButtonClickedInAdminMainWindow;
        admin_button_admin_main_window.Clicked += OnAdminButtonClickedInAdminMainWindow;
        block_button_admin_main_window.Clicked += OnBlockButtonClickedInAdminMainWindow;
        logout_button_admin_main_window.Clicked += OnLogoutButtonClickedInAdminMainWindow;
    }

    // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        transactions_admin_window.ShowAll();
    }

    public static void OnP2PButtonInAdminClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Admin_main_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		Admin_main_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
      //  Admin_main_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
      //  Admin_main_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInAdminMainWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }

    	
    	public static void RequestAndSaveServerAssets()
    	{
    		try
    		{
    			List<Cryptocurrency> listCrypto = new();
    			string RequestMessage = "GetServerAssetsList";

                        // Send the serialized Transaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listCrypto = WaitForServerAssets(_stream);
    			
    			for (int i = 0; i < listCrypto.Count; i++)
			{
			    Cryptocurrency crypto = listCrypto[i];
			    currencyName[i] = crypto.Name;
			    currencyPrice[i] = crypto.Price.ToString();
			    currencyVolume[i] = crypto.Amount.ToString();
			    currencyFee[i] = crypto.Fee.ToString();
			    currencyRank[i] = i.ToString();
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
	

        
        
        
    	public static void exchange_button_main_window_clicked(object sender, EventArgs e)
	{
	    if (sender is Button button)
	    {
		editIndex = Convert.ToInt32(button.Name);
		
		Edit_window.ShowAll();
	    }
	}


    
    	public static void AddFrameToMarketValuesServerAssetsWindow(int index)
    	{
		// Create a new frame
		Frame currencyFrame = new Frame("");

		// Create the frame
		currencyFrame.Visible = true;
		currencyFrame.CanFocus = false;
		//currencyFrame.MarginTop = 10;
		//currencyFrame.MarginBottom = 10;
		currencyFrame.LabelXalign = 0;
		currencyFrame.ShadowType = ShadowType.None;

		// Create the alignment
		Alignment alignment = new Alignment(0, 0, 0, 0);
		alignment.Visible = true;
		alignment.CanFocus = false;
		//alignment.LeftPadding = 12;

		
		// Create the inner grid
	    	Grid innerGrid = new Grid();
	    	innerGrid.Visible = false;
	 	   innerGrid.CanFocus = false;
	 	   //innerGrid.RowSpacing = 10;
	 	   //innerGrid.ColumnSpacing = 10;
	 	   innerGrid.RowHomogeneous = true;
	 	   innerGrid.ColumnHomogeneous = true;
	 	   
	 	   
	 	
	 	   

	 

		// Create the inner grid
		Grid currencyNameGrid = new Grid();
		//currencyNameGrid.MarginBottom = 9;
		//currencyNameGrid.MarginLeft = 30;
		currencyNameGrid.Visible = true;
		currencyNameGrid.CanFocus = false;
		currencyNameGrid.RowSpacing = 0;
		//currencyNameGrid.ColumnSpacing = 10;
		currencyNameGrid.RowHomogeneous = true;
		currencyNameGrid.ColumnHomogeneous = true;


		// Add child widgets to the inner grid (similar to your provided XML structure)
		// Here, you'd create and add GtkImage, GtkLabel, GtkButton, etc., to the innerGrid


		// Icon Image

		Image currencyIconImage = new Image(currencyIcons[index]);
		currencyIconImage.Visible = true;
		currencyIconImage.CanFocus = false;
		//currencyIconImage.MarginLeft = 40;
		currencyNameGrid.Attach(currencyIconImage, 0, 0, 1, 1);

		// Name Label

		Label currencyNameLabel = new Label(currencyName[index]);
		currencyNameLabel.Name = $"CurrencyName_{index}";
		currencyNameLabel.Visible = true;
		currencyNameLabel.CanFocus = false;
		currencyNameLabel.Halign = Align.Start; // Adjust horizontal alignment
		currencyNameLabel.Valign = Align.Center; // Adjust vertical alignment
		//currencyNameLabel.MarginRight = 30;
		
		
		currencyNameGrid.Attach(currencyNameLabel, 1, 0, 1, 1);


		// inner frame for currency name
		Frame currencyNameFrame= new Frame("");
		currencyNameFrame.ShadowType = ShadowType.None;
		currencyNameFrame.Add(currencyNameGrid);
		
		// Set fixed width for the currency Frame
		int fixedWidth = 200; // Set your desired fixed width
		currencyNameFrame.SetSizeRequest(fixedWidth, -1);

		innerGrid.Attach(currencyNameFrame, 0, 0, 1, 1);
		



		// Price Label

		Label currencyPriceLabel = new Label(currencyPrice[index]);
		currencyPriceLabel.Name = $"CurrencyPrice_{index}";
		currencyPriceLabel.Visible = true;
		currencyPriceLabel.CanFocus = false;
		//currencyPriceLabel.MarginBottom = 9;
		//currencyPriceLabel.Halign = Align.End;
		
		// inner frame for price
		Frame priceFrame= new Frame("");
		priceFrame.ShadowType = ShadowType.None;
		priceFrame.Add(currencyPriceLabel);
		
		// Set fixed width for the priceFrame
		//int fixedWidth = 150; // Set your desired fixed width
		priceFrame.SetSizeRequest(fixedWidth, -1);
		
		innerGrid.Attach(priceFrame, 1, 0, 1, 1);

		// Volume Label

		Label currencyVolumeLabel = new Label(currencyVolume[index]);
		currencyVolumeLabel.Name = $"Volume_{index}";
		currencyVolumeLabel.Visible = true;
		currencyVolumeLabel.CanFocus = false;
		//currencyVolumeLabel.MarginBottom = 9;
		//currencyVolumeLabel.Halign = Align.End;
		
		// inner frame for volume
		Frame volumeFrame= new Frame("");
		volumeFrame.ShadowType = ShadowType.None;
		volumeFrame.Add(currencyVolumeLabel);
		
		innerGrid.Attach(volumeFrame, 2, 0, 1, 1);

		// Rank Label

		Label currencyFeeLabel = new Label(currencyFee[index]);
		currencyFeeLabel.Name = $"CurrencyFee{index}";
		currencyFeeLabel.Visible = true;
		currencyFeeLabel.CanFocus = false;
		//currencyRankLabel.MarginBottom = 9;
		//currencyRankLabel.Halign = Align.End;
		
		// inner frame for rank
		Frame rankFrame= new Frame("");
		rankFrame.ShadowType = ShadowType.None;
		rankFrame.Add(currencyFeeLabel);
		
		innerGrid.Attach(rankFrame, 3, 0, 1, 1);

		// Exchange Button
		Button editButton = new Button("Edit");
		editButton.Name = $"{index}";
		//editButton.MarginBottom = 9;
		//editButton.MarginRight = 10;
		//editButton.Halign = Align.End;
		
		// inner frame for echange
		Frame echangeFrame= new Frame("");
		echangeFrame.ShadowType = ShadowType.None;
		echangeFrame.Add(editButton);
		
		innerGrid.Attach(echangeFrame, 4, 0, 1, 1);

		// Connect button click events for main_window
		editButton.Clicked += exchange_button_main_window_clicked;



		// Add the inner grid to the alignment
		alignment.Add(innerGrid);

		// Add the alignment to the frame
		currencyFrame.Add(alignment);
		
		// Align Frame
		
		currencyFrame.MarginEnd = 20;


			
		// Add the frame to the market_values_box
		server_assets_market_box.Add(currencyFrame);
		server_assets_market_box.ShowAll();
    	}
}
