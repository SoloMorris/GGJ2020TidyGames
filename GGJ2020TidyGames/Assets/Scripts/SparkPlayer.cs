using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum input
{
    DEFAULT = 0,
    LEFT = 1,
    RIGHT = 2,
    A = 3,
    X = 4,
    UP = 5,
    DOWN = 6
}

public class SparkPlayer : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    private CircuitBoard board;

    public Vector2Int maxPos;
    [SerializeField]
    private Vector2Int currentPos;
    private Vector2Int desiredMove = new Vector2Int(0,0);

    public int controllerInt;

    private string[] inputNames = new string[4];

    private float lastMoveTime = 0;
    public float moveDelay;

    [SerializeField]
    private GameObject aButton;
    private SpriteRenderer aButtonSprite;

    // Start is called before the first frame update
    void Start()
    {
        //tilemap = GameObject.FindGameObjectWithTag("Circuit_Board").GetComponent<Tilemap>();
        board = tilemap.gameObject.GetComponent<CircuitBoard>();


        currentPos = new Vector2Int(6, 6);
        Vector3Int startPos = new Vector3Int(6, 6, 0);
        transform.position = tilemap.GetCellCenterWorld(startPos);

        aButtonSprite = aButton.GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        UpdateControls();
        Repairing();
        Movement();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition();
    }

    private bool Repairing()
    {
        Vector3Int posV3 = new Vector3Int(currentPos.x, currentPos.y, 0);

        if (tilemap.GetTile(posV3).name == "ATile" || tilemap.GetTile(posV3).name == "XTile" ||
            tilemap.GetTile(posV3).name == "MoveTile" || tilemap.GetTile(posV3).name == "AmmoTile")
        {
            aButtonSprite.enabled = true;

        }
        else
        {
            aButtonSprite.enabled = false;
        }
        bool output = false;
        if (GetInputs(controllerInt, input.A))
        {
            if (tilemap.GetTile(posV3).name == "ATile")
            {
                output = board.RepairButton(button.DASH);
            }
            else if (tilemap.GetTile(posV3).name == "XTile")
            {
                output = board.RepairButton(button.SHOOT);
            }
            else if (tilemap.GetTile(posV3).name == "MoveTile")
            {
                output = board.RepairButton(button.MOVEMENT);
            }
            else if (tilemap.GetTile(posV3).name == "AmmoTile")
            {
                output = board.RepairButton(button.RELOAD);
            }
        }
        return output;
    }

    private void Movement()
    {
        desiredMove = Vector2Int.zero;

        if (GetInputs(controllerInt, input.RIGHT))
        {
            desiredMove.x = 1;
        }
        else if (GetInputs(controllerInt, input.LEFT))
        {
            desiredMove.x = -1;
        }

        if (GetInputs(controllerInt, input.UP))
        {
            desiredMove.y = 1;
        }
        else if (GetInputs(controllerInt, input.DOWN))
        {
            desiredMove.y = -1;
        }

        if (CheckGridSpace())
        {
            if (lastMoveTime + moveDelay <= Time.time)
            {
                currentPos += desiredMove;
                lastMoveTime = Time.time;
            }
        }
    }

    private bool CheckGridSpace()
    {
        bool output = true;

        if (currentPos.x + desiredMove.x > maxPos.x || currentPos.y + desiredMove.y > maxPos.y)
        {
            output = false;
        }

        Vector3Int horizMovV3 = new Vector3Int(currentPos.x + desiredMove.x, currentPos.y, 0);
        Vector3Int vertMovV3 = new Vector3Int(currentPos.x, currentPos.y + desiredMove.y, 0);

        if (tilemap.GetTile(horizMovV3).name == "WALL")
        {
            desiredMove.x = 0;
        }

        if (tilemap.GetTile(vertMovV3).name == "WALL")
        {
            desiredMove.y = 0;
        }

        Vector3Int fullMovV3 = new Vector3Int(currentPos.x + desiredMove.x, currentPos.y + desiredMove.y, 0);

        if (tilemap.GetTile(fullMovV3).name == "WALL")
        {
            output = false;
        }

        return output;
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

    private void UpdatePosition()
    {
        Vector3Int newPos = new Vector3Int(currentPos.x, currentPos.y, 0);
        transform.position = tilemap.GetCellCenterWorld(newPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    public void UpdateControls()
    {
        switch (controllerInt)
        {
            case 1:
                inputNames[0] = "HorizontalOne";
                inputNames[1] =   "VerticalOne";
                inputNames[2] =          "AOne";
                inputNames[3] =          "XOne";
                break;
            case 2:
                inputNames[0] = "HorizontalTwo";
                inputNames[1] =   "VerticalTwo";
                inputNames[2] =          "ATwo";
                inputNames[3] =          "XTwo";
                break;
            case 3:
                inputNames[0] = "HorizontalThree";
                inputNames[1] =   "VerticalThree";
                inputNames[2] =          "AThree";
                inputNames[3] =          "XThree";
                break;
            case 4:
                inputNames[0] = "HorizontalFour";
                inputNames[1] =   "VerticalFour";
                inputNames[2] =          "AFour";
                inputNames[3] =          "XFour";
                break;
        }
    }
}
