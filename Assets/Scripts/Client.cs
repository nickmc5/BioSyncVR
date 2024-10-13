using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;


public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 24554;
    public int myId = 0;
    //public TCP tcp;

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
        //IPHostEntry hostname = Dns.GetHostEntry(ip);
        //IPEndPoint endp = new IPEndPoint(hostname.AddressList[0], 12345);
        IPAddress ipa = IPAddress.Parse(ip);
        IPEndPoint endp = new IPEndPoint(ipa, port);
        Socket receiver = new Socket(endp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Before connect");
        try{
        receiver.Connect(endp);
        }

        catch(SocketException e){
            Debug.LogError($"Socket error: {e.Message}");
        }
        Debug.Log("After connect");

        byte[] buff = new byte[256];

        while(true){
            receiver.Receive(buff);
            string outputString = Encoding.UTF8.GetString(buff, 0, 256);
            int i = outputString.IndexOf('#');
            string goodformat = outputString.Substring(0,i);
            float outputFloat = float.Parse(goodformat, CultureInfo.InvariantCulture.NumberFormat);
            Debug.Log("Data: " + outputFloat);

        }
    }


  
}
