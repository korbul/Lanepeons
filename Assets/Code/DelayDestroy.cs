using UnityEngine;
using System.Collections;

public class DelayDestroy : MonoBehaviour {

    public float timeToDestroy;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDestroy);
	}
}
