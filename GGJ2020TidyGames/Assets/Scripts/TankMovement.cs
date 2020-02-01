using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    float movementSpeed = 2f;

    public Rigidbody2D rb;

    Vector2 movement;

    public float angle;

    public GameObject rotationTarget;

    private string[] inputNames = new string[4];
    public int controllerInt;

    // Start is called before the first frame update
    void Start()
    {
        UpdateControls();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = GetInputs(controllerInt, input.RIGHT);
        movement.y = GetInputs(controllerInt, input.UP);
        Debug.Log(movement);
    }

    private void FixedUpdate()
    {
        if (movement.sqrMagnitude > 0.0f)
        {
            //rb.MovePosition(rb.position + Vector2.up * movementSpeed * Time.fixedDeltaTime);
            rb.AddRelativeForce(Vector2.up * 4f, ForceMode2D.Force);
        }
        if (movement.sqrMagnitude > 0.0f)
        {
            angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            float rotate = Mathf.Lerp(rb.rotation, angle, 0.8f *Time.fixedDeltaTime);
            //rb.rotation = rotate;
            Vector3 targetDirection = rotationTarget.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.position, targetDirection, 10 * Time.fixedDeltaTime, 100.0f);
            //transform.rotation = Quaternion.LookRotation(new Vector3(0,0, Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90));
            transform.rotation = Quaternion.LookRotation(newDirection);
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
}
