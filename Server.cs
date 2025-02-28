using System;
using System.IO;
using System.IO.Pipes;

class Server
{
    static void Main(string[] args)
    {
        // Create a named pipe server
        using (var pipeServer = new NamedPipeServerStream("serverPipe", PipeDirection.InOut))
        {
            Console.WriteLine("Waiting for client connection...");
            pipeServer.WaitForConnection(); // Wait for the client to connect
            Console.WriteLine("Client connected.");

            // Write a question to the client
            using (var writer = new StreamWriter(pipeServer))
            {
                writer.WriteLine("What is 2+2?");
                writer.Flush();
            }

            // Read the response from the client
            using (var reader = new StreamReader(pipeServer))
            {
                string reply = reader.ReadLine();
                Console.WriteLine($"Server received: {reply}");
            }
        } // Pipe closes at this point
    }
}