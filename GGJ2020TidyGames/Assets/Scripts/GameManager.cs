using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum gameState
{
    MENU = 0,
    PLAYER_SELECT = 1,
    GAME_PLAY = 2,
    END_SCREEN = 3,
}

public enum unit
{
    RED_TANK = 0,
    BLU_TANK = 1,
    RED_CB = 2,
    BLU_CB = 3
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Maps = new GameObject[5];
    public Transform mapSpawn;
    private GameObject currentMap;

    private Transform[] coinSpawns = new Transform[5];
    public bool noCoin = false;
    private int coinLocation;

    public GameObject[] interfaces = new GameObject[6];

    [SerializeField]
    private GameObject coin;

    public Vector2 scores = Vector2.zero;

    private List<int> players = new List<int>();
    private int[] characters;
    public GameObject[] units = new GameObject[4];

    public Transform[] tankSpawns = new Transform[2];

    public gameState state;

    private string[] inputNames = new string[4];

    [SerializeField]
    private float roundTimer;

    public TextMeshPro timerDisplay;

    void Start()
    {
        state = gameState.MENU;
        roundTimer = -99;
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();

        switch (state)
        {
            case gameState.MENU:
                MenuUpdate();
                break;
            case gameState.PLAYER_SELECT:
                PlayerSelectUpdate();
                break;
            case gameState.GAME_PLAY:
                GameUpdate();
                break;
            case gameState.END_SCREEN:
                EndScreenUpdate();
                break;
            default:
                break;
        }
    }

    private void EndScreenUpdate()
    {
        for (int i = 1; i < 5; i++)
        {
            if(GetInputs(i, input.A))
            {
                state = gameState.MENU;
                players.Clear();
                units[(int)unit.RED_TANK].transform.position = tankSpawns[0].position;
                units[(int)unit.BLU_TANK].transform.position = tankSpawns[1].position;
                units[(int)unit.RED_TANK].transform.rotation = tankSpawns[1].rotation;
                units[(int)unit.BLU_TANK].transform.rotation = tankSpawns[0].rotation;

                units[(int)unit.RED_CB].GetComponent<SparkPlayer>().SetCurrentPos(6,6);
                units[(int)unit.BLU_CB].GetComponent<SparkPlayer>().SetCurrentPos(6,6);

                Destroy(currentMap);
            }
        }
    }

    private void GameUpdate()
    {
        if (roundTimer == -99)
        {
            GameStart();
        }

        if (roundTimer > 0 && roundTimer != -99)
        {
            roundTimer -= 1 * Time.deltaTime;

            if (noCoin)
            {
                int spawn = Random.Range(0, 5);

                while (spawn == coinLocation)
                {
                    spawn = Random.Range(0, 5);
                } 

                SpawnCoin(spawn);
            }
            string[] x;
            x = roundTimer.ToString().Split('.');
            timerDisplay.text = x[0];
        }
        else
        {
            roundTimer = -99;
            state = gameState.END_SCREEN;
        }
    }

    private void GameStart()
    {
        roundTimer = 60;
        LoadMap(Random.Range(0,5));
        SpawnCoin(0);
    }

    private void LoadMap(int x)
    {
        GameObject newMap = Instantiate(Maps[x], mapSpawn);
        coinSpawns = Maps[x].GetComponent<Mappy>().coinSpawns;
        currentMap = newMap;
    }

    private void SpawnCoin(int location)
    {
        GameObject newCoin = Instantiate(coin, currentMap.transform);
        newCoin.transform.localPosition = coinSpawns[location].position;
        noCoin = false;
        coinLocation = location;
    }

    private void PlayerSelectUpdate()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GetInputs(i, input.A))
            {
                players.Add(i);
            }

            if (GetInputs(i, input.X))
            {
                characters[1] = players[1];
                characters[2] = players[2];
                characters[3] = players[3];
                characters[4] = players[4];

                units[(int)unit.RED_TANK].GetComponent<TankMovement>().controllerInt = characters[1];
                units[(int)unit.BLU_TANK].GetComponent<TankMovement>().controllerInt = characters[2];
                units[(int)unit.RED_CB].GetComponent<TankMovement>().controllerInt = characters[3];
                units[(int)unit.BLU_CB].GetComponent<TankMovement>().controllerInt = characters[4];
                state = gameState.GAME_PLAY;
            }
        }
    }

    private void MenuUpdate()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GetInputs(i, input.A))
            {
                state = gameState.GAME_PLAY;
                //state = gameState.PLAYER_SELECT;
            }
        }
    }

    public void UpdateControls(int i)
    {
        switch (i)
        {
            case 1:
                inputNames[0] = "HorizontalOne";
                inputNames[1] = "VerticalOne";
                inputNames[2] = "AOne";
                inputNames[3] = "XOne";
                break;
            case 2:
                inputNames[0] = "HorizontalTwo";
                inputNames[1] = "VerticalTwo";
                inputNames[2] = "ATwo";
                inputNames[3] = "XTwo";
                break;
            case 3:
                inputNames[0] = "HorizontalThree";
                inputNames[1] = "VerticalThree";
                inputNames[2] = "AThree";
                inputNames[3] = "XThree";
                break;
            case 4:
                inputNames[0] = "HorizontalFour";
                inputNames[1] = "VerticalFour";
                inputNames[2] = "AFour";
                inputNames[3] = "XFour";
                break;
        }
    }

    private bool GetInputs(int controller, input inputType)
    {
        UpdateControls(controller);

        bool output = false;

        switch (inputType)
        {
            case input.LEFT:
                if (Input.GetAxis(inputNames[0]) < 0)
                {
                    output = true;
                }
                break;
            case input.RIGHT:
                if (Input.GetAxis(inputNames[0]) > 0)
                {
                    output = true;
                }
                break;
            case input.A:
                if (Input.GetButtonDown(inputNames[2]))
                {
                    output = true;
                }
                break;
            case input.X:
                if (Input.GetButtonUp(inputNames[3]))
                {
                    output = true;
                }
                break;
            case input.UP:
                if (Input.GetAxis(inputNames[1]) > 0)
                {
                    output = true;
                }
                break;
            case input.DOWN:
                if (Input.GetAxis(inputNames[1]) < 0)
                {
                    output = true;
                }
                break;
        }

        return output;
    }

    private void UIUpdate()
    {

        switch (state)
        {
            case gameState.MENU:
                interfaces[(int)gameState.MENU].SetActive(true);
                interfaces[(int)gameState.PLAYER_SELECT].SetActive(false);
                interfaces[(int)gameState.GAME_PLAY].SetActive(false);
                interfaces[(int)gameState.END_SCREEN].SetActive(false);
                interfaces[4].SetActive(false);
                interfaces[5].SetActive(false);
                break;
            case gameState.PLAYER_SELECT:
                interfaces[(int)gameState.MENU].SetActive(false);
                interfaces[(int)gameState.PLAYER_SELECT].SetActive(true);
                interfaces[(int)gameState.GAME_PLAY].SetActive(false);
                interfaces[(int)gameState.END_SCREEN].SetActive(false);
                interfaces[4].SetActive(false);
                interfaces[5].SetActive(false);
                break;
            case gameState.GAME_PLAY:
                interfaces[(int)gameState.MENU].SetActive(false);
                interfaces[(int)gameState.PLAYER_SELECT].SetActive(false);
                interfaces[(int)gameState.GAME_PLAY].SetActive(true);
                interfaces[(int)gameState.END_SCREEN].SetActive(false);
                interfaces[4].SetActive(false);
                interfaces[5].SetActive(false);

                break;
            case gameState.END_SCREEN:
                interfaces[(int)gameState.MENU].SetActive(false);
                interfaces[(int)gameState.PLAYER_SELECT].SetActive(false);
                interfaces[(int)gameState.GAME_PLAY].SetActive(false);
                interfaces[(int)gameState.END_SCREEN].SetActive(true);
                interfaces[4].SetActive(true);
                interfaces[5].SetActive(true);
                break;
            default:
                break;
        }
    }
}
