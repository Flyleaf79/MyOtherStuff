using UnityEngine;
using System.Collections;
using Hernan;

public class Ability : MonoBehaviour
{
    private Transform myTransform;
    public Vector3 startingPosition;
    public Vector3 endPoint;
    private static AbilityStats myAbilityStats = null;
    bool moving = true;


    private Vector3 rayStartPoint;
    private Vector3 rayEndPoint;
    private Vector3 rayAngleDirection;
    private RaycastHit hit;
    int targetLayerMask = 0;



    void Start()
    {
        myTransform = gameObject.transform;
        startingPosition = myTransform.position;
        endPoint = startingPosition * myAbilityStats.abilityDistance;
    }
}




    /*
    //RayCast Stuff Test
    void Start()
    {
        myTransform = gameObject.transform;
        myAbilityStats = myTransform.gameObject.GetComponent<AbilityStats>();
        DirectionLimit = myAbilityStats.abilityDistance;

        rayStartPoint = myTransform.position;
        rayEndPoint = myTransform.forward * myAbilityStats.abilityDistance;
        targetLayerMask = LayerMask.GetMask("Unit");
        // Debug.DrawRay(myTransform.position, Vector3.forward * myAbilityStats.abilityDistance, Color.green);
    }

    void FixedUpdate()
    {
        if (DirectionCounter < DirectionLimit)
        {
            DirectionCounter += Time.deltaTime * myAbilityStats.myspeed;
        }
        if (DirectionCounter >= DirectionLimit)
        {
            moving = false;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (moving)
        {
            Ray abilityRay = new Ray(rayStartPoint, rayEndPoint);
            Debug.DrawRay(rayStartPoint, rayEndPoint, Color.red);
            Debug.DrawRay(rayStartPoint, myTransform.forward * 60f, Color.green);

            myTransform.Translate(Vector3.forward * myAbilityStats.myspeed * Time.deltaTime, Space.Self);

            if (Physics.Raycast(abilityRay, out hit, myAbilityStats.abilityDistance, targetLayerMask))
            {
                GameObject hitGameobject = hit.transform.gameObject;
                Debug.DrawRay(hit.transform.position, hit.transform.position + Vector3.forward * 60, Color.blue);
                UnitStats hitStats = hitGameobject.GetComponent<UnitStats>();
                hitStats.Damaged(myAbilityStats);
            }
        }
    }

    // Put on DataBase
    void OnTriggerEnter(Collider otherObject)
    {

        if (otherObject.GetComponent<UnitStats>() != null)
        {
            UnitStats objectStats = otherObject.GetComponent<UnitStats>();
            if (otherObject.tag == "Enemy")
            {
                objectStats.healthPoints -= myAbilityStats.abilityDamage;
                moving = false;
                Destroy(gameObject);
            }
        }
    }
}
*/