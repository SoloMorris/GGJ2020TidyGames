using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public string target;
    public float travelSpeed = 0;
    public Vector2 direction;
    private float lifeTimer;
    [SerializeField] private float lifeDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = (direction * travelSpeed);
        if (lifeTimer >= lifeDuration)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit something");
        if (collision.collider.CompareTag(target))
        {
            Debug.Log("Hit " + target +" tank!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        VFXManager.instance.PlayParticleSystemFromVFXList(gameObject, "bulletExplode", true, true);
        ResetValues();
    }
    private void ResetValues()
    {
        transform.position = Vector2.zero;
        gameObject.SetActive(false);
        lifeTimer = 0;
    }
}
