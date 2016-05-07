using UnityEngine;
using System.Collections;

public class Strongpoint : MonoBehaviour {

    private float rotateSpeed = 24;

	// Use this for initialization
	void Start () {
        transform.Rotate(0, Random.Range(0,360), 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
	}
}
