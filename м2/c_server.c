// C Server-Side Program

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <arpa/inet.h>

#define PORT 8888

int main() {
    int clientSocket, serverSocket, cSharpServerSocket;

    char buffer[1024];

    struct sockaddr_in serverAddr, clientAddr;

    socklen_t addr_size = sizeof(struct sockaddr);

    // Create the server socket
    serverSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (serverSocket < 0) {
        perror("Error in socket creation");
        exit(1);
    }

    // Configure server address structure
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(PORT);
    serverAddr.sin_addr.s_addr = INADDR_ANY;

    // Bind the socket
    if (bind(serverSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) < 0) {
        perror("Error in binding");
        exit(1);
    }

    // Listen for incoming connections
    if (listen(serverSocket, 10) != 0) {
        perror("Error in listening\n");
        exit(1);
    }

    // Accept a connection from C client
    clientSocket = accept(serverSocket, (struct sockaddr*)&clientAddr, &addr_size);
    if (clientSocket < 0) {
        perror("Error in accepting C client");
        exit(1);
    }

    // Forward the data to C# server
    // Create a socket to connect to the C# server
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
    while (1) {
        printf("Listening...\n");

        // Receive data from C client
        ssize_t bytesRead = recv(clientSocket, buffer, sizeof(buffer), 0);
        if (bytesRead <= 0) {
            if (bytesRead == 0) {
                // Connection closed by C client
                printf("Connection closed by C client.\n");
            } else {
                perror("Error receiving data from C client");
            }
            break;
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
            break;
        }
        buffer[bytesRead] = '\0'; // Null-terminate the received data
        printf("Data received from C# server: %s\n", buffer);

        // Forward the data to C client
        send(clientSocket, buffer, strlen(buffer), 0);
        memset(buffer, 0, sizeof(buffer));
    }

    // Close the sockets
    close(clientSocket);
    close(cSharpServerSocket);
    close(serverSocket);

    return 0;
}

