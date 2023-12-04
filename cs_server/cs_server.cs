using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using static User;
using static Transaction;
using static Cryptocurrency;
using static UserPortfolio;
using static UserOffer;

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
        byte[] bytes = new byte[16384];

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
            
            string message = "";

            switch (data)
	    {
    		case string _ when data.Contains("\"ObjectType\":\"User\"") && data.Contains("\"Purpose\":\"Register\""):
        		if (HandleRegisteredUser(data, out message))
        		{
            			SendMessageToClient(stream, message);
       	 		}	
        		else
        		{		
            			SendMessageToClient(stream, message);
        		}
        	break;
        	
        	case string _ when data.Contains("\"ObjectType\":\"User\"") && data.Contains("\"Purpose\":\"Login\""):
        		if (HandleLoggedUser(data, out message))
        		{
            			SendMessageToClient(stream, message);
       	 		}	
        		else
        		{		
            			SendMessageToClient(stream, message);
        		}
        	break;

    		case string _ when data.Contains("\"ObjectType\":\"Transaction\"") && data.Contains("\"Purpose\":\"Publish\"") :
        		if (HandleReceivedTransaction(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;
        		
        	case string _ when data.Contains("GetServerAssetsList") :
        		if (HandleServerAssetsRequest(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;
        	
        	case string _ when data.Contains("\"ObjectType\":\"User\"") && data.Contains("\"Purpose\":\"GetPortfolio\"") :
        		if (HandleUserPortfolioRequest(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;
        		
        	case string _ when data.Contains("\"ObjectType\":\"Wallet\"") && data.Contains("\"Purpose\":\"GetTransactionList\"") :
        		if (HandleTransactionListRequest(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;
        		
        	case string _ when data.Contains("\"ObjectType\":\"UserOffer\"") && data.Contains("\"Purpose\":\"Publish\"") :
        		if (HandleReceivedUserOffer(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;
        		
        	case string _ when data.Contains("GetUserOffers") :
        		if (HandleUserOffersRequest(data, out message))
        		{
        	    		SendMessageToClient(stream, message);
        		}
        		else
        		{
            			SendMessageToClient(stream, message);
        		}
        		break;

    		default:
        		Console.WriteLine("Unknown object type or missing ObjectType property.");
        		break;
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

