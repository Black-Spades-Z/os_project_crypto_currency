using Gtk;


using static Transaction;
using static User;
using static Cryptocurrency;
using static UserPortfolio;
using static UserOffer;
using static Wallet;
using static Block;
using static Miner;



using static AdminWindow;
using static ServerAssetsWindow;
using static TransactionsWindow;
using static P2PWindow;
using static UserWindow;
using static MinerWindow;
using static WalletWindow;
using static UserPortfolioWindow;
using static BlockWindow;
// Pop UP for Hashed Labels

public class MinerWindow : CCTPSApp
{
	public static Box admin_miners_box;
	
	public static Window miners_admin_window;

	public static Button server_assets_button_miners_admin_window;
	public static Button transactions_button_miners_admin_window;
	public static Button p2p_button_miners_admin_window;
	public static Button users_button_miners_admin_window;
	public static Button user_portfolio_button_miners_admin_window;
	public static Button wallets_button_miners_admin_window;
	public static Button admin_button_miners_admin_window;
	public static Button block_button_miners_admin_window;
    public static Button miner_button_miners_admin_window;
	public static Button logout_button_miners_admin_window;
        
       
    	
    	public static List<int> minerId = new();
		public static List<string> minerJoinDate = new();


	static MinerWindow()
    	{
    		miners_admin_window = (Window)builder.GetObject("miners_admin_window");
    	
    		server_assets_button_miners_admin_window = (Button)builder.GetObject("server_assets_button_miners_admin_window");
        	logout_button_miners_admin_window = (Button)builder.GetObject("logout_button_miners_admin_window");
        	p2p_button_miners_admin_window = (Button)builder.GetObject("p2p_button_miners_admin_window");
        	transactions_button_miners_admin_window = (Button)builder.GetObject("transactions_button_miners_admin_window");
        	users_button_miners_admin_window = (Button)builder.GetObject("users_button_miners_admin_window");
        	user_portfolio_button_miners_admin_window = (Button)builder.GetObject("user_portfolio_button_miners_admin_window");
        	miner_button_miners_admin_window = (Button)builder.GetObject("miner_button_miners_admin_window");
        	wallets_button_miners_admin_window = (Button)builder.GetObject("wallets_button_miners_admin_window");
        	admin_button_miners_admin_window = (Button)builder.GetObject("admin_button_miners_admin_window");
        	block_button_miners_admin_window = (Button)builder.GetObject("block_button_miners_admin_window");
        	
        	admin_miners_box = (Box)builder.GetObject("admin_miners_box");
        	
        	RequestAndSaveMinersList();
        	for(int i=0; i < minerId.Count;i++)
        	{
        		AddFrameToMarketValuesMinerWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the miners admin window
        server_assets_button_miners_admin_window.Clicked += OnServerAssetsButtonClickedInMinersAdminWindow;
        transactions_button_miners_admin_window.Clicked += OnTransactionsButtonClickedInMinersAdminWindow;
        p2p_button_miners_admin_window.Clicked += OnP2PButtonInMinersAdminClickedInMinersAdminWindow;
        users_button_miners_admin_window.Clicked += OnUsersButtonClickedInMinersAdminWindow;
        user_portfolio_button_miners_admin_window.Clicked += OnUserPortfolioButtonClickedInMinersAdminWindow;
        wallets_button_miners_admin_window.Clicked += OnWalletsButtonClickedInMinersAdminWindow;
        admin_button_miners_admin_window.Clicked += OnAdminButtonClickedInMinersAdminWindow;
        block_button_miners_admin_window.Clicked += OnBlockButtonClickedInMinersAdminWindow;
        logout_button_miners_admin_window.Clicked += OnLogoutButtonClickedInMinersAdminWindow;
        miner_button_miners_admin_window.Clicked +=OnMinerButtonClickedInMinerAdminWindow;
    }

    // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        transactions_admin_window.ShowAll();
    }

    public static void OnP2PButtonInMinersAdminClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        miners_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		miners_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
      //  miners_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInMinerAdminWindow(object sender, EventArgs e)
    {
      //  miners_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInMinersAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
        
        public static void RequestAndSaveMinersList()
    	{
    		try
    		{
				minerId.Clear();
				minerJoinDate.Clear();

    			List<Miner> listMiners = new();
    			string RequestMessage = "GetMinerTable";

                        // Send the serialized Minersaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listMiners = WaitForMiningList(_stream);
    			
    			for (int i = 0; i < listMiners.Count; i++)
			{
			    Miner m = listMiners[i];

				minerId.Add(m.UserId);
				minerJoinDate.Add(m.JoinDate.ToString());
			    
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void AddFrameToMarketValuesMinerWindow(int index)
{
    // Create a new frame
    Frame minerFrame = new Frame("");

    // Create the frame
    minerFrame.Visible = true;
    minerFrame.CanFocus = false;
    minerFrame.LabelXalign = 0;
    minerFrame.ShadowType = ShadowType.None;

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

    // Miner ID Label
    Label minerIdLabel = new Label($"{minerId[index]}");
    minerIdLabel.Name = $"minerIdLabel_{index}";
    minerIdLabel.Visible = true;
    minerIdLabel.CanFocus = false;

    // inner frame for miner ID
    Frame minerIdFrame = new Frame("");
    minerIdFrame.ShadowType = ShadowType.None;
    minerIdFrame.Add(minerIdLabel);
    minerIdFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(minerIdFrame, 0, 0, 1, 1);

    // Miner Join Date Label
    Label minerJoinDateLabel = new Label(minerJoinDate[index]);
    minerJoinDateLabel.Name = $"minerJoinDateLabel_{index}";
    minerJoinDateLabel.Visible = true;
    minerJoinDateLabel.CanFocus = false;

    // inner frame for miner join date
    Frame minerJoinDateFrame = new Frame("");
    minerJoinDateFrame.ShadowType = ShadowType.None;
    minerJoinDateFrame.Add(minerJoinDateLabel);
    minerJoinDateFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(minerJoinDateFrame, 1, 0, 1, 1);

    // Connect any additional events or customize the widgets as needed

    // Add the inner grid to the alignment
    alignment.Add(innerGrid);

    // Add the alignment to the frame
    minerFrame.Add(alignment);

    // Align Frame
    minerFrame.MarginEnd = 20;

    // Add the frame to the miner_box (adjust as per your actual container)
    admin_miners_box.Add(minerFrame);
    admin_miners_box.ShowAll();
}


}
