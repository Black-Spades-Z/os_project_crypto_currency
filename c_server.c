#include <stdio.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string.h>
#include <stdbool.h>
#include <unistd.h>
#include <pthread.h>

#define PORT 8888


struct AcceptedSocket
{
    int acceptedSocketFD;
    struct sockaddr_in address;
    int error;
    bool acceptedSuccessfully;
};

struct AcceptedSocket * acceptIncomingConnection(int serverSocketFD);
void receiveAndPrintIncomingData(int socketFD);

void startAcceptingIncomingConnections(int serverSocketFD);

void receiveAndPrintIncomingDataOnSeparateThread(struct AcceptedSocket *pSocket);

void sendResponseToTheClient(char *buffer,int socketFD);


struct AcceptedSocket acceptedSockets[10] ;
int acceptedSocketsCount = 0;


void startAcceptingIncomingConnections(int serverSocketFD) {

    while(true)
    {
        struct AcceptedSocket* clientSocket  = acceptIncomingConnection(serverSocketFD);
        acceptedSockets[acceptedSocketsCount++] = *clientSocket;

        receiveAndPrintIncomingDataOnSeparateThread(clientSocket);
    }
}



void receiveAndPrintIncomingDataOnSeparateThread(struct AcceptedSocket *pSocket) {

    pthread_t id;
    pthread_create(&id,NULL,receiveAndPrintIncomingData,pSocket->acceptedSocketFD);
}

void receiveAndPrintIncomingData(int socketFD) {
    char buffer[1024];



    while (true)
    {
        ssize_t  amountReceived = recv(socketFD,buffer,1024,0);

        if(amountReceived>0)
        {
            buffer[amountReceived] = 0;
            printf("%s\n",buffer);
            printf("1. Size of buffer : %d\n",sizeof(buffer));

            sendResponseToTheClient(buffer,socketFD);
        }

        if(amountReceived==0)
            break;
    }

    close(socketFD);
}

void sendResponseToTheClient(char *buffer,int socketFD) {

    int cSharpServerSocket;

    printf("2. Size of buffer : %d", sizeof(buffer));

    cSharpServerSocket = socket(AF_INET, SOCK_STREAM, 0);

    if (cSharpServerSocket < 0) {
        perror("Error in socket creation");
        exit(1);
    }

    // Configure C# server address structure
    struct sockaddr_in cSharpServerAddr;
    cSharpServerAddr.sin_family = AF_INET;
    cSharpServerAddr.sin_port = htons(8887); // Assuming C# server is running on port 8887
    cSharpServerAddr.sin_addr.s_addr = inet_addr("127.0.0.1"); // Assuming C# server is running on the same machine

    // Connect to the C# server
    if (connect(cSharpServerSocket, (struct sockaddr*)&cSharpServerAddr, sizeof(cSharpServerAddr)) < 0) {
        perror("Error in connection to C# server");
        exit(1);
    }

    // Bidirectional communication loop
        ssize_t bytesRead = recv(socketFD, buffer, sizeof(buffer), 0);
        if (bytesRead <= 0) {
            if (bytesRead == 0) {
                // Connection closed by C client
                printf("Connection closed by C client.\n");
            } else {
                perror("Error receiving data from C client");
            }
            return;
        }
        buffer[bytesRead] = '\0'; // Null-terminate the received data
        printf("Data received from C client: %s\n", buffer);

        // Forward the data to C# server
        send(cSharpServerSocket, buffer, strlen(buffer), 0);
        memset(buffer, 0, sizeof(buffer));

        // Receive data from C# server
        bytesRead = recv(cSharpServerSocket, buffer, sizeof(buffer), 0);
        if (bytesRead <= 0) {
            if (bytesRead == 0) {
                // Connection closed by C# server
                printf("Connection closed by C# server.\n");
            } else {
                perror("Error receiving data from C# server");
            }
            return;
        }
        buffer[bytesRead] = '\0'; // Null-terminate the received data
        printf("Data received from C# server: %s\n", buffer);

    // Forward the data to C client
    send(socketFD, buffer, strlen(buffer),0);
    memset(buffer, 0, sizeof(buffer));

    // Close the sockets
    close(cSharpServerSocket);


}




struct AcceptedSocket * acceptIncomingConnection(int serverSocketFD) {
    struct sockaddr_in  clientAddress ;
    int clientAddressSize = sizeof (struct sockaddr_in);
    int clientSocketFD = accept(serverSocketFD,&clientAddress,&clientAddressSize);

    struct AcceptedSocket* acceptedSocket = malloc(sizeof (struct AcceptedSocket));
    acceptedSocket->address = clientAddress;
    acceptedSocket->acceptedSocketFD = clientSocketFD;
    acceptedSocket->acceptedSuccessfully = clientSocketFD>0;

    if(!acceptedSocket->acceptedSuccessfully)
        acceptedSocket->error = clientSocketFD;



    return acceptedSocket;
}


int main(int argc, char const *argv[]){


	int serverSocketFD = socket(AF_INET, SOCK_STREAM, 0);
	if (serverSocketFD < 0)
	{
		perror("\nSocket creation error \n");
		return -1;
	}
	else {
		printf("\nSocket created \n\n");
	}


	struct sockaddr_in serverAddress;
	serverAddress.sin_family = AF_INET;

	serverAddress.sin_addr.s_addr = INADDR_ANY;
    serverAddress.sin_port = htons(PORT);

	if (bind(serverSocketFD, (struct sockaddr *)&serverAddress, sizeof(serverAddress))<0)
	{
		perror("socket bind failed");
		exit(EXIT_FAILURE);
	}
	printf("socket was bound successfully\n");

	int listenResult = listen(serverSocketFD,10);

	startAcceptingIncomingConnections(serverSocketFD);

	shutdown(serverSocketFD, SHUT_RDWR);



 	return 0;

}
