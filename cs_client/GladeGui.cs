using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using Gtk;
using static CSharpClient;
using static Transaction;
using static User;
using static Cryptocurrency;
using static UserPortfolio;
using static UserOffer;
using static Wallet;
using static MinerUtil;
using static CustomAlertWindow;
using static GetCPUInformation;
using static Block;

using static ServerAssetsWindow;
using static TransactionsWindow;
using static P2PWindow;
using static UserWindow;



    public class CCTPSApp
    {

        // Socket variables

        public static TcpClient client = null;
        public static NetworkStream _stream;


        //Global variables

        private string FILEPATH = "User/account.json";

        // Class Variables

        static User user;
        static User miningUser;
        static Wallet accountDetails;
        static UserPortfolio accountPortfolio;
        static List<Transaction> userTransactionsList;
        static UserOffer userOffer;
        static List<UserOffer> userOffersList;
        static bool checkMiner;


        // Server Assets

        private int size;

        private string[] currencyName =  new string[50];
        private float[] currencyPrice =  new float[50];
        private float[] currencyVolume = new float[50];
        private string[] currencyRank =   new string[50];
        private string[] currencyIcons = new string[50];

        // Buy from Server

        private decimal cashValue = 100;
        private decimal cryptoValue = 10;
        private string cryptoCurrencyName = "Bitcoin";
        private string temporaryCryptoName;
        private float temporaryCryptoPrice;

        // List of Window

        private List<Window> allWindows = new List<Window>();

        // Admin
        public static Window Logout_window;

        // Main Window
        public static Builder builder = new ();

        private bool isAscendingOrderPrice = true; // Flag to track sorting order
        private bool isAscendingOrderRank = true; // Flag to track sorting order
        private bool isAscendingOrderVolume = true; // Flag to track sorting order
        private bool isAscendingOrderName = true; // Flag to track sorting order

        private Window main_window;
        private Frame card;
        private Label balance_label_card_main_window;
        private Label user_email_card_main_window;
        private Label user_address_card_main_window;
        private Label username_bar_main_window;
        private Entry search_bar_main_window;
        private Box portfolio_items_box;


        private Button exchange_button_main_window;
        private EventBox rank_event_box;
        private EventBox price_event_box;
        private EventBox currency_name_event_box;
        private EventBox volume_box;
        private Button dashboard_button_main_window;
        private Button p2p_button_main_window;
        private Button portfolio_button_main_window;
        private Button transactions_button_main_window;
        private Button miner_button_main_window;
        private Button settings_button_main_window;
        private Button help_button_main_window;
        private Button logout_button_main_window;
        private Box market_values_box_main_window;
        private Button deposit_button_main_window;
        private Button withdraw_button_main_window;







        // Window 1
        private string loginEmail;
        private string loginPassword;

        private Window login_window1;
        private Entry email_entry_login_window1;
        private Entry password_entry_login_window1;
        private Button login_button_login_window1;
        private Button authorization_button_login_window1;
        private Button terms_condition_login_window1;

        // Window 2
        private string registerEmail;
        private string registerPassword;
        private string registerConfirmPassword;
        private string registerFullName;
        private string registerDateOfBirth;
        private string registerAddress;
        private string registerPhoneNumber;
        private string registerNationality;

        private Window login_window2;
        private Entry name_entry_login_window2;
        private Entry email_entry_login_window2;
        private Entry password_entry_login_window2;
        private Entry password_confirm_entry_login_window2;
        private Button back_button_login_window2;
        private Button submit_button_login_window2;
        private CheckButton agreement_login_window2;

        // Window 3
        private Window login_window3;
        private Entry phone_number_entry_login_window3;
        private Entry dob_entry_login_window3;
        private Entry address_entry_login_window3;
        private ComboBox combo_box_login_window3;
        private Entry combo_box_entry_login_window3;
        private Button submit_button_login_window3;
        private Button back_button_login_window3;

        //Window portfolio
        private Window portfolio_window;
        private Entry search_portfolio;
        private Button dashboard_button_portfolio_window;
        private Button p2p_button_portfolio_window;
        private Button portfolio_button_portfolio_window;
        private Button transactions_button_portfolio_window;
        private Button miner_button_portfolio_window;
        private Button settings_button_portfolio_window;
        private Button help_button_portfolio_window;
        private Button logout_button_portfolio_window;
        private Box portfolio_maxi_box;
        private Entry search_bar_portfolio_window;


        //Window transactions

        private Window transactions_window;

        private Box transactions_box;
        private Button dashboard_button_transaction_window;
        private Button p2p_button_transaction_window;
        private Button portfolio_button_transaction_window;
        private Button transactions_button_transaction_window;
        private Button miner_button_transaction_window;
        private Button settings_button_transaction_window;
        private Button help_button_transaction_window;
        private Button logout_button_transaction_window;
        private EventBox currency_name_event_transaction;
        private EventBox date_event_transaction;
        private EventBox value_event_transaction;
        private EventBox volume_event_transaction;
        private EventBox fee_event_transaction;
        private Entry search_bar_transaction_window;




        //Window P2P

        private Window p2p_window1;

        private Box p2p_list_box;

        private Button sell_button_p2p_window1;
        private Button buy_button_p2p_window1;

        private Button dashboard_button_p2p_window1;
        private Button p2p_button_p2p_window1;
        private Button portfolio_button_p2p_window1;
        private Button transactions_button_p2p_window1;
        private Button miner_button_p2p_window1;
        private Button settings_button_p2p_window1;
        private Button help_button_p2p_window1;
        private Button logout_button_p2p_window1;


        private Window p2p_window2;
        private ComboBox combo_box_p2p_window2;

        private Button sell_button_p2p_window2;
        private Button buy_button_p2p_window2;

        private Button sell_offer_button_p2p_window2;
        private Entry price_entry_p2p_window2;
        private Entry limit_entry_p2p_window2;


        private Button dashboard_button_p2p_window2;
        private Button p2p_button_p2p_window2;
        private Button portfolio_button_p2p_window2;
        private Button transactions_button_p2p_window2;
        private Button miner_button_p2p_window2;
        private Button settings_button_p2p_window2;
        private Button help_button_p2p_window2;
        private Button logout_button_p2p_window2;

        // Window Miner

        private Window miner_window;

        private bool turnedOn = false;
        private TextView cpu_text_view_miner_window;

        private Button dashboard_button_miner_window;
        private Button p2p_button_miner_window;
        private Button portfolio_button_miner_window;
        private Button transactions_button_miner_window;
        private Button miner_button_miner_window;
        private Button settings_button_miner_window;
        private Button help_button_miner_window;
        private Button logout_button_miner_window;
        private Button mining_button_miner_window;

        // Window Settings

        private Window settings_window;


        private Label email_settings_window;
        private Label phone_number_settings_window;

        private Button dashboard_button_settings_window;
        private Button p2p_button_settings_window;
        private Button portfolio_button_settings_window;
        private Button transactions_button_settings_window;
        private Button miner_button_settings_window;
        private Button settings_button_settings_window;
        private Button help_button_settings_window;
        private Button logout_button_settings_window;


        // Window Help

        private Window help_window;

        private Button dashboard_button_help_window;
        private Button p2p_button_help_window;
        private Button portfolio_button_help_window;
        private Button transactions_button_help_window;
        private Button miner_button_help_window;
        private Button settings_button_help_window;
        private Button help_button_help_window;
        private Button logout_button_help_window;

        // Window Logout

        private Window logout_window;

        private Button exit_button_logout_window;
        private Button cancel_button_logout_window;



        // Window p2p_buy_window

        private Window p2p_buy_window;
        private Button buy_button_p2p_buy_window;
        private Button cancel_button_p2p_buy_window;
        private Entry buy_entry_p2p_buy_window;
        private Label value_label_p2p_window;


        // Window p2p_sell_window

        private Window p2p_sell_window;
        private Label available_label_p2p_sell_window;
        private Label limit_label_p2p_sell_window;
        private Label price_label_p2p_sell_window;
        private Button sell_button_p2p_sell_window;
        private Button cancel_button_p2p_sell_window;


        // Window Deposit

        private Window deposit_window;
        private Entry deposit_entry_deposit_window;
        private Image qr_code;
        private Button cancel_button_deposit_window;
        private Button deposit_button_deposit_window;

        // Window Withdraw

        private Window withdraw_window;
        private Entry withdraw_entry_withdraw_window;
        private Button cancel_button_withdraw_window;
        private Entry card_number_entry_withdraw_window;
        private Button withdraw_button_withdraw_window;

        // Window Terms

        private Window terms_window;
        private Window policy_window;


        // Usernames

        private Label username_p2p_1_window;
        private Label username_p2p_2_window;
        private Label username_bar_portfolio_window;
        private Label username_bar_transaction_window;
        private Label username_bar_miner_window;
        private Label username_bar_settings_window;
        private Label username_settings_window;
        // ServerSocket functions

        private void startServerAndListenToIt(){
             // Connect to the C client (acting as a server)
            string cClientIpAddress = "127.0.0.1";
            int cClientPort = 8889;

            ConnectToCServer(cClientIpAddress, cClientPort);

            // Start a thread for listening to C server
            Thread listenerThread = new Thread(ListenToCServer);
            listenerThread.Start();
        }

        private void startServerOnThread(){
            Thread clientThread = new Thread(startServerAndListenToIt);
            clientThread.Start();
        }

        public static void SendMessage(NetworkStream stream, string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"Sent to C client: {message}");
        }
        public static void WaitForResponse(NetworkStream stream)
    {
        try
        {
            byte[] buffer = new byte[16384];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead <= 0)
            {
                Console.WriteLine("Connection closed by server.");
                Environment.Exit(0); // Exit the client if the server closes the connection
            }

            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Response from server: {response}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in waiting for response: {e}");
        }
    }

        private void updateWallet(){
            client = getClient();

            accountDetails.Purpose = "UpdateWallet";

            string serializedWallet = accountDetails.Serialize();

            SendMessage(client.GetStream(), serializedWallet);
            Console.WriteLine("Getting response");
            WaitForResponse(client.GetStream());
            Console.WriteLine("Got response");




        }

        private void deleteUserOffer(int offerId){

            UserOffer uOffer = new();

            uOffer.OfferId = offerId;
            uOffer.Purpose = "DeleteUserOffer";

            string serializedUserOffer = uOffer.Serialize();
            SendMessage(_stream, serializedUserOffer);
            WaitForResponse(_stream);
        }

        private int sendUserLoginDetails(){

            client = getClient();
            _stream = client.GetStream();



            user = SetUserLoginDetails(loginEmail, loginPassword);


            user.Purpose = "GetWallet";
            string serializedUser = user.Serialize();

            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedUser);

            // Wait for a response from the server
            Console.WriteLine("Getting response");


            string message = "";
            accountDetails =  WaitForWallet(client.GetStream(), out message);

            if(message == "OpenAdmin")
            {
                return 2;
            }
            Console.WriteLine("Got response");
            if (accountDetails is null){
                return 0;
            }
            requestShowServerAssets();
            requestUserPortfolio();
            return 1;
        }
        private void sendUserRegisterDetails(){

            client = getClient();



            user = SetUserRegsitrationDetails(  registerEmail,  registerPassword,  registerFullName,  registerDateOfBirth, registerAddress,  registerPhoneNumber,  registerNationality);
            user.Purpose = "Register";
            string serializedUser = user.Serialize();

            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedUser);

            // Wait for a response from the server
            Console.WriteLine("Getting response");

            WaitForResponse(client.GetStream());
            Console.WriteLine("Got response");
            login_window3.Hide();
            login_window1.ShowAll();
        }
        private void requestBlockChain(){

            client = getClient();

            string RequestMessage = "GetBlockchain";
            SendMessage(client.GetStream(), RequestMessage);
            WaitForBLockchain(client.GetStream());



        }

        private void requestShowServerAssets(){



            client = getClient();


            string RequestMessage = "GetServerAssetsList";

            // Send the serialized Transaction object to C client
            SendMessage(client.GetStream(), RequestMessage);

            // Wait for a response from the server
            size = WaitForServerAssets(client.GetStream(),  currencyName,  currencyPrice,  currencyVolume,  currencyRank,  currencyIcons);

            if (size == 0){

                Console.WriteLine("Size is 0");

                return;
            }

            FillMarketValuesMainWindow();
            requestBlockChain();


        }

        private void buyFromServer(){


            client = getClient();




            // Create a Transaction object and serialize it
            Transaction transaction = setTransaction("server", accountDetails.WalletAddress, cashValue, cryptoValue, cryptoCurrencyName);
            transaction.Purpose = "Publish";

            string serializedTransaction = transaction.Serialize();

            // Send the serialized Transaction object to C client
            SendMessage(client.GetStream(), serializedTransaction);

            // Wait for a response from the server
            WaitForResponse(client.GetStream());

        }

        private void buy_offer_p2p_window(string fromAddress,decimal cashValue,decimal cryptoValue, string cryptoCurrencyName, int offerId)
        {
            client = getClient();

            deleteUserOffer(offerId);


            // Create a Transaction object and serialize it
            Transaction transaction = setTransaction(fromAddress,accountDetails.WalletAddress, cashValue, cryptoValue, cryptoCurrencyName);
            transaction.Purpose = "Publish";

            string serializedTransaction = transaction.Serialize();

            // Send the serialized Transaction object to C client
            SendMessage(client.GetStream(), serializedTransaction);

            // Wait for a response from the server
            WaitForResponse(client.GetStream());
            requestUserOfferList();
        }

        private void requestUserPortfolio(){

            client = getClient();


            string serializedUser;

            User portfolioUser = GetUserPortfolioDetails(accountDetails.UserId);
            portfolioUser.Purpose = "GetPortfolio";
            serializedUser = portfolioUser.Serialize();

            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedUser);

            // Wait for a response from the server
            accountPortfolio = WaitForAccountPortfolio(client.GetStream());
            FillPortfolioBoxMainWindow();
            FillPortfolioWindow();
            requestBlockChain();

        }

        private void requestTransactionList(){

            client = getClient();

            Wallet userWallet = new();
            userWallet.WalletAddress = accountDetails.WalletAddress;
            userWallet.Purpose = "GetTransactionList";
            string serializedWallet = userWallet.Serialize();

            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedWallet);

            // Wait for a response from the server
            userTransactionsList = WaitForUserTransactionList(client.GetStream());


            // Add this data to the window

            FillTransactionWindow();
            SortFramesByDate();
            requestBlockChain();

        }

        private void publishUserOffer(decimal cashValue, decimal cryptoValue, string cryptoCurrencyName){

            client = getClient();

            userOffer = setUserOffer(accountDetails.WalletAddress, cashValue, cryptoValue, cryptoCurrencyName);
            userOffer.Purpose = "Publish";
            string serializedUserOffer = userOffer.Serialize();

            SendMessage(client.GetStream(), serializedUserOffer);

            WaitForResponse(client.GetStream());


        }

        private void requestUserOfferList(){

            client = getClient();

            string RequestMessage = "GetUserOffers";

            SendMessage(client.GetStream(), RequestMessage);

            userOffersList = WaitForUserOffers(client.GetStream());
            FillP2PWindow();
            requestBlockChain();

        }

        private bool checkForMiner(){

            client = getClient();

            miningUser = new();
            miningUser.Purpose = "MinerCheck";
            miningUser.UserId = accountDetails.UserId;

            string serializedUser = miningUser.Serialize();

            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedUser);

            // Wait for a response from the server
            checkMiner = WaitForMinerCheck(client.GetStream());
            return checkMiner;
        }

        private void sendMiningUserDetails(){

            client = getClient();

             // Create a User object and serialize it
            miningUser = new();
            miningUser.Purpose = "MinerRegister";
            miningUser.UserId = accountDetails.UserId;

            string serializedUser = miningUser.Serialize();


            // Send the serialized User object to C client
            SendMessage(client.GetStream(), serializedUser);

            // Wait for a response from the server
            WaitForResponse(client.GetStream());
        }




        public CCTPSApp()
        {

            // Lets connect to C server
            startServerOnThread();


            Application.Init();
            builder.AddFromFile("GUI/Glade/CCTPS.glade");


            // CSS Declaration

            var cssProvider = new CssProvider();
            cssProvider.LoadFromPath("GUI/CSS/styles.css");

            main_window = (Window)builder.GetObject("main_window");
            main_window.DefaultSize = new Gdk.Size(1440, 968);
            login_window1 = (Window)builder.GetObject("login_window1");
            login_window1.DefaultSize = new Gdk.Size(1440, 968);
            login_window2 = (Window)builder.GetObject("login_window2");
            login_window2.DefaultSize = new Gdk.Size(1440, 968);
            login_window3 = (Window)builder.GetObject("login_window3");
            login_window3.DefaultSize = new Gdk.Size(1440, 968);
            p2p_window1 = (Window)builder.GetObject("p2p_window1");
            p2p_window1.DefaultSize = new Gdk.Size(1440, 968);
            p2p_window2 = (Window)builder.GetObject("p2p_window2");
            p2p_window2.DefaultSize = new Gdk.Size(1440, 968);
            portfolio_window = (Window)builder.GetObject("portfolio_window");
            portfolio_window.DefaultSize = new Gdk.Size(1440, 968);
            transactions_window = (Window)builder.GetObject("transactions_window");
            transactions_window.DefaultSize = new Gdk.Size(1440, 968);
            miner_window = (Window)builder.GetObject("miner_window");
            miner_window.DefaultSize = new Gdk.Size(1440, 968);
            settings_window = (Window)builder.GetObject("settings_window");
            settings_window.DefaultSize = new Gdk.Size(1440, 968);
            help_window = (Window)builder.GetObject("help_window");
            help_window.DefaultSize = new Gdk.Size(1440, 968);
            logout_window = (Window)builder.GetObject("logout_window");
            logout_window.DefaultSize = new Gdk.Size(600, 200);
            p2p_sell_window = (Window)builder.GetObject("p2p_sell_window");
            p2p_sell_window.DefaultSize = new Gdk.Size(600, 200);
            p2p_buy_window = (Window)builder.GetObject("p2p_buy_window");
            p2p_buy_window.DefaultSize = new Gdk.Size(600, 200);
            deposit_window =  (Window)builder.GetObject("deposit_window");
            withdraw_window = (Window)builder.GetObject("withdraw_window");
            terms_window = (Window)builder.GetObject("terms_window");
            policy_window = (Window)builder.GetObject("policy_window");


            //lABELS
            username_p2p_1_window = (Label)builder.GetObject("username_p2p_1_window");
            username_p2p_2_window = (Label)builder.GetObject("username_p2p_2_window");
            username_bar_portfolio_window = (Label)builder.GetObject("username_bar_portfolio_window");
            username_bar_transaction_window = (Label)builder.GetObject("username_bar_transaction_window");
            username_bar_miner_window = (Label)builder.GetObject("username_bar_miner_window");
            username_bar_settings_window = (Label)builder.GetObject("username_bar_settings_window");
            username_settings_window = (Label)builder.GetObject("username_settings_window");

            // Retrieve objects from Glade for main_window
            card = (Frame)builder.GetObject("card");
            transactions_box = (Box)builder.GetObject("transactions_box");
            exchange_button_main_window = (Button)builder.GetObject("exchange_button_main_window");
            rank_event_box = (EventBox)builder.GetObject("rank_event");
            price_event_box = (EventBox)builder.GetObject("price_event");
            currency_name_event_box = (EventBox)builder.GetObject("currency_name_event");
            volume_box = (EventBox)builder.GetObject("volume_event");
            search_bar_main_window = (Entry)builder.GetObject("search_bar_main_window");

            // Card

            balance_label_card_main_window = (Label)builder.GetObject("balance_label_card_main_window");
            user_email_card_main_window = (Label)builder.GetObject("user_email_card_main_window");
            user_address_card_main_window = (Label)builder.GetObject("user_address_card_main_window");
            username_bar_main_window = (Label)builder.GetObject("username_bar_main_window");

            dashboard_button_main_window = (Button)builder.GetObject("dashboard_button_main_window");
            p2p_button_main_window = (Button)builder.GetObject("p2p_button_main_window");
            portfolio_button_main_window = (Button)builder.GetObject("portfolio_button_main_window");
            transactions_button_main_window = (Button)builder.GetObject("transactions_button_main_window");
            miner_button_main_window = (Button)builder.GetObject("miner_button_main_window");
            settings_button_main_window = (Button)builder.GetObject("settings_button_main_window");
            help_button_main_window = (Button)builder.GetObject("help_button_main_window");
            logout_button_main_window = (Button)builder.GetObject("logout_button_main_window");
            market_values_box_main_window = (Box)builder.GetObject("market_values_box_main_window");
            deposit_button_main_window = (Button)builder.GetObject("deposit_button_main_window");
            withdraw_button_main_window = (Button)builder.GetObject("withdraw_button_main_window");
            portfolio_items_box = (Box)builder.GetObject("portfolio_items_box");




            // Retrieve objects from Glade for login_window1
            email_entry_login_window1 = (Entry)builder.GetObject("email_entry_login_window1");
            password_entry_login_window1 = (Entry)builder.GetObject("password_entry_login_window1");
            login_button_login_window1 = (Button)builder.GetObject("login_button_login_window1");
            authorization_button_login_window1 = (Button)builder.GetObject("authorization_button_login_window1");
            terms_condition_login_window1 = (Button)builder.GetObject("terms_condition_login_window1");

            // Retrieve objects from Glade for login_window2
            name_entry_login_window2 = (Entry)builder.GetObject("name_entry_login_window2");
            email_entry_login_window2 = (Entry)builder.GetObject("email_entry_login_window2");
            password_entry_login_window2 = (Entry)builder.GetObject("password_entry_login_window2");
            password_confirm_entry_login_window2 = (Entry)builder.GetObject("password_confirm_entry_login_window2");
            agreement_login_window2 = (CheckButton)builder.GetObject("agreement_login_window2");
            submit_button_login_window2 = (Button)builder.GetObject("submit_button_login_window2");
            back_button_login_window2 = (Button)builder.GetObject("back_button_login_window2");

            // Window 3
            phone_number_entry_login_window3 = (Entry)builder.GetObject("phone_number_entry_login_window3");
            dob_entry_login_window3 = (Entry)builder.GetObject("dob_entry_login_window3");
            dob_entry_login_window3.MaxLength = 10;
            combo_box_login_window3 = (ComboBox)builder.GetObject("combo_box_login_window3");
            combo_box_entry_login_window3 = combo_box_login_window3.Child as Entry;

            address_entry_login_window3 = (Entry)builder.GetObject("address_entry_login_window3");
            submit_button_login_window3 = (Button)builder.GetObject("submit_button_login_window3");
            back_button_login_window3 = (Button)builder.GetObject("back_button_login_window3");


            //Window portfolio
            dashboard_button_portfolio_window = (Button)builder.GetObject("dashboard_button_portfolio_window");
            p2p_button_portfolio_window = (Button)builder.GetObject("p2p_button_portfolio_window");
            portfolio_button_portfolio_window = (Button)builder.GetObject("portfolio_button_portfolio_window");
            transactions_button_portfolio_window = (Button)builder.GetObject("transactions_button_portfolio_window");
            miner_button_portfolio_window = (Button)builder.GetObject("miner_button_portfolio_window");
            settings_button_portfolio_window = (Button)builder.GetObject("settings_button_portfolio_window");
            help_button_portfolio_window = (Button)builder.GetObject("help_button_portfolio_window");
            logout_button_portfolio_window = (Button)builder.GetObject("logout_button_portfolio_window");
            portfolio_maxi_box = (Box)builder.GetObject("portfolio_maxi_box");
            search_bar_portfolio_window =  (Entry)builder.GetObject("search_bar_portfolio_window");



            // Window transactions
            dashboard_button_transaction_window = (Button)builder.GetObject("dashboard_button_transaction_window");
            p2p_button_transaction_window = (Button)builder.GetObject("p2p_button_transaction_window");
            portfolio_button_transaction_window = (Button)builder.GetObject("portfolio_button_transaction_window");
            transactions_button_transaction_window = (Button)builder.GetObject("transactions_button_transaction_window");
            miner_button_transaction_window = (Button)builder.GetObject("miner_button_transaction_window");
            settings_button_transaction_window = (Button)builder.GetObject("settings_button_transaction_window");
            help_button_transaction_window = (Button)builder.GetObject("help_button_transaction_window");
            logout_button_transaction_window = (Button)builder.GetObject("logout_button_transaction_window");
            currency_name_event_transaction = (EventBox)builder.GetObject("currency_name_event_transaction");
            date_event_transaction = (EventBox)builder.GetObject("date_event_transaction");
            value_event_transaction = (EventBox)builder.GetObject("value_event_transaction");
            volume_event_transaction = (EventBox)builder.GetObject("volume_event_transaction");
            fee_event_transaction = (EventBox)builder.GetObject("fee_event_transaction");
            search_bar_transaction_window = (Entry)builder.GetObject("search_bar_transaction_window");
            search_bar_transaction_window.HasFocus = false;

            // Window p2p

            p2p_list_box = (Box)builder.GetObject("p2p_list_box");
            sell_button_p2p_window1 = (Button)builder.GetObject("sell_button_p2p_window1");
            buy_button_p2p_window1 = (Button)builder.GetObject("buy_button_p2p_window1");
            dashboard_button_p2p_window1 = (Button)builder.GetObject("dashboard_button_p2p_window1");
            p2p_button_p2p_window1 = (Button)builder.GetObject("p2p_button_p2p_window1");
            portfolio_button_p2p_window1 = (Button)builder.GetObject("portfolio_button_p2p_window1");
            transactions_button_p2p_window1 = (Button)builder.GetObject("transactions_button_p2p_window1");
            miner_button_p2p_window1 = (Button)builder.GetObject("miner_button_p2p_window1");
            settings_button_p2p_window1 = (Button)builder.GetObject("settings_button_p2p_window1");
            help_button_p2p_window1 = (Button)builder.GetObject("help_button_p2p_window1");
            logout_button_p2p_window1 = (Button)builder.GetObject("logout_button_p2p_window1");

             // Window p2p

            sell_offer_button_p2p_window2 = (Button)builder.GetObject("sell_offer_button_p2p_window2");
            price_entry_p2p_window2 = (Entry)builder.GetObject("price_entry_p2p_window2");
            limit_entry_p2p_window2 = (Entry)builder.GetObject("limit_entry_p2p_window2");

            sell_button_p2p_window2 = (Button)builder.GetObject("sell_button_p2p_window2");
            buy_button_p2p_window2 = (Button)builder.GetObject("buy_button_p2p_window2");
            combo_box_p2p_window2 = (ComboBox)builder.GetObject("combo_box_p2p_window2");
            dashboard_button_p2p_window2 = (Button)builder.GetObject("dashboard_button_p2p_window2");
            p2p_button_p2p_window2 = (Button)builder.GetObject("p2p_button_p2p_window2");
            portfolio_button_p2p_window2 = (Button)builder.GetObject("portfolio_button_p2p_window2");
            transactions_button_p2p_window2 = (Button)builder.GetObject("transactions_button_p2p_window2");
            miner_button_p2p_window2 = (Button)builder.GetObject("miner_button_p2p_window2");
            settings_button_p2p_window2 = (Button)builder.GetObject("settings_button_p2p_window2");
            help_button_p2p_window2 = (Button)builder.GetObject("help_button_p2p_window2");
            logout_button_p2p_window2 = (Button)builder.GetObject("logout_button_p2p_window2");

            // Window p2p buy

            buy_button_p2p_buy_window = (Button)builder.GetObject("buy_button_p2p_buy_window");
            cancel_button_p2p_buy_window = (Button)builder.GetObject("cancel_button_p2p_buy_window");
            buy_entry_p2p_buy_window = (Entry)builder.GetObject("buy_entry_p2p_buy_window");
            value_label_p2p_window = (Label)builder.GetObject("value_label_p2p_window");

            // Window p2p sell

            sell_button_p2p_sell_window = (Button)builder.GetObject("sell_button_p2p_sell_window");
            cancel_button_p2p_sell_window = (Button)builder.GetObject("cancel_button_p2p_sell_window");
            available_label_p2p_sell_window = (Label)builder.GetObject("available_label_p2p_sell_window");
            limit_label_p2p_sell_window = (Label)builder.GetObject("limit_label_p2p_sell_window");
            price_label_p2p_sell_window = (Label)builder.GetObject("price_label_p2p_sell_window");


            // Window miner

            cpu_text_view_miner_window = (TextView)builder.GetObject("cpu_text_view_miner_window");

            dashboard_button_miner_window = (Button)builder.GetObject("dashboard_button_miner_window");
            p2p_button_miner_window = (Button)builder.GetObject("p2p_button_miner_window");
            portfolio_button_miner_window = (Button)builder.GetObject("portfolio_button_miner_window");
            transactions_button_miner_window = (Button)builder.GetObject("transactions_button_miner_window");
            miner_button_miner_window = (Button)builder.GetObject("miner_button_miner_window");
            settings_button_miner_window = (Button)builder.GetObject("settings_button_miner_window");
            help_button_miner_window = (Button)builder.GetObject("help_button_miner_window");
            logout_button_miner_window = (Button)builder.GetObject("logout_button_miner_window");
            mining_button_miner_window = (Button)builder.GetObject("mining_button_miner_window");

            // Window settings

            dashboard_button_settings_window = (Button)builder.GetObject("dashboard_button_settings_window");
            p2p_button_settings_window = (Button)builder.GetObject("p2p_button_settings_window");
            portfolio_button_settings_window = (Button)builder.GetObject("portfolio_button_settings_window");
            transactions_button_settings_window = (Button)builder.GetObject("transactions_button_settings_window");
            miner_button_settings_window = (Button)builder.GetObject("miner_button_settings_window");
            settings_button_settings_window = (Button)builder.GetObject("settings_button_settings_window");
            help_button_settings_window = (Button)builder.GetObject("help_button_settings_window");
            logout_button_settings_window = (Button)builder.GetObject("logout_button_settings_window");

            // Window help

            dashboard_button_help_window = (Button)builder.GetObject("dashboard_button_help_window");
            p2p_button_help_window = (Button)builder.GetObject("p2p_button_help_window");
            portfolio_button_help_window = (Button)builder.GetObject("portfolio_button_help_window");
            transactions_button_help_window = (Button)builder.GetObject("transactions_button_help_window");
            miner_button_help_window = (Button)builder.GetObject("miner_button_help_window");
            settings_button_help_window = (Button)builder.GetObject("settings_button_help_window");
            help_button_help_window = (Button)builder.GetObject("help_button_help_window");
            logout_button_help_window = (Button)builder.GetObject("logout_button_help_window");
            transactions_box = (Box)builder.GetObject("transactions_box");

            // Window deposit
            deposit_entry_deposit_window = (Entry)builder.GetObject("deposit_entry_deposit_window");
            qr_code = (Image)builder.GetObject("qr_code");
            cancel_button_deposit_window = (Button)builder.GetObject("cancel_button_deposit_window");
            deposit_button_deposit_window = (Button)builder.GetObject("deposit_button_deposit_window");

            // Window Withdraw

            withdraw_entry_withdraw_window = (Entry)builder.GetObject("withdraw_entry_withdraw_window");
            cancel_button_withdraw_window = (Button)builder.GetObject("cancel_button_withdraw_window");
            card_number_entry_withdraw_window = (Entry)builder.GetObject("card_number_entry_withdraw_window");
            withdraw_button_withdraw_window = (Button)builder.GetObject("withdraw_button_withdraw_window");

            // Window Logout

            exit_button_logout_window = (Button)builder.GetObject("exit_button_logout_window");
            cancel_button_logout_window = (Button)builder.GetObject("cancel_button_logout_window");



            // Window main

            dashboard_button_main_window.Clicked += dashboard_button_main_window_clicked;
            p2p_button_main_window.Clicked += p2p_button_main_window_clicked;
            portfolio_button_main_window.Clicked += portfolio_button_main_window_clicked;
            transactions_button_main_window.Clicked += transactions_button_main_window_clicked;
            miner_button_main_window.Clicked += miner_button_main_window_clicked;
            settings_button_main_window.Clicked += settings_button_main_window_clicked;
            help_button_main_window.Clicked += help_button_main_window_clicked;
            logout_button_main_window.Clicked += logout_button_main_window_clicked;
            deposit_button_main_window.Clicked += deposit_button_main_window_clicked;
            withdraw_button_main_window.Clicked += withdraw_button_main_window_clicked;
            search_bar_main_window.Changed += search_bar_name_main_window;

            // Window portfolio

            dashboard_button_portfolio_window.Clicked += dashboard_button_portfolio_window_clicked;
            p2p_button_portfolio_window.Clicked += p2p_button_portfolio_window_clicked;
            portfolio_button_portfolio_window.Clicked += portfolio_button_portfolio_window_clicked;
            transactions_button_portfolio_window.Clicked += transactions_button_portfolio_window_clicked;
            miner_button_portfolio_window.Clicked += miner_button_portfolio_window_clicked;
            settings_button_portfolio_window.Clicked += settings_button_portfolio_window_clicked;
            help_button_portfolio_window.Clicked += help_button_portfolio_window_clicked;
            logout_button_portfolio_window.Clicked += logout_button_portfolio_window_clicked;
            search_bar_portfolio_window.Changed += search_bar_portfolio_window_changed;

            // Window p2p

            sell_button_p2p_window1.Clicked += sell_button_p2p_window1_clicked;
            buy_button_p2p_window1.Clicked += buy_button_p2p_window1_clicked;

            dashboard_button_p2p_window1.Clicked += dashboard_button_p2p_window1_clicked;
            p2p_button_p2p_window1.Clicked += p2p_button_p2p_window1_clicked;
            portfolio_button_p2p_window1.Clicked += portfolio_button_p2p_window1_clicked;
            transactions_button_p2p_window1.Clicked += transactions_button_p2p_window1_clicked;
            miner_button_p2p_window1.Clicked += miner_button_p2p_window1_clicked;
            settings_button_p2p_window1.Clicked += settings_button_p2p_window1_clicked;
            help_button_p2p_window1.Clicked += help_button_p2p_window1_clicked;
            logout_button_p2p_window1.Clicked += logout_button_p2p_window1_clicked;

            // Window p2p

            sell_button_p2p_window2.Clicked += sell_button_p2p_window2_clicked;
            buy_button_p2p_window2.Clicked += buy_button_p2p_window2_clicked;
            price_entry_p2p_window2.Changed += price_entry_p2p_window2_changed;
            limit_entry_p2p_window2.Changed += limit_entry_p2p_window2_changed;

            sell_offer_button_p2p_window2.Clicked += sell_offer_button_p2p_window2_clicked;
            dashboard_button_p2p_window2.Clicked += dashboard_button_p2p_window2_clicked;
            p2p_button_p2p_window2.Clicked += p2p_button_p2p_window2_clicked;
            portfolio_button_p2p_window2.Clicked += portfolio_button_p2p_window2_clicked;
            transactions_button_p2p_window2.Clicked += transactions_button_p2p_window2_clicked;
            miner_button_p2p_window2.Clicked += miner_button_p2p_window2_clicked;
            settings_button_p2p_window2.Clicked += settings_button_p2p_window2_clicked;
            help_button_p2p_window2.Clicked += help_button_p2p_window2_clicked;
            logout_button_p2p_window2.Clicked += logout_button_p2p_window2_clicked;

            // Window transaction

            dashboard_button_transaction_window.Clicked += dashboard_button_transaction_window_clicked;
            p2p_button_transaction_window.Clicked += p2p_button_transaction_window_clicked;
            portfolio_button_transaction_window.Clicked += portfolio_button_transaction_window_clicked;
            transactions_button_transaction_window.Clicked += transactions_button_transaction_window_clicked;
            miner_button_transaction_window.Clicked += miner_button_transaction_window_clicked;
            settings_button_transaction_window.Clicked += settings_button_transaction_window_clicked;
            help_button_transaction_window.Clicked += help_button_transaction_window_clicked;
            logout_button_transaction_window.Clicked += logout_button_transaction_window_clicked;
            currency_name_event_transaction.ButtonPressEvent += currency_name_event_transaction_clicked;
            date_event_transaction.ButtonPressEvent += date_event_transaction_clicked;
            value_event_transaction.ButtonPressEvent += value_event_transaction_clicked;
            volume_event_transaction.ButtonPressEvent += volume_event_transaction_clicked;
            search_bar_transaction_window.Changed += search_bar_transaction_window_changed;




            // Window settings

            dashboard_button_settings_window.Clicked += dashboard_button_settings_window_clicked;
            p2p_button_settings_window.Clicked += p2p_button_settings_window_clicked;
            portfolio_button_settings_window.Clicked += portfolio_button_settings_window_clicked;
            transactions_button_settings_window.Clicked += transactions_button_settings_window_clicked;
            miner_button_settings_window.Clicked += miner_button_settings_window_clicked;
            settings_button_settings_window.Clicked += settings_button_settings_window_clicked;
            help_button_settings_window.Clicked += help_button_settings_window_clicked;
            logout_button_settings_window.Clicked += logout_button_settings_window_clicked;

            // Window miner

            dashboard_button_miner_window.Clicked += dashboard_button_miner_window_clicked;
            p2p_button_miner_window.Clicked += p2p_button_miner_window_clicked;
            portfolio_button_miner_window.Clicked += portfolio_button_miner_window_clicked;
            transactions_button_miner_window.Clicked += transactions_button_miner_window_clicked;
            miner_button_miner_window.Clicked += miner_button_miner_window_clicked;
            settings_button_miner_window.Clicked += settings_button_miner_window_clicked;
            help_button_miner_window.Clicked += help_button_miner_window_clicked;
            logout_button_miner_window.Clicked += logout_button_miner_window_clicked;
            mining_button_miner_window.Clicked += mining_button_miner_window_clicked;

            // Window help

            dashboard_button_help_window.Clicked += dashboard_button_help_window_clicked;
            p2p_button_help_window.Clicked += p2p_button_help_window_clicked;
            portfolio_button_help_window.Clicked += portfolio_button_help_window_clicked;
            transactions_button_help_window.Clicked += transactions_button_help_window_clicked;
            miner_button_help_window.Clicked += miner_button_help_window_clicked;
            settings_button_help_window.Clicked += settings_button_help_window_clicked;
            help_button_help_window.Clicked += help_button_help_window_clicked;
            logout_button_help_window.Clicked += logout_button_help_window_clicked;

            // Window buy p2p

            buy_button_p2p_buy_window.Clicked += buy_button_p2p_buy_window_clicked;
            cancel_button_p2p_buy_window.Clicked += cancel_button_p2p_buy_window_clicked;
            buy_entry_p2p_buy_window.Changed += buy_entry_p2p_buy_window_changed;

            // Window sell p2p

            sell_button_p2p_sell_window.Clicked += sell_button_p2p_sell_window_clicked;
            cancel_button_p2p_sell_window.Clicked += cancel_button_p2p_sell_window_clicked;

            // Window deposit

            deposit_entry_deposit_window.Activated += deposit_entry_deposit_window_enter;
            deposit_entry_deposit_window.Changed += deposit_entry_deposit_window_changed;
            cancel_button_deposit_window.Clicked += cancel_button_deposit_window_clicked;
            deposit_button_deposit_window.Clicked += deposit_button_deposit_window_clicked;

            // Window withdraw

            cancel_button_withdraw_window.Clicked += cancel_button_withdraw_window_clicked;
            withdraw_button_withdraw_window.Clicked += withdraw_button_withdraw_window_clicked;
            withdraw_entry_withdraw_window.Changed += withdraw_entry_withdraw_window_changed;

            // Window Logout

            exit_button_logout_window.Clicked += exit_button_logout_window_clicked;
            cancel_button_logout_window.Clicked += cancel_button_logout_window_clicked;




            // Connect button click events for login_window1
            login_button_login_window1.Clicked += login_button_login_window1_clicked;
            authorization_button_login_window1.Clicked += authorization_button_login_window1_clicked;
            terms_condition_login_window1.Clicked += terms_condition_login_window1_clicked;

            // Connect button click events for login_window2
            submit_button_login_window2.Clicked += submit_button_login_window2_clicked;
            back_button_login_window2.Clicked += back_button_login_window2_clicked;
            submit_button_login_window3.Clicked += submit_button_login_window3_clicked;

            // Connect toggle event for agreement CheckButton
            agreement_login_window2.Toggled += agreement_login_window2_toggled;

            //Window 3
            back_button_login_window3.Clicked += back_button_login_window3_clicked;
            dob_entry_login_window3.Changed += OnEntryChanged;

            // Main window Sorting
            price_event_box.ButtonPressEvent += OnSortButtonPrice;
            rank_event_box.ButtonPressEvent += OnSortButtonRank;
            currency_name_event_box.ButtonPressEvent += OnSortCurrencyName;
            volume_box.ButtonPressEvent +=OnSortVolume;



            // CSS Button

            var terms_condition_login_window1_css = terms_condition_login_window1.StyleContext;
            terms_condition_login_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            terms_condition_login_window1_css.AddClass("terms-conditions");

            var login_button_login_window1_css = login_button_login_window1.StyleContext;
            login_button_login_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            login_button_login_window1_css.AddClass("login-button-login-window1");

            var authorization_button_login_window1_css = authorization_button_login_window1.StyleContext;
            authorization_button_login_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            authorization_button_login_window1_css.AddClass("authorization-button-login-window1");

            var submit_button_login_window2_css = submit_button_login_window2.StyleContext;
            submit_button_login_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            submit_button_login_window2_css.AddClass("submit-button-login-window2");

            var submit_button_login_window3_css = submit_button_login_window3.StyleContext;
            submit_button_login_window3_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            submit_button_login_window3_css.AddClass("submit-button-login-window3");

            var back_button_login_window3_css = back_button_login_window3.StyleContext;
            back_button_login_window3_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            back_button_login_window3_css.AddClass("back-button-login-window3");

            // Buttons main window css

            var dashboard_button_main_window_css = dashboard_button_main_window.StyleContext;
            dashboard_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_main_window_css.AddClass("navbar-button");

            var p2p_button_main_window_css = p2p_button_main_window.StyleContext;
            p2p_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_main_window_css.AddClass("navbar-button");

            var portfolio_button_main_window_css = portfolio_button_main_window.StyleContext;
            portfolio_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_main_window_css.AddClass("navbar-button");

            var transactions_button_main_window_css = transactions_button_main_window.StyleContext;
            transactions_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_main_window_css.AddClass("navbar-button");

            var miner_button_main_window_css = miner_button_main_window.StyleContext;
            miner_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_main_window_css.AddClass("navbar-button");

            var settings_button_main_window_css = settings_button_main_window.StyleContext;
            settings_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_main_window_css.AddClass("navbar-button");

            var help_button_main_window_css = help_button_main_window.StyleContext;
            help_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_main_window_css.AddClass("navbar-button");

            var logout_button_main_window_css = logout_button_main_window.StyleContext;
            logout_button_main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_main_window_css.AddClass("navbar-button");

            // Buttons portfolio window css

            var dashboard_button_portfolio_window_css = dashboard_button_portfolio_window.StyleContext;
            dashboard_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_portfolio_window_css.AddClass("navbar-button");

            var p2p_button_portfolio_window_css = p2p_button_portfolio_window.StyleContext;
            p2p_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_portfolio_window_css.AddClass("navbar-button");

            var portfolio_button_portfolio_window_css = portfolio_button_portfolio_window.StyleContext;
            portfolio_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_portfolio_window_css.AddClass("navbar-button");

            var transactions_button_portfolio_window_css = transactions_button_portfolio_window.StyleContext;
            transactions_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_portfolio_window_css.AddClass("navbar-button");

            var miner_button_portfolio_window_css = miner_button_portfolio_window.StyleContext;
            miner_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_portfolio_window_css.AddClass("navbar-button");

            var settings_button_portfolio_window_css = settings_button_portfolio_window.StyleContext;
            settings_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_portfolio_window_css.AddClass("navbar-button");

            var help_button_portfolio_window_css = help_button_portfolio_window.StyleContext;
            help_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_portfolio_window_css.AddClass("navbar-button");

            var logout_button_portfolio_window_css = logout_button_portfolio_window.StyleContext;
            logout_button_portfolio_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_portfolio_window_css.AddClass("navbar-button");

            // Buttons p2p window css

            var dashboard_button_p2p_window1_css = dashboard_button_p2p_window1.StyleContext;
            dashboard_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_p2p_window1_css.AddClass("navbar-button");

            var p2p_button_p2p_window1_css = p2p_button_p2p_window1.StyleContext;
            p2p_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_p2p_window1_css.AddClass("navbar-button");

            var portfolio_button_p2p_window1_css = portfolio_button_p2p_window1.StyleContext;
            portfolio_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_p2p_window1_css.AddClass("navbar-button");

            var transactions_button_p2p_window1_css = transactions_button_p2p_window1.StyleContext;
            transactions_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_p2p_window1_css.AddClass("navbar-button");

            var miner_button_p2p_window1_css = miner_button_p2p_window1.StyleContext;
            miner_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_p2p_window1_css.AddClass("navbar-button");

            var settings_button_p2p_window1_css = settings_button_p2p_window1.StyleContext;
            settings_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_p2p_window1_css.AddClass("navbar-button");

            var help_button_p2p_window1_css = help_button_p2p_window1.StyleContext;
            help_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_p2p_window1_css.AddClass("navbar-button");

            var logout_button_p2p_window1_css = logout_button_p2p_window1.StyleContext;
            logout_button_p2p_window1_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_p2p_window1_css.AddClass("navbar-button");

            // Buttons p2p window css

            var dashboard_button_p2p_window2_css = dashboard_button_p2p_window2.StyleContext;
            dashboard_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_p2p_window2_css.AddClass("navbar-button");

            var p2p_button_p2p_window2_css = p2p_button_p2p_window2.StyleContext;
            p2p_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_p2p_window2_css.AddClass("navbar-button");

            var portfolio_button_p2p_window2_css = portfolio_button_p2p_window2.StyleContext;
            portfolio_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_p2p_window2_css.AddClass("navbar-button");

            var transactions_button_p2p_window2_css = transactions_button_p2p_window2.StyleContext;
            transactions_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_p2p_window2_css.AddClass("navbar-button");

            var miner_button_p2p_window2_css = miner_button_p2p_window2.StyleContext;
            miner_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_p2p_window2_css.AddClass("navbar-button");

            var settings_button_p2p_window2_css = settings_button_p2p_window2.StyleContext;
            settings_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_p2p_window2_css.AddClass("navbar-button");

            var help_button_p2p_window2_css = help_button_p2p_window2.StyleContext;
            help_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_p2p_window2_css.AddClass("navbar-button");

            var logout_button_p2p_window2_css = logout_button_p2p_window2.StyleContext;
            logout_button_p2p_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_p2p_window2_css.AddClass("navbar-button");

            // Buttons transaction window css

            var dashboard_button_transaction_window_css = dashboard_button_transaction_window.StyleContext;
            dashboard_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_transaction_window_css.AddClass("navbar-button");

            var p2p_button_transaction_window_css = p2p_button_transaction_window.StyleContext;
            p2p_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_transaction_window_css.AddClass("navbar-button");

            var portfolio_button_transaction_window_css = portfolio_button_transaction_window.StyleContext;
            portfolio_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_transaction_window_css.AddClass("navbar-button");

            var transactions_button_transaction_window_css = transactions_button_transaction_window.StyleContext;
            transactions_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_transaction_window_css.AddClass("navbar-button");

            var miner_button_transaction_window_css = miner_button_transaction_window.StyleContext;
            miner_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_transaction_window_css.AddClass("navbar-button");

            var settings_button_transaction_window_css = settings_button_transaction_window.StyleContext;
            settings_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_transaction_window_css.AddClass("navbar-button");

            var help_button_transaction_window_css = help_button_transaction_window.StyleContext;
            help_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_transaction_window_css.AddClass("navbar-button");

            var logout_button_transaction_window_css = logout_button_transaction_window.StyleContext;
            logout_button_transaction_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_transaction_window_css.AddClass("navbar-button");

            // Buttons miner window css

            var dashboard_button_miner_window_css = dashboard_button_miner_window.StyleContext;
            dashboard_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_miner_window_css.AddClass("navbar-button");

            var p2p_button_miner_window_css = p2p_button_miner_window.StyleContext;
            p2p_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_miner_window_css.AddClass("navbar-button");

            var portfolio_button_miner_window_css = portfolio_button_miner_window.StyleContext;
            portfolio_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_miner_window_css.AddClass("navbar-button");

            var transactions_button_miner_window_css = transactions_button_miner_window.StyleContext;
            transactions_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_miner_window_css.AddClass("navbar-button");

            var miner_button_miner_window_css = miner_button_miner_window.StyleContext;
            miner_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_miner_window_css.AddClass("navbar-button");

            var settings_button_miner_window_css = settings_button_miner_window.StyleContext;
            settings_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_miner_window_css.AddClass("navbar-button");

            var help_button_miner_window_css = help_button_miner_window.StyleContext;
            help_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_miner_window_css.AddClass("navbar-button");

            var logout_button_miner_window_css = logout_button_miner_window.StyleContext;
            logout_button_miner_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_miner_window_css.AddClass("navbar-button");

            // Buttons settings window css

            var dashboard_button_settings_window_css = dashboard_button_settings_window.StyleContext;
            dashboard_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_settings_window_css.AddClass("navbar-button");

            var p2p_button_settings_window_css = p2p_button_settings_window.StyleContext;
            p2p_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_settings_window_css.AddClass("navbar-button");

            var portfolio_button_settings_window_css = portfolio_button_settings_window.StyleContext;
            portfolio_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_settings_window_css.AddClass("navbar-button");

            var transactions_button_settings_window_css = transactions_button_settings_window.StyleContext;
            transactions_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_settings_window_css.AddClass("navbar-button");

            var miner_button_settings_window_css = miner_button_settings_window.StyleContext;
            miner_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_settings_window_css.AddClass("navbar-button");

            var settings_button_settings_window_css = settings_button_settings_window.StyleContext;
            settings_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_settings_window_css.AddClass("navbar-button");

            var help_button_settings_window_css = help_button_settings_window.StyleContext;
            help_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_settings_window_css.AddClass("navbar-button");

            var logout_button_settings_window_css = logout_button_settings_window.StyleContext;
            logout_button_settings_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_settings_window_css.AddClass("navbar-button");

            // Buttons help window css

            var dashboard_button_help_window_css = dashboard_button_help_window.StyleContext;
            dashboard_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            dashboard_button_help_window_css.AddClass("navbar-button");

            var p2p_button_help_window_css = p2p_button_help_window.StyleContext;
            p2p_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            p2p_button_help_window_css.AddClass("navbar-button");

            var portfolio_button_help_window_css = portfolio_button_help_window.StyleContext;
            portfolio_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            portfolio_button_help_window_css.AddClass("navbar-button");

            var transactions_button_help_window_css = transactions_button_help_window.StyleContext;
            transactions_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            transactions_button_help_window_css.AddClass("navbar-button");

            var miner_button_help_window_css = miner_button_help_window.StyleContext;
            miner_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            miner_button_help_window_css.AddClass("navbar-button");

            var settings_button_help_window_css = settings_button_help_window.StyleContext;
            settings_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            settings_button_help_window_css.AddClass("navbar-button");

            var help_button_help_window_css = help_button_help_window.StyleContext;
            help_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            help_button_help_window_css.AddClass("navbar-button");

            var logout_button_help_window_css = logout_button_help_window.StyleContext;
            logout_button_help_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            logout_button_help_window_css.AddClass("navbar-button");


            // CSS Entries

            var name_entry_login_window2_css = name_entry_login_window2.StyleContext;
            name_entry_login_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            name_entry_login_window2_css.AddClass("name-entry-login-window2");

            var email_entry_login_window2_css = email_entry_login_window2.StyleContext;
            email_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            email_entry_login_window2_css.AddClass("email-entry-login-window2");

            var password_entry_login_window2_css = password_entry_login_window2.StyleContext;
            password_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            password_entry_login_window2_css.AddClass("password-entry-login-window2");

            var password_confirm_entry_login_window2_css = password_confirm_entry_login_window2.StyleContext;
            password_confirm_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            password_confirm_entry_login_window2_css.AddClass("password-confirm-entry-login-window2");

            var email_entry_login_window1_css = email_entry_login_window1.StyleContext;
            email_entry_login_window1_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            email_entry_login_window1_css.AddClass("email-entry-login-window1");

            var password_entry_login_window1_css = password_entry_login_window1.StyleContext;
            password_entry_login_window1_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            password_entry_login_window1_css.AddClass("password-entry-login-window1");

            var phone_number_entry_login_window3_css = phone_number_entry_login_window3.StyleContext;
            phone_number_entry_login_window3_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            phone_number_entry_login_window3_css.AddClass("phone-number-entry-login-window3");

            var dob_entry_login_window3_css = dob_entry_login_window3.StyleContext;
            dob_entry_login_window3_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            dob_entry_login_window3_css.AddClass("dob-entry-login-window3");

            var combo_box_entry_login_window3_css = combo_box_entry_login_window3.StyleContext;
            combo_box_entry_login_window3_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            combo_box_entry_login_window3_css.AddClass("combo-box-entry-login-window3");

            var address_entry_login_window3_css = address_entry_login_window3.StyleContext;
            address_entry_login_window3_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            address_entry_login_window3_css.AddClass("address-entry-login-window3");



            // CSS Card

            var card_css = card.StyleContext;
            card_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            card_css.AddClass("card");

            var main_window_css = main_window.StyleContext;
            main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            main_window_css.AddClass("main-window");



            main_window.DeleteEvent += delegate { Application.Quit(); };
            login_window1.DeleteEvent += delegate { Application.Quit(); };
            login_window2.DeleteEvent += delegate { Application.Quit(); };
            login_window3.DeleteEvent += delegate { Application.Quit(); };
            p2p_window1.DeleteEvent += delegate { Application.Quit(); };
            p2p_window2.DeleteEvent += delegate { Application.Quit(); };
            portfolio_window.DeleteEvent += delegate { Application.Quit(); };
            transactions_window.DeleteEvent += delegate { Application.Quit(); };
            miner_window.DeleteEvent += delegate { Application.Quit(); };
            settings_window.DeleteEvent += delegate { Application.Quit(); };
            help_window.DeleteEvent += delegate { Application.Quit(); };


            allWindows.Add(main_window);
            allWindows.Add(login_window1);
            allWindows.Add(login_window2);
            allWindows.Add(login_window3);
            allWindows.Add(p2p_window1);
            allWindows.Add(p2p_window2);
            allWindows.Add(portfolio_window);
            allWindows.Add(transactions_window);
            allWindows.Add(miner_window);
            allWindows.Add(settings_window);
            allWindows.Add(help_window);
            allWindows.Add(deposit_window);
            allWindows.Add(withdraw_window);
            allWindows.Add(logout_window);





            login_window1.ShowAll();


            Application.Run();
        }


// Main Window



    private void deposit_button_main_window_clicked (object sender, EventArgs e){

        deposit_window.ShowAll();
        qr_code.Visible = false;
    }
    private void withdraw_button_main_window_clicked (object sender, EventArgs e){
        withdraw_window.ShowAll();
    }
    private void cancel_button_deposit_window_clicked (object sender, EventArgs e){
        deposit_window.Hide();
        deposit_entry_deposit_window.Text = "";
        qr_code.Visible = false;
    }
    private void cancel_button_withdraw_window_clicked (object sender, EventArgs e){
        withdraw_window.Hide();
        withdraw_entry_withdraw_window.Text ="";
        card_number_entry_withdraw_window.Text ="";
    }


    private void deposit_entry_deposit_window_changed(object sender, EventArgs args){
        string entryText = deposit_entry_deposit_window.Text;
        string filteredText = FilterNonInteger(entryText);
        deposit_entry_deposit_window.Text = filteredText;
    }

    private void deposit_entry_deposit_window_enter(object sender, EventArgs args)
    {

        if (deposit_entry_deposit_window.Text != "")
        {
            qr_code.Visible = true;
        }

    }
    private void deposit_button_deposit_window_clicked (object sender, EventArgs e){

        if (decimal.TryParse(deposit_entry_deposit_window.Text, out decimal price))
        {
            accountDetails.Balance += price;
        }
        updateWallet();
        fill_card_details();
    }

    private void withdraw_entry_withdraw_window_changed(object sender, EventArgs args){
        string entryText = withdraw_entry_withdraw_window.Text;
        string filteredText = FilterNonInteger(entryText);
        withdraw_entry_withdraw_window.Text = filteredText;
    }

    private void withdraw_button_withdraw_window_clicked (object sender, EventArgs e){

        if (decimal.TryParse(withdraw_entry_withdraw_window.Text, out decimal price))
        {
            accountDetails.Balance -= price;
        }
        updateWallet();
        fill_card_details();
    }

    private void terms_condition_login_window1_clicked(object sender, EventArgs e){
        terms_window.ShowAll();
    }



    private void buy_button_p2p_buy_window_clicked (object sender, EventArgs e) {


        cryptoCurrencyName = temporaryCryptoName;

        buyFromServer();


        p2p_buy_window.Hide();
        buy_entry_p2p_buy_window.Text = "";
        value_label_p2p_window.Text = "0";


    }

    private void buy_entry_p2p_buy_window_changed(object sender, EventArgs args)
    {
        string entryText = buy_entry_p2p_buy_window.Text;
        string filteredText = FilterNonInteger(entryText);
        buy_entry_p2p_buy_window.Text = filteredText;

        // Update the label or perform any computation with the filtered integer
        if (decimal.TryParse(filteredText, out decimal intValue))
        {
            decimal intPriceValue = (decimal)temporaryCryptoPrice;
            cryptoValue = intValue;
            cashValue = intValue * intPriceValue;
            value_label_p2p_window.Text = $"{temporaryCryptoName} price : {cashValue}";
        }
        else{
            value_label_p2p_window.Text = $"{temporaryCryptoName} price : 0";
        }
    }

    private string FilterNonInteger(string input)
    {
        string filtered = string.Concat(input.Where(c => char.IsDigit(c) || char.IsPunctuation(c) && c != '.'));
        return filtered;
    }




    private void cancel_button_p2p_buy_window_clicked (object sender, EventArgs e) {
        p2p_buy_window.Hide();
        buy_entry_p2p_buy_window.Text = "";
        value_label_p2p_window.Text = $"{temporaryCryptoName} price : 0";

    }

    private void sell_button_p2p_sell_window_clicked (object sender, EventArgs e){

    }
    private void cancel_button_p2p_sell_window_clicked (object sender, EventArgs e){
        p2p_sell_window.Hide();

    }
    private void deleteChildren(Box fieldBox){
        foreach (var child in fieldBox.Children.ToList())
        {
            fieldBox.Remove(child);
        }
    }

    private void price_entry_p2p_window2_changed(object sender, EventArgs args){
        string cashEntry = price_entry_p2p_window2.Text;
        string cashEntryFiltered = FilterNonInteger(cashEntry);
        price_entry_p2p_window2.Text = cashEntryFiltered;


    }
    private void limit_entry_p2p_window2_changed(object sender, EventArgs args){
        string cryptoEntry = limit_entry_p2p_window2.Text;
        string cryptoEntryFiltered = FilterNonInteger(cryptoEntry);
        limit_entry_p2p_window2.Text = cryptoEntryFiltered;
    }

    private void sell_offer_button_p2p_window2_clicked(object sender, EventArgs e){

        TreeIter iter;
        string comboBoxValue = "";
        if (combo_box_p2p_window2.GetActiveIter(out iter))
        {
            comboBoxValue = combo_box_p2p_window2.Model.GetValue(iter, 0).ToString();
            // Do something with activeText
        }

        int index = comboBoxValue.IndexOf(':');

        string cryptoCurrencyName = index >= 0 ? comboBoxValue.Substring(0, index) : comboBoxValue;


        decimal cashValue = decimal.Parse(price_entry_p2p_window2.Text);
        decimal cryptoValue = decimal.Parse(limit_entry_p2p_window2.Text);
        publishUserOffer(cashValue, cryptoValue,cryptoCurrencyName);
    }




    private void fillComboBoxP2pWindow2(){
        // Create a ListStore to hold the data for the ComboBox
        ListStore listStore = new ListStore(typeof(string));
        var userDataDictionary = accountPortfolio.GetUserPortfolioAsDictionary();
        var sortedByValue = userDataDictionary.OrderBy(x => x.Value);


        List<KeyValuePair<string, decimal>> sortedList = sortedByValue.ToList();


        foreach (var kvp in sortedList)
        {

           if (kvp.Key == "UserId"){
                continue;
            }
            if (( kvp.Value) > 0 ){
                    listStore.AppendValues($"{kvp.Key}: {kvp.Value}");
            }



        }


        // Assign the model to the ComboBox
        combo_box_p2p_window2.Model = listStore;

        // Set up how the ComboBox should display the items
        combo_box_p2p_window2.EntryTextColumn = 0;



    }

     private void fill_card_details(){

        string user_address_card = ($"{accountDetails.WalletAddress}").Substring(0, 7);
        balance_label_card_main_window.Text = $"{accountDetails.Balance}";
        user_email_card_main_window.Text = $"{user.Email} ";
        username_bar_main_window.Text = $"{user.Email}";
        user_address_card_main_window.Text = $"{user_address_card}";
        username_p2p_1_window.Text = $"{user.Email}";
        username_p2p_2_window.Text = $"{user.Email}";
        username_bar_portfolio_window.Text = $"{user.Email}";
        username_bar_transaction_window.Text = $"{user.Email}";
        username_bar_miner_window.Text = $"{user.Email}";
        username_bar_settings_window.Text = $"{user.Email}";
        username_settings_window.Text = $"{user.Email}";
    }




    // Sorting and adding ;
    //---------Add Market Values in Main---------------
    	// index counter
    private void exchange_button_main_window_clicked(string frameCurrencyName, float frameCurrencyPrice){

        temporaryCryptoName = frameCurrencyName;
        temporaryCryptoPrice = frameCurrencyPrice;

        buy_entry_p2p_buy_window.Text = "";
        value_label_p2p_window.Text = $"{temporaryCryptoName} price : 0";


        p2p_buy_window.ShowAll();

    }


    private void FillMarketValuesMainWindow(){
        for(int i = 0; i < size; i++){
            AddFrameToMarketValuesMainWindow(i);
        }
    }


    private void AddFrameToMarketValuesMainWindow(int index)
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

        Label currencyPriceLabel = new Label("$" + currencyPrice[index].ToString());
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

        Label currencyVolumeLabel = new Label(currencyVolume[index].ToString());
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

        Label currencyRankLabel = new Label(currencyRank[index]);
        currencyRankLabel.Name = $"CurrencyRank_{index}";
        currencyRankLabel.Visible = true;
        currencyRankLabel.CanFocus = false;
        //currencyRankLabel.MarginBottom = 9;
        //currencyRankLabel.Halign = Align.End;

        // inner frame for rank
        Frame rankFrame= new Frame("");
        rankFrame.ShadowType = ShadowType.None;
        rankFrame.Add(currencyRankLabel);

        innerGrid.Attach(rankFrame, 3, 0, 1, 1);

        // Exchange Button
        Button exchangeButton = new Button("Exchange");
        exchangeButton.Name = $"ExchangeButton_{index}";

        //exchangeButton.MarginBottom = 9;
        //exchangeButton.MarginRight = 10;
        //exchangeButton.Halign = Align.End;

        // inner frame for echange
        Frame echangeFrame= new Frame("");
        echangeFrame.ShadowType = ShadowType.None;
        echangeFrame.Add(exchangeButton);

        innerGrid.Attach(echangeFrame, 4, 0, 1, 1);

        // Connect button click events for main_window
        exchangeButton.Clicked += (sender, args) => exchange_button_main_window_clicked(currencyName[index], currencyPrice[index]);




        // Add the inner grid to the alignment
        alignment.Add(innerGrid);

        // Add the alignment to the frame
        currencyFrame.Add(alignment);

        // Align Frame

        currencyFrame.MarginEnd = 20;



        // Add the frame to the transactions_box
        market_values_box_main_window.Add(currencyFrame);
        market_values_box_main_window.ShowAll();
    }

    // -------------------- Add Mini PortFolio --------------------

    private void FillPortfolioBoxMainWindow(){

        deleteChildren(portfolio_items_box);


        var userDataDictionary = accountPortfolio.GetUserPortfolioAsDictionary();
        var sortedByValue = userDataDictionary.OrderByDescending(x => x.Value);


        List<KeyValuePair<string, decimal>> sortedList = sortedByValue.ToList();


        int limit = 0;

        foreach (var kvp in sortedList)
        {

            if (kvp.Key == "UserId"){
                continue;
            }
            AddFrameToPortfolioBoxMainWindow(kvp.Key, kvp.Value);

            limit++;
            if (limit == 5){
                break;
            }


        }

    }

    private void AddFrameToPortfolioBoxMainWindow(string userPortfolioCurrencyName, decimal userPortfolioCurrencyPrice)
    {
        Frame portfolioItemFrame = new Frame("");
        portfolioItemFrame.ShadowType = ShadowType.None;
        Frame currencyFrame = new Frame("");

        currencyFrame.Visible = true;
        currencyFrame.CanFocus = false;
        currencyFrame.LabelXalign = 0;
        currencyFrame.ShadowType = ShadowType.None;

        Alignment alignment = new Alignment(0, 0, 0, 0);
        alignment.Visible = true;
        alignment.CanFocus = false;

        Grid portfolioItemGrid = new Grid();
        portfolioItemGrid.Visible = false;
        portfolioItemGrid.CanFocus = false;
        portfolioItemGrid.RowHomogeneous = true;
        portfolioItemGrid.ColumnHomogeneous = true;

        Grid currencyInfoGrid = new Grid();
        currencyInfoGrid.Visible = true;
        currencyInfoGrid.CanFocus = false;
        currencyInfoGrid.RowSpacing = 0;
        currencyInfoGrid.RowHomogeneous = true;
        currencyInfoGrid.ColumnHomogeneous = true;

        Image currencyIconImage = new Image($"GUI/Glade/images/icons/{userPortfolioCurrencyName}.png");
        currencyIconImage.Visible = true;
        currencyIconImage.CanFocus = false;

        Label currencyNameLabel = new Label($"{userPortfolioCurrencyName}");
        currencyNameLabel.Name = $"CurrencyName_{userPortfolioCurrencyName}";
        currencyNameLabel.Visible = true;
        currencyNameLabel.CanFocus = false;
        currencyNameLabel.Halign = Align.Start;
        currencyNameLabel.Valign = Align.Center;

        Frame currencyNameFrame = new Frame("");
        currencyNameFrame.ShadowType = ShadowType.None;
        currencyNameFrame.Add(currencyNameLabel);
        currencyNameFrame.SetSizeRequest(100, -1);

        currencyInfoGrid.Attach(currencyIconImage, 0, 0, 1, 2);
        currencyInfoGrid.Attach(currencyNameFrame, 1, 0, 1, 1);


        int index = Array.IndexOf(currencyName, userPortfolioCurrencyName);

        decimal priceValue = (decimal)currencyPrice[index];
        decimal volumeValue = (decimal) userPortfolioCurrencyPrice;

        decimal priceOfCurrency = priceValue * volumeValue;
        Label currencyPriceLabel = new Label($"${priceOfCurrency }");
        currencyPriceLabel.Name = $"CurrencyPrice_{userPortfolioCurrencyName}";
        currencyPriceLabel.Visible = true;
        currencyPriceLabel.CanFocus = false;
        currencyPriceLabel.Halign = Align.Start;
        currencyPriceLabel.Valign = Align.Center;

        Frame priceFrame = new Frame("");
        priceFrame.ShadowType = ShadowType.None;
        priceFrame.Add(currencyPriceLabel);
        priceFrame.SetSizeRequest(100,-1);

        currencyInfoGrid.Attach(priceFrame, 1, 1, 1, 1);

        Label currencyVolumeLabel = new Label($"{userPortfolioCurrencyPrice}");
        currencyVolumeLabel.Name = $"CurrencyVolume_{userPortfolioCurrencyName}";
        currencyVolumeLabel.Visible = true;
        currencyVolumeLabel.CanFocus = false;

        Frame volumeFrame = new Frame("");
        volumeFrame.ShadowType = ShadowType.None;
        volumeFrame.Add(currencyVolumeLabel);
        volumeFrame.SetSizeRequest(100,-1);

        currencyInfoGrid.Attach(volumeFrame, 2, 0, 1, 1);

        alignment.Add(currencyInfoGrid);
        portfolioItemFrame.Add(alignment);

        portfolio_items_box.Add(portfolioItemFrame);
        portfolio_items_box.ShowAll();
    }



//----------------Sort with Labels---------------------------




// -NAME-
    private void OnSortCurrencyName(object sender, ButtonPressEventArgs args)
    {
        // Handle the click event
        //var label = (Label)((EventBox)sender).Child;
        //label.Text = "Clicked!";
        isAscendingOrderName = !isAscendingOrderName;
        SortFramesByCurrencyName();
    }

    private void SortFramesByCurrencyName()
    {
        // Get all child frames in market_values_box_main_window
        var frames = market_values_box_main_window.Children
            .OfType<Frame>()
            .ToList();

        // Sort frames based on the currency name
        frames.Sort((frame1, frame2) =>
        {
            // Extract currency names from the frames
            string currencyName1 = GetCurrencyNameFromFrame(frame1);
            string currencyName2 = GetCurrencyNameFromFrame(frame2);

            // Compare currency names
            int result = currencyName1.CompareTo(currencyName2);
            return isAscendingOrderName? result: -result;
        });

        // Clear existing components in the box
        foreach (var child in market_values_box_main_window.Children.ToList())
        {
            market_values_box_main_window.Remove(child);
        }

        // Add the frames back to the box in the sorted order
        foreach (var frame in frames)
        {
            market_values_box_main_window.Add(frame);
        }

        // Show all widgets in the box
        market_values_box_main_window.ShowAll();
    }

    private string GetCurrencyNameFromFrame(Frame frame)
    {
        // Assuming the currencyNameLabel is nested within another container within the frame
        Container container = frame.Children.FirstOrDefault() as Container;

        if (container != null)
        {
            Label currencyNameLabel = FindCurrencyNameLabelInGrid(container);
            //Console.WriteLine(currencyNameLabel);

            // Return the text of the label, or a default value if not found
            return currencyNameLabel?.Text ?? "Unknown";
        }

        // No suitable container found in the children
        return "Unknown";
    }

    // Helper method to find a Label widget for currency name in the children of a container
    private Label FindCurrencyNameLabelInGrid(Container container)
    {
        foreach (var child in container.Children)
        {
            if (child is Label label && IsCurrencyNameLabel(label))
            {
                // Found the label representing the currency name
                return label;
            }

            // If the child is a container, recursively search for a currency name label
            if (child is Container subContainer)
            {
                var currencyNameLabelInChildren = FindCurrencyNameLabelInGrid(subContainer);
                if (currencyNameLabelInChildren != null)
                {
                    // Found a currency name label in a sub-container
                    return currencyNameLabelInChildren;
                }
            }

            // You might need to handle other widget types based on your actual hierarchy
        }

        // No currency name label found in the children
        return null;
    }

    private bool IsCurrencyNameLabel(Label label)
    {
        // Example: Check if the label's Name property matches a specific identifier
        return label.Name.StartsWith("CurrencyName_");
    }

    //------------------

    // -PRICE-
    private void OnSortButtonPrice(object sender, ButtonPressEventArgs args)
    {
        // Handle the click event
        var label = (Label)((EventBox)sender).Child;
        //label.Text = "Clicked!";

        // Toggle the sorting order
        isAscendingOrderPrice = !isAscendingOrderPrice;

        // Handle the click event and sort frames by price
        SortFramesByPrice();
    }

    private void SortFramesByPrice()
    {
        // Get all child frames in market_values_box_main_window
        var frames = market_values_box_main_window.Children
            .OfType<Frame>()
            .ToList();

        // Sort frames based on the price label
        frames.Sort((frame1, frame2) =>
        {
            // Extract price labels from the frames
            string priceLabel1 = GetPriceLabelFromFrame(frame1);
            string priceLabel2 = GetPriceLabelFromFrame(frame2);

            // Convert price labels to integers for comparison
            decimal price1 = ExtractPriceFromLabel(priceLabel1);
            decimal price2 = ExtractPriceFromLabel(priceLabel2);

            // Compare prices
            //return price1.CompareTo(price2);
            // Compare price values based on the sorting order
            int result = price1.CompareTo(price2);
            return isAscendingOrderPrice ? result : -result; // Reverse order if descending

        });

        // Clear existing components in the box
        foreach (var child in market_values_box_main_window.Children.ToList())
        {
            market_values_box_main_window.Remove(child);
        }

        // Add the frames back to the box in the sorted order
        foreach (var frame in frames)
        {
            market_values_box_main_window.Add(frame);
        }

        // Show all widgets in the box
        market_values_box_main_window.ShowAll();
    }

    private string GetPriceLabelFromFrame(Frame frame)
    {
        // Assuming the currencyPriceLabel is nested within another container within the frame
        Container container = frame.Children.FirstOrDefault() as Container;

        if (container != null)
        {
            Label currencyPriceLabel = FindPriceLabelInGrid(container);
            //Console.WriteLine(currencyPriceLabel);

            // Return the text of the label, or a default value if not found
            return currencyPriceLabel?.Text ?? "$0.00"; // Default to "$0.00" if label not found
        }

        // No suitable container found in the children
        return "$0.00"; // Default to "$0.00" if container not found
    }

    // Helper method to find a Label widget in the children of a container
    private Label FindPriceLabelInGrid(Container container)
    {
        foreach (var child in container.Children)
        {
            if (child is Label label && IsPriceLabel(label))
            {
                // Found the label representing the price
                return label;
            }

            // If the child is a container, recursively search for a price label
            if (child is Container subContainer)
            {
                var priceLabelInChildren = FindPriceLabelInGrid(subContainer);
                if (priceLabelInChildren != null)
                {
                    // Found a price label in a sub-container
                    return priceLabelInChildren;
                }
            }

            // You might need to handle other widget types based on your actual hierarchy
        }

        // No price label found in the children
        return null;
    }

    private bool IsPriceLabel(Label label)
    {
        // Add your criteria to identify the price label
        return label.Text.StartsWith("$"); // Example: Price label starts with "$"
    }

    private decimal ExtractPriceFromLabel(string priceLabel)
    {
        // Assuming your price label is a string representation of a decimal
        if (decimal.TryParse(priceLabel.TrimStart('$'), out decimal price))
        {
            return price;
        }
        return 0.00m; // Default to 0.00 if parsing fails
    }

    //---


    // -RANK- (Should start # at start)
    private void OnSortButtonRank(object sender, ButtonPressEventArgs args){
        // Handle the click event
            var label = (Label)((EventBox)sender).Child;
            //label.Text = "Clicked!";

            // Toggle the sorting order
        isAscendingOrderRank = !isAscendingOrderRank;

        SortFramesByRank();
    }

    private void SortFramesByRank()
    {
        // Get all child frames in market_values_box_main_window
    var frames = market_values_box_main_window.Children
        .OfType<Frame>()  // Use OfType<T> for better readability
        .ToList();

    // Sort frames based on the rank label
    frames.Sort((frame1, frame2) =>
    {
        // Extract rank labels from the frames
        string rankLabel1 = GetRankLabelFromFrame(frame1);
        string rankLabel2 = GetRankLabelFromFrame(frame2);

        // Convert rank labels to integers for comparison
        int rank1 = ExtractRankFromLabel(rankLabel1);
        int rank2 = ExtractRankFromLabel(rankLabel2);

        // Compare ranks
        int result = rank1.CompareTo(rank2);
        return isAscendingOrderRank ? result : -result;
    });

    // Clear existing components in the box
    foreach (var child in market_values_box_main_window.Children.ToList())
    {
        market_values_box_main_window.Remove(child);
    }

    // Add the frames back to the box in the sorted order
    foreach (var frame in frames)
    {
        market_values_box_main_window.Add(frame);
    }

    // Show all widgets in the box
    market_values_box_main_window.ShowAll();

    }

    private string GetRankLabelFromFrame(Frame frame)
    {
        // Assuming the currencyRankLabel is nested within another container within the frame
        Container container = frame.Children.FirstOrDefault() as Container;

        if (container != null)
        {
            Label currencyRankLabel = FindRankLabelInGrid(container);
            //Console.WriteLine(currencyRankLabel);

            // Return the text of the label, or a default value if not found
            return currencyRankLabel?.Text ?? "#0";
        }

        // No suitable container found in the children
        return "#0";
    }


    // Helper method to find a Label widget in the children of a container
    private Label FindRankLabelInGrid(Container container)
    {
        foreach (var child in container.Children)
        {
            if (child is Label label && IsRankLabel(label))
            {
                // Found the label representing the rank
                return label;
            }

            // If the child is a container, recursively search for a rank label
            if (child is Container subContainer)
            {
                var rankLabelInChildren = FindRankLabelInGrid(subContainer);
                if (rankLabelInChildren != null)
                {
                    // Found a rank label in a sub-container
                    return rankLabelInChildren;
                }
            }

            // You might need to handle other widget types based on your actual hierarchy
        }

        // No rank label found in the children
        return null;
    }

    private bool IsRankLabel(Label label)
    {
        // Add your criteria to identify the rank label
        return label.Text.StartsWith("#"); // Assuming rank labels start with "#"
    }

    private int ExtractRankFromLabel(string rankLabel)
    {
        // Assuming your rank label is in the format "#X"
        if (rankLabel.StartsWith("#") && int.TryParse(rankLabel.Substring(1), out int rank))
        {
            return rank;
        }
        return 0; // Default to 0 if parsing fails
    }


    // -VOLUME-

    // Event handler for the sort button click
    private void OnSortVolume(object sender, ButtonPressEventArgs args){
        // Handle the click event
            var label = (Label)((EventBox)sender).Child;
            //label.Text = "Clicked!";

            // Toggle the sorting order
        isAscendingOrderVolume = !isAscendingOrderVolume;

        SortFramesByVolume();
    }

    private void SortFramesByVolume()
    {
        // Get all child frames in market_values_box_main_window
    var frames = market_values_box_main_window.Children
        .OfType<Frame>()  // Use OfType<T> for better readability
        .ToList();

    // Sort frames based on the rank label
    frames.Sort((frame1, frame2) =>
    {
        // Extract rank labels from the frames
        string volumeLabel1 = GetVolumeLabelFromFrame(frame1);
        string volumeLabel2 = GetVolumeLabelFromFrame(frame2);

        // Convert rank labels to integers for comparison
        int volume1 = ExtractVolumeFromLabel(volumeLabel1);
        //Console.WriteLine(volume1);
        int volume2 = ExtractVolumeFromLabel(volumeLabel2);
        //Console.WriteLine(volume2);

        // Compare ranks
        int result = volume1.CompareTo(volume2);
        return isAscendingOrderVolume ? result : -result;
    });

    // Clear existing components in the box
    foreach (var child in market_values_box_main_window.Children.ToList())
    {
        market_values_box_main_window.Remove(child);
    }

    // Add the frames back to the box in the sorted order
    foreach (var frame in frames)
    {
        market_values_box_main_window.Add(frame);
    }

    // Show all widgets in the box
    market_values_box_main_window.ShowAll();

    }

    private string GetVolumeLabelFromFrame(Frame frame)
    {
        // Assuming the currencyRankLabel is nested within another container within the frame
        Container container = frame.Children.FirstOrDefault() as Container;

        if (container != null)
        {
            Label currencyVolumeLabel = FindVolumeLabelInGrid(container);
            //Console.WriteLine(currencyRankLabel);

            // Return the text of the label, or a default value if not found
            return currencyVolumeLabel?.Text ?? "0";
        }

        // No suitable container found in the children
        return "0";
    }


    // Helper method to find a Label widget in the children of a container
    private Label FindVolumeLabelInGrid(Container container)
    {
        foreach (var child in container.Children)
        {
            if (child is Label label && IsVolumeLabel(label))
            {
                // Found the label representing the volume
                return label;
            }

            // If the child is a container, recursively search for a volume label
            if (child is Container subContainer)
            {
                var volumeLabelInChildren = FindVolumeLabelInGrid(subContainer);
                if (volumeLabelInChildren != null)
                {
                    // Found a rank label in a sub-container
                    return volumeLabelInChildren;
                }
            }

            // You might need to handle other widget types based on your actual hierarchy
        }

        // No rank label found in the children
        return null;
    }

    private bool IsVolumeLabel(Label label)
    {
        // Add your criteria to identify the rank label
        return label.Name.StartsWith("CurrencyCryptoValue_"); // Assuming rank labels start with "#"
    }

    private int ExtractVolumeFromLabel(string volume)
    {
        // Implement your logic to extract the numeric value from the volume string
        // Example: Assume the numeric value is the first part of the string
        if (int.TryParse(volume.Split(' ')[0], out int numericValue))
        {

            return numericValue;
        }

        return 0; // Default to 0 if parsing fails
    }
    //---

//------------------------------------------------------------------------------------------

// ------------------ Search Name ------------------

    private void search_bar_name_main_window(object sender, EventArgs e)
        {

            if (sender is Entry entry)
            {
                string searchTerm = entry.Text.Trim();



                // Check if the search term is not empty before searching
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    SearchRows(searchTerm);
                }
                else
                {
                  FillMarketValuesMainWindow();
                }


            }
        }

    private void SearchRows(string searchTerm)
    {

        searchTerm = searchTerm.ToLower();


        int index = 0;
        bool found =  false;
        for (int i = 0; i < size; i++)
            {
                if (currencyName[i].ToLower() == searchTerm)
                {
                    index = i; // Return the index if the name is found
                    found = true;

                }
            }

        if (found){
            // Clear existing components in the box
        foreach (var child in market_values_box_main_window.Children.ToList())
        {
            market_values_box_main_window.Remove(child);

        }
        AddFrameToMarketValuesMainWindow(index);
        }
        else{


        }
    }


// ------------------------------------------------

// ----------------Transactions Sort ----------------------------------------------
// -NAME-
    private void currency_name_event_transaction_clicked(object sender, ButtonPressEventArgs args)
    {
        // Handle the click event
        isAscendingOrderName = !isAscendingOrderName;
        SortFramesByCurrencyNameTransaction();
    }

    private void SortFramesByCurrencyNameTransaction()
    {
        // Get all child frames in transactions_box
        var frames = transactions_box.Children
            .OfType<Frame>()
            .ToList();

        // Sort frames based on the currency name
        frames.Sort((frame1, frame2) =>
        {
            // Extract currency names from the frames
            string currencyName1 = GetCurrencyNameFromFrame(frame1);
            string currencyName2 = GetCurrencyNameFromFrame(frame2);

            // Compare currency names
            int result = currencyName1.CompareTo(currencyName2);
            return isAscendingOrderName? result: -result;
        });

        // Clear existing components in the box
        foreach (var child in transactions_box.Children.ToList())
        {
            transactions_box.Remove(child);
        }

        // Add the frames back to the box in the sorted order
        foreach (var frame in frames)
        {
            transactions_box.Add(frame);
        }

        // Show all widgets in the box
        transactions_box.ShowAll();
    }
    //------------------

// -Date-
    private bool isAscendingOrderDate = false;
    private void date_event_transaction_clicked(object sender, ButtonPressEventArgs args){
        // Handle the click event
            var label = (Label)((EventBox)sender).Child;
            //label.Text = "Clicked!";

            // Toggle the sorting order
        isAscendingOrderDate = !isAscendingOrderDate;

        SortFramesByDate();
    }

    private void SortFramesByDate()
    {
        // Get all child frames in transactions_box
    var frames = transactions_box.Children
        .OfType<Frame>()  // Use OfType<T> for better readability
        .ToList();

    // Sort frames based on the Date label
    frames.Sort((frame1, frame2) =>
    {
        // Extract Date labels from the frames
        string DateLabel1 = GetDateLabelFromFrame(frame1);
        string DateLabel2 = GetDateLabelFromFrame(frame2);

        // Convert Date labels to integers for comparison
        long Date1 = ExtractDateFromLabel(DateLabel1);
        long Date2 = ExtractDateFromLabel(DateLabel2);

        // Compare Dates
        int result = Date1.CompareTo(Date2);
        return isAscendingOrderDate ? result : -result;
    });

    // Clear existing components in the box
    foreach (var child in transactions_box.Children.ToList())
    {
        transactions_box.Remove(child);
    }

    // Add the frames back to the box in the sorted order
    foreach (var frame in frames)
    {
        transactions_box.Add(frame);
    }

    // Show all widgets in the box
    transactions_box.ShowAll();

    }

    private string GetDateLabelFromFrame(Frame frame)
    {
        // Assuming the currencyDateLabel is nested within another container within the frame
        Container container = frame.Children.FirstOrDefault() as Container;

        if (container != null)
        {
            Label currencyDateLabel = FindDateLabelInGrid(container);
            //Console.WriteLine(currencyDateLabel);

            // Return the text of the label, or a default value if not found
            return currencyDateLabel?.Text ?? "0000-00-00";;
        }

        // No suitable container found in the children
        return "0000-00-00";
    }
    // Helper method to find a Label widget in the children of a container
    private Label FindDateLabelInGrid(Container container)
    {
        foreach (var child in container.Children)
        {
            if (child is Label label && IsDateLabel(label))
            {
                // Found the label representing the Date
                return label;
            }

            // If the child is a container, recursively search for a Date label
            if (child is Container subContainer)
            {
                var DateLabelInChildren = FindDateLabelInGrid(subContainer);
                if (DateLabelInChildren != null)
                {
                    // Found a Date label in a sub-container
                    return DateLabelInChildren;
                }
            }

            // You might need to handle other widget types based on your actual hierarchy
        }

        // No Date label found in the children
        return null;
    }

    private bool IsDateLabel(Label label)
    {
        // Example: Check if the label's Name property matches a specific identifier
        return label.Name.StartsWith("CurrencyDateTime_");
    }

    private long ExtractDateFromLabel(string DateLabel)
    {
        string digitsOnly = new string(DateLabel.Where(char.IsDigit).ToArray());
        digitsOnly = digitsOnly.Trim();
        //Console.WriteLine($"{digitsOnly}");
        string paddedString = digitsOnly.PadRight(14, '0').Substring(0, 14);
        Console.WriteLine($"{paddedString}");
        if (long.TryParse(paddedString, out long result))
        {
       // Console.WriteLine($"Resulting Integer2: {result}");
            return result;
        }
        return 0; // Default to 0 if parsing fails
    }
    //------------------

// -VALUE-
    private void value_event_transaction_clicked(object sender, ButtonPressEventArgs args)
    {
        // Handle the click event
        var label = (Label)((EventBox)sender).Child;
        //label.Text = "Clicked!";

        // Toggle the sorting order
        isAscendingOrderPrice = !isAscendingOrderPrice;

        // Handle the click event and sort frames by price
        SortFramesByValue();
    }

    private void SortFramesByValue()
    {
        // Get all child frames in transactions_box
        var frames = transactions_box.Children
            .OfType<Frame>()
            .ToList();

        // Sort frames based on the price label
        frames.Sort((frame1, frame2) =>
        {
            // Extract price labels from the frames
            string priceLabel1 = GetPriceLabelFromFrame(frame1);
            string priceLabel2 = GetPriceLabelFromFrame(frame2);

            // Convert price labels to integers for comparison
            decimal price1 = ExtractPriceFromLabel(priceLabel1);
            decimal price2 = ExtractPriceFromLabel(priceLabel2);

            // Compare prices
            //return price1.CompareTo(price2);
            // Compare price values based on the sorting order
            int result = price1.CompareTo(price2);
            return isAscendingOrderPrice ? result : -result; // Reverse order if descending

        });

        // Clear existing components in the box
        foreach (var child in transactions_box.Children.ToList())
        {
            transactions_box.Remove(child);
        }

        // Add the frames back to the box in the sorted order
        foreach (var frame in frames)
        {
            transactions_box.Add(frame);
        }

        // Show all widgets in the box
        transactions_box.ShowAll();
    }
    // -------------

// -VOLUME-

    private void volume_event_transaction_clicked(object sender, ButtonPressEventArgs args){
        // Handle the click event
            var label = (Label)((EventBox)sender).Child;
            //label.Text = "Clicked!";

            // Toggle the sorting order
        isAscendingOrderVolume = !isAscendingOrderVolume;

        SortFramesByVolumeTransaction();
    }

    private void SortFramesByVolumeTransaction()
    {
        // Get all child frames in transactions_box
    var frames = transactions_box.Children
        .OfType<Frame>()  // Use OfType<T> for better readability
        .ToList();

    // Sort frames based on the rank label
    frames.Sort((frame1, frame2) =>
    {
        // Extract rank labels from the frames
        string volumeLabel1 = GetVolumeLabelFromFrame(frame1);
        string volumeLabel2 = GetVolumeLabelFromFrame(frame2);

        // Convert rank labels to integers for comparison
        int volume1 = ExtractVolumeFromLabel(volumeLabel1);
        //Console.WriteLine(volume1);
        int volume2 = ExtractVolumeFromLabel(volumeLabel2);
        //Console.WriteLine(volume2);

        // Compare ranks
        int result = volume1.CompareTo(volume2);
        return isAscendingOrderVolume ? result : -result;
    });

    // Clear existing components in the box
    foreach (var child in transactions_box.Children.ToList())
    {
        transactions_box.Remove(child);
    }

    // Add the frames back to the box in the sorted order
    foreach (var frame in frames)
    {
        transactions_box.Add(frame);
    }

    // Show all widgets in the box
    transactions_box.ShowAll();

    }
    // ------------





//-------------------------------------------------------

// ---------- SEAКCH IN TRANSACTION -----------

    // Search function to filter rows based on the search criteria
    private void search_bar_transaction_window_changed(object sender, EventArgs e)
    {

        if (sender is Entry entry)
        {
            string searchTerm = entry.Text.Trim();

            // Check if the search term is not empty before searching
            if (!string.IsNullOrEmpty(searchTerm))
            {
                SearchRowsTransaction(searchTerm);
            }
            else
            {
                // Handle the case when the search term is empty
                FillTransactionWindow();
            }
        }
    }


    private void SearchRowsTransaction(string searchTerm)
    {
        foreach (var child in transactions_box.Children.ToList())
        {
            transactions_box.Remove(child);
        }
        searchTerm= searchTerm.ToLower();
        searchTerm = searchTerm.ToLower();

        if (char.IsDigit(searchTerm[0]))
        {
            if (searchTerm.Contains('/'))
            {
                // Split the string into parts based on the '/' symbol
                string[] DateParts = searchTerm.Split('/');
                long date1 = ExtractDateFromLabel(DateParts[0]);
                long date2 = ExtractDateFromLabel(DateParts[1]);

                int index = 0;
                bool found = false;
                for (int i = 0; i < userTransactionsList.Count; i++)
                {
                    DateTime thisDateTime = userTransactionsList[i].DateTime;
                    string readyDateTime = thisDateTime.ToString("yyyy-MM-dd HH:mm:ss");


                   if (ExtractDateFromLabel(readyDateTime) >= date1 &&
                        ExtractDateFromLabel(readyDateTime) <= date2)
                    {

                        index = i; // Return the index if the name is found
                        found = true;
                    }
                    if (found)
                    {
                        // Clear existing components in the box
                        AddFrameToTransactionWindow(index);
                    }
                    else
                    {
                        // Additional actions if not found
                    }
                    found = false;
                }
            }
            else
            {
                long date1 = ExtractDateFromLabel(searchTerm);
                int index = 0;
                bool found = false;
                for (int i = 0; i < userTransactionsList.Count; i++)
                {
                    DateTime thisDateTime = userTransactionsList[i].DateTime;
                    string readyDateTime = thisDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    if (ExtractDateFromLabel(readyDateTime) >= date1)
                    {

                        index = i; // Return the index if the name is found
                        found = true;
                    }
                    if (found)
                    {
                        // Clear existing components in the box
                        AddFrameToTransactionWindow(index);
                    }
                    else
                    {
                        // Additional actions if not found
                    }
                    found = false;
                }
            }
        }
        else
        {
            List<string> searchparts = new List<string>();
            int indexOfSpace = searchTerm.IndexOf(' ');
            if (indexOfSpace != -1)
            {
                searchparts.Add(searchTerm.Substring(0, indexOfSpace));
                searchparts.Add(searchTerm.Substring(indexOfSpace + 1));
            };
            Console.WriteLine($"{searchparts.Count}");

            if (searchparts.Count < 2)
            {
                //Console.WriteLine($"{searchparts.Count} ");
                int index = 0;
                bool found = false;
                for (int i = 0; i < userTransactionsList.Count; i++)
                {
                    if ((userTransactionsList[i].CryptocurrencyName).ToLower() == searchTerm)
                    {
                        index = i; // Return the index if the name is found
                        found = true;
                    }
                    if (found)
                    {
                        // Clear existing components in the box
                        AddFrameToTransactionWindow(index);
                    }
                    else
                    {
                        // Additional actions if not found
                    }
                    found = false;
                }
            }
            else if (searchparts.Count == 2)
            {
                if (searchparts[1].Contains('/'))
                {
                    // Split the string into parts based on the '/' symbol
                    string[] DateParts = searchparts[1].Split('/');
                    long date1 = ExtractDateFromLabel(DateParts[0]);
                    long date2 = ExtractDateFromLabel(DateParts[1]);

                    int index = 0;
                    bool found = false;
                    for (int i = 0; i < userTransactionsList.Count; i++)
                    {
                        DateTime thisDateTime = userTransactionsList[i].DateTime;
                        string readyDateTime = thisDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        if ((userTransactionsList[i].CryptocurrencyName).ToLower() == searchparts[0] && ExtractDateFromLabel(readyDateTime) >= date1 && ExtractDateFromLabel(readyDateTime) <= date2)
                        {
                            //Console.WriteLine($"It Contains and {currencyDate[i]} > {date1}");
                            //Console.WriteLine($"It Contains and {currencyDate[i]} < {date2}");
                            index = i; // Return the index if the name is found
                            found = true;
                        }
                        if (found)
                        {
                            // Clear existing components in the box
                            AddFrameToTransactionWindow(index);
                        }
                        else
                        {
                            // Additional actions if not found
                        }
                        found = false;
                    }
                }
                else
                {
                    long date1 = ExtractDateFromLabel(searchparts[1]);

                    int index = 0;
                    bool found = false;
                    for (int i = 0; i < userTransactionsList.Count; i++)
                    {
                        DateTime thisDateTime = userTransactionsList[i].DateTime;
                        string readyDateTime = thisDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        if ((userTransactionsList[i].CryptocurrencyName).ToLower() == searchparts[0] && ExtractDateFromLabel(readyDateTime) >= date1)
                        {

                            index = i; // Return the index if the name is found
                            found = true;
                        }
                        if (found)
                        {
                            // Clear existing components in the box
                            AddFrameToTransactionWindow(index);
                        }
                        else
                        {
                            // Additional actions if not found
                        }
                        found = false;
                    }
                }
            }
        }
    }
//---------------------------------------
/// -------------------Search in portfolio-----------------

// ------------------ Search Name ------------------

    private void search_bar_portfolio_window_changed(object sender, EventArgs e)
        {

            if (sender is Entry entry)
            {
                string searchTerm = entry.Text.Trim();



                // Check if the search term is not empty before searching
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    SearchRowsPortfolio(searchTerm);
                }
                else
                {
                FillPortfolioWindow();
                }


            }
        }

    private void SearchRowsPortfolio(string searchTerm)
    {

        searchTerm = searchTerm.ToLower();


        int index = 0;
        bool found =  false;
        for (int i = 0; i < size; i++)
            {
                if (currencyName[i].ToLower() == searchTerm)
                {
                    index = i; // Return the index if the name is found
                    found = true;

                }
            }

        if (found){
            // Clear existing components in the box
        foreach (var child in portfolio_maxi_box.Children.ToList())
        {
            portfolio_maxi_box.Remove(child);

        }
       // AddFrameToPortfolioWindow(index);
        }
        else{


        }
    }



    //-------------------------------------




// Window 1

        private void login_button_login_window1_clicked(object sender, EventArgs e)
        {
            loginEmail = email_entry_login_window1.Text;
            loginPassword = password_entry_login_window1.Text;


            if (IsValidEmail(loginEmail))
            {
                int access;

                Console.WriteLine($"Login successful. Email: {loginEmail}, Password: {loginPassword}");
                  // Close login_window1


                access = sendUserLoginDetails();

                if (access == 0){
                    showErrorAllert("Incorrect Email or Password");
                    return;
                }else if ( access == 2){

                    Logout_window = (Window)builder.GetObject("Logout_window");

                    login_window1.Hide();
                    Admin_main_window.ShowAll();
                    return;
                }
                login_window1.Hide();

                main_window.ShowAll();

                //requestTransactionList();

                fill_card_details();
                if (!checkForMiner()){
                    sendMiningUserDetails();
                }




            }
            else
            {
                Console.WriteLine("Invalid email address.");
                showErrorAllert("Invalid email address");
            }
        }

        private void authorization_button_login_window1_clicked(object sender, EventArgs e)
        {

            Console.WriteLine("Submit button clicked.");

            // Close login_window1
            login_window1.Hide();

            // Show login_window2
            login_window2.ShowAll();
        }

// Window 2

        private void submit_button_login_window2_clicked(object sender, EventArgs e)
        {
            // Check if the agreement CheckButton is checked
            if (agreement_login_window2.Active)
            {
                registerFullName= name_entry_login_window2.Text;
                registerEmail = email_entry_login_window2.Text;
                registerPassword = password_entry_login_window2.Text;
                registerConfirmPassword = password_confirm_entry_login_window2.Text;
    // Validate email
                if (!IsValidEmail(registerEmail))
                {
                    Console.WriteLine("Invalid email address.");
                    showErrorAllert("Invalid email address.");
                    // Optionally, you can show an error message or take other actions
                    return;
                }

                // Validate password
                if (registerPassword.Length < 7)
                {
                    Console.WriteLine("Password must be at least 7 characters long.");
                    showErrorAllert("Password less than 7 digits");
                    return;
                }
                else if (PasswordContainsDigitAndUppercase(registerPassword) == 1)
                {
                    Console.WriteLine("Password must be  contain at least one digit and one uppercase character.");
                    showErrorAllert("Password has no digits");
                    // Optionally, you can show an error message or take other actions
                    return;
                }
                else if (PasswordContainsDigitAndUppercase(registerPassword) == 2)
                {
                    Console.WriteLine("Password must be one uppercase character.");
                    showErrorAllert("Password has no Uppercase");
                    return;
                }
                else if (registerPassword != registerConfirmPassword){

                    Console.WriteLine("Passwords does not match");
                    showErrorAllert("Passwords does not match");
                    return;
                }

                // Optionally, you can close login_window2 or perform other actions
                login_window2.Hide();
                login_window3.ShowAll();
            }
            else
            {
                Console.WriteLine("Please agree to the terms before submitting.");
                showErrorAllert("Please agree to terms");

                // Optionally, you can show an error message or take other actions
            }
        }
        private bool IsValidEmail(string email)
        {
            // You can implement your email validation logic here
            return email.Contains('@');
        }

        private int PasswordContainsDigitAndUppercase(string password)
        {
            // Check if the password contains at least one digit and one uppercase character
            if (!password.Any(char.IsDigit))
            {
                return 1;
            }
            else if (!password.Any(char.IsUpper)){
                return 2;
            };
            return 0;
        }

        private void back_button_login_window2_clicked(object sender, EventArgs e)
        {
            // Add your logic for going back from login_window2 to login_window1

            // Optionally, you can close login_window2 or perform other actions
            login_window2.Hide();
            login_window1.ShowAll();
        }
        private void agreement_login_window2_toggled(object sender, EventArgs e)
        {
            // Add your logic for handling the agreement CheckButton state change
            bool isChecked = agreement_login_window2.Active;

            // Optionally, you can perform actions based on whether the CheckButton is checked or unchecked
            Console.WriteLine($"Agreement CheckButton state changed: {isChecked}");
        }

// Window 3

        private void submit_button_login_window3_clicked(object sender, EventArgs e)
        {
            registerPhoneNumber = phone_number_entry_login_window3.Text;
            registerDateOfBirth = dob_entry_login_window3.Text;
            registerAddress = address_entry_login_window3.Text;
            registerNationality = combo_box_entry_login_window3.Text;

            // Validate phone number (only digits allowed)
            if (!IsValidPhoneNumber(registerPhoneNumber))
            {
                Console.WriteLine("Invalid phone number. Please enter only digits.");
                showErrorAllert("Invalid phone number");
                // Optionally, you can show an error message or take other actions
                return;
            }

            // Validate date of birth (in the format 00/00/0000)
            if (!IsValidDateOfBirth(registerDateOfBirth))
            {
                Console.WriteLine("Invalid date of birth. Please enter the date in the format 00/00/0000.");
                showErrorAllert("Invalid Data of Birth");
                // Optionally, you can show an error message or take other actions
                return;
            }


            // Optionally, you can perform other actions, for example, submit the data or close the window
            Console.WriteLine($"Submitted data: Phone Number: {registerPhoneNumber}, Date of Birth: {registerDateOfBirth}, Address: {registerAddress}, ComboBox Value: {registerNationality}");
            sendUserRegisterDetails();

        }

        private static void OnEntryChanged(object sender, EventArgs args)
        {
            if (sender is Entry entry)
            {


                if ((entry.Text).Length == 2){


                    entry.Text += "/" ;
                    entry.Position += 1;
                }
                else if  ((entry.Text).Length == 5){
                    entry.Text += "/";
                    entry.Position += 1;

                }
                entry.Position = -1;



            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Check if the phone number contains only digits
            return phoneNumber.All(char.IsDigit);
        }

        private bool IsValidDateOfBirth(string dob)
        {
            // Validate date of birth format (00/00/0000)
            if (DateTime.TryParseExact(dob, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                return true;
            }
            return false;
        }

        private void back_button_login_window3_clicked(object sender, EventArgs e)
        {
            GoBackToWindow2();
        }

        private void GoBackToWindow2()
        {
            // Optionally, you can perform other actions before switching to login_window2
            // For example, you might want to clear the entries in login_window3.

            // Close login_window3
            login_window3.Hide();

            // Show login_window2
            login_window2.ShowAll();
        }



// Transactions Window

    private void FillTransactionWindow(){

        deleteChildren(transactions_box);

        for (int i = 0; i < userTransactionsList.Count; i++){

            AddFrameToTransactionWindow(i);
        }

    }

    private void AddFrameToTransactionWindow(int index)
    {




        // Create a new frame
        Frame transactionFrame = new Frame("");

        // Create the frame
        transactionFrame.Visible = true;
        transactionFrame.CanFocus = false;
        //transactionFrame.MarginTop = 10;
        //transactionFrame.MarginBottom = 10;
        transactionFrame.LabelXalign = 0;
        transactionFrame.ShadowType = ShadowType.None;

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

        Image currencyIconImage = new Image($"GUI/Glade/images/icons/{userTransactionsList[index].CryptocurrencyName}.png");
        currencyIconImage.Visible = true;
        currencyIconImage.CanFocus = false;
        //currencyIconImage.MarginLeft = 40;
        currencyNameGrid.Attach(currencyIconImage, 0, 0, 1, 1);

        // Name Label

        Label currencyNameLabel = new Label($"{userTransactionsList[index].CryptocurrencyName}");
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




        // DateTime Label

        DateTime transactionDateTime = userTransactionsList[index].DateTime;
        // Using a format specifier
        string formattedDateTime = transactionDateTime.ToString("yyyy-MM-dd HH:mm:ss");


        Label currencyDateTimeLabel = new Label(formattedDateTime);
        currencyDateTimeLabel.Name = $"CurrencyDateTime_{index}";
        currencyDateTimeLabel.Visible = true;
        currencyDateTimeLabel.CanFocus = false;
        //currencyDateTimeLabel.MarginBottom = 9;
        //currencyDateTimeLabel.Halign = Align.End;

        // inner frame for DateTime
        Frame dateTimeFrame= new Frame("");
        dateTimeFrame.ShadowType = ShadowType.None;
        dateTimeFrame.Add(currencyDateTimeLabel);

        // Set fixed width for the DateTimeFrame
        //int fixedWidth = 150; // Set your desired fixed width
        dateTimeFrame.SetSizeRequest(fixedWidth, -1);

        innerGrid.Attach(dateTimeFrame, 1, 0, 1, 1);

        // CashValue Label

        Label currencyCashValueLabel = new Label($"${userTransactionsList[index].CashValue}");
        currencyCashValueLabel.Name = $"CurrencyCashValue_{index}";
        currencyCashValueLabel.Visible = true;
        currencyCashValueLabel.CanFocus = false;
        //currencyCashValueLabel.MarginBottom = 9;
        //currencyCashValueLabel.Halign = Align.End;

        // inner frame for cashValue
        Frame cashValueFrame= new Frame("");
        cashValueFrame.ShadowType = ShadowType.None;
        cashValueFrame.Add(currencyCashValueLabel);

        innerGrid.Attach(cashValueFrame, 2, 0, 1, 1);

        // CryptoValue Label

        Label currencyCryptoValueLabel = new Label($"{userTransactionsList[index].CryptoValue}");
        currencyCryptoValueLabel.Name = $"CurrencyCryptoValue_{index}";
        currencyCryptoValueLabel.Visible = true;
        currencyCryptoValueLabel.CanFocus = false;
        //currencyCryptoValueLabel.MarginBottom = 9;
        //currencyCryptoValueLabel.Halign = Align.End;

        // inner frame for cryptoValue
        Frame cryptoValueFrame= new Frame("");
        cryptoValueFrame.ShadowType = ShadowType.None;
        cryptoValueFrame.Add(currencyCryptoValueLabel);

        innerGrid.Attach(cryptoValueFrame, 3, 0, 1, 1);

        // CryptoStatus Label

        Label currencyCryptoStatusLabel = new Label($"{userTransactionsList[index].ValidationStatus}");
        currencyCryptoStatusLabel.Name = $"CurrencyCryptoStatus_{index}";
        currencyCryptoStatusLabel.Visible = true;
        currencyCryptoStatusLabel.CanFocus = false;
        //currencyCryptoStatusLabel.MarginBottom = 9;
        //currencyCryptoStatusLabel.Halign = Align.End;

        // inner frame for cryptoStatus
        Frame cryptoStatusFrame= new Frame("");
        cryptoStatusFrame.ShadowType = ShadowType.None;
        cryptoStatusFrame.Add(currencyCryptoStatusLabel);

        innerGrid.Attach(cryptoStatusFrame, 4, 0, 1, 1);









        // Add the inner grid to the alignment
        alignment.Add(innerGrid);

        // Add the alignment to the frame
        transactionFrame.Add(alignment);

        // Align Frame

        transactionFrame.MarginEnd = 20;



        // Add the frame to the transactions_box
        transactions_box.Add(transactionFrame);
        transactions_box.ShowAll();
    }


// P2P Window

    private void FillP2PWindow(){
        deleteChildren(p2p_list_box);


        for (int i = 0; i < userOffersList.Count; i++){
            AddFrameToP2PWindow(i);
        }

    }

    private void AddFrameToP2PWindow(int index)
    {

        string addressWallet = (string)(userOffersList[index].FromAddress);
        if (addressWallet == accountDetails.WalletAddress){
            return;
        }

        // Create a new frame
        Frame p2pFrame = new Frame("");

        // Create the frame
        p2pFrame.Visible = true;
        p2pFrame.CanFocus = false;
        //p2pFrame.MarginTop = 10;
        //p2pFrame.MarginBottom = 10;
        p2pFrame.LabelXalign = 0;
        p2pFrame.ShadowType = ShadowType.None;

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

        Image currencyIconImage = new Image($"GUI/Glade/images/icons/{userOffersList[index].CryptocurrencyName}.png");
        currencyIconImage.Visible = true;
        currencyIconImage.CanFocus = false;
        //currencyIconImage.MarginLeft = 40;
        currencyNameGrid.Attach(currencyIconImage, 0, 0, 1, 1);

        // Name Label

        Label currencyNameLabel = new Label($"{userOffersList[index].CryptocurrencyName}");
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

        // CryptoValue Label

        Label currencyCryptoValueLabel = new Label($"{userOffersList[index].CryptoValue}");
        currencyCryptoValueLabel.Name = $"CurrencyCryptoValue_{index}";
        currencyCryptoValueLabel.Visible = true;
        currencyCryptoValueLabel.CanFocus = false;
        //currencyCryptoValueLabel.MarginBottom = 9;
        //currencyCryptoValueLabel.Halign = Align.End;

        // inner frame for cryptoValue
        Frame cryptoValueFrame= new Frame("");
        cryptoValueFrame.ShadowType = ShadowType.None;
        cryptoValueFrame.Add(currencyCryptoValueLabel);

        innerGrid.Attach(cryptoValueFrame, 1, 0, 1, 1);

         // CashValue Label

        Label currencyCashValueLabel = new Label($"${userOffersList[index].CashValue}");
        currencyCashValueLabel.Name = $"CurrencyCashValue_{index}";
        currencyCashValueLabel.Visible = true;
        currencyCashValueLabel.CanFocus = false;
        //currencyCashValueLabel.MarginBottom = 9;
        //currencyCashValueLabel.Halign = Align.End;

        // inner frame for cashValue
        Frame cashValueFrame= new Frame("");
        cashValueFrame.ShadowType = ShadowType.None;
        cashValueFrame.Add(currencyCashValueLabel);

        innerGrid.Attach(cashValueFrame, 2, 0, 1, 1);



        string shortAddress = new string(addressWallet.Take(10).ToArray());



        Label currencyDateTimeLabel = new Label($"{shortAddress}..");
        currencyDateTimeLabel.Name = $"CurrencyDateTime_{index}";
        currencyDateTimeLabel.Visible = true;
        currencyDateTimeLabel.CanFocus = false;
        //currencyDateTimeLabel.MarginBottom = 9;
        //currencyDateTimeLabel.Halign = Align.End;

        // inner frame for DateTime
        Frame dateTimeFrame= new Frame("");
        dateTimeFrame.ShadowType = ShadowType.None;
        dateTimeFrame.Add(currencyDateTimeLabel);

        // Set fixed width for the DateTimeFrame
        //int fixedWidth = 150; // Set your desired fixed width
        dateTimeFrame.SetSizeRequest(fixedWidth, -1);

        innerGrid.Attach(dateTimeFrame, 3, 0, 1, 1);





        // Exchange Button
        Button buyButton = new Button("Buy");
        buyButton.Name = $"BuyButton_{index}";

        //exchangeButton.MarginBottom = 9;
        //exchangeButton.MarginRight = 10;
        //exchangeButton.Halign = Align.End;

        // inner frame for echange
        Frame echangeFrame= new Frame("");
        echangeFrame.ShadowType = ShadowType.None;
        echangeFrame.Add(buyButton);

        innerGrid.Attach(echangeFrame, 4, 0, 1, 1);

        // Connect button click events for main_window
        buyButton.Clicked += (sender, args) => buy_offer_p2p_window(addressWallet,userOffersList[index].CashValue, userOffersList[index].CryptoValue, userOffersList[index].CryptocurrencyName, userOffersList[index].OfferId);




        // Add the inner grid to the alignment
        alignment.Add(innerGrid);

        // Add the alignment to the frame
        p2pFrame.Add(alignment);

        // Align Frame

        p2pFrame.MarginEnd = 20;



        // Add the frame to the transactions_box
        p2p_list_box.Add(p2pFrame);
        p2p_list_box.ShowAll();
    }

// PortFolio Window



    private void FillPortfolioWindow(){

        deleteChildren(portfolio_maxi_box);


        var userDataDictionary = accountPortfolio.GetUserPortfolioAsDictionary();
        var sortedByValue = userDataDictionary.OrderByDescending(x => x.Value);


        List<KeyValuePair<string, decimal>> sortedList = sortedByValue.ToList();




        foreach (var kvp in sortedList)
        {


            if (kvp.Key == "UserId"){
                continue;
            }
            AddFrameToPortfolioWindow(kvp.Key, kvp.Value);




        }

    }


    private void AddFrameToPortfolioWindow( string userPortfolioCurrencyName, decimal userPortfolioCurrencyPrice)
    {
        // Create a new frame
        Frame p2pFrame = new Frame("");

        // Create the frame
        p2pFrame.Visible = true;
        p2pFrame.CanFocus = false;
        //p2pFrame.MarginTop = 10;
        //p2pFrame.MarginBottom = 10;
        p2pFrame.LabelXalign = 0;
        p2pFrame.ShadowType = ShadowType.None;

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
        innerGrid.ColumnSpacing = 100;
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

        Image currencyIconImage = new Image($"GUI/Glade/images/icons/{userPortfolioCurrencyName}.png");
        currencyIconImage.Visible = true;
        currencyIconImage.CanFocus = false;
        //currencyIconImage.MarginLeft = 40;
        currencyNameGrid.Attach(currencyIconImage, 0, 0, 1, 1);

        // Name Label

        Label currencyNameLabel = new Label($"{userPortfolioCurrencyName}");
        currencyNameLabel.Name = $"CurrencyName_{userPortfolioCurrencyName}";
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
        currencyNameFrame.MarginStart = 110;
        int fixedWidth = 200; // Set your desired fixed width
        currencyNameFrame.SetSizeRequest(fixedWidth, -1);

        innerGrid.Attach(currencyNameFrame, 0, 0, 1, 1);

        // CryptoValue Label

        Label currencyCryptoValueLabel = new Label($"{userPortfolioCurrencyPrice}");
        currencyCryptoValueLabel.Name = $"CurrencyCryptoValue_{userPortfolioCurrencyName}";
        currencyCryptoValueLabel.Visible = true;
        currencyCryptoValueLabel.CanFocus = false;
        //currencyCryptoValueLabel.MarginBottom = 9;
        //currencyCryptoValueLabel.Halign = Align.End;

        // inner frame for cryptoValue
        Frame cryptoValueFrame= new Frame("");
        cryptoValueFrame.ShadowType = ShadowType.None;
        cryptoValueFrame.Add(currencyCryptoValueLabel);
        cryptoValueFrame.MarginStart = 69;

        innerGrid.Attach(cryptoValueFrame, 1, 0, 1, 1);

        // Add the inner grid to the alignment
        alignment.Add(innerGrid);

        // Add the alignment to the frame
        p2pFrame.Add(alignment);

        // Align Frame

        p2pFrame.MarginEnd = 20;



        // Add the frame to the transactions_box
        portfolio_maxi_box.Add(p2pFrame);
        portfolio_maxi_box.ShowAll();
    }


// Miner Window

    private void mining_button_miner_window_clicked( object sender, EventArgs e){

        Thread miningThread;

         if (turnedOn)
        {
            turnedOn = false; // Stop the mining loop
            // Additional logic to handle stopping the mining process
        }
        else
        {
            turnedOn = true; // Start the mining loop
            miningThread = new Thread(startMining);
            miningThread.Start();
        }


    }

    private void startMining()
    {
        client = getClient();
        string justMessage;
        var stream = client.GetStream();

        while(turnedOn){
            Thread.Sleep(5);


            string RequestMessage = "TransactionValidationStatusForMiner";

            SendMessage(stream, RequestMessage);

            if(WaitForValidationResponse(stream))
            {
                //Get Transaction
                RequestMessage = "GetTransactionForValidation";
                SendMessage(stream, RequestMessage);
                Transaction userTransaction = WaitForTransactionForValidation(stream);



                if(userTransaction.FromAddress == "server")
                {
                    RequestMessage = "GetServerAssetsList";

                    // Send the serialized Transaction object to C client
                    SendMessage(stream, RequestMessage);

                    // Wait for a response from the server
                    List<Cryptocurrency> assets= WaitForServerAssets(stream);

                    //Get Wallet of Buyer
                    Wallet recipientWallet = new();
                    recipientWallet.Purpose = "GetWallet";
                    recipientWallet.WalletAddress = userTransaction.ToAddress;
                    string serializedWallet = recipientWallet.Serialize();

                    SendMessage(stream, serializedWallet);

                    recipientWallet = WaitForWallet(stream, out justMessage);

                    //Get Portfolio of Buyer
                    User recipientUser = new();
                    recipientUser.Purpose = "GetPortfolio";
                    recipientUser.UserId = recipientWallet.UserId;
                    string serializedUser = recipientUser.Serialize();

                    SendMessage(stream, serializedUser);
                    UserPortfolio recipientPortfolioUser = WaitForAccountPortfolio(stream);

                    Cryptocurrency crypto = new();
                    if(ValidateServerTransaction(assets, recipientWallet, recipientPortfolioUser, userTransaction, out crypto))
                    {
                        crypto.Purpose = "UpdateInServerAssets";
                        string serializedCrypto = crypto.Serialize();
                        SendMessage(stream, serializedCrypto);
                        WaitForResponse(stream);
                        recipientWallet.Purpose = "UpdateWallet";
                        serializedWallet = recipientWallet.Serialize();
                        SendMessage(stream, serializedWallet);
                        WaitForResponse(stream);

                        serializedUser = recipientPortfolioUser.Serialize();
                        SendMessage(stream, serializedUser);
                        WaitForResponse(stream);

                        // Create a Transaction object and serialize it
                        userTransaction.Purpose = "Valid";
                        userTransaction.MinerId = accountDetails.UserId; // this userId
                        string validatedSerializedTransaction = userTransaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, validatedSerializedTransaction);

                        // Wait for a response from the server
                        WaitForValidationResponse(stream);
                    }
                    else
                    {
                        // Create a Transaction object and serialize it
                        userTransaction.Purpose = "NotValid";
                        string validatedSerializedTransaction = userTransaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, validatedSerializedTransaction);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                    }
                }
                else
                {
                    //Get Wallet of Seller
                    Wallet senderWallet = new();
                    senderWallet.Purpose = "GetWallet";
                    senderWallet.WalletAddress = userTransaction.FromAddress;
                    string serializedWallet = senderWallet.Serialize();

                    SendMessage(stream, serializedWallet);
                    senderWallet = WaitForWallet(stream, out justMessage);

                    //Get Portfolio of Seller
                    User senderUser = new();
                    senderUser.Purpose = "GetPortfolio";
                    senderUser.UserId = senderWallet.UserId;
                    string serializedUser = senderUser.Serialize();

                    SendMessage(stream, serializedUser);
                    UserPortfolio senderPortfolioUser = WaitForAccountPortfolio(stream);

                    //Get Wallet of Buyer
                    Wallet recipientWallet = new();
                    recipientWallet.Purpose = "GetWallet";
                    recipientWallet.WalletAddress = userTransaction.ToAddress;
                    serializedWallet = recipientWallet.Serialize();

                    SendMessage(stream, serializedWallet);
                    recipientWallet = WaitForWallet(stream,out justMessage);

                    //Get Portfolio of Buyer
                    User recipientUser = new();
                    recipientUser.Purpose = "GetPortfolio";
                    recipientUser.UserId = recipientWallet.UserId;
                    serializedUser = recipientUser.Serialize();

                    SendMessage(stream, serializedUser);
                    UserPortfolio recipientPortfolioUser = WaitForAccountPortfolio(stream);


                    string message = "";
                    if(ValidateTransaction(senderWallet, recipientWallet, senderPortfolioUser, recipientPortfolioUser, userTransaction, out message))
                    {
                        recipientWallet.Purpose = "UpdateWallet";
                        serializedWallet = recipientWallet.Serialize();
                        SendMessage(stream, serializedWallet);
                        WaitForResponse(stream);

                        serializedUser = senderPortfolioUser.Serialize();
                        SendMessage(stream, serializedUser);
                        WaitForResponse(stream);

                        senderWallet.Purpose = "UpdateWallet";
                        serializedWallet = senderWallet.Serialize();
                        SendMessage(stream, serializedWallet);
                        WaitForResponse(stream);

                        serializedUser = recipientPortfolioUser.Serialize();
                        SendMessage(stream, serializedUser);
                        WaitForResponse(stream);


                        // Create a Transaction object and serialize it
                        userTransaction.Purpose = "Valid";
                        userTransaction.MinerId = 11; // this userId
                        string validatedSerializedTransaction = userTransaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, validatedSerializedTransaction);

                        // Wait for a response from the server
                        WaitForValidationResponse(stream);
                    }
                    else
                    {
                        // Create a Transaction object and serialize it
                        userTransaction.Purpose = "NotValid";
                        string validatedSerializedTransaction = userTransaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, validatedSerializedTransaction);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                    }
                }
            }
        }
    }



// Navigation bar functions

    private void exit_button_logout_window_clicked(object sender, EventArgs e ){
        foreach (var window in allWindows){
            window.Dispose();
        }
        Application.Quit();
        client.Close();
    }
    private void cancel_button_logout_window_clicked(object sender, EventArgs e ){
        logout_window.Hide();
    }

// Nav Bar main

    private void dashboard_button_main_window_clicked(object sender, EventArgs e){
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();

    }
    private void p2p_button_main_window_clicked(object sender, EventArgs e){
        main_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();


    }
    private void portfolio_button_main_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_main_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();


    }
    private void miner_button_main_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);

    }
    private void settings_button_main_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        settings_window.ShowAll();

    }
    private void help_button_main_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_main_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }


    // Nav Bar portfolio

    private void dashboard_button_portfolio_window_clicked(object sender, EventArgs e){
        portfolio_window.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();


    }
    private void p2p_button_portfolio_window_clicked(object sender, EventArgs e){
        portfolio_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();


    }
    private void portfolio_button_portfolio_window_clicked (object sender, EventArgs e){
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_portfolio_window_clicked (object sender, EventArgs e){
        portfolio_window.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_portfolio_window_clicked (object sender, EventArgs e){
        portfolio_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);

    }
    private void settings_button_portfolio_window_clicked (object sender, EventArgs e){
        portfolio_window.Hide();
        settings_window.ShowAll();

    }
    private void help_button_portfolio_window_clicked (object sender, EventArgs e){
        portfolio_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_portfolio_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }

    // Nav Bar p2p

    private void dashboard_button_p2p_window1_clicked(object sender, EventArgs e){
        p2p_window1.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();
    }
    private void p2p_button_p2p_window1_clicked(object sender, EventArgs e){
        deleteChildren(p2p_list_box);
        requestUserOfferList();

    }
    private void portfolio_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);

    }
    private void settings_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        settings_window.ShowAll();

    }
    private void help_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        help_window.ShowAll();

    }
    private void logout_button_p2p_window1_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }

    // Nav Bar p2p

    private void dashboard_button_p2p_window2_clicked(object sender, EventArgs e){
        p2p_window2.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();


    }
    private void p2p_button_p2p_window2_clicked(object sender, EventArgs e){
        deleteChildren(p2p_list_box);
        requestUserOfferList();

    }
    private void portfolio_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);


    }
    private void settings_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        settings_window.ShowAll();

    }
    private void help_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        help_window.ShowAll();

    }
    private void logout_button_p2p_window2_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }

    // Nav Bar transaction

    private void dashboard_button_transaction_window_clicked(object sender, EventArgs e){
        transactions_window.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();

    }
    private void p2p_button_transaction_window_clicked(object sender, EventArgs e){
        transactions_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();



    }
    private void portfolio_button_transaction_window_clicked (object sender, EventArgs e){
        transactions_window.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_transaction_window_clicked (object sender, EventArgs e){
        deleteChildren(transactions_box);
        requestTransactionList();
    }
    private void miner_button_transaction_window_clicked (object sender, EventArgs e){
        transactions_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);


    }
    private void settings_button_transaction_window_clicked (object sender, EventArgs e){
        transactions_window.Hide();
        settings_window.ShowAll();

    }
    private void help_button_transaction_window_clicked (object sender, EventArgs e){
        transactions_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_transaction_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }

     // Nav Bar miner

    private void dashboard_button_miner_window_clicked(object sender, EventArgs e){
        miner_window.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();

    }
    private void p2p_button_miner_window_clicked(object sender, EventArgs e){
        miner_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();


    }
    private void portfolio_button_miner_window_clicked (object sender, EventArgs e){
        miner_window.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_miner_window_clicked (object sender, EventArgs e){
        miner_window.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_miner_window_clicked (object sender, EventArgs e){
        miner_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);


    }
    private void settings_button_miner_window_clicked (object sender, EventArgs e){
        miner_window.Hide();
        settings_window.ShowAll();

    }
    private void help_button_miner_window_clicked (object sender, EventArgs e){
        miner_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_miner_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }

    // Nav Bar settings

    private void dashboard_button_settings_window_clicked(object sender, EventArgs e){
        settings_window.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();

    }
    private void p2p_button_settings_window_clicked(object sender, EventArgs e){
        settings_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();

    }
    private void portfolio_button_settings_window_clicked (object sender, EventArgs e){
        settings_window.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();

    }
    private void transactions_button_settings_window_clicked (object sender, EventArgs e){
        settings_window.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_settings_window_clicked (object sender, EventArgs e){
        settings_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);

    }
    private void settings_button_settings_window_clicked (object sender, EventArgs e){
        settings_window.Hide();
        settings_window.ShowAll();

    }
    private void help_button_settings_window_clicked (object sender, EventArgs e){
        settings_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_settings_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }



    // Nav Bar help

    private void dashboard_button_help_window_clicked(object sender, EventArgs e){
        help_window.Hide();
        main_window.ShowAll();
        deleteChildren(market_values_box_main_window);
        sendUserLoginDetails();
        fill_card_details();
    }
    private void p2p_button_help_window_clicked(object sender, EventArgs e){
        help_window.Hide();
        p2p_window1.ShowAll();
        deleteChildren(p2p_list_box);
        requestUserOfferList();


    }
    private void portfolio_button_help_window_clicked (object sender, EventArgs e){
        help_window.Hide();
        portfolio_window.ShowAll();
        deleteChildren(portfolio_maxi_box);
        requestUserPortfolio();
    }
    private void transactions_button_help_window_clicked (object sender, EventArgs e){
        help_window.Hide();
        transactions_window.ShowAll();
        deleteChildren(transactions_box);
        requestTransactionList();

    }
    private void miner_button_help_window_clicked (object sender, EventArgs e){
        help_window.Hide();
        miner_window.Show();
        GetCPUInfo(cpu_text_view_miner_window);
    }
    private void settings_button_help_window_clicked (object sender, EventArgs e){
        help_window.Hide();
        settings_window.ShowAll();
        GetCPUInfo(cpu_text_view_miner_window);
    }
    private void help_button_help_window_clicked (object sender, EventArgs e){
        main_window.Hide();
        help_window.ShowAll();

    }
    private void logout_button_help_window_clicked (object sender, EventArgs e){

        logout_window.ShowAll();
    }
    private void sell_button_p2p_window1_clicked (object sender, EventArgs e){
        p2p_window1.Hide();
        p2p_window2.ShowAll();
        fillComboBoxP2pWindow2();
    }
    private void buy_button_p2p_window1_clicked (object sender, EventArgs e){

    }

     private void sell_button_p2p_window2_clicked (object sender, EventArgs e){
         fillComboBoxP2pWindow2();
    }
    private void buy_button_p2p_window2_clicked (object sender, EventArgs e){
        p2p_window2.Hide();
        p2p_window1.ShowAll();
        requestUserOfferList();
    }




// Main Functions


    static void Main(){
             new CCTPSApp();
        }

    }
