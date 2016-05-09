using System;
using System.Collections.Generic;

[Serializable]
public class NetworkAction
{
    public int messageType;
}

[Serializable]
public class NetworkCardPlay : NetworkAction
{
    public const int ID = 1;

    public int cardChampionId;
    public List<RuntimeVariables> variables;

    public NetworkCardPlay()
    {
        messageType = ID;
    }
}

[Serializable]
public class NetworkRollFirst : NetworkAction
{
    public const int ID = 2;

    public int rollAmount;

    public NetworkRollFirst()
    {
        messageType = ID;
    }
}