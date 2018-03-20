using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


namespace Hernan
{
    [System.Serializable]
    public class Timer
    {
        public bool isEnabled = false;
        public float timerCountMax = 0f;
        public float timerCountDown = 0f;
        public float timerCountInterval = 1f; // Default Interval Timer

        public Timer(float _timerCountMax, float _timerCountDown = 0, float _timerCountInterval = 1f)
        {
            _timerCountMax = timerCountMax;
            _timerCountDown = timerCountDown;
            _timerCountInterval = timerCountInterval;
        }

        public bool getTimerStatus()
        {
            return isEnabled;
        }

        public bool TimerCountAndIfFinished()
        {
            if (isEnabled)
            {
                if (timerCountDown < timerCountMax)
                {
                    Debug.Log("1");
                    timerCountDown += timerCountInterval * Time.deltaTime;
                }
                else if (timerCountDown >= timerCountMax)
                {
                    Debug.Log("2");
                    isEnabled = false;
                }
            }
            Debug.Log("Timer");
            return isEnabled;
        }

        public void ResetTimer()
        {
            isEnabled = false;
            timerCountDown = 0f;
        }
    }

    public enum AbilityNames
    {
        Fire,
        Ice,
    }

    [System.Serializable]
    public class DefenceStats
    {
        /* elemental Direction

        The ice mixes water and wind, wood mixes water and earth, lava mixes earth and fire, storm mixes water and lightning,
        boil mixes fire and water, dust mixes earth, wind and fire. 
        There are also elements unique to the anime and movie medium, among them crystal, darkness, steel and swift.

        */

        public float baseDefence = 0f; // Base defence is more of a permanent defence. // When an item adds to base defence, when removing item, the added base will stay
        public float otherDefence = 0f; // armour added to the character 
        public float windResistance = 0f; // 
        public float earthResistance = 0f;
        public float fireResistance = 0f;
        public float waterResistance = 0f;

        public DefenceStats(float _baseDefence = 0f, float _otherDefence = 0, float _windResistance = 0f, float _earthResistance = 0f, float _fireResistance = 0f,float _waterResistance = 0f)
        {
            baseDefence = _baseDefence;
            otherDefence = _otherDefence;
            windResistance = _windResistance;
            earthResistance = _earthResistance;
            fireResistance = _fireResistance;
            waterResistance = _waterResistance;
    }

        public float DefenceCheck(AbilityStats AS)
        {
            float totalDamageAfterDefence = 0f;
            float totalDefence = baseDefence + otherDefence;
            float fireCheck = 0f;
            float waterCheck = 0f;
            float earthCheck = 0f;
            float windCheck = 0f;

            if (AS.baseDamage < totalDefence)
                totalDamageAfterDefence += AS.baseDamage - totalDefence;
            if (AS.fireDamage < fireResistance)
                fireCheck = AS.fireDamage - fireResistance;
            if (AS.waterDamage < waterResistance)
                waterCheck = AS.waterDamage - waterResistance;
            if (AS.earthDamage < earthResistance)
                earthCheck = AS.earthDamage - earthResistance;
            if (AS.windDamange < windResistance)
                windCheck = AS.windDamange - windResistance;

            totalDamageAfterDefence = totalDamageAfterDefence + fireCheck + waterCheck + earthCheck + windCheck;

            // Elemental effects take a percent 
            return totalDamageAfterDefence;
        }

        public void addToBase(DefenceStats myDefenceStats, float numToAdd = 0f)
        {
            myDefenceStats.baseDefence = numToAdd;
        }

        public void EquipUnEquip (DefenceStats defenseStats, bool equipItem = true)
        {
            int multiplayer = 1; // default to sebtracting
            int makeNegative = -1;

            if (!equipItem)
                multiplayer *= makeNegative;

            otherDefence += multiplayer * (defenseStats.baseDefence + defenseStats.otherDefence);
            windResistance += multiplayer * (defenseStats.windResistance);
            earthResistance += multiplayer * (defenseStats.earthResistance);
            fireResistance += multiplayer * (defenseStats.fireResistance);
            waterResistance += multiplayer * (defenseStats.waterResistance);
        }
    }

    public enum Menus
    {
        // [IMPORTANT NOTE] all enums created must match exact name of menu folder inside "Resources"
        QuestGiverMenu,
        HUD

    }

    public class QuestSystem : MonoBehaviour
    {

        public enum Quests
        {
            RevengeOfThud,
            TheGreatRaAd, // Final Quest
            None,
        }

        public static QuestSystem _Instance = null;
        public static QuestSystem Instance
        {
            get
            {
                if (_Instance == null)
                {

                    _Instance = (QuestSystem)FindObjectOfType(typeof(QuestSystem));
                }

                return _Instance;
            }
        }

        public Quests currentDisplayedQuest; // Quest Currently displayed on the menu
        public Quests currentActiveQuest; // Quest Active after pressing btnAccept

        public List<GameObject> CurrentMenuUi = new List<GameObject>();

        //public GameObject itemReward = null;
        //public float coinReward = 0f;
        public float expReward = 0f;
        public int questKillCount = 0;
        public int questRequiredKillMax = 10;
        public bool completedQuest = false;  // IMPORTANT TO ALWAYS SET TO FALSE ONCE QUEST HAS BEEN HANDED IN.

        public void QuestSetup(Quests nameofQuest)
        {
            Sprite questIcon = Resources.Load<Sprite>("UI/" + nameofQuest.ToString() + "/QuestIcon");
            currentDisplayedQuest = nameofQuest;

            for (int i = 0; i < CurrentMenuUi.Count; i++)
            {
                if (CurrentMenuUi[i].name == questIcon.name)
                {
                    Image uiImage = CurrentMenuUi[i].GetComponent<Image>();
                    uiImage.sprite = questIcon;
                }

                else if (CurrentMenuUi[i].name == "txtQuestTitle")
                {
                    Text txt = CurrentMenuUi[i].GetComponent<Text>();
                    txt.text = getQuestTitle(nameofQuest);

                }
                else if (CurrentMenuUi[i].name == "txtCompletionRequirement")
                {
                    Text txt = CurrentMenuUi[i].GetComponent<Text>();
                    txt.text = getCompletionRequirement(nameofQuest);
                }
                else if (CurrentMenuUi[i].name == "txtDescription")
                {
                    Text txt = CurrentMenuUi[i].GetComponent<Text>();
                    txt.text = getDescription(nameofQuest);
                }

                if (currentDisplayedQuest != currentActiveQuest)
                {
                    if (CurrentMenuUi[i].name == "btnReject")
                        setObjectActive(CurrentMenuUi[i], false);
                    else if (CurrentMenuUi[i].name == "btnConclude")
                        setObjectActive(CurrentMenuUi[i], false);
                    else if (CurrentMenuUi[i].name == "btnAccept")
                        setObjectActive(CurrentMenuUi[i], true);
                }
                else if (currentDisplayedQuest == currentActiveQuest)
                {
                    if (completedQuest)
                    {
                        if (CurrentMenuUi[i].name == "btnReject")
                            setObjectActive(CurrentMenuUi[i], false);
                        else if (CurrentMenuUi[i].name == "btnConclude")
                            setObjectActive(CurrentMenuUi[i], true);
                        else if (CurrentMenuUi[i].name == "btnAccept")
                            setObjectActive(CurrentMenuUi[i], false);
                    }
                    else if (!completedQuest)
                    {
                        if (CurrentMenuUi[i].name == "btnReject")
                            setObjectActive(CurrentMenuUi[i], true);
                        else if (CurrentMenuUi[i].name == "btnConclude")
                            setObjectActive(CurrentMenuUi[i], false);
                        else if (CurrentMenuUi[i].name == "btnAccept")
                            setObjectActive(CurrentMenuUi[i], false);
                    }
                }
            }
        }

        void setObjectActive(GameObject Object, bool _enable)
        {
            Object.SetActive(_enable);
        }

        string getQuestTitle(Quests questName = Quests.RevengeOfThud)
        {
            string newTitle = "Revenge Of Thud"; // default value;

            if (questName == Quests.RevengeOfThud)
                newTitle = "Revenge of Thud";
            if (questName == Quests.TheGreatRaAd)
                newTitle = "The Great Ra-Ad";

            return newTitle;
        }

        string getCompletionRequirement(Quests questName = Quests.RevengeOfThud)
        {
            string newRequirement = "Kill 10 Borra Bears around the city to reduce their numbers";

            if (questName == Quests.RevengeOfThud)
                newRequirement = "Kill 10 Borra Bears around the city to reduce their numbers";
            if (questName == Quests.TheGreatRaAd)
                newRequirement = "Kill The Great Ra-Ad!";

            return newRequirement;
        }

        string getDescription(Quests questName = Quests.RevengeOfThud)
        {
            string newDescription = "Thud and his family were attacked by a pack of borra bears while taking a walk near the big mushroom tree east of the city. He lost his wife during the brawl, his daughter was injured but recovered, hes ask me to find someone to kill those bastards";

            if (questName == Quests.RevengeOfThud)
                newDescription = "Thud and his family were attacked by a pack of borra bears while taking a walk near the big mushroom tree east of the city. He lost his wife during the brawl, his daughter was injured but recovered, hes ask me to find someone to kill those bastards";
            if (questName == Quests.TheGreatRaAd)
                newDescription = "The Great Ra-Ad has become unreasonable and has threatened this town! Get him before its too late, this will not be an easy battle. Goodluck";

            return newDescription;
        }

        public void closeMenu()
        {
            GameObject parent = CurrentMenuUi[0].transform.parent.gameObject;
            parent.SetActive(false);
        }

        public string getQuestEnemyNameAndPreStart()
        {
            string _questEnemyName = "";
            if (currentDisplayedQuest == Quests.RevengeOfThud)
            {
                currentActiveQuest = currentDisplayedQuest;
                _questEnemyName = "Borra Bear";
            }
            else if (currentDisplayedQuest == Quests.TheGreatRaAd)
            {
                currentActiveQuest = currentDisplayedQuest;
                _questEnemyName = "The Great Ra-Ad";
            }

            return _questEnemyName;
        }

        public void StartQuest()
        {
            if (currentActiveQuest == Quests.RevengeOfThud)
            {
                expReward = 100f;
                questKillCount = 0;
                questRequiredKillMax = 10;
            }
            else if (currentActiveQuest == Quests.TheGreatRaAd)
            {
                expReward = 1000f;
                questKillCount = 0;
                questRequiredKillMax = 1;
            }
        }
    }

    public class MenuHandler : MonoBehaviour
    {
        //This function turns on the menu and sets up the basic ui, like banners and btnExit icons
        List<GameObject> openedMenus = new List<GameObject>();

        public void getMenu(Menus menus, GameObject canvas)
        {
            Sprite[] resources = Resources.LoadAll<Sprite>("UI/" + menus.ToString());
            //Object[] _resources = Resources.LoadAll<Sprite>("UI/" + menus.ToString());
            // from here we are going to find all the objects in resources and match em to the objects in the canvas/selectedmenu
            // after matching and changing the images and text to proper -- we will then set the canvas to active.
            // Code will be written as if everything is inactive, GameObject.find does not work for inactive objects.

            GameObject selectedMenu;
            selectedMenu = canvas.transform.Find(menus.ToString()).gameObject;
            List<GameObject> selectedMenuUis = new List<GameObject>();

            selectedMenu.SetActive(true);
            openedMenus.Add(selectedMenu);

            for (int i = 0; i < selectedMenu.transform.childCount; i++)
            {
                selectedMenuUis.Add(selectedMenu.transform.GetChild(i).gameObject);

                //OBJECTS must be checked if they are parents to more childrens, IF so then add them to the list of UIs
                // This is implemented incase of other pictures needing to be declared. Also to be able to look for txt prefix
                if (selectedMenuUis[i].transform.childCount > 0)
                {
                    for (int h = 0; h < selectedMenuUis[i].transform.childCount; h++)
                    {
                        selectedMenuUis.Add(selectedMenuUis[i].transform.GetChild(h).gameObject);
                    }
                }
            }

            for (int x = 0; x < selectedMenuUis.Count; x++)
            {
                //ADDING IMAGES
                if (selectedMenuUis[x].GetComponent<Image>() == true)
                {
                    Image uiImage = selectedMenuUis[x].GetComponent<Image>();

                    for (int i = 0; i < resources.Length; i++)
                    {
                        if (uiImage.name == resources[i].name)
                        {
                            uiImage.sprite = resources[i];
                        }
                    }
                }
                else if (selectedMenuUis[x].GetComponent<RawImage>() == true)
                {
                    RawImage uiImage = selectedMenuUis[x].GetComponent<RawImage>();

                    for (int i = 0; i < resources.Length; i++)
                    {
                        if (uiImage.name == resources[i].name)
                        {
                            uiImage.texture = resources[i].texture;
                        }
                    }
                }
            }
            QuestSystem.Instance.CurrentMenuUi = selectedMenuUis;
            if (menus == Menus.QuestGiverMenu)
                QuestSystem.Instance.QuestSetup(QuestSystem.Quests.RevengeOfThud); // sets up the first quest
        }
    }
}

namespace UnityEngine
{
    public class Debugger : MonoBehaviour
    {
        public static int countofDebuggers = 0;
        public static void Logger(string text = "This Code is working")
        {
            Debug.Log(text + countofDebuggers);
            countofDebuggers++;
        }
    }
}