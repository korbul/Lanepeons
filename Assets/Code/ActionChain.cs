using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Action chain")]
public class ActionChain : ScriptableObject {
    [SerializeField]
    List<Action> actions;

    [System.NonSerialized]
    int currentActionIdx = 0;

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
