using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;


using static User;

public class Transaction
{
    public string ObjectType => "Transaction";
    public string FromAddress { get; init; }
    public string ToAddress { get; init; }
    public int CryptoValue { get; init; }
    public int CashValue { get; init; }
    public Cryptocurrency Cryptocurrency { get; init; }
    public DateTime DateTime { get; init; }
    public string TransactionsHash { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static Transaction Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Transaction>(json);
    }
}

public enum Cryptocurrency
{
    Bitcoin,
    Ethereum,
    Ripple,
    Litecoin,
    Cardano,
    Polkadot,
    BinanceCoin,
    Chainlink,
    Stellar,
    BitcoinCash,
    Dogecoin,
    USD_Coin,
    Aave,
    Cosmos,
    Monero,
    Neo,
    Tezos,
    Maker,
    EOS,
    TRON,
    VeChain,
    Solana,
    Theta,
    Dash,
    Uniswap,
    Compound
}


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

    static void StartMessaging1()
    {
        try
        {
            using (NetworkStream stream = client.GetStream())
            {
                while (true)
                {
                    // Send some data to C client
                    Console.Write("Enter message: ");
                    string message = Console.ReadLine();

                    if (string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine("Message cannot be empty.");
                        continue;
                    }

                    SendMessage(stream, message);

                    // Check for an exit command
                    if (message.ToLower() == "exit")
                    {
                        break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in messaging: {e}");
        }
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
                Console.WriteLine("2. Transaction");
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
                        Console.WriteLine("Getting response");
                        WaitForResponse(stream);
                        Console.WriteLine("Got response");
                        break;
                        
                    case "2":
                        // Create a User object and serialize it
                        User logUser = GetUserLoginDetails();
                        logUser.Purpose = "Login";
                        serializedUser = logUser.Serialize();

                        // Send the serialized User object to C client
                        SendMessage(stream, serializedUser);

                        // Wait for a response from the server
                        Console.WriteLine("Getting response");
                        WaitForResponse(stream);
                        Console.WriteLine("Got response");
                        break;

                    case "3":
                    case "transaction":
                        // Create a Transaction object and serialize it
                        Transaction transaction = GetTransaction();
                        string serializedTransaction = transaction.Serialize();

                        // Send the serialized Transaction object to C client
                        SendMessage(stream, serializedTransaction);

                        // Wait for a response from the server
                        WaitForResponse(stream);
                        break;

                    case "exit":
                        return; // Exit the method and close the client
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
        byte[] buffer = new byte[1024];
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

    
    
    static Transaction GetTransaction()
{
    Console.WriteLine("Enter transaction details:");

    Console.Write("From Address: ");
    string fromAddress = Console.ReadLine();

    Console.Write("To Address: ");
    string toAddress = Console.ReadLine();

    Console.Write("Cash Value: ");
    int cashValue;
    while (!int.TryParse(Console.ReadLine(), out cashValue))
    {
        Console.Clear(); // Clear the console to ensure a clean input prompt
        Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
        Console.Write("Value: ");
    }
    
    Console.Write("Value: ");
    int cryptoValue;
    while (!int.TryParse(Console.ReadLine(), out cryptoValue))
    {
        Console.Clear(); // Clear the console to ensure a clean input prompt
        Console.WriteLine("Invalid input. Please enter a valid integer for Value.");
        Console.Write("Value: ");
    }

    Console.Write("Cryptocurrency (Bitcoin, Ethereum, Litecoin, etc.): ");
    Cryptocurrency cryptocurrency;
    Enum.TryParse(Console.ReadLine(), out cryptocurrency);

    Console.Write("Date and Time (YYYY-MM-DD HH:mm:ss): ");
    DateTime dateTime;
    while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
    {
        Console.Clear(); // Clear the console to ensure a clean input prompt
        Console.WriteLine("Invalid input. Please enter a valid date and time.");
        Console.Write("Date and Time (YYYY-MM-DD HH:mm:ss): ");
    }

    Console.Write("Transaction Hash: ");
    string transactionsHash = Console.ReadLine();

    return new Transaction
    {
        FromAddress = fromAddress,
        ToAddress = toAddress,
        CashValue = cashValue,
        CryptoValue = cryptoValue,
        Cryptocurrency = cryptocurrency,
        DateTime = dateTime,
        TransactionsHash = transactionsHash
    };
}
}

