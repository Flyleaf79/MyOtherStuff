using UnityEngine;
using System.Collections;
using Hernan;

public class RespawnSystem : MonoBehaviour
{

    // RESPAWNSYSYEMS ARE SET TO EVERY OBJECT THAT CAN BE REVIVED.
    // THIS WILL ALSO HOLD THE OBJECTS RESPAWN LOCATION. 
    // FOR PLAYERS THIS WILL SETUP THE TOWN LOCATION

    QuestManager questManager = null;

    GameObject myGameObject = null;
    Vector3 myInitialPostion = Vector3.zero;

    public Timer myTimer;
    private bool isPlayer = false;

    GameObject currentRespawnPos = null;
    Vector3 currentRespawnPosOffset = Vector3.zero;


    void Start()
    {
        //Set Intials
        myGameObject = gameObject;
        myInitialPostion = myGameObject.transform.position;
        currentRespawnPos = GameObject.FindGameObjectWithTag("RespawnLocation");
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        if (myGameObject.tag == "Enemy")
            isPlayer = false;
        if (myGameObject.tag == "Player")
            isPlayer = true;
    }

    void Update()
    {
        // Check if the timer has been enabled and Counts down 
        if (myTimer.isEnabled)
        {
            myTimer.timerCountDown += Time.deltaTime;
            if (myTimer.timerCountDown >= myTimer.timerCountMax)
            {
                if (isPlayer)
                    StartToRespawnPlayer();
                else if (!isPlayer)
                    StartToRespawnMob();
                myTimer.timerCountDown = 0;
                myTimer.isEnabled = false;
            }
        }
    }

    public void StartToRespawnMob()
    {
        // Respawns the Mob
        myGameObject.transform.position = myInitialPostion;
        EnemyScript enemyScript = myGameObject.GetComponent<EnemyScript>();
        TheSetup(myGameObject, true);
        enemyScript.Reset();
    }
    public void StartToRespawnPlayer()
    {
        // Respawns the Player
        myGameObject.transform.position = currentRespawnPos.transform.position;
        PlayerScript playerScript = myGameObject.GetComponent<PlayerScript>();
        playerScript.Reset();
        TheSetup(myGameObject, true);
    }
    public void DeadObjectSetup(GameObject otherGameObject, bool doIActivate = false)
    {
        // Object will have its mesh turned and Colliders turned off to appear like they are dead.
        // Respawn Timer will also be set to true
        TheSetup(otherGameObject, doIActivate);
        myTimer.isEnabled = true;
    }

    void TheSetup(GameObject _otherGameObject, bool _doIActive)
    {
        // Gets Collider, MeshRenderer, GameObject, RigidBody and also the QuestIndicator Object if the Object is part of a Quest Requirement
        // And turns them all off or On.

        Collider otherGameObjectCollider = null;
        MeshRenderer otherGameObjectMeshRenderer = null;
        GameObject otherGameObjectModel = null;
        Rigidbody otherGameObjectRigidbody = null;

        GameObject otherquestIndicator = null;

        otherGameObjectCollider = _otherGameObject.GetComponent<Collider>();
        otherGameObjectModel = _otherGameObject.transform.GetChild(0).gameObject; // IMPORTANT: Strict rules : game models must be put at index 0
        Debug.Log(otherGameObjectModel);
        Debug.Log(_doIActive);
        otherGameObjectMeshRenderer = otherGameObjectModel.GetComponent<MeshRenderer>();
        otherGameObjectRigidbody = _otherGameObject.GetComponent<Rigidbody>();
        otherGameObjectRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        if (questManager.currentActiveQuest != QuestSystem.Quests.None)
        {
            otherquestIndicator = questManager.getIndicator(myGameObject);
            otherquestIndicator.SetActive(_doIActive);
        }
        if (_doIActive)
            otherGameObjectRigidbody.constraints = RigidbodyConstraints.None;

        otherGameObjectCollider.enabled = _doIActive;
        otherGameObjectMeshRenderer.enabled = _doIActive;


    }
}
