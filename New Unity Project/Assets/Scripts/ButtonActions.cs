using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour
{
    QuestManager questManager;
    public bool leftArrow = true;
    public bool isArrowButton = false;
    void Start()
    {
        GameObject temp = GameObject.Find("QuestManager");
        questManager = temp.GetComponent<QuestManager>();
    }

    void Update()
    {
        if (isArrowButton)
            questManager.btnCheckLimit(leftArrow, gameObject);
    }

    public void btnExit()
    {
        // closes the menu.
        questManager.closeMenu();
    }

    public void btnLeftArrow()
    {
        // thiss button will jump to the previous quest info and setup
        if (questManager.btnQuestDisplayLimit(true, gameObject) == false)
        {
            questManager.displayNextAndPreviousQuest(true);
        }
    }

    public void btnRightArrow()
    {
        // this button will jump to the next quest on the quest list
        if (questManager.btnQuestDisplayLimit(true, gameObject) == true)
        {
            questManager.displayNextAndPreviousQuest(false);
        }
    }

    public void btnAccept()
    {
        // this button will start the quest. Sets up the indicators of monsters, and tell the game that quest has been activated
        questManager.AcceptQuest();
    }

    public void btnReject()
    {
        questManager.RejectQuest();
    }

    public void btnConclude()
    {
        questManager.ConcludeQuest();
    }
}
