using ConsoleAppConnectionAndSecurity;

Console.WriteLine("Выберите один из трёх заданий");
Console.WriteLine("1- Подключиться к Wikipedia по HTTPS протоколу.");
Console.WriteLine("2- Настроить заголовки авторизации для HTTP подключения.");
Console.WriteLine("3- Создать  клиент  и  сервер  на  основе  TCP  протокола,  при  этом  организовать  многопоточное подключение клиентов.");
Console.Write("Введите номер задании:");
int numberTask = Convert.ToInt32(Console.ReadLine());

switch(numberTask)
{
    case 1:
        Tcp_Http_HTTPS_Protocols.TaskOneHTTPS_Protocol();
        break;
    case 2:
        Tcp_Http_HTTPS_Protocols.TaskTwoHTTP_Protocol();
        break;
    case 3:
        Tcp_Http_HTTPS_Protocols.TaskThreeTCP_Protocol();
        break;
    default:
        Console.WriteLine("Incorrect number task");
        break;
}