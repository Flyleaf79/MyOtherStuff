using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public enum State
    {
        idle,
        attacking,
        dead
    }


    //PathFinding
    Vector3[] path;
    //Vector3 currentWaypoint;
    int targetIndex; 

    private GameObject myGameObject = null;
    private Rigidbody myRigidbody = null;
    private Transform myTransform = null;

    private QuestManager questManager = null;
    private RespawnSystem respawnSystem = null;
    private UnitStats stats = null;
    

    public State state = State.idle;

    public GameObject pre_AbilityDirectionTracker = null;
    public bool isAbilityTrackerInstantiated = false;
    public GameObject currentTarget = null;
    public float currentTargetAngle = 0;

    // Dodge Values
    bool valuesSet = false;
    int minchance = 0;
    int maxchance = 100;
    int choice = 40;
    Vector3 pointa;
    Vector3 pointb;
    float jouneyLength;
   public bool timeToDodge = false;
    public float dodgeDistance = 10f;


    void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        respawnSystem = gameObject.GetComponent<RespawnSystem>();
        stats = gameObject.GetComponent<UnitStats>();

        myGameObject = gameObject;
        myTransform = gameObject.transform;
        myRigidbody = GetComponent<Rigidbody>();

        pointa = myTransform.position;
        pointb = myTransform.position * dodgeDistance;
        jouneyLength = Vector3.Distance(pointa, pointb);

        Reset();
    }

    void OnTriggerStay(Collider otherCollision)
    {
        if (state != State.dead)
        {
            if (otherCollision.gameObject.tag == "Player")
            {
                state = State.attacking;
                currentTarget = otherCollision.gameObject;
            }
        }
    }

    void OnTriggerEnter(Collider otherCollision)
    {
        if (otherCollision.gameObject.tag == "Player")
        {
            state = State.attacking;
            currentTarget = otherCollision.gameObject;
            /*
            if (p_AbilityDirectionTracker != null)
            {
                if (!isAbilityTrackerInstantiated)
                {
                    GameObject tempObject = null;
                    tempObject = (GameObject)Instantiate(p_AbilityDirectionTracker, currentTarget.transform.position, Quaternion.identity);
                    tempObject.transform.SetParent(currentTarget.transform);
                    isAbilityTrackerInstantiated = true;
                }
            }
                */
        }
        if (otherCollision.gameObject.tag == "Ability")
        {
            timeToDodge = true;
            Debug.Log(otherCollision.gameObject + " TimeToDodge");
        }
    }

    void Update()
    {

        if ((state == State.attacking) && (currentTarget != null))
        {
            PathRequestManager.RequestPath(myTransform.position, currentTarget.transform.position, OnPathFound);
            Vector3 newFlatVector = myTransform.position - currentTarget.transform.position;
            newFlatVector.y = 0f;
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(newFlatVector), stats.rotationSpeed * Time.deltaTime);
            //myRigidbody.velocity = Vector3.Lerp(myTransform.position, currentTarget.transform.position, stats.movementSpeed * Time.deltaTime);
        }

        if (state != State.dead)
        {
            if (stats.healthPoints <= 0)
            {
                Dead();
            }
        }
    }

    

    void Dead()
    {
        if (myGameObject.name == questManager._getQuestEnemyName())
        {
            questManager.questKillCount++;
            DisplayText displayTextScript = questManager.myGameObject.GetComponent<DisplayText>();
            displayTextScript.stringOfText = "You killed " + questManager.questKillCount + " of " + questManager.questRequiredKillMax;
            displayTextScript.turnOnTimer();
        }

        state = State.dead;
        respawnSystem.DeadObjectSetup(myGameObject, false);
        //respawnSystem.StartToRespawnMob();
    }

    public void Reset()
    {
        state = State.idle; 
        myTransform.localRotation = Quaternion.Euler(Vector3.zero);
        stats.healthPoints = stats.intialHealthPoints;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path!=null && path.Length >= 1)
        {
       Vector3 currentWaypoint = path[0];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                float step = stats.movementSpeed / 2 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, step);

                yield return null;
            }

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
