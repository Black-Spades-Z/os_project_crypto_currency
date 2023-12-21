using Gtk;

using static Admin;
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

// Pop UP for Hashed Labels
public class PopupWindow : Gtk.Window
{
    public PopupWindow(string message) : base(Gtk.WindowType.Toplevel)
    {
        // Set up the pop-up window
        this.Title = "Hash";
        this.SetDefaultSize(300, 200);

        // Add widgets to the pop-up window
        var label = new Gtk.Label(message);
        this.Add(label);
    }
}

public class AdminWindow : CCTPSApp
{
	public static Box admin_box;
	
	public static Window admin_window;

	public static Button server_assets_button_admin_window;
	public static Button transactions_button_admin_window;
	public static Button p2p_button_admin_window;
	public static Button users_button_admin_window;
	public static Button user_portfolio_button_admin_window;
	public static Button wallets_button_admin_window;
	public static Button admin_button_admin_window;
	public static Button miner_button_admin_window;
	public static Button block_button_admin_window;
	public static Button logout_button_admin_window;
        
       
	public static List<int> adminId = new();
	public static List<string> adminEmail = new();
 	public static List<string> adminPasswordHash = new();

	public static List<Admin> listAdmin = new();


	static AdminWindow()
    	{
    		admin_window = (Window)builder.GetObject("admin_window");
    	
    		server_assets_button_admin_window = (Button)builder.GetObject("server_assets_button_admin_window");
        	transactions_button_admin_window = (Button)builder.GetObject("transactions_button_admin_window");
			p2p_button_admin_window = (Button)builder.GetObject("p2p_button_admin_window");
			users_button_admin_window = (Button)builder.GetObject("users_button_admin_window");
			user_portfolio_button_admin_window = (Button)builder.GetObject("user_portfolio_button_admin_window");
			wallets_button_admin_window = (Button)builder.GetObject("wallets_button_admin_window");
			admin_button_admin_window = (Button)builder.GetObject("admin_button_admin_window");
			block_button_admin_window = (Button)builder.GetObject("block_button_admin_window");
			miner_button_admin_window = (Button)builder.GetObject("miner_button_admin_window");
			logout_button_admin_window = (Button)builder.GetObject("logout_button_admin_window");
        	admin_box = (Box)builder.GetObject("admin_box");
        	
        	RequestAndSaveAdminList();
			// modification needed Count should be variable
        	for(int i=0; i < adminId.Count;i++)
        	{
				Console.WriteLine(adminPasswordHash[i]);
        		AddFrameToMarketValuesTransactionWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button
        server_assets_button_admin_window.Clicked += OnServerAssetsButtonClickedInAdminWindow;
        transactions_button_admin_window.Clicked += OnTransactionsButtonClickedInAdminWindow;
        p2p_button_admin_window.Clicked += OnP2PButtonInAdminAdminClickedInAdminWindow;
        users_button_admin_window.Clicked += OnUsersButtonClickedInAdminWindow;
        wallets_button_admin_window.Clicked += OnWalletsButtonClickedInAdminWindow;
        admin_button_admin_window.Clicked += OnAdminButtonClickedInAdminWindow;
        block_button_admin_window.Clicked += OnBlockButtonClickedInAdminWindow;
        user_portfolio_button_admin_window.Clicked += OnUserPortfolioButtonClickedInAdminWindow;
        logout_button_admin_window.Clicked += OnLogoutButtonClickedInAdminWindow;
	}

    // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnP2PButtonInAdminAdminClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInAdminWindow(object sender, EventArgs e)
    {
      //  admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInAdminWindow(object sender, EventArgs e)
    {
      //  admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
        public static void RequestAndSaveAdminList()
    	{
    		try
    		{

				adminId.Clear();
				adminEmail.Clear();
				adminPasswordHash.Clear();


    			string RequestMessage = "GetAdminTable";

                        // Send the serialized Adminaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listAdmin = WaitForAdminList(_stream);
    			
    			for (int i = 0; i < listAdmin.Count; i++)
			{
			    Admin u = listAdmin[i];


				adminId.Add(u.id);
				adminEmail.Add(u.Email);
				adminPasswordHash.Add(u.PasswordHash);

			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void AddFrameToMarketValuesTransactionWindow(int index)
    	{
		// Create a new frame
		Frame offerFrame = new Frame("");

		// Create the frame
		offerFrame.Visible = true;
		offerFrame.CanFocus = false;
		offerFrame.LabelXalign = 0;
		offerFrame.ShadowType = ShadowType.None;

		// Create the alignment
		Alignment alignment = new Alignment(0, 0, 0, 0);
		alignment.Visible = true;
		alignment.CanFocus = false;

		
		// Create the inner grid
		Grid innerGrid = new Grid();
		innerGrid.Visible = false;
		innerGrid.CanFocus = false;
		innerGrid.ColumnSpacing = 70;
		innerGrid.RowHomogeneous = true;


		// Add child widgets to the inner grid (similar to your provided XML structure)
		// Here, you'd create and add GtkImage, GtkLabel, GtkButton, etc., to the innerGrid


		// Admin ID Label

		Label adminIDLabel = new Label($"{adminId[index]}");
		adminIDLabel.Name = $"adminID_{index}";
		adminIDLabel.Visible = true;
		adminIDLabel.CanFocus = false;


		// inner frame for admin ID
		Frame adminIDFrame= new Frame("");
		adminIDFrame.ShadowType = ShadowType.None;
		adminIDFrame.Add(adminIDLabel);
		adminIDFrame.SetSizeRequest(200, -1);

		innerGrid.Attach(adminIDFrame, 0, 0, 1, 1);

		// adminEmail Label

		Label adminEmailLabel = new Label(adminEmail[index]);
		adminEmailLabel.Name = $"adminEmailLable_{index}";
		adminEmailLabel.Visible = true;
		adminEmailLabel.CanFocus = false;

		// inner frame for rank
		Frame adminEmailFrame= new Frame("");
		adminEmailFrame.ShadowType = ShadowType.None;
		adminEmailFrame.Add(adminEmailLabel);
		adminEmailFrame.SetSizeRequest(200, -1);

		innerGrid.Attach(adminEmailFrame, 1, 0, 1, 1);


		// Admin Password Hash Label

		Button adminPasswordHashButton = new Button("show");
		adminPasswordHashButton.Name = $"adminPasswordHash_{index}";


		// inner frame for echange
		Frame adminPasswordHashFrame= new Frame("");
		adminPasswordHashFrame.ShadowType = ShadowType.None;
		adminPasswordHashFrame.Add(adminPasswordHashButton);
		adminPasswordHashFrame.SetSizeRequest(200, -1);

		innerGrid.Attach(adminPasswordHashFrame, 2, 0, 1, 1);

		// Connect button click events for main_window
		adminPasswordHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(adminPasswordHash[index]);

		
		// Add the inner grid to the alignment
		alignment.Add(innerGrid);

		// Add the alignment to the frame
		offerFrame.Add(alignment);
		
		// Align Frame
		
		offerFrame.MarginEnd = 20;


			
		// Add the frame to the market_values_box
		admin_box.Add(offerFrame);
		admin_box.ShowAll();
    	}

    	public static void adminPasswordHashButton_clicked(string PasswordHash){
			// Create and show the pop-up window with a custom message
			var popupWindow = new PopupWindow(PasswordHash);
			popupWindow.ShowAll();

			// Run the Gtk main loop to handle the pop-up display
			Application.Run();
		}

}
