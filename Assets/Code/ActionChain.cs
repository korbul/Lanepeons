using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Action chain")]
public class ActionChain : ScriptableObject {
    [SerializeField]
    List<Action> actions;

    [System.NonSerialized]
    int currentActionIdx = 0;

    public List<RuntimeVariables> ExtractVariables()
    {
        List<RuntimeVariables> list = new List<RuntimeVariables>();
        foreach(Action action in actions)
        {
            list.Add(action.Variables);
        }

        return list;
    }

    public void InjectVariables(List<RuntimeVariables> list)
    {
        if(list.Count != actions.Count)
        {
            Debug.LogError("Injection failed, incorrect number of variables");
            return;
        }

        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Variables = list[i];
        }
    }

    public bool GetNextAction(out Action action)
    {
        action = null;

        if (currentActionIdx == actions.Count)
        {
            currentActionIdx = 0;
            return false;
        }

        action = actions[currentActionIdx];
        currentActionIdx++;

        return true;
    }

    public void SetCardReference(Card card)
    {
        foreach(Action action in actions)
        {
            action.CallerCard = card;
        }
    }
}
