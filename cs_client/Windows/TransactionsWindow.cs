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



public class TransactionsWindow : CCTPSApp
{
	public static Box admin_transactions_box;
	
	public static Window transactions_admin_window;

	public static Button server_assets_button_transactions_admin_window;
	public static Button transactions_button_transactions_admin_window;
	public static Button p2p_button_transactions_admin_window;
	public static Button users_button_transactions_admin_window;
	public static Button user_portfolio_button_transactions_admin_window;
	public static Button wallets_button_transactions_admin_window;
	public static Button admin_button_transactions_admin_window;
	public static Button block_button_transactions_admin_window;
	public static Button logout_button_transactions_admin_window;
    public static Button miner_button_transactions_admin_window;

	public static List<int> transactionId = new();
	public static List<string> transactionFromAddress = new();
	public static List<string> transactionToAddress = new();
	public static List<string> transactionVolume = new();
	public static List<string> transactionValue = new();
	public static List<string> transactionCurrencyName = new();
	public static List<string> transactionTimestamp = new();
	public static List<string> transactionHash = new();
	public static List<string> transactionValidationStatus = new();

	static TransactionsWindow()
    	{
    		transactions_admin_window = (Window)builder.GetObject("transactions_admin_window");
    	
    		server_assets_button_transactions_admin_window = (Button)builder.GetObject("server_assets_button_transactions_admin_window");
        	logout_button_transactions_admin_window = (Button)builder.GetObject("logout_button_transactions_admin_window");
        	p2p_button_transactions_admin_window = (Button)builder.GetObject("p2p_button_transactions_admin_window");
        	transactions_button_transactions_admin_window = (Button)builder.GetObject("transactions_button_transactions_admin_window");
        	users_button_transactions_admin_window = (Button)builder.GetObject("users_button_transactions_admin_window");
        	user_portfolio_button_transactions_admin_window = (Button)builder.GetObject("user_portfolio_button_transactions_admin_window");
        	miner_button_transactions_admin_window = (Button)builder.GetObject("miner_button_transactions_admin_window");
        	wallets_button_transactions_admin_window = (Button)builder.GetObject("wallets_button_transactions_admin_window");
        	admin_button_transactions_admin_window = (Button)builder.GetObject("admin_button_transactions_admin_window");
        	block_button_transactions_admin_window = (Button)builder.GetObject("block_button_transactions_admin_window");
        	
        	admin_transactions_box = (Box)builder.GetObject("admin_transactions_box");
        	
        	RequestAndSaveTransactionList();
        	for(int i=0; i < transactionTimestamp.Count;i++)
        	{
        		AddFrameToMarketValuesTransactionWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the transactions admin window
        server_assets_button_transactions_admin_window.Clicked += OnServerAssetsButtonClickedInTransactionsAdminWindow;
        transactions_button_transactions_admin_window.Clicked += OnTransactionsButtonClickedInTransactionsAdminWindow;
        p2p_button_transactions_admin_window.Clicked += OnP2PButtonInTransactionsAdminClickedInTransactionsAdminWindow;
        users_button_transactions_admin_window.Clicked += OnUsersButtonClickedInTransactionsAdminWindow;
        user_portfolio_button_transactions_admin_window.Clicked += OnUserPortfolioButtonClickedInTransactionsAdminWindow;
        wallets_button_transactions_admin_window.Clicked += OnWalletsButtonClickedInTransactionsAdminWindow;
        admin_button_transactions_admin_window.Clicked += OnAdminButtonClickedInTransactionsAdminWindow;
        block_button_transactions_admin_window.Clicked += OnBlockButtonClickedInTransactionsAdminWindow;
        logout_button_transactions_admin_window.Clicked += OnLogoutButtonClickedInTransactionsAdminWindow;
    }

     // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        transactions_admin_window.ShowAll();
    }

    public static void OnP2PButtonInTransactionsAdminClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        transactions_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		transactions_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
      //  transactions_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
      //  transactions_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInTransactionsAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
        
        public static void RequestAndSaveTransactionList()
    	{
    		try
    		{
    			List<Transaction> listTrans = new();
    			string RequestMessage = "GetFullTransactionList";

                        // Send the serialized Transaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listTrans = WaitForUserTransactionList(_stream);
    			
    			for (int i = 0; i < listTrans.Count; i++)
				{
					Transaction trans = listTrans[i];

					transactionId.Add(trans.TransactionId);
					transactionFromAddress.Add(trans.FromAddress);
					transactionToAddress.Add(trans.ToAddress);
					transactionVolume.Add(trans.CryptoValue.ToString());
					transactionValue.Add(trans.CashValue.ToString());
					transactionCurrencyName.Add(trans.CryptocurrencyName);
					transactionTimestamp.Add(trans.DateTime.ToString());
					transactionHash.Add(trans.TransactionsHash);
					transactionValidationStatus.Add(trans.ValidationStatus);
			   
			    
				}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void AddFrameToTransactionWindow(int index)
{
    // Create a new frame
    Frame transactionFrame = new Frame("");

    // Create the frame
    transactionFrame.Visible = true;
    transactionFrame.CanFocus = false;
    transactionFrame.LabelXalign = 0;
    transactionFrame.ShadowType = ShadowType.None;

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

    // Transaction ID Label
    Label transactionIdLabel = new Label($"{transactionId[index]}");
    transactionIdLabel.Name = $"transactionIdLabel_{index}";
    transactionIdLabel.Visible = true;
    transactionIdLabel.CanFocus = false;

    // inner frame for transaction ID
    Frame transactionIdFrame = new Frame("");
    transactionIdFrame.ShadowType = ShadowType.None;
    transactionIdFrame.Add(transactionIdLabel);
    transactionIdFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionIdFrame, 0, 0, 1, 1);

    // Transaction From Address Label
    Label transactionFromAddressLabel = new Label(transactionFromAddress[index]);
    transactionFromAddressLabel.Name = $"transactionFromAddressLabel_{index}";
    transactionFromAddressLabel.Visible = true;
    transactionFromAddressLabel.CanFocus = false;

    // inner frame for transaction from address
    Frame transactionFromAddressFrame = new Frame("");
    transactionFromAddressFrame.ShadowType = ShadowType.None;
    transactionFromAddressFrame.Add(transactionFromAddressLabel);
    transactionFromAddressFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionFromAddressFrame, 1, 0, 1, 1);

    // Transaction To Address Label
    Button transactionToAddressButton = new Button("show");
    transactionToAddressButton.Name = $"transactionToAddressLabel_{index}";
    transactionToAddressButton.Visible = true;
    transactionToAddressButton.CanFocus = false;

    // inner frame for transaction to address
    Frame transactionToAddressFrame = new Frame("");
    transactionToAddressFrame.ShadowType = ShadowType.None;
    transactionToAddressFrame.Add(transactionToAddressButton);
    transactionToAddressFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionToAddressFrame, 2, 0, 1, 1);

	// Connect button click events for main_window
	transactionToAddressButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(transactionToAddress[index]);

    // Transaction Volume Label
    Label transactionVolumeLabel = new Label(transactionVolume[index]);
    transactionVolumeLabel.Name = $"transactionVolumeLabel_{index}";
    transactionVolumeLabel.Visible = true;
    transactionVolumeLabel.CanFocus = false;

    // inner frame for transaction volume
    Frame transactionVolumeFrame = new Frame("");
    transactionVolumeFrame.ShadowType = ShadowType.None;
    transactionVolumeFrame.Add(transactionVolumeLabel);
    transactionVolumeFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionVolumeFrame, 3, 0, 1, 1);

    // Transaction Value Label
    Label transactionValueLabel = new Label(transactionValue[index]);
    transactionValueLabel.Name = $"transactionValueLabel_{index}";
    transactionValueLabel.Visible = true;
    transactionValueLabel.CanFocus = false;

    // inner frame for transaction value
    Frame transactionValueFrame = new Frame("");
    transactionValueFrame.ShadowType = ShadowType.None;
    transactionValueFrame.Add(transactionValueLabel);
    transactionValueFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionValueFrame, 4, 0, 1, 1);

    // Transaction Currency Name Label
    Label transactionCurrencyNameLabel = new Label(transactionCurrencyName[index]);
    transactionCurrencyNameLabel.Name = $"transactionCurrencyNameLabel_{index}";
    transactionCurrencyNameLabel.Visible = true;
    transactionCurrencyNameLabel.CanFocus = false;

    // inner frame for transaction currency name
    Frame transactionCurrencyNameFrame = new Frame("");
    transactionCurrencyNameFrame.ShadowType = ShadowType.None;
    transactionCurrencyNameFrame.Add(transactionCurrencyNameLabel);
    transactionCurrencyNameFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionCurrencyNameFrame, 5, 0, 1, 1);

    // Transaction Timestamp Label
    Label transactionTimestampLabel = new Label(transactionTimestamp[index]);
    transactionTimestampLabel.Name = $"transactionTimestampLabel_{index}";
    transactionTimestampLabel.Visible = true;
    transactionTimestampLabel.CanFocus = false;

    // inner frame for transaction timestamp
    Frame transactionTimestampFrame = new Frame("");
    transactionTimestampFrame.ShadowType = ShadowType.None;
    transactionTimestampFrame.Add(transactionTimestampLabel);
    transactionTimestampFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionTimestampFrame, 6, 0, 1, 1);

    // Transaction Hash Label
    Button transactionHashButton = new Button("show");
    transactionHashButton.Name = $"transactionHashLabel_{index}";
    transactionHashButton.Visible = true;
    transactionHashButton.CanFocus = false;

    // inner frame for transaction hash
    Frame transactionHashFrame = new Frame("");
    transactionHashFrame.ShadowType = ShadowType.None;
    transactionHashFrame.Add(transactionHashButton);
    transactionHashFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionHashFrame, 7, 0, 1, 1);

	// Connect button click events for main_window
		transactionHashButton.Clicked += (sender, args) => adminPasswordHashButton_clicked(transactionHash[index]);

    // Transaction Validation Status Label
    Label transactionValidationStatusLabel = new Label(transactionValidationStatus[index]);
    transactionValidationStatusLabel.Name = $"transactionValidationStatusLabel_{index}";
    transactionValidationStatusLabel.Visible = true;
    transactionValidationStatusLabel.CanFocus = false;

    // inner frame for transaction validation status
    Frame transactionValidationStatusFrame = new Frame("");
    transactionValidationStatusFrame.ShadowType = ShadowType.None;
    transactionValidationStatusFrame.Add(transactionValidationStatusLabel);
    transactionValidationStatusFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(transactionValidationStatusFrame, 8, 0, 1, 1);

    // Connect any additional events or customize the widgets as needed

    // Add the inner grid to the alignment
    alignment.Add(innerGrid);

    // Add the alignment to the frame
    transactionFrame.Add(alignment);

    // Align Frame
    transactionFrame.MarginEnd = 20;

    // Add the frame to the transaction_box (adjust as per your actual container)
    admin_transactions_box.Add(transactionFrame);
    admin_transactions_box.ShowAll();
}


}
