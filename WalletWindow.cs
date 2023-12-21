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
using static BlockWindow;
using static UserPortfolioWindow;
//using static UpdateServerAssetsWindow;

// Pop UP for Hashed Labels


public class WalletWindow : CCTPSApp
{
	public static Box admin_wallet_box;
	
	public static Window wallet_admin_window;

	public static Button server_assets_button_wallet_admin_window;
	public static Button transactions_button_wallet_admin_window;
	public static Button p2p_button_wallet_admin_window;
	public static Button users_button_wallet_admin_window;
	public static Button user_portfolio_button_wallet_admin_window;
	public static Button wallets_button_wallet_admin_window;
	public static Button admin_button_wallet_admin_window;
	public static Button block_button_wallet_admin_window;
	public static Button logout_button_wallet_admin_window;
        


		public static List<int> walletId = new();
		public static List<string> walletAddress = new();
		public static List<int> walletUserId = new();
		public static List<decimal> walletBalance = new();
		public static List<string> walletPrivateKey = new();
		public static List<string> walletPublicKey = new();


	static WalletWindow()
    	{
    		wallet_admin_window = (Window)builder.GetObject("wallet_admin_window");
    	
    		server_assets_button_wallet_admin_window = (Button)builder.GetObject("server_assets_button_wallet_admin_window");
        	logout_button_wallet_admin_window = (Button)builder.GetObject("logout_button_wallet_admin_window");
        	p2p_button_wallet_admin_window = (Button)builder.GetObject("p2p_button_wallet_admin_window");
        	transactions_button_wallet_admin_window = (Button)builder.GetObject("transactions_button_wallet_admin_window");
        	users_button_wallet_admin_window = (Button)builder.GetObject("users_button_wallet_admin_window");
        	user_portfolio_button_wallet_admin_window = (Button)builder.GetObject("user_portfolio_button_wallet_admin_window");
        	miner_button_wallet_admin_window = (Button)builder.GetObject("miner_button_wallet_admin_window");
        	wallets_button_wallet_admin_window = (Button)builder.GetObject("wallets_button_wallet_admin_window");
        	admin_button_wallet_admin_window = (Button)builder.GetObject("admin_button_wallet_admin_window");
        	block_button_wallet_admin_window = (Button)builder.GetObject("block_button_wallet_admin_window");
        	
        	admin_wallet_box = (Box)builder.GetObject("admin_wallet_box");
        	
        	RequestAndSaveWalletList();
        	for(int i=0; i < walletTimestamp.Count;i++)
        	{
        		AddFrameToMarketValuesWalletWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the wallet admin window
        server_assets_button_wallet_admin_window.Clicked += OnServerAssetsButtonClickedInWalletAdminWindow;
        transactions_button_wallet_admin_window.Clicked += OnTransactionsButtonClickedInWalletAdminWindow;
        p2p_button_wallet_admin_window.Clicked += OnP2PButtonInWalletAdminClickedInWalletAdminWindow;
        users_button_wallet_admin_window.Clicked += OnUsersButtonClickedInWalletAdminWindow;
        user_portfolio_button_wallet_admin_window.Clicked += OnUserPortfolioButtonClickedInWalletAdminWindow;
        wallets_button_wallet_admin_window.Clicked += OnWalletsButtonClickedInWalletAdminWindow;
        admin_button_wallet_admin_window.Clicked += OnAdminButtonClickedInWalletAdminWindow;
        block_button_wallet_admin_window.Clicked += OnBlockButtonClickedInWalletAdminWindow;
        logout_button_wallet_admin_window.Clicked += OnLogoutButtonClickedInWalletAdminWindow;
    }

    // Define the event handler methods for each button in the wallet admin window
    public static void OnServerAssetsButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }

    public static void OnTransactionsButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        transactions_wallet_admin_window.ShowAll();
    }

    public static void OnP2PButtonInWalletAdminClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        p2p_wallet_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        users_wallet_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        user_portfolio_wallet_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        // Logic for wallets button click in wallet admin window (if needed)
    }

    public static void OnAdminButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        admin_wallet_admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        wallet_admin_window.Hide();
        block_wallet_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInWalletAdminWindow(object sender, EventArgs e)
    {
        // Add your logic for logout button click in wallet admin window
    }
        
        public static void RequestAndSaveWalletList()
    	{
    		try
    		{

				walletId.Clear();
				walletAddress.Clear();
				walletUserId.Clear();
				walletBalance.Clear();
				walletPrivateKey.Clear();
				walletPublicKey.Clear();


    			List<Wallet> listWallet = new();
    			string RequestMessage = "GetWalletTable";

                        // Send the serialized Wallet object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listWallet = WaitForWalletList(_stream);
    			
    			for (int i = 0; i < listWallet.Count; i++)
			{
			    Wallet wallet = listWallet[i];
			    
			    walletId.Add(wallet.WalletId);
				walletAddress.Add(wallet.WalletAddress);
				walletUserId.Add(wallet.UserId);
				walletBalance.Add(wallet.Balance);
				walletPrivateKey.Add(wallet.PrivateKey);
				walletPublicKey.Add(wallet.PublicKey);

			   
			    
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void AddFrameToWalletTransactionWindow(int index)
{
    // Create a new frame
    Frame walletFrame = new Frame("");

    // Create the frame
    walletFrame.Visible = true;
    walletFrame.CanFocus = false;
    walletFrame.LabelXalign = 0;
    walletFrame.ShadowType = ShadowType.None;

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

    // Wallet ID Label
    Label walletIdLabel = new Label($"{walletId[index]}");
    walletIdLabel.Name = $"walletIdLabel_{index}";
    walletIdLabel.Visible = true;
    walletIdLabel.CanFocus = false;

    // inner frame for wallet ID
    Frame walletIdFrame = new Frame("");
    walletIdFrame.ShadowType = ShadowType.None;
    walletIdFrame.Add(walletIdLabel);
    walletIdFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletIdFrame, 0, 0, 1, 1);

    // Wallet Address Label
    Label walletAddressButton = new Button("show");
    walletAddressButton.Name = $"walletAddressLabel_{index}";
    walletAddressButton.Visible = true;
    walletAddressButton.CanFocus = false;

    // inner frame for wallet address
    Frame walletAddressFrame = new Frame("");
    walletAddressFrame.ShadowType = ShadowType.None;
    walletAddressFrame.Add(walletAddressButton);
    walletAddressFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletAddressFrame, 1, 0, 1, 1);

	// Connect button click events for main_window
		walletAddressButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(walletAddress[index]);


    // Wallet User ID Label
    Label walletUserIdLabel = new Label($"{walletUserId[index]}");
    walletUserIdLabel.Name = $"walletUserIdLabel_{index}";
    walletUserIdLabel.Visible = true;
    walletUserIdLabel.CanFocus = false;

    // inner frame for wallet user ID
    Frame walletUserIdFrame = new Frame("");
    walletUserIdFrame.ShadowType = ShadowType.None;
    walletUserIdFrame.Add(walletUserIdLabel);
    walletUserIdFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletUserIdFrame, 2, 0, 1, 1);

    // Wallet Balance Label
    Label walletBalanceLabel = new Label($"{walletBalance[index]}");
    walletBalanceLabel.Name = $"walletBalanceLabel_{index}";
    walletBalanceLabel.Visible = true;
    walletBalanceLabel.CanFocus = false;

    // inner frame for wallet balance
    Frame walletBalanceFrame = new Frame("");
    walletBalanceFrame.ShadowType = ShadowType.None;
    walletBalanceFrame.Add(walletBalanceLabel);
    walletBalanceFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletBalanceFrame, 3, 0, 1, 1);

    // Wallet Private Key Label
    Label walletPrivateKeyLabel = new Label(walletPrivateKey[index]);
    walletPrivateKeyLabel.Name = $"walletPrivateKeyLabel_{index}";
    walletPrivateKeyLabel.Visible = true;
    walletPrivateKeyLabel.CanFocus = false;

    // inner frame for wallet private key
    Frame walletPrivateKeyFrame = new Frame("");
    walletPrivateKeyFrame.ShadowType = ShadowType.None;
    walletPrivateKeyFrame.Add(walletPrivateKeyLabel);
    walletPrivateKeyFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletPrivateKeyFrame, 4, 0, 1, 1);

    // Wallet Public Key Label
    Label walletPublicKeyLabel = new Label(walletPublicKey[index]);
    walletPublicKeyLabel.Name = $"walletPublicKeyLabel_{index}";
    walletPublicKeyLabel.Visible = true;
    walletPublicKeyLabel.CanFocus = false;

    // inner frame for wallet public key
    Frame walletPublicKeyFrame = new Frame("");
    walletPublicKeyFrame.ShadowType = ShadowType.None;
    walletPublicKeyFrame.Add(walletPublicKeyLabel);
    walletPublicKeyFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(walletPublicKeyFrame, 5, 0, 1, 1);

    // Connect any additional events or customize the widgets as needed

    // Add the inner grid to the alignment
    alignment.Add(innerGrid);

    // Add the alignment to the frame
    walletFrame.Add(alignment);

    // Align Frame
    walletFrame.MarginEnd = 20;

    // Add the frame to the wallet_box (adjust as per your actual container)
    admin_wallet_box.Add(walletFrame);
    admin_wallet_box.ShowAll();
}
}
