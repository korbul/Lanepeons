using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CardPresenterView : MonoBehaviour {
    [HideInInspector]
    public CardPresenterController controller;
    public Text instructionText;

    private bool waitingForTarget = false;
    private Action currentAction;
    private Target potentialTarget;

    private Target PotentialTarget
    {
        get
        {
            return potentialTarget;
        }

        set
        {
            if(null != potentialTarget)
            {
                if(null == value)
                {
                    potentialTarget.HideHoverHighlight();
                }
                else
                {
                    if (potentialTarget != value)
                    {
                        potentialTarget.HideHoverHighlight();
                        value.ShowHoverHighlight();
                    }
                }
            }
            else
            {
                if (null != value)
                {
                    value.ShowHoverHighlight();
                }
            }

            potentialTarget = value;
        }
    }

    public void ProcessAction(Action action)
    {
        currentAction = action;

        if (!string.IsNullOrEmpty(action.InstuctionMessage))
        {
            instructionText.text = action.InstuctionMessage;
        }

        if(action.Type == CardActionType.SELF)
        {
            //done
            ActionComplete();
        }
        else if(action.Type == CardActionType.TARGET)
        {
            //wait for target selection
            HighlightTargets();
            waitingForTarget = true;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
    void Update()
    {
        if(waitingForTarget)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(null != PotentialTarget)
                {
                    currentAction.Target = PotentialTarget.name;

                    //done
                    ActionComplete();
                }
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
	    if(waitingForTarget)
        {
            ScanForTarget();
        }
	}

    private void HighlightTargets()
    {
        Target[] targets = FindObjectsOfType<Target>();
        foreach (Target target in targets)
        {
            if (target.GetTargetType() == currentAction.TargetType)
            {
                target.ShowHighlight();
            }
        }
    }

    private void UnhighlightTargets()
    {
        Target[] targets = FindObjectsOfType<Target>();
        foreach (Target target in targets)
        {
            if (target.GetTargetType() == currentAction.TargetType)
            {
                target.HideHighlight();
            }
        }
    }

    private void ActionComplete()
    {
        PotentialTarget = null;
        UnhighlightTargets();
        instructionText.text = "";
        waitingForTarget = false;
        controller.ActionComplete(currentAction);
    }

    private void ScanForTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            Target target = info.transform.GetComponent<Target>();
            if(null != target)
            {
                if(target.GetTargetType() == currentAction.TargetType)
                {
                    PotentialTarget = target;
                }
            }
        }
        else
        {
            PotentialTarget = null;
        }
    }
}
