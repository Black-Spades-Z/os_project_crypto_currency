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
using static EditUserWindow;



public class UserWindow : CCTPSApp
{
	public static Box user_admin_box;

	public static Window User_admin_window;
	
	public static Button server_assets_user_admin_window;
        public static Button logout_button_user_admin_window;
        public static Button p2p_button_user_admin_window;
        public static Button transactions_button_user_admin_window;
        public static Button user_button_user_admin_window;
        
        public static List<string> userEmail = new();
 	public static List<string> userFullName = new();
  	public static List<string> userDateOfBirth = new();
  	public static List<string> userPhoneNumber = new();
    	public static List<string> userNationality = new();	
    	public static List<string> userJoinDate = new();
    	
    	public static List<User> listUser = new();
	
	static UserWindow()
    	{
    		User_admin_window = (Window)builder.GetObject("User_admin_window");
    	
    		logout_button_user_admin_window = (Button)builder.GetObject("logout_button_user_admin_window");
        	server_assets_user_admin_window = (Button)builder.GetObject("server_assets_user_admin_window");
        	transactions_button_user_admin_window = (Button)builder.GetObject("transactions_button_user_admin_window");
        	p2p_button_user_admin_window = (Button)builder.GetObject("p2p_button_user_admin_window");
        	
        	user_admin_box = (Box)builder.GetObject("user_admin_box");
        	
        	RequestAndSaveUserPList();
        	for(int i=0; i < listUser.Count;i++)
        	{
        		AddFrameToMarketValuesUserWindow(i);
        	}
        	
        	logout_button_user_admin_window.Clicked += OnLogoutButtonInUserAdminClicked;
            	server_assets_user_admin_window.Clicked += OnServerAssetsButtonInUserClicked;
            	p2p_button_user_admin_window.Clicked += OnP2PButtonInUserAdminClicked;
            	transactions_button_user_admin_window.Clicked += OnTransactionsButtonInUserAdminClicked;
    	}
    	
    	public static void RequestAndSaveUserPList()
    	{
    		try
    		{


    			listUser.Clear();
    			userEmail.Clear();
    			userFullName.Clear();
    			userDateOfBirth.Clear();
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
			    
			    
			userEmail.Add(u.Email);
			userFullName.Add(u.FullName);
			userDateOfBirth.Add(u.DateOfBirth.ToString());
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
		int fixedWidth = 200; // Set your desired fixed width
		offerTimeStampFrame.SetSizeRequest(fixedWidth, -1);
		
		innerGrid.Attach(offerTimeStampFrame, 0, 0, 1, 1);
		
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
		
		innerGrid.Attach(offerCryptocurrenceFrame, 1, 0, 1, 1);





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
		
		innerGrid.Attach(transactionVolumeFrame, 2, 0, 1, 1);

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
		
		innerGrid.Attach(valueFrame, 3, 0, 1, 1);

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
		
		innerGrid.Attach(validationFrame, 4, 0, 1, 1);
		
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
		
		innerGrid.Attach(userFrame, 5, 0, 1, 1);

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
