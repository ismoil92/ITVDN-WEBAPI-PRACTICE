using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = null;

try
{
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
    server = new TcpListener(localAddr, 7777);

    Console.WriteLine("TCP Start:");
    server.Start();

    while (true)
    {
        Console.WriteLine("TCP Wait for a client");
        TcpClient client = server.AcceptTcpClient();

        NetworkStream stream = client.GetStream();

        string _response = "Hello from the server";

        byte[] data = Encoding.UTF8.GetBytes(_response);

        stream.Write(data, 0, data.Length);

        Console.WriteLine($"Send: {_response}");

        stream.Close();
        client.Close();
        
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    if(server != null)
        server.Stop();
}
