using UnityEngine;
using System.Collections;

public class SocketIOClient : MonoBehaviour {

    public GameplayState gameState;

    public void OnGameStart(string data)
    {
        gameState.OnGameStart(data);
        Debug.Log(data);
    }

    public void OnPlayerMove(string data)
    {
        gameState.OnPlayerMove(data);
        Debug.Log(data);
    }

    public static void Send(string data)
    {
        Application.ExternalCall("sendData", data);
    }

    public static void Connect()
    {
        Application.ExternalCall("connect");
    }
}
