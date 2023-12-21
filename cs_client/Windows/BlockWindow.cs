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




public class BlockWindow : CCTPSApp
{
	// BOX
	public static Box block_admin_box;
	
	//Window
	public static Window block_admin_window;
	
	//Navbar Buttons
	public static Button server_assets_button_block_admin_window;
	public static Button transactions_button_block_admin_window;
	public static Button p2p_button_block_admin_window;
	public static Button users_button_block_admin_window;
	public static Button user_portfolio_button_block_admin_window;
	public static Button wallets_button_block_admin_window;
	public static Button admin_button_block_admin_window;
	public static Button block_button_block_admin_window;
	public static Button miner_button_block_admin_window;
	public static Button logout_button_block_admin_window;
    	
    	//Lists (fields)
    	public static List<string> blockNumber = new();
		public static List<string> blockRootHash = new();
		public static List<string> blockTotalAmount = new();
		public static List<string> blockTimestamp = new();
		public static List<string> blockTotalTransactions = new();
    	public static List<string> blockPreviousHash = new();
    	public static List<string> blockHash = new();

	static void TransactionsWindow()
    	{
    		block_admin_window = (Window)builder.GetObject("block_admin_window");
    	
    		server_assets_button_block_admin_window = (Button)builder.GetObject("server_assets_button_block_admin_window");
        	logout_button_block_admin_window = (Button)builder.GetObject("logout_button_block_admin_window");
        	p2p_button_block_admin_window = (Button)builder.GetObject("p2p_button_block_admin_window");
        	transactions_button_block_admin_window = (Button)builder.GetObject("transactions_button_block_admin_window");
        	users_button_block_admin_window = (Button)builder.GetObject("users_button_block_admin_window");
        	user_portfolio_button_block_admin_window = (Button)builder.GetObject("user_portfolio_button_block_admin_window");
        	miner_button_block_admin_window = (Button)builder.GetObject("miner_button_block_admin_window");
        	wallets_button_block_admin_window = (Button)builder.GetObject("wallets_button_block_admin_window");
        	admin_button_block_admin_window = (Button)builder.GetObject("admin_button_block_admin_window");
        	block_button_block_admin_window = (Button)builder.GetObject("block_button_block_admin_window");
        	
        	block_admin_box = (Box)builder.GetObject("block_admin_box");
        	
        	AddFrameToBlockWindow();
        	for(int i=0; i < blockNumber.Count;i++)
        	{
        		AddFrameToMarketValuesBlockWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the block admin window
        server_assets_button_block_admin_window.Clicked += OnServerAssetsButtonClickedInBlockAdminWindow;
        transactions_button_block_admin_window.Clicked += OnTransactionsButtonClickedInBlockAdminWindow;
        p2p_button_block_admin_window.Clicked += OnP2PButtonInBlockAdminClickedInBlockAdminWindow;
        users_button_block_admin_window.Clicked += OnUsersButtonClickedInBlockAdminWindow;
        user_portfolio_button_block_admin_window.Clicked += OnUserPortfolioButtonClickedInBlockAdminWindow;
        wallets_button_block_admin_window.Clicked += OnWalletsButtonClickedInBlockAdminWindow;
        admin_button_block_admin_window.Clicked += OnAdminButtonClickedInBlockAdminWindow;
        block_button_block_admin_window.Clicked += OnBlockButtonClickedInBlockAdminWindow;
        logout_button_block_admin_window.Clicked += OnLogoutButtonClickedInBlockAdminWindow;
		miner_button_block_admin_window.Clicked += OnMinerButtonClickedInBlockAdminWindow;
    }

    // Define the event handler methods for each button in the block admin window
    public static void OnServerAssetsButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }

    public static void OnTransactionsButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        transactions_admin_window.ShowAll();
    }

    public static void OnP2PButtonInBlockAdminClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        block_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		block_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
      //  block_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
      //  block_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInBlockAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
        
        public static void AddFrameToBlockWindow()
    	{
    		try
    		{
    			List<Block> listBlock = new();
    			string RequestMessage = "GetBlockchain";

                        // Send the serialized Transaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listBlock = WaitForBLockchain(_stream);
    			
    			for (int i = 0; i < listBlock.Count; i++)
			{
			    	Block block = listBlock[i];
			    			    
			   	blockNumber.Add(block.BlockNumber.ToString());
				blockRootHash.Add(block.RootHash);
				blockTotalAmount.Add(block.TotalAmount.ToString());
				blockTotalTransactions.Add(block.TotalTransactions.ToString());
				blockTimestamp.Add(block.Timestamp.ToString());
				blockPreviousHash.Add(block.PreviousHash);
				blockHash.Add(block.Hash);			    
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	//adjust
    	public static void AddFrameToMarketValuesBlockWindow(int index)
		{
			// Create a new frame
			Frame blockFrame = new Frame("");

			// Create the frame
			blockFrame.Visible = true;
			blockFrame.CanFocus = false;
			blockFrame.LabelXalign = 0;
			blockFrame.ShadowType = ShadowType.None;

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

			// Block Number Label
			Label blockNumberLabel = new Label($"{blockNumber[index]}");
			blockNumberLabel.Name = $"blockNumber_{index}";
			blockNumberLabel.Visible = true;
			blockNumberLabel.CanFocus = false;

			// inner frame for block number
			Frame blockNumberFrame = new Frame("");
			blockNumberFrame.ShadowType = ShadowType.None;
			blockNumberFrame.Add(blockNumberLabel);
			blockNumberFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockNumberFrame, 0, 0, 1, 1);

			// Block Root Hash Label
			Button blockRootHashButton = new Button("show");
			blockRootHashButton.Name = $"blockRootHashLabel_{index}";
			blockRootHashButton.Visible = true;
			blockRootHashButton.CanFocus = false;

			// inner frame for block root hash
			Frame blockRootHashFrame = new Frame("");
			blockRootHashFrame.ShadowType = ShadowType.None;
			blockRootHashFrame.Add(blockRootHashButton);
			blockRootHashFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockRootHashFrame, 1, 0, 1, 1);

			blockRootHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(blockRootHash[index]);

			// Block Total Amount Label
			Label blockTotalAmountLabel = new Label(blockTotalAmount[index]);
			blockTotalAmountLabel.Name = $"blockTotalAmountLabel_{index}";
			blockTotalAmountLabel.Visible = true;
			blockTotalAmountLabel.CanFocus = false;

			// inner frame for block total amount
			Frame blockTotalAmountFrame = new Frame("");
			blockTotalAmountFrame.ShadowType = ShadowType.None;
			blockTotalAmountFrame.Add(blockTotalAmountLabel);
			blockTotalAmountFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockTotalAmountFrame, 2, 0, 1, 1);


			// Block Timestamp Label
			Label blockTimestampLabel = new Label(blockTimestamp[index]);
			blockTimestampLabel.Name = $"blockTimestampLabel_{index}";
			blockTimestampLabel.Visible = true;
			blockTimestampLabel.CanFocus = false;

			// inner frame for block timestamp
			Frame blockTimestampFrame = new Frame("");
			blockTimestampFrame.ShadowType = ShadowType.None;
			blockTimestampFrame.Add(blockTimestampLabel);
			blockTimestampFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockTimestampFrame, 3, 0, 1, 1);

			// Block Total Transactions Label
			Label blockTotalTransactionsLabel = new Label(blockTotalTransactions[index]);
			blockTotalTransactionsLabel.Name = $"blockTotalTransactionsLabel_{index}";
			blockTotalTransactionsLabel.Visible = true;
			blockTotalTransactionsLabel.CanFocus = false;

			// inner frame for block total transactions
			Frame blockTotalTransactionsFrame = new Frame("");
			blockTotalTransactionsFrame.ShadowType = ShadowType.None;
			blockTotalTransactionsFrame.Add(blockTotalTransactionsLabel);
			blockTotalTransactionsFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockTotalTransactionsFrame, 4, 0, 1, 1);

			// Block Previous Hash Label
			Button blockPreviousHashButton = new Button("show");
			blockPreviousHashButton.Name = $"blockPreviousHashLabel_{index}";
			blockPreviousHashButton.Visible = true;
			blockPreviousHashButton.CanFocus = false;

			// inner frame for block previous hash
			Frame blockPreviousHashFrame = new Frame("");
			blockPreviousHashFrame.ShadowType = ShadowType.None;
			blockPreviousHashFrame.Add(blockPreviousHashButton);
			blockPreviousHashFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockPreviousHashFrame, 5, 0, 1, 1);

			blockPreviousHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(blockPreviousHash[index]);

			// Block Hash Label
			Button blockHashButton = new Button("show");
			blockHashButton.Name = $"blockHashLabel_{index}";
			blockHashButton.Visible = true;
			blockHashButton.CanFocus = false;

			// inner frame for block hash
			Frame blockHashFrame = new Frame("");
			blockHashFrame.ShadowType = ShadowType.None;
			blockHashFrame.Add(blockHashButton);
			blockHashFrame.SetSizeRequest(150, -1);

			innerGrid.Attach(blockHashFrame, 6, 0, 1, 1);
			blockHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(blockHash[index]);


			// Connect any additional events or customize the widgets as needed

			// Add the inner grid to the alignment
			alignment.Add(innerGrid);

			// Add the alignment to the frame
			blockFrame.Add(alignment);

			// Align Frame
			blockFrame.MarginEnd = 20;

			// Add the frame to the block_box (adjust as per your actual container)
			block_admin_box.Add(blockFrame);
			block_admin_box.ShowAll();
		}



}
