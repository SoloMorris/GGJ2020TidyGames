using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState
{
    MENU = 0,
    PLAYER_SELECT = 1,
    GAME_PLAY = 3,
    END_SCREEN = 4,
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ParticleSystem firePuff;

    public GameObject[] Maps = new GameObject[5];

    public Vector3[] coinSpawns = new Vector3[5];

    [SerializeField]
    private GameObject coin;

    public Vector2 scores = Vector2.zero;

    private int[] players;
    private int[] characters;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }
}
