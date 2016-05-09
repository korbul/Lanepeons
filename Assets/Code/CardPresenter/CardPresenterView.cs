using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CardPresenterView : MonoBehaviour {
    [HideInInspector]
    public CardPresenterController controller;
    public Text instructionText;
    public CanvasGroup instructionCanvasGroup;

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
            ShowInstruction(action);
        }

        if (action.Type == CardActionType.SELF)
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

    private void ShowInstruction(Action action)
    {
        instructionText.text = string.Format(action.InstuctionMessage, action.CallerCard.name);
        DOTween.To(() => instructionCanvasGroup.alpha, x => instructionCanvasGroup.alpha = x, 1, 1);
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
        DOTween.To(() => instructionCanvasGroup.alpha, x => instructionCanvasGroup.alpha = x, 0, 1);
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
