using UnityEngine;
using UnityEngine.UI;
using Hernan;
using System.Collections;
using System.Collections.Generic;
using System;

public class AbilityHandler : MonoBehaviour
{

    private GameObject myGameObject = null;
    private Transform myTransform = null;
    private static AbilityManager abilityManager = null;

    public enum WhatAmI
    {
        Player,
        Enemy,
        Item,
    }

    public WhatAmI whatAmI = WhatAmI.Player; // Look at Start. This needs to be declared locally in order to know if the Objects attached to the script is a player,item or enemy.
    public List<AbilityNames> myAbilityNames = new List<AbilityNames>(); // Holds all abilities names : Note : This is where i can set abilities locally in order to know which abilities each object will have.  Default: Fire
    // Timer List must have the same count(Size) of list, as "myAbilityNames" Which will 
    public List<Timer> myAbilityTimerList = new List<Timer>(); // Holds all the timers/cooldown times of each of myAbilties
    public List<Image> myActiveAbilitySlots = new List<Image>(); // Be used as a way of filling in the Images Fill Count. This Variable will only Add to its list and not be instantiated with any fixed size.
    public List<AbilityStats> myAbilityStats = new List<AbilityStats>();


    public int currentAbilityBeingCasted = 0;
    public bool abilityBeingCasted = false; // if ability is being casted than set to true. turn on timer than set this to true
    public Timer abilityCastingTimer; // this is not in a list because we only want to be able to cast 1 ability at a time.
    private GameObject tempCastingAbilityHold; // Stores the Gameobject that is going to be casted.

    void Start()
    {
        //Intials
        myGameObject = gameObject;
        myTransform = gameObject.transform;
        abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();


        // Gets the ability name of each ability and sets the timers according to their cooldown Variable
        for (int i = 0; i < myAbilityNames.Count; i++)
        {
            abilityManager.SetTimers(myAbilityNames[i], myAbilityTimerList[i]);
            myAbilityStats.Add(abilityManager.getAbilityObject(myAbilityNames[i]).GetComponent<AbilityStats>());



        }
        for (int i = 0; i < myActiveAbilitySlots.Count; i++)
        {
            myActiveAbilitySlots[i] = abilityManager.SetActiveSlots(i);
        }

        // Check if the Object of this script is an enemy, player or Item
        // This will help with setting up certain abilities.
        // Enemy kinda works like players with abilities, where they will cast as will
        // Items will be set to use abilities based on interactions and triggers.
        if (myGameObject.tag == "Enemy")
            whatAmI = WhatAmI.Enemy;
        else if (myGameObject.tag == "Player")
            whatAmI = WhatAmI.Player;
        else if (myGameObject.tag == "Item")
            whatAmI = WhatAmI.Item;

    }

    public void castAbility(AbilityNames _abilityNames, Transform _myTransform, int ID)
    {
        // if the ability cooldown timer is enabled and the inputed ability number is correct compared to the number of abilities in roster is valid.
        //EX: Timer is not enabled and ability chosen is 2. Cast ability number 2 and set timer on so that this code doesnt run again.
        // This making it so that the ability CANT cooldown twice.


        // when i cast the ability i want thewre to be a ability castiung time. WWhen ability casting time is happening.
        // Players will be able to switch abilities before casting time is done but will suffer the cost of their original option
        // Ex: Abilities Mana Cost and the player will be forced to wait for the cooldown of the ability.
        if ((!myAbilityTimerList[ID].isEnabled) && (ID <= myAbilityNames.Count))
        {
            tempCastingAbilityHold = abilityManager.getAbilityObject(_abilityNames);
            //Instantiate(abilityManager.getAbilityObject(_abilityNames), _myTransform.position, _myTransform.rotation);
            abilityManager.TurnOnCooldownTimer(myAbilityTimerList[ID], true);

            abilityCastingTimer.ResetTimer();
            currentAbilityBeingCasted = ID; // Store ability index number (This is associated with players chose of 1,2,3,4)
            abilityBeingCasted = true; // Store true so that we know if ability is being casted. If player decides to change mid way through cast then Look at GDD on changing abilities mid cast. (Combat System)

            abilityCastingTimer.timerCountMax = myAbilityStats[ID].castingTime; // Set the AbilityCasting timer to the indexed abillities stored casting time. 
            abilityManager.startCasting(myAbilityNames[currentAbilityBeingCasted]);
            abilityCastingTimer.isEnabled = true;

            if (whatAmI == WhatAmI.Player)
                abilityManager.TurnOnSlot(myActiveAbilitySlots[ID]);
        }




    }

    void Update()
    {
        //ADD TO TIMERS AND FILL COUNTS.
        addToTimers();
        //check casting
        if ((abilityBeingCasted && abilityCastingTimer.isEnabled)) //Check if the ability is bering casted and if the timer is enabled
        {
            // if casting timercheck is true then 
            if (abilityManager.castingTimerCheck())
            {
                abilityManager.endCastingAbility();
                abilityCastingTimer.ResetTimer();
                abilityBeingCasted = false;
                Instantiate(tempCastingAbilityHold, myTransform.position, myTransform.rotation);

            }
        }
        // IF BUTTONS 1 2 3 4  keys are pressed; cast abilities according to the number on the list.
        if (whatAmI == WhatAmI.Player)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("PS4_L2") == true)
            {
                castAbility(myAbilityNames[0], transform, 0);
                //startCastingTimer(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("PS4_L1") == true)
            {

                castAbility(myAbilityNames[1], transform, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("PS4_R2") == true)
            {
                castAbility(myAbilityNames[2], transform, 2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("PS4_R1") == true)
            {
                castAbility(myAbilityNames[3], transform, 3);
            }
        }
        else if (whatAmI == WhatAmI.Enemy || whatAmI == WhatAmI.Item)
        {

        }
    }

/*

    void startCastingTimer(int indexNumber)
    {

        Debugger.Logger();
        abilityCastingTimer.ResetTimer();
        currentAbilityBeingCasted = indexNumber; // Store ability index number (This is associated with players chose of 1,2,3,4)
        abilityBeingCasted = true; // Store true so that we know if ability is being casted. If player decides to change mid way through cast then Look at GDD on changing abilities mid cast. (Combat System)

        abilityCastingTimer.timerCountMax = myAbilityStats[indexNumber].castingTime; // Set the AbilityCasting timer to the indexed abillities stored casting time. 
        abilityManager.startCasting(myAbilityNames[currentAbilityBeingCasted]); 
        abilityCastingTimer.isEnabled = true;


    }

    */
/*
    void Casting()
    {
        // set high standards in order for this not to be buggy.
        if (!abilityCastingTimer.TimerCountAndIfFinished())
        {
            castAbility(myAbilityNames[currentAbilityBeingCasted], myGameObject.transform, currentAbilityBeingCasted);
            abilityBeingCasted = false;
            abilityCastingTimer.ResetTimer();
        }
    }

*/

    void addToTimers()
    {
        // Pretty much asking if there is any abilities on this object that have a timer.
        // If everything is set right this code should check if there is any timers that are turned on.
        if (myAbilityTimerList.Count >= 1)
        {
            for (int i = 0; i < myAbilityTimerList.Count; i++)
            {
                //Checks if any of the timers in the list are turned on. If they are then sstart counting.
                if (myAbilityTimerList[i].isEnabled)
                {
                    if (myAbilityTimerList[i].timerCountDown <= myAbilityTimerList[i].timerCountMax)
                    {
                        //COUNTS DOWN TO THE TIMERS MAX COUNT   
                        myAbilityTimerList[i].timerCountDown += myAbilityTimerList[i].timerCountInterval * Time.deltaTime;
                        if (whatAmI == WhatAmI.Player)
                        {
                            addToFill(i);
                        }

                    }
                    else if (myAbilityTimerList[i].timerCountDown >= myAbilityTimerList[i].timerCountMax)
                    {
                        //TURNS OFF TIMER.
                        abilityManager.TurnOnCooldownTimer(myAbilityTimerList[i], false);
                    }
                }
            }
        }
    }

    void addToFill(int _Id)
    {
        myActiveAbilitySlots[_Id].fillAmount += 1.0f / myAbilityTimerList[_Id].timerCountMax * Time.deltaTime;
    }
}