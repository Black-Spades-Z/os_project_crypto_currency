using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

using static Transaction;
using static User;
using static Cryptocurrency;
using static UserPortfolio;


public class CSharpClient
{
    static TcpClient client = null;

    public static void clientMain()
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
            while(true){

            }

            // // Send and receive messages
            // StartMessaging();

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

    public static void SendMessage(NetworkStream stream, string message)
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

    public static  TcpClient getClient(){
        return client;
    }


}


