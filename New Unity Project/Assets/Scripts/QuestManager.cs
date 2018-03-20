using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Hernan;
using UnityEngine.UI;

public class QuestManager : QuestSystem
{
    public GameObject myGameObject = null;

    public GameObject questObjectiveIndicator = null;
    public Vector3 questObjectiveIndicatorOffsets = Vector3.zero;
    public GameObject[] allEnemies = null;
    public List<GameObject> allQuestObjectiveIndicator = new List<GameObject>();
    public GameObject PlayerObject = null;

    // Holds the active quest creature name. This variable will be a value to check everytime something is killd.
    // remember to clear this variable after quest has been completed.




    private string questEnemyName = "";

    public void Awake()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        myGameObject = gameObject;
    }

    public void Update()
    {
        if (currentActiveQuest != Quests.None)
        {
            // Only used to tell theplayer thats he finished the quest. Also to set the quest as completed.
            if (questKillCount >= questRequiredKillMax)
            {
                completedQuest = true;
            }
        }
    }

    public bool btnQuestDisplayLimit(bool goingNegative, GameObject btnObject)
    {
        bool hitLimit = false;
        Image btnImageScript = btnObject.GetComponent<Image>();

        if ((goingNegative && currentDisplayedQuest == 0) || (!goingNegative && currentDisplayedQuest == Quests.TheGreatRaAd))
        {
            btnImageScript.color = Color.gray;
            hitLimit = true;
        }
        if (hitLimit == false)
            btnImageScript.color = Color.white;
        return hitLimit;
    }

    public void btnCheckLimit(bool isLeftArrow, GameObject btnObject)
    {
        bool hitLimit = false;
        Image btnImageScript = btnObject.GetComponent<Image>();

        if ((isLeftArrow && currentDisplayedQuest == 0) || (!isLeftArrow && currentDisplayedQuest == Quests.TheGreatRaAd))
        {
            btnImageScript.color = Color.gray;
            hitLimit = true;
        }
        if (hitLimit == false)
            btnImageScript.color = Color.white;
    }

    public void displayNextAndPreviousQuest(bool goingNegative)
    {
        if (!goingNegative)
            currentDisplayedQuest++;
        else if (goingNegative)
            currentDisplayedQuest--;

        QuestSetup(currentDisplayedQuest);
    }

    public void GiveReward()
    {
        PlayerScript playerScript = PlayerObject.GetComponent<PlayerScript>();
        playerScript.experience += expReward;
    }

    public void AcceptQuest()
    {
        //Since i already have a variable holding the currently displayed quest. im able to attach a script according to the name of the quyest
        // thus whenn the button is pressed the accept quest will attach the component of whatever name of the quest is to the player object.

        //NEVERMIND. turns out the unity api has changed and grabbing a component through string has become obsolete... which doesnt make sense.
        // i will just put all the quest scripts into this game object and refrence them

        questEnemyName = getQuestEnemyNameAndPreStart();
        completedQuest = false;
        StartQuest();
        
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].name == questEnemyName)
            {
                float x = allEnemies[i].transform.position.x + questObjectiveIndicatorOffsets.x;
                float y = allEnemies[i].transform.position.y + questObjectiveIndicatorOffsets.y;
                float z = allEnemies[i].transform.position.z + questObjectiveIndicatorOffsets.z;

                Vector3 newQuestObjectiveIndicatorPosition = new Vector3(x, y, z);

                GameObject tempIndicator = (GameObject)Instantiate(questObjectiveIndicator, newQuestObjectiveIndicatorPosition, Quaternion.identity);
                tempIndicator.transform.SetParent(allEnemies[i].transform);
                allQuestObjectiveIndicator.Add(tempIndicator);
            }
            
        }

        closeMenu();
    }

    public GameObject getIndicator(GameObject enemyObject)
    {
        GameObject temp = enemyObject.transform.Find(questObjectiveIndicator.name + "(Clone)").gameObject;
        return temp;
    }

    public void RejectQuest()
    {
        questEnemyName = "";
        completedQuest = false;
        currentActiveQuest = Quests.None;
        expReward = 0f;
        questRequiredKillMax = 0;
        for (int i = 0; i < allQuestObjectiveIndicator.Count; i++)
            DestroyObject(allQuestObjectiveIndicator[i]);
        closeMenu();
    }

    public void ConcludeQuest()
    {
        GiveReward();
        RejectQuest(); // This also closes the menu
    }

    public string _getQuestEnemyName()
    {
        // "_" prefix, because the same function is inside QuestSystem. This was made to distinquish between 
        // QuestManager and QuestSystem. _getQuestEnemyName will be more for readonly.
        return questEnemyName;
    }
}
