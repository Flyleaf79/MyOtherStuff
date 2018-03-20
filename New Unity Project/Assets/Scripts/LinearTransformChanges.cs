using UnityEngine;
using System.Collections;
using Hernan;

public class LinearTransformChanges : MonoBehaviour
{
    public Timer lifeTime;
    // if true then object will go back to its orignal location and start again from point a to b
    public bool loop = false;
    //public float speed = 0; // Obsolete because desiredDirection can act as my Speed;
    public Vector3 desiredDirection = Vector3.up;
    private Transform myTransform = null;


    void Start()
    {
        myTransform = gameObject.transform;
        lifeTime.isEnabled = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (lifeTime.isEnabled)
        {
            lifeTime.timerCountDown += Time.deltaTime;

            myTransform.Translate(desiredDirection * Time.deltaTime, Space.World);

            if ((lifeTime.timerCountDown >= lifeTime.timerCountMax) && (!loop))
                lifeTime.isEnabled = !lifeTime.isEnabled;
            else if ((lifeTime.timerCountDown >= lifeTime.timerCountMax) && (loop))
            {
                desiredDirection = -(desiredDirection);
                lifeTime.timerCountDown = 0;
            }

        }
        else if (!lifeTime.isEnabled)
        {
            Destroy(myTransform.gameObject);
        }
    }
}
