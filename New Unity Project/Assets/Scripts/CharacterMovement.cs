using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    //public float floorTranslateSpeed = 1f; // This is the frames of which the player will be moving on the floor 
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    public float floorChangeRate = 1f;
    public float playerSpeed = 10f;

    public Vector3 moveDirection = Vector3.zero;

    private Transform myTransform;
    private Rigidbody myRigidbody;

    void Awake()
    {
        myTransform = gameObject.transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // makes the rigidbody.velocity equal its own current velocity + the movementDirection * playerSpeed
        // Slerp is an interpolate of two vectors spherically. Slerp deals with more angles
        // the difference between slerp and lerp is that the vectors are treated as directions rather than points in space.

        myRigidbody.velocity = Vector3.Slerp(myRigidbody.velocity, moveDirection * playerSpeed, Time.time);
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Debug.Log("RightStickX : " + Input.GetAxisRaw("PS4_RightStickX") + " RightStickY : " + Input.GetAxisRaw("PS4_RightStickY"));

    }

}

