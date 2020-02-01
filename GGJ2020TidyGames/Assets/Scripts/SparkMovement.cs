using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("No player assigend to " + gameObject);
        }
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != player.transform.position)
        {
            transform.position = Vector3.Slerp(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
