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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("HorizontalRed");
        movement.y = Input.GetAxisRaw("VerticalRed");
    }

    private void FixedUpdate()
    {
        if (movement.magnitude > 0.0f)
        {
            //rb.MovePosition(rb.position + Vector2.up * movementSpeed * Time.fixedDeltaTime);
            rb.AddRelativeForce(Vector2.up * 10f, ForceMode2D.Force);



        }
        if (movement.magnitude > 0.0f)
        {
            //angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            ////float rotate = Mathf.Lerp(rb.rotation, angle, 0.8f *Time.fixedDeltaTime);
            ////rb.rotation = rotate;
            ////Vector3 targetDirection = rotationTarget.transform.position - transform.position;
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, movement, 10 * Time.fixedDeltaTime, 100.0f);
            //Debug.Log(movement);
            ////transform.rotation = Quaternion.LookRotation(new Vector3(0,0, Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg - 90));
            //transform.rotation = Quaternion.LookRotation(newDirection);

            //var angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            //Quaternion something = Quaternion.AngleAxis(angle, Vector3.forward);
            //Vector3 newRotation = Vector3.RotateTowards(transform.forward,new Vector3(0,0,something.eulerAngles.z), 100 * Time.deltaTime, 0.0f);
            //Debug.Log(something.eulerAngles);
            //// Draw a ray pointing at our target in
            //Debug.DrawRay(transform.position, newRotation, Color.red);
            //transform.rotation = Quaternion.LookRotation(newRotation);

            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 4);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        }
        
    }
}
