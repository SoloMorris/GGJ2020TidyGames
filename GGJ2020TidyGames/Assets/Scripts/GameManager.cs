using System;
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

    public GameObject[] interfaces = new GameObject[4];

    [SerializeField]
    private GameObject coin;

    private Vector2 scores = Vector2.zero;

    private List<int> players = new List<int>();
    private int[] characters;

    public gameState state;

    public int controllerInt;
    private string[] inputNames = new string[4];

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateControls();
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
            }
        }
    }

    private void GameUpdate()
    {
        throw new NotImplementedException();
    }

    private void PlayerSelectUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GetInputs(i, input.A))
            {
                players.Add(i);
            }

        }
    }

    private void MenuUpdate()
    {
        throw new NotImplementedException();
    }

    public void UpdateControls()
    {
        switch (controllerInt)
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

    private bool GetInputs(int controllerInt, input inputType)
    {
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
        for (int i = 0; i < 4; i++)
        {
            interfaces[i].SetActive(false);
        }

        switch (state)
        {
            case gameState.MENU:
                interfaces[(int)gameState.MENU].SetActive(true);
                break;
            case gameState.PLAYER_SELECT:
                interfaces[(int)gameState.PLAYER_SELECT].SetActive(true);
                break;
            case gameState.GAME_PLAY:
                interfaces[(int)gameState.GAME_PLAY].SetActive(true);
                break;
            case gameState.END_SCREEN:
                interfaces[(int)gameState.END_SCREEN].SetActive(true);
                break;
            default:
                break;
        }
    }
}
