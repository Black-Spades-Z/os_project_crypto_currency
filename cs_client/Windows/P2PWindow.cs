using Gtk;

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
using static UpdateServerAssetsWindow;
using static DeleteP2POfferWindow;
using static EditUserWindow;

public class P2PWindow : CCTPSApp
{
	public static Box p2p_admin_box;

	public static Window p2p_admin_window;
	

	public static Button server_assets_button_p2p_admin_window;
	public static Button transactions_button_p2p_admin_window;
	public static Button p2p_button_p2p_admin_window;
	public static Button users_button_p2p_admin_window;
	public static Button user_portfolio_button_p2p_admin_window;
	public static Button wallets_button_p2p_admin_window;
	public static Button admin_button_p2p_admin_window;
	public static Button block_button_p2p_admin_window;
	public static Button miner_button_p2p_admin_window;
	public static Button logout_button_p2p_admin_window;
        
        public static List<UserOffer> listOffer = new(); 
        



		public static List<string> FromAddress = new();
		public static List<string> offerVolume = new();
		public static List<string> offerValue = new();
		public static List<string> offerCurrencyName = new();
        public static List<string> offerTimestamp = new();
		public static List<string> offerAvailable = new();
		public static List<int> offerId = new();


	
	static P2PWindow()
    	{
    		p2p_admin_window = (Window)builder.GetObject("p2p_admin_window");
    		
    	
    		server_assets_button_p2p_admin_window = (Button)builder.GetObject("server_assets_button_p2p_admin_window");
        	logout_button_p2p_admin_window = (Button)builder.GetObject("logout_button_p2p_admin_window");
        	p2p_button_p2p_admin_window = (Button)builder.GetObject("p2p_button_p2p_admin_window");
        	transactions_button_p2p_admin_window = (Button)builder.GetObject("transactions_button_p2p_admin_window");
        	users_button_p2p_admin_window = (Button)builder.GetObject("users_button_p2p_admin_window");
        	user_portfolio_button_p2p_admin_window = (Button)builder.GetObject("user_portfolio_button_p2p_admin_window");
        	miner_button_p2p_admin_window = (Button)builder.GetObject("miner_button_p2p_admin_window");
        	wallets_button_p2p_admin_window = (Button)builder.GetObject("wallets_button_p2p_admin_window");
        	admin_button_p2p_admin_window = (Button)builder.GetObject("admin_button_p2p_admin_window");
        	block_button_p2p_admin_window = (Button)builder.GetObject("block_button_p2p_admin_window");
        	
        	p2p_admin_box = (Box)builder.GetObject("p2p_admin_box");
        	
        	RequestAndSaveP2PList();
        	for(int i=0; i < offerTimestamp.Count;i++)
        	{
        		AddFrameToMarketValuesUserOfferWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the P2P admin window
        server_assets_button_p2p_admin_window.Clicked += OnServerAssetsButtonClickedInP2PAdminWindow;
        transactions_button_p2p_admin_window.Clicked += OnTransactionsButtonClickedInP2PAdminWindow;
        p2p_button_p2p_admin_window.Clicked += OnP2PButtonInP2PAdminClickedInP2PAdminWindow;
        users_button_p2p_admin_window.Clicked += OnUsersButtonClickedInP2PAdminWindow;
        user_portfolio_button_p2p_admin_window.Clicked += OnUserPortfolioButtonClickedInP2PAdminWindow;
        wallets_button_p2p_admin_window.Clicked += OnWalletsButtonClickedInP2PAdminWindow;
        admin_button_p2p_admin_window.Clicked += OnAdminButtonClickedInP2PAdminWindow;
        block_button_p2p_admin_window.Clicked += OnBlockButtonClickedInP2PAdminWindow;
        logout_button_p2p_admin_window.Clicked += OnLogoutButtonClickedInP2PAdminWindow;
    }

     // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        transactions_admin_window.ShowAll();
    }

    public static void OnP2PButtonInP2PAdminClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        p2p_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		p2p_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
      //  p2p_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
      //  p2p_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInP2PAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
    	
    	public static void RequestAndSaveP2PList()
    	{
    		try
    		{
    			listOffer.Clear();
				FromAddress.Clear();
				offerVolume.Clear();
				offerValue.Clear();
				offerCurrencyName.Clear();
				offerTimestamp.Clear();
				offerAvailable.Clear();
				offerId.Clear();
    			
    			string RequestMessage = "GetUserOffers";

                        // Send the serialized Transaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listOffer = WaitForUserOffers(_stream);
    			
    			for (int i = 0; i < listOffer.Count; i++)
			{
			    UserOffer u = listOffer[i];
			    
			FromAddress.Add(u.FromAddress);
			offerVolume.Add(u.CryptoValue.ToString());
			offerValue.Add(u.CashValue.ToString());
			offerCurrencyName.Add(u.CryptocurrencyName);
			offerTimestamp.Add(u.DateTime.ToString());
			offerAvailable.Add(u.Available.ToString());  
			offerId.Add(u.OfferId);
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
	
	public static void OnServerAssetsButtonInP2PClicked(object sender, EventArgs e)
        {
            p2p_admin_window.Hide();
            Admin_main_window.ShowAll();
        }
        
        public static void OnTransactionsButtonInP2PAdminClicked(object sender, EventArgs e)
        {
            p2p_admin_window.Hide();
            transactions_admin_window.ShowAll();
        }

        public static void OnUserButtonInP2PAdminClicked(object sender, EventArgs e)
        {   
            p2p_admin_window.Hide();
            User_admin_window.ShowAll();
        }
        
        public static void OnLogoutButtonInP2PAdminClicked(object sender, EventArgs e)
        {
           Logout_window.ShowAll();
        }
        
        
        public static void delete_button_p2p_window_clicked(object sender, EventArgs e)
	{
	    if (sender is Button button)
	    {
		editIndex = Convert.ToInt32(button.Name);		
		user_offer_confiramtion_window.ShowAll();
	    }
	}
        
        public static void AddFrameToMarketValuesUserOfferWindow(int index)
    	{
		// Create a new frame
		Frame offerFrame = new Frame("");

		// Create the frame
		offerFrame.Visible = true;
		offerFrame.CanFocus = false;
		//transactionFrame.MarginTop = 10;
		//transactionFrame.MarginBottom = 10;
		offerFrame.LabelXalign = 0;
		offerFrame.ShadowType = ShadowType.None;

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
	 	   innerGrid.ColumnSpacing = 70;
	 	   innerGrid.RowHomogeneous = true;
	 	   //innerGrid.ColumnHomogeneous = true;
	 	   
	 	   
	 	
	 	   

	 

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


		// Password Hash Label

		Button fromAddressButton = new Button("show");
		fromAddressButton.Name = $"password_{index}";
		//editButton.MarginBottom = 9;
		//editButton.MarginRight = 10;
		//editButton.Halign = Align.End;

		// inner frame for echange
		Frame fromAddressFrame= new Frame("");
		fromAddressFrame.ShadowType = ShadowType.None;
		fromAddressFrame.Add(fromAddressButton);
		fromAddressFrame.SetSizeRequest(100, -1);

		innerGrid.Attach(fromAddressFrame, 0, 0, 1, 1);

		// Connect button click events for main_window
		fromAddressButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(FromAddress[index]);


		// Price Label

		Label offerVolumeLabel = new Label(offerVolume[index]);
		offerVolumeLabel.Name = $"offerVolume_{index}";
		offerVolumeLabel.Visible = true;
		offerVolumeLabel.CanFocus = false;
		//offerVolumeLabel.MarginRight = 50;
		//currencyPriceLabel.Halign = Align.End;

		// inner frame for price
		Frame transactionVolumeFrame= new Frame("");
		transactionVolumeFrame.ShadowType = ShadowType.None;
		transactionVolumeFrame.Add(offerVolumeLabel);

		// Set fixed width for the priceFrame
		//int fixedWidth = 150; // Set your desired fixed width
		transactionVolumeFrame.SetSizeRequest(150, -1);

		innerGrid.Attach(transactionVolumeFrame, 1, 0, 1, 1);

		// Volume Label

		Label offerValueLabel = new Label(offerValue[index]);
		offerValueLabel.Name = $"offerValue_{index}";
		offerValueLabel.Visible = true;
		offerValueLabel.CanFocus = false;
		//currencyVolumeLabel.MarginBottom = 9;
		//currencyVolumeLabel.Halign = Align.End;

		// inner frame for volume
		Frame valueFrame= new Frame("");
		valueFrame.ShadowType = ShadowType.None;
		valueFrame.Add(offerValueLabel);
		valueFrame.SetSizeRequest(100, -1);

		innerGrid.Attach(valueFrame, 2, 0, 1, 1);


		// Name Label

		Label offerCryptocurrenceLabel = new Label(offerCurrencyName[index]);
		offerCryptocurrenceLabel.Name = $"offerTimestamp_{index}";
		offerCryptocurrenceLabel.Visible = true;
		offerCryptocurrenceLabel.CanFocus = false;
		//currencyPriceLabel.MarginBottom = 9;
		//currencyPriceLabel.Halign = Align.End;

		// inner frame for price
		Frame offerCryptocurrenceFrame= new Frame("");
		offerCryptocurrenceFrame.ShadowType = ShadowType.None;
		offerCryptocurrenceFrame.Add(offerCryptocurrenceLabel);

		// Set fixed width for the priceFrame
		//int fixedWidth = 150; // Set your desired fixed width
		offerCryptocurrenceFrame.SetSizeRequest(160, -1);

		innerGrid.Attach(offerCryptocurrenceFrame, 3, 0, 1, 1);
		
		// DateTime Label

		Label offerTimeStampLabel = new Label(offerTimestamp[index]);
		offerTimeStampLabel.Name = $"offerTimestamp_{index}";
		offerTimeStampLabel.Visible = true;
		offerTimeStampLabel.CanFocus = false;
		//currencyPriceLabel.MarginBottom = 9;
		//currencyPriceLabel.Halign = Align.End;
		
		// inner frame for price
		Frame offerTimeStampFrame= new Frame("");
		offerTimeStampFrame.ShadowType = ShadowType.None;
		offerTimeStampFrame.Add(offerTimeStampLabel);
		
		// Set fixed width for the priceFrame
		int fixedWidth = 200; // Set your desired fixed width
		offerTimeStampFrame.SetSizeRequest(fixedWidth, -1);
		
		innerGrid.Attach(offerTimeStampFrame, 4, 0, 1, 1);
		







		// Rank Label

		Label offerAvailLabel = new Label(offerAvailable[index]);
		offerAvailLabel.Name = $"offerAvailable_{index}";
		offerAvailLabel.Visible = true;
		offerAvailLabel.CanFocus = false;
		//currencyRankLabel.MarginBottom = 9;
		//offerAvailLabel.MarginLeft = 70;
		
		// inner frame for rank
		Frame validationFrame= new Frame("");
		validationFrame.ShadowType = ShadowType.None;
		validationFrame.Add(offerAvailLabel);
		validationFrame.SetSizeRequest(100, -1);
		
		innerGrid.Attach(validationFrame, 5, 0, 1, 1);

		Label idAvailLabel = new Label($"{offerId[index]}");
		idAvailLabel.Name = $"idAvailable_{index}";
		idAvailLabel.Visible = true;
		idAvailLabel.CanFocus = false;
		//currencyRankLabel.MarginBottom = 9;
		//idAvailLabel.MarginLeft = 70;

		// inner frame for rank
		validationFrame= new Frame("");
		validationFrame.ShadowType = ShadowType.None;
		validationFrame.Add(idAvailLabel);
		validationFrame.SetSizeRequest(69, -1);

		innerGrid.Attach(validationFrame, 6, 0, 1, 1);

		
		// Exchange Button
		Button deleteButton = new Button("Delete");
		deleteButton.Name = $"{index}";
		//editButton.MarginBottom = 9;
		//editButton.MarginRight = 10;
		//editButton.Halign = Align.End;
		
		// inner frame for echange
		Frame userFrame= new Frame("");
		userFrame.ShadowType = ShadowType.None;
		userFrame.Add(deleteButton);
		userFrame.SetSizeRequest(100, -1);
		
		innerGrid.Attach(userFrame, 7, 0, 1, 1);

		// Connect button click events for main_window
		deleteButton.Clicked += delete_button_p2p_window_clicked;





		// Add the inner grid to the alignment
		alignment.Add(innerGrid);

		// Add the alignment to the frame
		offerFrame.Add(alignment);
		
		// Align Frame
		
		offerFrame.MarginEnd = 20;


			
		// Add the frame to the market_values_box
		p2p_admin_box.Add(offerFrame);
		p2p_admin_box.ShowAll();
    	}



}
