using UnityEngine;
using System.Collections;
using Hernan; // For the timer

public class Heal : MonoBehaviour
{
    GameObject myGameObject = null;
    GameObject myParentGameObject = null;

    UnitStats unitStats = null; // So that we can minupulate any values
    public bool instantHeal = false; // if true than heal amount will be instant to the object. if false the healing amount will be over a span of time
    public float currentHeal = 0f;
    public float healAmount = 100f;
    public Timer myTimer = null; // timer for the countdown of the total healing amount

    void Update()
    {

    }

    void Start()
    {
        myGameObject = gameObject;
        myParentGameObject = myGameObject.transform.parent.gameObject;
    }
}
