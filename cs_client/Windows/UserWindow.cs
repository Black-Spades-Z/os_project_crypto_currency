using Gtk;
using static ServerAssetsWindow;
using static TransactionsWindow;
using static P2PWindow;

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
using static EditUserWindow;



public class UserWindow : CCTPSApp
{
	public static Box user_admin_box;

	public static Window User_admin_window;
	
	public static Button server_assets_button_user_admin_window;
	public static Button transactions_button_user_admin_window;
	public static Button p2p_button_user_admin_window;
	public static Button users_button_user_admin_window;
	public static Button user_portfolio_button_user_admin_window;
	public static Button wallets_button_user_admin_window;
	public static Button admin_button_user_admin_window;
	public static Button block_button_user_admin_window;
	public static Button miner_button_user_admin_window;
	public static Button logout_button_user_admin_window;
        

		public static List<int> userId = new();
        public static List<string> userEmail = new();
		public static List<string> PasswordHash= new();
		public static List<string> userFullName = new();
		public static List<string> userDateOfBirth = new();
		public static List<string> Address = new();
		public static List<string> userPhoneNumber = new();
		public static List<string> userNationality = new();
    	public static List<string> userJoinDate = new();
    	
    	public static List<User> listUser = new();
	
	static UserWindow()
    	{
    		User_admin_window = (Window)builder.GetObject("User_admin_window");
    	
    		server_assets_button_user_admin_window = (Button)builder.GetObject("server_assets_button_user_admin_window");
        	logout_button_user_admin_window = (Button)builder.GetObject("logout_button_user_admin_window");
        	p2p_button_user_admin_window = (Button)builder.GetObject("p2p_button_user_admin_window");
        	transactions_button_user_admin_window = (Button)builder.GetObject("transactions_button_user_admin_window");
        	users_button_user_admin_window = (Button)builder.GetObject("users_button_user_admin_window");
        	user_portfolio_button_user_admin_window = (Button)builder.GetObject("user_portfolio_button_user_admin_window");
        	miner_button_user_admin_window = (Button)builder.GetObject("miner_button_user_admin_window");
        	wallets_button_user_admin_window = (Button)builder.GetObject("wallets_button_user_admin_window");
        	admin_button_user_admin_window = (Button)builder.GetObject("admin_button_user_admin_window");
        	block_button_user_admin_window = (Button)builder.GetObject("block_button_user_admin_window");
        	
        	user_admin_box = (Box)builder.GetObject("user_admin_box");
        	
        	RequestAndSaveUserPList();
        	for(int i=0; i < listUser.Count;i++)
        	{
        		AddFrameToMarketValuesUserWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the user admin window
        server_assets_button_user_admin_window.Clicked += OnServerAssetsButtonClickedInUserAdminWindow;
        transactions_button_user_admin_window.Clicked += OnTransactionsButtonClickedInUserAdminWindow;
        p2p_button_user_admin_window.Clicked += OnP2PButtonInUserAdminClickedInUserAdminWindow;
        users_button_user_admin_window.Clicked += OnUsersButtonClickedInUserAdminWindow;
        user_portfolio_button_user_admin_window.Clicked += OnUserPortfolioButtonClickedInUserAdminWindow;
        wallets_button_user_admin_window.Clicked += OnWalletsButtonClickedInUserAdminWindow;
        admin_button_user_admin_window.Clicked += OnAdminButtonClickedInUserAdminWindow;
        block_button_user_admin_window.Clicked += OnBlockButtonClickedInUserAdminWindow;
		miner_button_user_admin_window.Clicked += OnMinerButtonClickedInUserAdminWindow;
        logout_button_user_admin_window.Clicked += OnLogoutButtonClickedInUserAdminWindow;
    }

    // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnP2PButtonInUserAdminClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        User_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		User_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
      //  User_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
      //  User_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInUserAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
    	
    	public static void RequestAndSaveUserPList()
    	{
    		try
    		{


    			listUser.Clear();
				userId.Clear();
    			userEmail.Clear();
				PasswordHash.Clear();
    			userFullName.Clear();
    			userDateOfBirth.Clear();
				Address.Clear();
    			userPhoneNumber.Clear();
    			userNationality.Clear();
    			userJoinDate.Clear();


    			
    			string RequestMessage = "GetUserList";

                        // Send the serialized Transaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listUser = WaitForUserList(_stream);
    			
    			for (int i = 0; i < listUser.Count; i++)
			{
			    User u = listUser[i];
			    
			userId.Add(u.UserId);
			userEmail.Add(u.Email);
			PasswordHash.Add(u.PasswordHash);
			userFullName.Add(u.FullName);
			userDateOfBirth.Add(u.DateOfBirth.ToString());
			Address.Add(u.Address);
			userPhoneNumber.Add(u.PhoneNumber);
			userNationality.Add(u.Nationality); 
			userJoinDate.Add(u.JoinDate.ToString());  
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void update_button_users_window_clicked(object sender, EventArgs e)
	{
	    if (sender is Button button)
	    {
		editIndex = Convert.ToInt32(button.Name);
		
		edit_user_window.ShowAll();
	    }
	}
    	
    	public static void AddFrameToMarketValuesUserWindow(int index)
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

		// userId Label

		Label userIdLabel = new Label(userId[index]);
		userIdLabel.Name = $"userId_{index}";
		userIdLabel.Visible = true;
		userIdLabel.CanFocus = false;
		//currencyPriceLabel.MarginBottom = 9;
		//currencyPriceLabel.Halign = Align.End;

		// inner frame for price
		Frame userIdFrame= new Frame("");
		userIdFrame.ShadowType = ShadowType.None;
		userIdFrame.Add(userIdLabel);

		// Set fixed width for the priceFrame
		int fixedWidth = 69; // Set your desired fixed width
		userIdFrame.SetSizeRequest(fixedWidth, -1);

		innerGrid.Attach(userIdFrame, 0, 0, 1, 1);

		
		// DateTime Label

		Label offerTimeStampLabel = new Label(userEmail[index]);
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
		fixedWidth = 200; // Set your desired fixed width
		offerTimeStampFrame.SetSizeRequest(fixedWidth, -1);
		
		innerGrid.Attach(offerTimeStampFrame, 1, 0, 1, 1);

		// Password Hash Label

		Button passwordHashButton = new Button("show");
		passwordHashButton.Name = $"password_{index}";
		//editButton.MarginBottom = 9;
		//editButton.MarginRight = 10;
		//editButton.Halign = Align.End;

		// inner frame for echange
		Frame passwordHashFrame= new Frame("");
		passwordHashFrame.ShadowType = ShadowType.None;
		passwordHashFrame.Add(passwordHashButton);
		passwordHashFrame.SetSizeRequest(100, -1);

		innerGrid.Attach(passwordHashFrame, 2, 0, 1, 1);

		// Connect button click events for main_window
		passwordHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(PasswordHash[index]);


		
		// Name Label

		Label offerCryptocurrenceLabel = new Label(userFullName[index]);
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





		// Price Label

		Label offerVolumeLabel = new Label(userDateOfBirth[index]);
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
		
		innerGrid.Attach(transactionVolumeFrame, 4, 0, 1, 1);


		Label addressLabel = new Label(Address[index]);
		addressLabel.Name = $"address_{index}";
		addressLabel.Visible = true;
		addressLabel.CanFocus = false;
		//currencyVolumeLabel.MarginBottom = 9;
		//currencyVolumeLabel.Halign = Align.End;

		// inner frame for volume
		Frame addressFrame= new Frame("");
		addressFrame.ShadowType = ShadowType.None;
		addressFrame.Add(addressLabel);
		addressFrame.SetSizeRequest(100, -1);

		innerGrid.Attach(addressFrame, 5, 0, 1, 1);

		// Volume Label

		Label offerValueLabel = new Label(userPhoneNumber[index]);
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
		
		innerGrid.Attach(valueFrame, 6, 0, 1, 1);

		// Rank Label

		Label offerAvailLabel = new Label(userNationality[index]);
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
		
		innerGrid.Attach(validationFrame, 7, 0, 1, 1);
		
		Button deleteButton = new Button("Edit");
		deleteButton.Name = $"{index}";
		//editButton.MarginBottom = 9;
		//editButton.MarginRight = 10;
		//editButton.Halign = Align.End;
		
		// inner frame for echange
		Frame userFrame= new Frame("");
		userFrame.ShadowType = ShadowType.None;
		userFrame.Add(deleteButton);
		userFrame.SetSizeRequest(100, -1);
		
		innerGrid.Attach(userFrame, 8, 0, 1, 1);

		// Connect button click events for main_window
		deleteButton.Clicked += update_button_users_window_clicked;
		
		// Add the inner grid to the alignment
		alignment.Add(innerGrid);

		// Add the alignment to the frame
		offerFrame.Add(alignment);
		
		// Align Frame
		
		offerFrame.MarginEnd = 20;


			
		// Add the frame to the market_values_box
		user_admin_box.Add(offerFrame);
		user_admin_box.ShowAll();
    	}
	



	public static void OnServerAssetsButtonInUserClicked(object sender, EventArgs e)
        {
            User_admin_window.Hide();
            Admin_main_window.ShowAll();
        }
        
        public static void OnP2PButtonInUserAdminClicked(object sender, EventArgs e)
        {
            User_admin_window.Hide();
            p2p_admin_window.ShowAll();
        }

        public static void OnTransactionsButtonInUserAdminClicked(object sender, EventArgs e)
        {   
            User_admin_window.Hide();
            transactions_admin_window.ShowAll();
        }
        
        public static void OnLogoutButtonInUserAdminClicked(object sender, EventArgs e)
        {
            Logout_window.ShowAll();
        }
}
