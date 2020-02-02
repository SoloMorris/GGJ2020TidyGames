using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 20000f)]
    private float movementSpeed = 2f;

    public Rigidbody2D rb;

    Vector2 movement;

    public float angle;

    private string[] inputNames = new string[4];
    public int controllerInt;
    public bool blue;
    [SerializeField] private ParticleSystem movementFX;

    [SerializeField]
    private CircuitBoard board;

    private GameManager gm;

    //AUDIO CODE
    public string turretShootEvent = "";
    FMOD.Studio.EventInstance turretShoot;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        VFXManager.instance.AddParticleSystemToVFXList(movementFX, "moveTrails");
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.state == gameState.GAME_PLAY)
        {
            UpdateControls();
            movement.x = GetInputs(controllerInt, input.RIGHT);
            movement.y = GetInputs(controllerInt, input.UP);

            if (GetInputs(controllerInt, input.X) == 1 && board.CheckButton(button.SHOOT) && board.CheckButton(button.RELOAD))
            {
                if (!blue)
                {

                    if (MissileManager.instance.FireMissile("red"))
                    {
                        //AUDIO CODE
                        turretShoot = FMODUnity.RuntimeManager.CreateInstance(turretShootEvent);
                        turretShoot.start();

                        board.DamageButton(button.RELOAD, 1);
                    }
                }
                else if (blue)
                {

                    if (MissileManager.instance.FireMissile("blue"))
                    {
                        //AUDIO CODE
                        turretShoot = FMODUnity.RuntimeManager.CreateInstance(turretShootEvent);
                        turretShoot.start();

                        board.DamageButton(button.RELOAD, 1);
                    }
                }
            }

            if (GetInputs(controllerInt, input.A) == 1 && board.CheckButton(button.DASH))
            {
                rb.AddRelativeForce(Vector2.up * movementSpeed / 2 * Time.deltaTime, ForceMode2D.Impulse);
                board.DamageButton(button.DASH, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if (gm.state == gameState.GAME_PLAY)
        {
            if (movement.magnitude > 0.0f && board.CheckButton(button.MOVEMENT))
            {
                rb.AddRelativeForce(Vector2.up * movementSpeed * Time.deltaTime, ForceMode2D.Force);

                Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.back);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 4);
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
            }
        }

    }

    private float GetInputs(int controllerInt, input inputType)
    {
        float output = 0;

        switch (inputType)
        {
            case input.LEFT:
            case input.RIGHT:
                output = Input.GetAxis(inputNames[0]);
                break;
            case input.A:
                if (Input.GetButtonDown(inputNames[2]))
                {
                    output = 1;
                }
                break;
            case input.X:
                if (Input.GetButtonUp(inputNames[3]))
                {
                    output = 1;
                }
                break;
            case input.UP:
            case input.DOWN:
                output = Input.GetAxis(inputNames[1]);
                break;
        }

        return output;
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

    public void GetHit(GameObject missile)
    {
        Vector2 pushbackDir = missile.transform.position  - transform.position;
        pushbackDir.Normalize();
        rb.AddForce(-pushbackDir * movementSpeed / 4 * Time.deltaTime, ForceMode2D.Impulse);
        board.DamageButton(button.MOVEMENT, 10);
    }


}
