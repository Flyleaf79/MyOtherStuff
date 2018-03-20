using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hernan;
using UnityEngine.UI;

public class AbilityStats : MonoBehaviour
{
    public string myName = ""; // Name of ability
    public int   level   = 0;
    public float myspeed = 0f; // Speed of ability
    public float castingTime = 0f; // how long it takes for ability to actually be instantiated
    public float coolingDownTime = 0f; // after ability is used, let it cooldown with this amount than you can use it again
    public float abilityDistance = 10f; // the total Distance the ability can travel till it disappears/Destroys
    public float manaCost = 0f; // the cost of mana the ability takes.
    public float castRange = 0f; // Most likely not going to be used. But its for how far the player can use an ability.
    public float areaOfEffect = 0f; // The total Area of effect spherecally 
    public float? duration = null;
    public bool hasCastingEffects = true;
    public GameObject castingEffect = null;
    public DefenceStats bonusStats = new DefenceStats();

    //Elemental Influences

    public float baseDamage = 0f;
    public float windDamange = 0f;
    public float earthDamage = 0f;
    public float waterDamage = 0f;
    public float fireDamage = 0f;
    public float healingDamage = 0f;


    public Sprite myIcon = null;
    public AudioClip mySound = null;

    
}
