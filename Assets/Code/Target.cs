using UnityEngine;
using System.Collections;

public abstract class Target : MonoBehaviour {
    abstract public TargetType GetTargetType();

    abstract public void ShowHighlight();
    abstract public void HideHighlight();

    abstract public void ShowHoverHighlight();
    abstract public void HideHoverHighlight();
}
