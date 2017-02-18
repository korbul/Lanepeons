using UnityEngine;
using System.Collections;

public class CardLeanMovement : MonoBehaviour {

    public enum axis
    {
        forward = 0,
        back    = 1,
        left    = 2,
        right   = 3,
        up      = 4,
        down    = 5
    }

    [Tooltip("How much should the card lean with the card movement. 0 no lean, high values multiply it.")]
    public float leanAmount = 1;
    [Tooltip("How fast will the card rotate back to initial rotation. 0 will always be at initial rotation, high values will rotate slower.")]
    [Range(0,1)]
    public float smoothAmount = 0.5f;
    public axis leanAxis;

    private Vector3 targetVelocity;
    private Vector3 lastPosition;
    private Vector3 initialAxis;
    private static Vector3[] axisMap = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    //components
    new private Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
        initialAxis = transform.TransformDirection(axisMap[(int)leanAxis]);
        lastPosition = transform.position;
    }
	
	void Update () {
        targetVelocity += (transform.position - lastPosition) * leanAmount;
        targetVelocity *= smoothAmount;

        Debug.DrawRay(transform.position, transform.TransformDirection(axisMap[(int)leanAxis]), Color.red);
        Debug.DrawRay(transform.position, initialAxis + targetVelocity, Color.blue);

        transform.rotation = Quaternion.FromToRotation(axisMap[(int)leanAxis], initialAxis + targetVelocity);
        //transform.up = Vector3.up;
        //transform.forward = initialAxis + targetVelocity;

        lastPosition = transform.position;
    }
}
