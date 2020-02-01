using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject firepoint;
    public GameObject tankBody;

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
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            float rotate = Mathf.Lerp(rb.rotation, angle, 0.1f);
            rb.rotation = rotate;
        }
       
        //transform.Rotate(0, 0, Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg);
        //rb.MoveRotation(Quaternion.Euler(aim.x, 0, aim.y));
    }
}
