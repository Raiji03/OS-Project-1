using System;
using System.IO;
using System.IO.Pipes;

namespace IPCCodePartB
{
    internal class Client
    {
        static void Main(string[] args)
        {
            // Create a named pipe client
            using (var pipeClient = new NamedPipeClientStream(".", "serverPipe", PipeDirection.InOut))
            {
                Console.WriteLine("Connecting to server...");
                pipeClient.Connect(); // Connect to the server
                Console.WriteLine("Connected to server.");

                // Read the question from the server
                using (var reader = new StreamReader(pipeClient))
                {
                    string message = reader.ReadLine();
                    Console.WriteLine($"Client received: {message}");
                }

                // Write the answer to the server
                using (var writer = new StreamWriter(pipeClient))
                {
                    writer.WriteLine("Response from Client: The answer is 4!");
                    writer.Flush();
                }
            } // Pipe closes at this point
        }
    }
}