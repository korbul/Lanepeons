using UnityEngine;
using System.Collections;
using Lidgren.Network;
using System;

public class Client : MonoBehaviour {

    NetClient netClient;

    // Use this for initialization
    void Start () {
        var config = new NetPeerConfiguration("LOLTCG");
        netClient = new NetClient(config);
        netClient.Start();
        netClient.Connect(host: "127.0.0.1", port: 12345);
    }
	
    void OnApplicationQuit()
    {
        if(netClient.ConnectionStatus == NetConnectionStatus.Connected)
            netClient.Disconnect("bye");
    }


	// Update is called once per frame
	void Update ()
    {
        Listen();
    }

    private void Listen()
    {
        NetIncomingMessage message;
        while ((message = netClient.ReadMessage()) != null)
        {
            switch (message.MessageType)
            {
                case NetIncomingMessageType.Data:
                    Debug.Log(message.ToString());
                    ProcessDataMessage(message);
                    // handle custom messages
                    //var data = message.Read * ();
                    break;

                case NetIncomingMessageType.StatusChanged:
                    // handle connection status messages
                    Debug.Log(message.SenderConnection.Status.ToString());
                    switch (message.SenderConnection.Status)
                    {
                        /* .. */
                    }
                    break;

                case NetIncomingMessageType.DebugMessage:
                    // handle debug messages
                    // (only received when compiled in DEBUG mode)
                    Debug.Log(message.ReadString());
                    break;

                /* .. */
                default:
                    Debug.Log("unhandled message with type: "
                        + message.MessageType);
                    break;
            }
        }
    }

    private void ProcessDataMessage(NetIncomingMessage message)
    {
        string type = message.ReadString();

        if(type == "startgame")
        {
            Debug.Log("Game started");
        }

        if(type == "endgame")
        {
            Debug.Log("Game ended");
        }
    }
}
