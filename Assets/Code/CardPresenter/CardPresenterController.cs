using UnityEngine;
using System.Collections;
using System;

public class CardPresenterController : MonoBehaviour {

    public CardPresenterView view;

	public void ExecuteActionChain(ActionChain actionChain, System.Action callback)
    {
        if (actionChain.skipView)
        {
            Action action;
            while (actionChain.GetNextAction(out action))
            {
                action.ExecuteBehaviours();
            }
        }
        else
        {
            this.callback = callback;
            currentChain = actionChain;
            ProcessNextAction();
        }
    }

    public void ActionComplete(Action completedAction)
    {
        completedAction.ExecuteBehaviours();

        ProcessNextAction();
    }

    private ActionChain currentChain;
    private System.Action callback;

    private void ProcessNextAction()
    {
        Action action;
        if (currentChain.GetNextAction(out action))
        {
            view.ProcessAction(action);
        }
        else
        {
            ChainComplete();
        }
    }

    private void ChainComplete()
    {
        if (null != callback)
            callback();
    }

    private void Awake()
    {
        view.controller = this;
    }
}
