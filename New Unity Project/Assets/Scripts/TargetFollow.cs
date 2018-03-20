using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour
{   
    
    public enum DefaultTargets
    {
        Camera,
        TextSpawn,
    }
    public DefaultTargets defaultTarget = DefaultTargets.Camera;


    public float smoothing = 5f;        // The speed with which the camera will be following.
    public Vector3 offset = Vector3.zero;                     // The initial offset from the target

    public GameObject Target = null;   // The position that that camera will be following.
    void Awake()
    {
        if (Target == null)
        {
            switch (defaultTarget)
            {
                case DefaultTargets.Camera:
                    Target = GameObject.FindGameObjectWithTag("CameraTarget");
                    break;
                case DefaultTargets.TextSpawn:
                    Target = GameObject.FindGameObjectWithTag("TextSpawnTarget");
                    break;
            }
        }
    }
    void Start()
    {
        //Calculate the initial offset.
        offset = transform.position - Target.transform.position;
    }

    void FixedUpdate()
    {
        //Create a postion the object is aiming for based on the offset from the target.
        Vector3 targetPos = Target.transform.position + offset;

        // Smoothly interpolate between the object and current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }
}
