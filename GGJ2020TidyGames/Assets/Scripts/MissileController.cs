using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public string target;
    public float travelSpeed = 0;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (direction * travelSpeed);
        print(transform.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit something");
        if (collision.collider.CompareTag(target))
        {
            Debug.Log("Hit " + target +" tank!");
        }
    }
}
