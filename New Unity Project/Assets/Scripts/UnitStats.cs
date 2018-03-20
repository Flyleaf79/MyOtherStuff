using UnityEngine;
using System.Collections;
using Hernan;

public class UnitStats : MonoBehaviour
{
    // Stats
    public string characterName = "Lord Bengo";
    public float intialHealthPoints = 100f;
    public float healthPoints = 100f;
    public float movementSpeed = 0f;
    public float baseAttackDamage = 5f;
    public float rotationSpeed = 10f;

    DefenceStats myDefenceSystem = new DefenceStats();

    public int level = 1;
    public float experience = 0f;
    public float experienceToNextLevel = 100f;

    public void addBaseDefence(float moreBase) // 
    {

    }

    // See if yuou can clean this up a littl
    public void Damaged(AbilityStats _abilityStats)
    {
        // float step = stats.movementSpeed / 2 * Time.deltaTime;
        // Do Dodge.
        // go through defence system 
       // healthPoints -= DefenceCheck(_abilityStats);
    }

    public void LevelUp()
    {

    }

    public void ExperienceGain(float _exerpeienceGain)
    {

    }

}
