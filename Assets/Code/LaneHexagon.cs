using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class LaneHexagon : MonoBehaviour {

    public Text timer;
    public Slider powerSlider;

    public void SetTurn(int turn)
    {
        timer.text = turn.ToString();
        DOTween.To(() => sliderGroup.alpha, x => sliderGroup.alpha = x, 1, 0.5f);
    }

    public void ResetTurn()
    {
        timer.text = initialLane;
        DOTween.To(() => sliderGroup.alpha, x => sliderGroup.alpha = x, 0, 0.5f);
    }

    public void UpdatePowerSlider(float friendlyPower, float enemyPower)
    {
        float target = friendlyPower / (friendlyPower + enemyPower);
        DOTween.To(() => powerSlider.value, x => powerSlider.value = x, target, 0.5f);
    }

    CanvasGroup sliderGroup;
    string initialLane;

	// Use this for initialization
	void Start () {
        initialLane = timer.text;
        sliderGroup = powerSlider.GetComponent<CanvasGroup>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
