using UnityEngine;
using UnityEngine.UI;
using Hernan;
using System.Collections;
using System.Collections.Generic;
using System;

public class AbilityManager : MonoBehaviour
{
    public GameObject[] resource_AllAbilityObjects = null;
    public GameObject abilityPanel = null;
    public GameObject abilityCastingTimerOverlay = null; // The casting image for when a spell is casted.
    public Image fillImage;
    public Text abilityNameTxt;
    private float abilityCastingTime = 0; 
    private GameObject playerObject = null;

    private AbilityHandler playerAbilityHandler = null;

    public List<GameObject> playerAbilitySlots = new List<GameObject>();

    void Awake()
    {

        resource_AllAbilityObjects = Resources.LoadAll<GameObject>("Abilities");

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerAbilityHandler = playerObject.GetComponent<AbilityHandler>();

        abilityPanel = GameObject.Find("AbilityPanel");
        abilityCastingTimerOverlay = GameObject.Find("AbilityCastingTimerOverlay");
        abilityCastingTimerOverlay.SetActive(false);
        ResetAbilitySlot();
    }

    public GameObject getAbilityObject(AbilityNames _AbilityName)
    {

        GameObject _abilityObject = null;

        for (int i = 0; i < resource_AllAbilityObjects.Length; i++)
        {
            if (resource_AllAbilityObjects[i].name == _AbilityName.ToString())
                _abilityObject = resource_AllAbilityObjects[i];
        }

        return _abilityObject;
    }



    public void startCasting(AbilityNames _abilityName)
    {
        // THIS FUNCTION DOES NOT INSTANTIATE ANY GAMEOBJECT.

        
        abilityCastingTimerOverlay.SetActive(true); // Lets player have a visual on its casting time.
        GameObject _abilityObject = getAbilityObject(_abilityName); // gets the ability object/

        AbilityStats currentAbilityStat = _abilityObject.GetComponent<AbilityStats>();

        fillImage = abilityCastingTimerOverlay.transform.GetChild(0).GetComponent<Image>(); // fill image is in the order of the child number indicated in code.
        abilityNameTxt = abilityCastingTimerOverlay.transform.GetChild(1).GetComponent<Text>();

        abilityCastingTime = currentAbilityStat.castingTime; // Sets the 
        fillImage.fillAmount = 0;
        abilityNameTxt.text = _abilityName.ToString();

    }

    public bool castingTimerCheck()
    {
        if (abilityCastingTimerOverlay.activeSelf == true && fillImage.fillAmount != 1)
        {
            fillImage.fillAmount += 1.0f / abilityCastingTime * Time.deltaTime;
        }
        else if (fillImage.fillAmount >= 1)
        {
            Debugger.Logger("2");
            return true;
        }
        return false;
    }

    public void endCastingAbility()
    {
        abilityCastingTimerOverlay.SetActive(false);
    }


    #region Ability Cooldown Members

    public void SetTimers(AbilityNames _AbilityName, Timer _timer, int _countDownInterval = 1)
    {
        GameObject _abilityObject = getAbilityObject(_AbilityName);
        AbilityStats _abilityStats = null;
        _abilityStats = _abilityObject.GetComponent<AbilityStats>();
        _timer.timerCountMax = _abilityStats.coolingDownTime;
        _timer.timerCountInterval = _countDownInterval;
    }

    public void TurnOnCooldownTimer(Timer _timer, bool setActive)
    {
        _timer.isEnabled = setActive;
        if (!setActive)
            _timer.timerCountDown = 0f;
    }

    public void ResetAbilitySlot()
    {
        int lengthOfPlayerAbilties = 0;
        lengthOfPlayerAbilties = playerAbilityHandler.myAbilityNames.Count;

        // Procedrual codeing;

        for (int i = 0; i < abilityPanel.transform.childCount; i++)
        {
            if (abilityPanel.transform.GetChild(i).name == "Slot" + i)
            {
                playerAbilitySlots.Add(abilityPanel.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < playerAbilitySlots.Count; i++)
        {
            AbilityStats playerAbilityStats = null;
            GameObject activeImageGameObject = null;
            Image activeImageObjectImageScript = null;
            Sprite activeImageSprite = null; // This variable is the same as slotNumberSprite;

            Image slotNumberImageScript = null;
            Sprite slotNumberSprite = null;

            //AbilityHandler -> FindAbilityName[0] -> AbilityGameObject -> AbilityStats -> AbilityStats.Icon;

            // This gets the first child object  in each slot. 
            // there is only ever going to be one object in each slot

            activeImageGameObject = playerAbilitySlots[i].transform.GetChild(0).gameObject;
            activeImageObjectImageScript = activeImageGameObject.GetComponent<Image>();
            slotNumberImageScript = playerAbilitySlots[i].GetComponent<Image>();

            if (i == playerAbilityHandler.myAbilityNames.Count)
            {
                break;
            }
            else if (i <= playerAbilityHandler.myAbilityNames.Count)
            {
                playerAbilityStats = getAbilityObject(playerAbilityHandler.myAbilityNames[i]).GetComponent<AbilityStats>();
                slotNumberSprite = playerAbilityStats.myIcon;
                activeImageSprite = slotNumberSprite;

                slotNumberImageScript.sprite = slotNumberSprite;
                activeImageObjectImageScript.sprite = activeImageSprite;
            }
        }
    }

    public void TurnOnSlot(Image image)
    {
        // Sets the image fill to 0 so it appears to be dark.
        image.fillAmount = 0f;
    }

    public Image SetActiveSlots(int _id)
    {
        //This gets the Active slot image so I can change the fill count
        GameObject slotObject = null;
        slotObject = playerAbilitySlots[_id].transform.GetChild(0).gameObject;
        Image temp;
        temp = slotObject.GetComponent<Image>();
        return temp;
    } 
    #endregion

}
