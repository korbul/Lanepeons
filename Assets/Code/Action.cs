using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum CardActionType
{
    NONE = 0,
    TARGET = 1,
    SELF = 2
}

public enum TargetType
{
    NONE = 0,
    LANE = 1,
    CHAMPION = 2
}

[System.Serializable]
public class Action {
    public void ExecuteBehaviours()
    {
        foreach (string behaviour in behaviours)
        {
            Type behaviourType = System.Type.GetType(behaviour);
            IBehaviour instance = (IBehaviour)Activator.CreateInstance(behaviourType);
            instance.Execute(this);
        }
    }

    public string InstuctionMessage
    {
        get
        {
            return instuctionMessage;
        }
    }

    public TargetType TargetType
    {
        get
        {
            return targetType;
        }
    }

    public CardActionType Type
    {
        get
        {
            return type;
        }
    }

    public string Target
    {
        get
        {
            return variables.target;
        }

        set
        {
            variables.target = value;
        }
    }

    public Card CallerCard
    {
        get
        {
            return callerCard;
        }

        set
        {
            callerCard = value;
        }
    }

    public RuntimeVariables Variables
    {
        get
        {
            return variables;
        }

        set
        {
            variables = value;
        }
    }

    [SerializeField]
    string instuctionMessage;
    [SerializeField]
    TargetType targetType;
    [SerializeField]
    CardActionType type;
    [SerializeField]
    List<string> behaviours;

    [HideInInspector]
    RuntimeVariables variables;
    Card callerCard;
}

[Serializable]
public struct RuntimeVariables
{
    public string target;
}