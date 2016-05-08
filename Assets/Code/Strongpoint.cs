using UnityEngine;
using System.Collections;

public class Strongpoint : MonoBehaviour {

    public GameObject explostionPrefab;
    public AudioClip towerExplode;

    public void DestroyStrongpoint()
    {
        Instantiate(explostionPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(towerExplode);
        meshRenderer.material.color = Color.black;
        enabled = false;
    }

    private float rotateSpeed = 24;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        transform.Rotate(0, Random.Range(0,360), 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
	}
}
