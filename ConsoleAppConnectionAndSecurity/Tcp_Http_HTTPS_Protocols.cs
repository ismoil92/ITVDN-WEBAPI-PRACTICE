using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleAppConnectionAndSecurity;

public class Tcp_Http_HTTPS_Protocols
{

    /// <summary>
    /// Подключиться к Wikipedia по HTTPS протоколу. 
    /// </summary>
    public static void TaskOneHTTPS_Protocol()
    {
        try
        {
            string host = "ru.wikipedia.org";
            TcpClient client = new TcpClient();
            client.Connect(host, 443);

            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null);


            sslStream.AuthenticateAsClient("");
            sslStream.ReadTimeout = 2000;

            StringBuilder dataComplier = new StringBuilder();

            dataComplier.AppendLine("GET / HTTP/1.1");
            dataComplier.AppendLine($"Host: {host}");
            dataComplier.AppendLine("Accept: text/html");
            dataComplier.AppendLine("Connection: close");
            dataComplier.AppendLine($"User-Agent: {Assembly.GetExecutingAssembly().GetName().Name}");
            dataComplier.AppendLine("");

            string? _requestData = dataComplier.ToString();

            sslStream.Write(Encoding.UTF8.GetBytes(_requestData));

            Console.WriteLine(_requestData);

            var _reader = new StreamReader(sslStream, Encoding.UTF8);

            Console.WriteLine("TCP Received");
            Console.WriteLine(_reader.ReadToEnd());

            _reader.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    /// <summary>
    /// Настроить заголовки авторизации для HTTP подключения. 
    /// </summary>
    public static void TaskTwoHTTP_Protocol()
    {
        try
        {
            string host = "habr.com";
            TcpClient client = new TcpClient();
            client.Connect(host, 80);

            NetworkStream networkStream = client.GetStream();
            networkStream.ReadTimeout = 2000;

            StringBuilder dataComplier = new StringBuilder();

            dataComplier.AppendLine("GET / HTTP/1.1");
            dataComplier.AppendLine($"Host: {host}");
            dataComplier.AppendLine("Accept: text/html");
            dataComplier.AppendLine("Connect: close");
            dataComplier.AppendLine($"User-Agent: {Assembly.GetExecutingAssembly().GetName().Name}");
            dataComplier.AppendLine("");

            string _requestData = dataComplier.ToString();

            networkStream.Write(Encoding.UTF8.GetBytes(_requestData));

            Console.WriteLine(_requestData);

            var _reader = new StreamReader(networkStream, Encoding.UTF8);

            Console.WriteLine("TCP Received:");
            Console.WriteLine(_reader.ReadToEnd());

            _reader.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    /// <summary>
    /// Создать  клиент  и  сервер  на  основе  TCP  протокола,  при  этом  организовать  многопоточное 
    /// подключение клиентов.
    /// 
    /// Надо будет запускать одновременно два проекта, иначе не будет работать
    /// </summary>
    public static void TaskThreeTCP_Protocol()
    {
        try
        {
            string host = "127.0.0.1";
            TcpClient client = new TcpClient();
            client.Connect(host, 7777);

            NetworkStream networkStream = client.GetStream();

            var _reader = new StreamReader(networkStream, Encoding.UTF8);

            Console.WriteLine("TCP Received:");
            Console.WriteLine(_reader.ReadToEnd());

            _reader.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors) => true;
}