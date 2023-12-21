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

public class UserPortfolioWindow : CCTPSApp
{
	public static Box admin_userPortfolio_box;
	
	public static Window user_portfolio_admin_window;

	public static Button server_assets_button_portfolio_admin_window;
	public static Button portfolio_button_portfolio_admin_window;
	public static Button p2p_button_portfolio_admin_window;
	public static Button users_button_portfolio_admin_window;
	public static Button user_portfolio_button_portfolio_admin_window;
	public static Button wallets_button_portfolio_admin_window;
	public static Button admin_button_portfolio_admin_window;
	public static Button miner_button_portfolio_admin_window;
	public static Button block_button_portfolio_admin_window;
	public static Button logout_button_portfolio_admin_window;



       
	public static List<int> UserPortfolioId = new();
	public static List<decimal> UserPortfolioBitcoin = new();
	public static List<decimal> UserPortfolioEthereum = new();
	public static List<decimal> UserPortfolioRipple = new();
	public static List<decimal> UserPortfolioLitecoin = new();
	public static List<decimal> UserPortfolioCardano = new();
	public static List<decimal> UserPortfolioPolkadot = new();
	public static List<decimal> UserPortfolioBinanceCoin = new();
	public static List<decimal> UserPortfolioChainlink = new();
	public static List<decimal> UserPortfolioStellar = new();
	public static List<decimal> UserPortfolioBitcoinCash = new();
	public static List<decimal> UserPortfolioDogecoin = new();
	public static List<decimal> UserPortfolioUSD_Coin = new();
	public static List<decimal> UserPortfolioAave = new();
	public static List<decimal> UserPortfolioCosmos = new();
	public static List<decimal> UserPortfolioMonero = new();
	public static List<decimal> UserPortfolioNeo = new();
	public static List<decimal> UserPortfolioTezos = new();
	public static List<decimal> UserPortfolioMaker = new();
	public static List<decimal> UserPortfolioEOS = new();
	public static List<decimal> UserPortfolioTRON = new();
	public static List<decimal> UserPortfolioVeChain = new();
	public static List<decimal> UserPortfolioSolana = new();
	public static List<decimal> UserPortfolioTheta = new();
	public static List<decimal> UserPortfolioDash = new();
	public static List<decimal> UserPortfolioUniswap = new();
	public static List<decimal> UserPortfolioCompound = new();


	static UserPortfolioWindow()
    	{
    		user_portfolio_admin_window = (Window)builder.GetObject("user_portfolio_admin_window");
    	
        	
        	admin_userPortfolio_box = (Box)builder.GetObject("admin_userPortfolio_box");
        	
        	RequestAndSaveUserPortfolioList();
        	for(int i=0; i < transactionTimestamp.Count;i++)
        	{
        		AddFrameToMarketValuesUserPortfolioWindow(i);
        	}
        	
        	// Assign the Clicked event handlers for each button in the portfolio admin window
        server_assets_button_portfolio_admin_window = (Button)builder.GetObject("server_assets_button_portfolio_admin_window");
        	logout_button_portfolio_admin_window = (Button)builder.GetObject("logout_button_portfolio_admin_window");
        	p2p_button_portfolio_admin_window = (Button)builder.GetObject("p2p_button_portfolio_admin_window");
        	portfolio_button_portfolio_admin_window = (Button)builder.GetObject("portfolio_button_portfolio_admin_window");
        	users_button_portfolio_admin_window = (Button)builder.GetObject("users_button_portfolio_admin_window");
        	user_portfolio_button_portfolio_admin_window = (Button)builder.GetObject("user_portfolio_button_portfolio_admin_window");
        	miner_button_portfolio_admin_window = (Button)builder.GetObject("miner_button_portfolio_admin_window");
        	wallets_button_portfolio_admin_window = (Button)builder.GetObject("wallets_button_portfolio_admin_window");
        	admin_button_portfolio_admin_window = (Button)builder.GetObject("admin_button_portfolio_admin_window");
        	block_button_portfolio_admin_window = (Button)builder.GetObject("block_button_portfolio_admin_window");


			server_assets_button_portfolio_admin_window.Clicked += OnServerAssetsButtonClickedInPortfolioAdminWindow;
			portfolio_button_portfolio_admin_window.Clicked += OnTransactionsButtonClickedInPortfolioAdminWindow;
			p2p_button_portfolio_admin_window.Clicked += OnP2PButtonInPortfolioAdminClickedInPortfolioAdminWindow;
			users_button_portfolio_admin_window.Clicked += OnUsersButtonClickedInPortfolioAdminWindow;
			user_portfolio_button_portfolio_admin_window.Clicked += OnUserPortfolioButtonClickedInPortfolioAdminWindow;
			wallets_button_portfolio_admin_window.Clicked += OnWalletsButtonClickedInPortfolioAdminWindow;
			admin_button_portfolio_admin_window.Clicked += OnAdminButtonClickedInPortfolioAdminWindow;
			block_button_portfolio_admin_window.Clicked += OnBlockButtonClickedInPortfolioAdminWindow;
			logout_button_portfolio_admin_window.Clicked += OnLogoutButtonClickedInPortfolioAdminWindow;
    }


  // Define the event handler methods for each button in the miners admin window
    public static void OnServerAssetsButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        // Show the corresponding window for the button
        Admin_main_window.ShowAll();
    }


    public static void OnTransactionsButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnP2PButtonInPortfolioAdminClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        p2p_admin_window.ShowAll();
    }

    public static void OnUsersButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        User_admin_window.ShowAll();
    }

    public static void OnUserPortfolioButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        user_portfolio_admin_window.ShowAll();
    }

    public static void OnWalletsButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        user_portfolio_admin_window.Hide();
        wallet_admin_window.ShowAll();
    }

    public static void OnAdminButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        // Logic for admin button click in block admin window (if needed)

		user_portfolio_admin_window.Hide();
		admin_window.ShowAll();
    }

    public static void OnBlockButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
      //  user_portfolio_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }
    public static void OnMinerButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
      //  user_portfolio_admin_window.Hide();
	//	block_admin_window.ShowAll();
    }

    public static void OnLogoutButtonClickedInPortfolioAdminWindow(object sender, EventArgs e)
    {
        Logout_window.ShowAll();
    }
        
        public static void RequestAndSaveUserPortfolioList()
    	{
    		try
    		{
				UserPortfolioId.Clear();
				UserPortfolioBitcoin.Clear();
				UserPortfolioEthereum.Clear();
				UserPortfolioRipple.Clear();
				UserPortfolioLitecoin.Clear();
				UserPortfolioCardano.Clear();
				UserPortfolioPolkadot.Clear();
				UserPortfolioBinanceCoin.Clear();
				UserPortfolioChainlink.Clear();
				UserPortfolioStellar.Clear();
				UserPortfolioBitcoinCash.Clear();
				UserPortfolioDogecoin.Clear();
				UserPortfolioUSD_Coin.Clear();
				UserPortfolioAave.Clear();
				UserPortfolioCosmos.Clear();
				UserPortfolioMonero.Clear();
				UserPortfolioNeo.Clear();
				UserPortfolioTezos.Clear();
				UserPortfolioMaker.Clear();
				UserPortfolioEOS.Clear();
				UserPortfolioTRON.Clear();
				UserPortfolioVeChain.Clear();
				UserPortfolioSolana.Clear();
				UserPortfolioTheta.Clear();
				UserPortfolioDash.Clear();
				UserPortfolioUniswap.Clear();
				UserPortfolioCompound.Clear();



    			List<UserPortfolio> listPortfolio = new();
    			string RequestMessage = "GetUserPortfolioTable";

                        // Send the serialized Portfolioaction object to C client
                        SendMessage(_stream, RequestMessage);

		        // Wait for a response from the server
		        listPortfolio = WaitForUserPortfolioList(_stream);
    			
    			for (int i = 0; i < listPortfolio.Count; i++)
			{
			    UserPortfolio portfolio = listPortfolio[i];


				UserPortfolioId.Add(portfolio.UserId);
				UserPortfolioBitcoin.Add(portfolio.Bitcoin);
				UserPortfolioEthereum.Add(portfolio.Ethereum);
				UserPortfolioRipple.Add(portfolio.Ripple);
				UserPortfolioLitecoin.Add(portfolio.Litecoin);
				UserPortfolioCardano.Add(portfolio.Cardano);
				UserPortfolioPolkadot.Add(portfolio.Polkadot);
				UserPortfolioBinanceCoin.Add(portfolio.BinanceCoin);
				UserPortfolioChainlink.Add(portfolio.Chainlink);
				UserPortfolioStellar.Add(portfolio.Stellar);
				UserPortfolioBitcoinCash.Add(portfolio.BitcoinCash);
				UserPortfolioDogecoin.Add(portfolio.Dogecoin);
				UserPortfolioUSD_Coin.Add(portfolio.USD_Coin);
				UserPortfolioAave.Add(portfolio.Aave);
				UserPortfolioCosmos.Add(portfolio.Cosmos);
				UserPortfolioMonero.Add(portfolio.Monero);
				UserPortfolioNeo.Add(portfolio.Neo);
				UserPortfolioTezos.Add(portfolio.Tezos);
				UserPortfolioMaker.Add(portfolio.Maker);
				UserPortfolioEOS.Add(portfolio.EOS);
				UserPortfolioTRON.Add(portfolio.TRON);
				UserPortfolioVeChain.Add(portfolio.VeChain);
				UserPortfolioSolana.Add(portfolio.Solana);
				UserPortfolioTheta.Add(portfolio.Theta);
				UserPortfolioDash.Add(portfolio.Dash);
				UserPortfolioUniswap.Add(portfolio.Uniswap);
				UserPortfolioCompound.Add(portfolio.Compound);
			    
			}
			
			
    		}
    		catch (Exception e)
	    	{
			Console.WriteLine($"Error in messaging: {e}");
	    	}
    	}
    	
    	public static void AddFrameToMarketValuesUserPortfolioWindow(int index)
{
    // Create a new frame
    Frame portfolioFrame = new Frame("");

    // Create the frame
    portfolioFrame.Visible = true;
    portfolioFrame.CanFocus = false;
    portfolioFrame.LabelXalign = 0;
    portfolioFrame.ShadowType = ShadowType.None;

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

    // User Portfolio ID Label
    Label userPortfolioIdLabel = new Label($"{UserPortfolioId[index]}");
    userPortfolioIdLabel.Name = $"userPortfolioIdLabel_{index}";
    userPortfolioIdLabel.Visible = true;
    userPortfolioIdLabel.CanFocus = false;

    // inner frame for user portfolio ID
    Frame userPortfolioIdFrame = new Frame("");
    userPortfolioIdFrame.ShadowType = ShadowType.None;
    userPortfolioIdFrame.Add(userPortfolioIdLabel);
    userPortfolioIdFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(userPortfolioIdFrame, 0, 0, 1, 1);

    // Create labels for each cryptocurrency in the portfolio (Bitcoin, Ethereum, Ripple, etc.)
    // Repeat the following pattern for each cryptocurrency label

    Label bitcoinLabel = new Label($"{UserPortfolioBitcoin[index]}");
    bitcoinLabel.Name = $"bitcoinLabel_{index}";
    bitcoinLabel.Visible = true;
    bitcoinLabel.CanFocus = false;

    Frame bitcoinFrame = new Frame("");
    bitcoinFrame.ShadowType = ShadowType.None;
    bitcoinFrame.Add(bitcoinLabel);
    bitcoinFrame.SetSizeRequest(150, -1);

    innerGrid.Attach(bitcoinFrame, 1, 0, 1, 1);

    // Ethereum Label
Label ethereumLabel = new Label($"{UserPortfolioEthereum[index]}");
ethereumLabel.Name = $"ethereumLabel_{index}";
ethereumLabel.Visible = true;
ethereumLabel.CanFocus = false;

Frame ethereumFrame = new Frame("");
ethereumFrame.ShadowType = ShadowType.None;
ethereumFrame.Add(ethereumLabel);
ethereumFrame.SetSizeRequest(150, -1);

innerGrid.Attach(ethereumFrame, 2, 0, 1, 1);

// Ripple Label
Label rippleLabel = new Label($"{UserPortfolioRipple[index]}");
rippleLabel.Name = $"rippleLabel_{index}";
rippleLabel.Visible = true;
rippleLabel.CanFocus = false;

Frame rippleFrame = new Frame("");
rippleFrame.ShadowType = ShadowType.None;
rippleFrame.Add(rippleLabel);
rippleFrame.SetSizeRequest(150, -1);

innerGrid.Attach(rippleFrame, 3, 0, 1, 1);

// Litecoin Label
Label litecoinLabel = new Label($"{UserPortfolioLitecoin[index]}");
litecoinLabel.Name = $"litecoinLabel_{index}";
litecoinLabel.Visible = true;
litecoinLabel.CanFocus = false;

Frame litecoinFrame = new Frame("");
litecoinFrame.ShadowType = ShadowType.None;
litecoinFrame.Add(litecoinLabel);
litecoinFrame.SetSizeRequest(150, -1);

innerGrid.Attach(litecoinFrame, 4, 0, 1, 1);

// Cardano Label
Label cardanoLabel = new Label($"{UserPortfolioCardano[index]}");
cardanoLabel.Name = $"cardanoLabel_{index}";
cardanoLabel.Visible = true;
cardanoLabel.CanFocus = false;

Frame cardanoFrame = new Frame("");
cardanoFrame.ShadowType = ShadowType.None;
cardanoFrame.Add(cardanoLabel);
cardanoFrame.SetSizeRequest(150, -1);

innerGrid.Attach(cardanoFrame, 5, 0, 1, 1);

// Polkadot Label
Label polkadotLabel = new Label($"{UserPortfolioPolkadot[index]}");
polkadotLabel.Name = $"polkadotLabel_{index}";
polkadotLabel.Visible = true;
polkadotLabel.CanFocus = false;

Frame polkadotFrame = new Frame("");
polkadotFrame.ShadowType = ShadowType.None;
polkadotFrame.Add(polkadotLabel);
polkadotFrame.SetSizeRequest(150, -1);

innerGrid.Attach(polkadotFrame, 6, 0, 1, 1);

// Binance Coin Label
Label binanceCoinLabel = new Label($"{UserPortfolioBinanceCoin[index]}");
binanceCoinLabel.Name = $"binanceCoinLabel_{index}";
binanceCoinLabel.Visible = true;
binanceCoinLabel.CanFocus = false;

Frame binanceCoinFrame = new Frame("");
binanceCoinFrame.ShadowType = ShadowType.None;
binanceCoinFrame.Add(binanceCoinLabel);
binanceCoinFrame.SetSizeRequest(150, -1);

innerGrid.Attach(binanceCoinFrame, 7, 0, 1, 1);

// Chainlink Label
Label chainlinkLabel = new Label($"{UserPortfolioChainlink[index]}");
chainlinkLabel.Name = $"chainlinkLabel_{index}";
chainlinkLabel.Visible = true;
chainlinkLabel.CanFocus = false;

Frame chainlinkFrame = new Frame("");
chainlinkFrame.ShadowType = ShadowType.None;
chainlinkFrame.Add(chainlinkLabel);
chainlinkFrame.SetSizeRequest(150, -1);

innerGrid.Attach(chainlinkFrame, 8, 0, 1, 1);

// Stellar Label
Label stellarLabel = new Label($"{UserPortfolioStellar[index]}");
stellarLabel.Name = $"stellarLabel_{index}";
stellarLabel.Visible = true;
stellarLabel.CanFocus = false;

Frame stellarFrame = new Frame("");
stellarFrame.ShadowType = ShadowType.None;
stellarFrame.Add(stellarLabel);
stellarFrame.SetSizeRequest(150, -1);

innerGrid.Attach(stellarFrame, 9, 0, 1, 1);

// Bitcoin Cash Label
Label bitcoinCashLabel = new Label($"{UserPortfolioBitcoinCash[index]}");
bitcoinCashLabel.Name = $"bitcoinCashLabel_{index}";
bitcoinCashLabel.Visible = true;
bitcoinCashLabel.CanFocus = false;

Frame bitcoinCashFrame = new Frame("");
bitcoinCashFrame.ShadowType = ShadowType.None;
bitcoinCashFrame.Add(bitcoinCashLabel);
bitcoinCashFrame.SetSizeRequest(150, -1);

innerGrid.Attach(bitcoinCashFrame, 10, 0, 1, 1);

// Dogecoin Label
Label dogecoinLabel = new Label($"{UserPortfolioDogecoin[index]}");
dogecoinLabel.Name = $"dogecoinLabel_{index}";
dogecoinLabel.Visible = true;
dogecoinLabel.CanFocus = false;

Frame dogecoinFrame = new Frame("");
dogecoinFrame.ShadowType = ShadowType.None;
dogecoinFrame.Add(dogecoinLabel);
dogecoinFrame.SetSizeRequest(150, -1);

innerGrid.Attach(dogecoinFrame, 11, 0, 1, 1);

// USD Coin Label
Label usdCoinLabel = new Label($"{UserPortfolioUSD_Coin[index]}");
usdCoinLabel.Name = $"usdCoinLabel_{index}";
usdCoinLabel.Visible = true;
usdCoinLabel.CanFocus = false;

Frame usdCoinFrame = new Frame("");
usdCoinFrame.ShadowType = ShadowType.None;
usdCoinFrame.Add(usdCoinLabel);
usdCoinFrame.SetSizeRequest(150, -1);

innerGrid.Attach(usdCoinFrame, 12, 0, 1, 1);

// Aave Label
Label aaveLabel = new Label($"{UserPortfolioAave[index]}");
aaveLabel.Name = $"aaveLabel_{index}";
aaveLabel.Visible = true;
aaveLabel.CanFocus = false;

Frame aaveFrame = new Frame("");
aaveFrame.ShadowType = ShadowType.None;
aaveFrame.Add(aaveLabel);
aaveFrame.SetSizeRequest(150, -1);

innerGrid.Attach(aaveFrame, 13, 0, 1, 1);

// Cosmos Label
Label cosmosLabel = new Label($"{UserPortfolioCosmos[index]}");
cosmosLabel.Name = $"cosmosLabel_{index}";
cosmosLabel.Visible = true;
cosmosLabel.CanFocus = false;

Frame cosmosFrame = new Frame("");
cosmosFrame.ShadowType = ShadowType.None;
cosmosFrame.Add(cosmosLabel);
cosmosFrame.SetSizeRequest(150, -1);

innerGrid.Attach(cosmosFrame, 14, 0, 1, 1);

// Monero Label
Label moneroLabel = new Label($"{UserPortfolioMonero[index]}");
moneroLabel.Name = $"moneroLabel_{index}";
moneroLabel.Visible = true;
moneroLabel.CanFocus = false;

Frame moneroFrame = new Frame("");
moneroFrame.ShadowType = ShadowType.None;
moneroFrame.Add(moneroLabel);
moneroFrame.SetSizeRequest(150, -1);

innerGrid.Attach(moneroFrame, 15, 0, 1, 1);

// Neo Label
Label neoLabel = new Label($"{UserPortfolioNeo[index]}");
neoLabel.Name = $"neoLabel_{index}";
neoLabel.Visible = true;
neoLabel.CanFocus = false;

Frame neoFrame = new Frame("");
neoFrame.ShadowType = ShadowType.None;
neoFrame.Add(neoLabel);
neoFrame.SetSizeRequest(150, -1);

innerGrid.Attach(neoFrame, 16, 0, 1, 1);

// Tezos Label
Label tezosLabel = new Label($"{UserPortfolioTezos[index]}");
tezosLabel.Name = $"tezosLabel_{index}";
tezosLabel.Visible = true;
tezosLabel.CanFocus = false;

Frame tezosFrame = new Frame("");
tezosFrame.ShadowType = ShadowType.None;
tezosFrame.Add(tezosLabel);
tezosFrame.SetSizeRequest(150, -1);

innerGrid.Attach(tezosFrame, 17, 0, 1, 1);

// Maker Label
Label makerLabel = new Label($"{UserPortfolioMaker[index]}");
makerLabel.Name = $"makerLabel_{index}";
makerLabel.Visible = true;
makerLabel.CanFocus = false;

Frame makerFrame = new Frame("");
makerFrame.ShadowType = ShadowType.None;
makerFrame.Add(makerLabel);
makerFrame.SetSizeRequest(150, -1);

innerGrid.Attach(makerFrame, 18, 0, 1, 1);

// EOS Label
Label eosLabel = new Label($"{UserPortfolioEOS[index]}");
eosLabel.Name = $"eosLabel_{index}";
eosLabel.Visible = true;
eosLabel.CanFocus = false;

Frame eosFrame = new Frame("");
eosFrame.ShadowType = ShadowType.None;
eosFrame.Add(eosLabel);
eosFrame.SetSizeRequest(150, -1);

innerGrid.Attach(eosFrame, 19, 0, 1, 1);

// TRON Label
Label tronLabel = new Label($"{UserPortfolioTRON[index]}");
tronLabel.Name = $"tronLabel_{index}";
tronLabel.Visible = true;
tronLabel.CanFocus = false;

Frame tronFrame = new Frame("");
tronFrame.ShadowType = ShadowType.None;
tronFrame.Add(tronLabel);
tronFrame.SetSizeRequest(150, -1);

innerGrid.Attach(tronFrame, 20, 0, 1, 1);

// VeChain Label
Label veChainLabel = new Label($"{UserPortfolioVeChain[index]}");
veChainLabel.Name = $"veChainLabel_{index}";
veChainLabel.Visible = true;
veChainLabel.CanFocus = false;

Frame veChainFrame = new Frame("");
veChainFrame.ShadowType = ShadowType.None;
veChainFrame.Add(veChainLabel);
veChainFrame.SetSizeRequest(150, -1);

innerGrid.Attach(veChainFrame, 21, 0, 1, 1);

// Solana Label
Label solanaLabel = new Label($"{UserPortfolioSolana[index]}");
solanaLabel.Name = $"solanaLabel_{index}";
solanaLabel.Visible = true;
solanaLabel.CanFocus = false;

Frame solanaFrame = new Frame("");
solanaFrame.ShadowType = ShadowType.None;
solanaFrame.Add(solanaLabel);
solanaFrame.SetSizeRequest(150, -1);

innerGrid.Attach(solanaFrame, 22, 0, 1, 1);

// Theta Label
Label thetaLabel = new Label($"{UserPortfolioTheta[index]}");
thetaLabel.Name = $"thetaLabel_{index}";
thetaLabel.Visible = true;
thetaLabel.CanFocus = false;

Frame thetaFrame = new Frame("");
thetaFrame.ShadowType = ShadowType.None;
thetaFrame.Add(thetaLabel);
thetaFrame.SetSizeRequest(150, -1);

innerGrid.Attach(thetaFrame, 23, 0, 1, 1);

// Dash Label
Label dashLabel = new Label($"{UserPortfolioDash[index]}");
dashLabel.Name = $"dashLabel_{index}";
dashLabel.Visible = true;
dashLabel.CanFocus = false;

Frame dashFrame = new Frame("");
dashFrame.ShadowType = ShadowType.None;
dashFrame.Add(dashLabel);
dashFrame.SetSizeRequest(150, -1);

innerGrid.Attach(dashFrame, 24, 0, 1, 1);

// Uniswap Label
Label uniswapLabel = new Label($"{UserPortfolioUniswap[index]}");
uniswapLabel.Name = $"uniswapLabel_{index}";
uniswapLabel.Visible = true;
uniswapLabel.CanFocus = false;

Frame uniswapFrame = new Frame("");
uniswapFrame.ShadowType = ShadowType.None;
uniswapFrame.Add(uniswapLabel);
uniswapFrame.SetSizeRequest(150, -1);

innerGrid.Attach(uniswapFrame, 25, 0, 1, 1);

// Compound Label
Label compoundLabel = new Label($"{UserPortfolioCompound[index]}");
compoundLabel.Name = $"compoundLabel_{index}";
compoundLabel.Visible = true;
compoundLabel.CanFocus = false;

Frame compoundFrame = new Frame("");
compoundFrame.ShadowType = ShadowType.None;
compoundFrame.Add(compoundLabel);
compoundFrame.SetSizeRequest(150, -1);

innerGrid.Attach(compoundFrame, 26, 0, 1, 1);

    // Connect any additional events or customize the widgets as needed

    // Add the inner grid to the alignment
    alignment.Add(innerGrid);

    // Add the alignment to the frame
    portfolioFrame.Add(alignment);

    // Align Frame
    portfolioFrame.MarginEnd = 20;

    // Add the frame to the user_portfolio_box (adjust as per your actual container)
    admin_userPortfolio_box.Add(portfolioFrame);
    admin_userPortfolio_box.ShowAll();
}
}
