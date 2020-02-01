using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject firepoint;
    public GameObject tankBody;

    public Vector2 aim;

    public int controllerInt;
    private string[] inputNames = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        controllerInt = tankBody.GetComponent<TankMovement>().controllerInt;
        UpdateControls();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tankBody.transform.position;

        aim.x = GetInputs(controllerInt, input.LEFT);
        aim.y = GetInputs(controllerInt, input.UP);
    }

    private void FixedUpdate()
    {
        if (aim.sqrMagnitude > 0.0f)
        {
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            float rotate = Mathf.Lerp(rb.rotation, angle, 1f * Time.fixedDeltaTime);
            rb.rotation = rotate;
        }
       
        //transform.Rotate(0, 0, Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg);
        //rb.MoveRotation(Quaternion.Euler(aim.x, 0, aim.y));
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
                inputNames[0] = "AimHorizontalOne";
                inputNames[1] = "AimVerticalOne";
                break;
            case 2:
                inputNames[0] = "AimHorizontalTwo";
                inputNames[1] = "AimVerticalTwo";
                break;
            case 3:
                inputNames[0] = "AimHorizontalThree";
                inputNames[1] = "AimVerticalThree";
                break;
            case 4:
                inputNames[0] = "AimHorizontalFour";
                inputNames[1] = "AimVerticalFour";
                break;
        }
    }
}
