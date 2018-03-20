using UnityEngine;
using System.Collections;
using Hernan;

public class DisplayText : MenuHandler
{
    /* [Description]

    This script will be implemented into any gameobject that needs to display text.
    In order for this script to work, the gameobject must have a "RegionCollisionBox" 

    This will allow the main player to view desired text when interacting with the gameobject
    examples of objects that may have this script: 
    

    
    NPC
    Doors
    Enemies


    This Script will also display any menus, such as quest giver UI menus after inteactions
    */

    //Quests displayedQuest = Quests.RevengeOfThud;
    public GameObject PF_InteractionText = null;
    private GameObject instantiatedPrefab = null; // This variable will store a instantiated version of PF_InteractionText
    private GameObject textSpawnTarget = null; //This is found inside of the player object. This variable will be the location of where the Displayed text is going to spawn from.
    public bool textIsOn = false; // Toggle check to see if interactiontext is on already
                                  // public float textTimerCount = 0f;
    public bool timerIsOn = false;
    public float timerCountDown = 0f;
    public float timerMax = 2f;
    public string stringOfText = "Press Q to interact";

    public bool targetInRange = false; // toggle check to see if player is inside the region

    public bool menuOn = false; // This is a toggle check to see if the menu is already displaied or not.
    public Menus menuToLoad = Menus.QuestGiverMenu; // This Variable will be use to select which menu the object has to load after pressing the interaction key
    private GameObject menuHolder = null;
    public GameObject canvas = null; // Holds the canvas - Canvas is a gameobject in engine that holds all the UI

    void Awake()
    {
        textSpawnTarget = GameObject.FindGameObjectWithTag("TextSpawnTarget");
        menuHolder = GameObject.Find("MenuHolder");
        canvas = menuHolder.transform.Find("Canvas").gameObject;
    }

    void Update()
    {
        if (targetInRange && Input.GetKeyDown(KeyCode.Q))
        {
            getMenu(menuToLoad, canvas);
            menuOn = true;
        }

        if (timerIsOn)
        {
            timerCountDown += Time.deltaTime;
            if (timerCountDown >= timerMax)
            {
                turnOffTxt();
                timerCountDown = 0f;
                timerIsOn = false;
            }
        }
    }

    void OnTriggerEnter()
    {
        turnOnTxt();
    }
    void OnTriggerExit()
    {
        turnOffTxt();
    }

    public void turnOnTxt()
    {
        if (!textIsOn)
        {
            instantiatedPrefab = (GameObject)Instantiate(PF_InteractionText, textSpawnTarget.transform.position, Quaternion.identity);
            TextMesh TextMesh_InteractionText = instantiatedPrefab.GetComponentInChildren<TextMesh>();
            TextMesh_InteractionText.text = stringOfText;
            textIsOn = true;
            targetInRange = true;
        }
    }

    public void turnOffTxt()
    {
        if (textIsOn)
        {
            DestroyObject(instantiatedPrefab);
            textIsOn = false;
            targetInRange = false;
            menuOn = false;
        }
    }

    public void turnOnTimer()
    {
        turnOnTxt();
        timerIsOn = true;
    }
}
