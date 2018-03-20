using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playerGameobject = null;
    public GameObject pre_damageText = null;
    public enum damageTextColors
    {
        Red, // Damage
        Yellow, // Criticals
        Green // Healing
    }

    GameManager instance = null;
    public GameManager Instance()
    {
        if (instance == null)
        {
            instance = (GameManager)FindObjectOfType<GameManager>();
        }
        return instance;
    }

    void Start()
    {
        playerGameobject = GameObject.Find("Player");
    }

    // Keeps track of unwanted Objects
    // Destroys objects that are outside of GameZones

    void OnTriggerEnter(Collider otherObject)
    {
        Debug.Log("RestrictionAlert!");
        if (otherObject.GetComponent<UnitStats>() != null)
        {
            UnitStats otherObjectsStats = otherObject.GetComponent<UnitStats>();
            otherObjectsStats.healthPoints = 0;
        }
    }

    void instantiateDamageText(Vector3 pos, float damage, damageTextColors textColors = damageTextColors.Red)
    {
        GameObject newDamageText = Instantiate<GameObject>(pre_damageText, pos, Quaternion.identity);
        TextMesh damageTextTextMesh = newDamageText.GetComponent<TextMesh>();
        switch (textColors)
        {
            case damageTextColors.Red:
                {
                    damageTextTextMesh.color = Color.red;
                    break;
                }
            case damageTextColors.Yellow:
                {
                    damageTextTextMesh.color = Color.yellow;
                    break;
                }
            case damageTextColors.Green:
                {
                    damageTextTextMesh.color = Color.green;
                    break;
                }
        }
        damageTextTextMesh.text = damage.ToString();
    }
}
