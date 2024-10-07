using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 12345;
    public int myId = 0;
    public TCP tcp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object.");
            Destroy(this);
        }
    }

    private void Start()
    {
        IPHostEntry hostname = Dns.GetHostEntry(Dns.GetHostName());
        IPEndPoint endp = new IPEndPoint(hostname.AddressList[0], 12345);

        Socket receiver = new Socket(hostname.AddressList[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        receiver.Connect(endp);

        byte[] buff = new byte[256];

        while(true){
            receiver.Receive(buff);
            string outputString = Encoding.UTF8.GetString(buff, 0, 256);
            int i = outputString.IndexOf('#');
            string goodformat = outputString.Substring(0,i);
            float outputFloat = float.Parse(goodformat, CultureInfo.InvariantCulture.NumberFormat);

        }
    }


  
}
