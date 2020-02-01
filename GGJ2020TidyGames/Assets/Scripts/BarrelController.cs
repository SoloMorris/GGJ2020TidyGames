using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject firepoint;
    public GameObject tankBody;

    public Quaternion targetRotation;

    public Vector2 aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tankBody.transform.position;

        aim.x = Input.GetAxis("AimHorizontalRed");
        aim.y = Input.GetAxis("AimVerticalRed");
    }

    private void FixedUpdate()
    {
        if (aim.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);

            targetRotation = Quaternion.LookRotation(aim, Vector3.back);
            //targetRotation.eulerAngles.Set(0, 0, targetRotation.eulerAngles.z - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 4f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
            //float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            //float rotate = Mathf.Lerp(rb.rotation, angle, 1f * Time.fixedDeltaTime);
            //rb.rotation = rotate;
        }
       
        //transform.Rotate(0, 0, Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg);
        //rb.MoveRotation(Quaternion.Euler(aim.x, 0, aim.y));
    }
}
