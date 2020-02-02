using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
       gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TankRed"))
        {
            gm.scores.x += 1;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("TankBlue"))
        {
            gm.scores.y += 1;
            Destroy(this.gameObject);
        }
    }
}
