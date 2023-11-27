using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using static User;
using static Transaction;

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

	    while(true){
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
            
            string message="";

            // Check for the presence of specific properties to determine the type
            if (data.Contains("\"ObjectType\":\"User\""))
            {
                if(HandleReceivedUser(data, out message)){
                   SendMessageToClient(stream, message);
                }
                else{
                   SendMessageToClient(stream, message);
                }
            }
            else if (data.Contains("\"ObjectType\":\"Transaction\""))
            {
            	if(HandleReceivedTransaction(data)){
                   SendMessageToClient(stream, message);
                }
                else{
                   SendMessageToClient(stream, message);
                }
            }
            else
            {
                Console.WriteLine("Unknown object type or missing ObjectType property.");
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error in handling client communication: {e}");
    }
}

    static void SendMessageToClient(NetworkStream stream, string message)
    {
        byte[] messageData = Encoding.ASCII.GetBytes(message);
        stream.Write(messageData, 0, messageData.Length);
    }
}

