using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;


class User
{
    public string ObjectType => "User";
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Nationality { get; set; }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static User Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<User>(json);
    }
}

public class Transaction
{
    public string ObjectType => "Transaction";
    public string FromAddress { get; init; }
    public string ToAddress { get; init; }
    public int Value { get; init; }
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


class CSharpServer
{
    static void Main()
    {
        TcpListener server = null;

        try
        {
            int port = 8887;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = StartServer(localAddr, port);

            while (true)
            {
                using (TcpClient client = AcceptClient(server))
                using (NetworkStream stream = client.GetStream())
                {
                    HandleClientCommunication(stream);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            server?.Stop();
        }

        Console.WriteLine("Server closing...");
    }

    static TcpListener StartServer(IPAddress localAddr, int port)
    {
        TcpListener server = new TcpListener(localAddr, port);
        server.Start();
        Console.WriteLine($"Server started on {localAddr}:{port}");
        return server;
    }

    static TcpClient AcceptClient(TcpListener server)
    {
        Console.WriteLine("Waiting for a connection...");
        return server.AcceptTcpClient();
    }

    static void HandleClientCommunication(NetworkStream stream)
    {
        try
        {
            byte[] bytes = new byte[1024];

            while (true)
            {
                int bytesRead = stream.Read(bytes, 0, bytes.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Connection closed by client.");
                    break;
                }

                string data = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                Console.WriteLine($"Received from client: {data}");

                // Check for the presence of specific properties to determine the type
                if (data.Contains("\"ObjectType\":\"User\""))
                {
                    HandleReceivedUser(data);
                }
                else if (data.Contains("\"ObjectType\":\"Transaction\""))
                {
                    HandleReceivedTransaction(data);
                }
                else
                {
                    Console.WriteLine("Unknown object type or missing ObjectType property.");
                }

                // Respond back to the client
                SendMessageToClient(stream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in handling client communication: {e}");
        }
    }

    static void HandleReceivedUser(string data)
    {
        // Deserialize the User object
        User receivedUser = JsonConvert.DeserializeObject<User>(data);

        // Process the received user object as needed
        Console.WriteLine($"Deserialized User: {receivedUser.Username}, {receivedUser.Email}, {receivedUser.Address}");
    }

    static void HandleReceivedTransaction(string data)
    {
        // Deserialize the Transaction object
        Transaction receivedTransaction = JsonConvert.DeserializeObject<Transaction>(data);

        // Process the received transaction object as needed
        Console.WriteLine($"Deserialized Transaction: {receivedTransaction.FromAddress}, {receivedTransaction.ToAddress}, {receivedTransaction.Value}");
    }

    static void SendMessageToClient(NetworkStream stream)
    {
        Console.Write("Enter message to send to client: ");
        string messageToSend = Console.ReadLine();
        byte[] messageData = Encoding.ASCII.GetBytes(messageToSend);
        stream.Write(messageData, 0, messageData.Length);
    }
}