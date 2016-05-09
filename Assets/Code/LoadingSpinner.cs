using UnityEngine;
using System.Collections;

public class LoadingSpinner : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, Time.deltaTime * speed);
	}
}
