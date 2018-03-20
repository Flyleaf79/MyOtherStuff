using UnityEngine;
using System.Collections;
using Hernan;

public class PlayerScript : MonoBehaviour
{
    public enum PlayersState
    {
        alive,
        dead
    }

    private GameObject myGameObject = null;

    public PlayersState state = PlayersState.alive;

    public GameObject currentBoundTown = null;
    private Vector3 currentBoundTownLoc = Vector3.zero;
    private RespawnSystem respawnSystem = null;

    public float initialHealthPoint = 100f;
    public float healthPoint = 100f;
    public float experience = 0f;

    void Start()
    {
        myGameObject = gameObject;
        respawnSystem = GetComponent<RespawnSystem>();
    }

    void Update()
    {

    }

    void Attack()
    {

    }
    void LevelUp()
    {

    }
    void Dead()
    {
        state = PlayersState.dead;
    }
    public void Reset()
    {
        state = PlayersState.alive;
        healthPoint = initialHealthPoint;
    }
}