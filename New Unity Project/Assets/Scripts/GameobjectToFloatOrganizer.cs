using UnityEngine;
using System.Collections;

public class GameobjectToFloatOrganizer : MonoBehaviour
{
    private GameObject myGameObject;
    private float myFloat;

    public GameobjectToFloatOrganizer(float _myFloat, GameObject _myGameObject)
    {
        myGameObject = _myGameObject;
        myFloat = _myFloat;
    }
    public bool CheckObjectMatch(GameObject _GameObejct)
    {
        if (_GameObejct == myGameObject)
            return true;
        return false;
    }
    public float getFloatfromGameObject()
    {
        return myFloat;
    }
}
