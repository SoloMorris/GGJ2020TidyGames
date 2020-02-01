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

    public int turretTurning = 0;
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
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);

            targetRotation = Quaternion.LookRotation(aim, Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 4f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);

        }
       

    }
}
