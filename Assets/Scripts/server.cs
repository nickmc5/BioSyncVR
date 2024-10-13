using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server : MonoBehaviour
{
    public int port = 12345;
    private TcpListener serv;
    private TcpClient client;

    void Start()
    {
        serv = new TcpListener(IPAddress.Any, port);
        serv.Start();
        Debug.Log("Server started");

        client = serv.AcceptTcpClient();
        Debug.Log("Client connected");

        while (true)
        {
            NetworkStream stream = client.GetStream();
            byte[] buff = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(buff, 0, buff.Length);
            
            if (bytesRead > 0)
            {
                string data = Encoding.UTF8.GetString(buff, 0, bytesRead);
                int ind = data.IndexOf('#');
                if (ind != -1)
                {
                    string ne = data.Substring(0, ind);
                    Debug.Log("Received data: " + ne);
                    
                    
                    double value = Convert.ToDouble(ne);
                    Debug.Log("Converted to double: " + value);
                    
                   
                }
            }
        }
    }
}
