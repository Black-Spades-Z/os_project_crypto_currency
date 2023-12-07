using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using static Transaction;
using static User;
using static Cryptocurrency;
using static UserPortfolio;
using static UserOffer;
using static Wallet;
using static MinerUtil;

class CSharpClient
{
    static TcpClient client = null;

    static void Main()
    {
        try
        {
            // Connect to the C client (acting as a server)
            string cClientIpAddress = "127.0.0.1";
            int cClientPort = 8889;

            ConnectToCServer(cClientIpAddress, cClientPort);

            // Start a thread for listening to C server
            Thread listenerThread = new Thread(ListenToCServer);
            listenerThread.Start();

            // Send and receive messages
            StartMessaging();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            client?.Close();
        }

        Console.WriteLine("Client closing...");
    }

    static void ConnectToCServer(string ipAddress, int port)
    {
        client = new TcpClient(ipAddress, port);
        Console.WriteLine($"Connected to C client at {ipAddress}:{port}");
    }

    static void SendMessage(NetworkStream stream, string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Console.WriteLine($"Sent to C client: {message}");
    }

    static void ListenToCServer()
    {
        try
        {
            using (NetworkStream stream = client.GetStream())
            {
                while (true)
                {

                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in listening to C server: {e}");
        }
    }
    
    static void StartMessaging()
{
    try
    {
        using (NetworkStream stream = client.GetStream())
        {
            while (true)
            {
                Console.WriteLine("Choose what to send:");
                Console.WriteLine("1. User register");
                Console.WriteLine("2. User login");
                Console.WriteLine("3. Transaction");
                Console.WriteLine("4. Server Assets");
                Console.WriteLine("5. Account Portfolio");
                Console.WriteLine("6. User transactions");
                Console.WriteLine("7. Publish User Offer (P2P)");
                Console.WriteLine("8. Request User Offers");
                Console.WriteLine("9. Check miner");
                Console.WriteLine("10. Insert miner");
                Console.WriteLine("11. Start validation");
                Console.WriteLine("Type 'exit' to quit.");

                string choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "1":
                        // Create a User object and serialize it
                        User user = GetUserRegsitrationDetails();
                        user.Purpose = "Register";
                        string serializedUser = user.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                        break;
                        
                    case "2":
                        // Create a User object and serialize it
                        User logUser = GetUserLoginDetails();
                        logUser.Purpose = "GetWallet";
                        serializedUser = logUser.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        WaitForWallet(stream);
                        break;

                    case "3":
                        // Create a Transaction object and serialize it
                        Transaction transaction = GetTransaction();
                        transaction.Purpose = "Publish";
                        string serializedTransaction = transaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, serializedTransaction);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                        break;
                        
                    case "4":
                        string RequestMessage = "GetServerAssetsList";

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, RequestMessage);

                        // Wait for a response from the server
                        WaitForServerAssets(stream);
                        break;
                        
                    case "5":
                        User portfolioUser = GetUserPortfolioDetails();
                        portfolioUser.Purpose = "GetPortfolio";
                        serializedUser = portfolioUser.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        WaitForAccountPortfolio(stream);
                        break;
                        
                    case "6":
                        Wallet userWallet = new();
                        userWallet.WalletAddress = "qwe";
                        userWallet.Purpose = "GetTransactionList";
                        string serializedWallet = userWallet.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedWallet);

                        // Wait for a response from the server
                        WaitForUserTransactionList(stream);
                        break;
                        
                   case "7":
                        UserOffer userOffer = GetUserOffer();
                        userOffer.Purpose = "Publish";
                        string serializedUserOffer = userOffer.Serialize();

                        SendMessage(stream, serializedUserOffer);

                        WaitForResponse(stream);
                        break;
                        
                    case "8":
                        RequestMessage = "GetUserOffers";

                        SendMessage(stream, RequestMessage);

                        WaitForUserOffers(stream);
                        break;
                        
                    case "9":
                        // Create a User object and serialize it
                        User miningUser = new();
                        miningUser.Purpose = "MinerCheck";
                        miningUser.UserId = Convert.ToInt32(Console.ReadLine());
                        serializedUser = miningUser.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                        break;
                    
                    case "10":
                        // Create a User object and serialize it
                        miningUser = new();
                        miningUser.Purpose = "MinerRegister";
                        miningUser.UserId = Convert.ToInt32(Console.ReadLine());
                        serializedUser = miningUser.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                        break;
                        
                    case "11":
                    	while(true){
                    	    Console.ReadLine();
                            RequestMessage = "ValidationStatusForMiner";

                            SendMessage(stream, RequestMessage);

                            if(WaitForValidationResponse(stream))
                            {
                                //Get Transaction List
                            	RequestMessage = "GetTransactionForValidation";
                                SendMessage(stream, RequestMessage);
                                Transaction userTransaction = WaitForTransactionForValidation(stream);
                                
                                //Get Wallet of Seller
                            	Wallet senderWallet = new();
                            	senderWallet.Purpose = "GetWallet";
                            	senderWallet.WalletAddress = userTransaction.FromAddress;
                            	serializedWallet = senderWallet.Serialize();
                      
                                SendMessage(stream, serializedWallet);
                                senderWallet = WaitForWallet(stream);
                                
                                //Get Portfolio of Seller
                                User senderUser = new();
		                senderUser.Purpose = "GetPortfolio";
		                senderUser.UserId = senderWallet.UserId;
		                serializedUser = senderUser.Serialize();

		                SendMessage(stream, serializedUser);
		                UserPortfolio senderPortfolioUser = WaitForAccountPortfolio(stream);
                        
                                //Get Wallet of Buyer
                                Wallet recipientWallet = new();
                            	recipientWallet.Purpose = "GetWallet";
                            	recipientWallet.WalletAddress = userTransaction.ToAddress;
                            	serializedWallet = recipientWallet.Serialize();
                      
                                SendMessage(stream, serializedWallet);
                                recipientWallet = WaitForWallet(stream);
                                
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
                        break;

                    case "exit":
                        Environment.Exit(0);
                        return; 
                }
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error in messaging: {e}");
    }
}

static void WaitForResponse(NetworkStream stream)
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

   
    
    
}

