using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickupFX;
    [SerializeField] private ParticleSystem ambientFX;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
       gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        VFXManager.instance.AddParticleSystemToVFXList(pickupFX, "coinPickup");

        VFXManager.instance.AddParticleSystemToVFXList(ambientFX, "coinAmbient");
    }

    // Update is called once per frame
    void Update()
    {
        VFXManager.instance.PlayParticleSystemFromVFXList(gameObject, "coinAmbient");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TankRed"))
        {
            gm.scores.x += 1;
            gm.noCoin = true;
            VFXManager.instance.PlayParticleSystemFromVFXList(gameObject, "coinPickup");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("TankBlue"))
        {
            gm.scores.y += 1;
            gm.noCoin = true;
            VFXManager.instance.PlayParticleSystemFromVFXList(gameObject, "coinPickup");
            Destroy(this.gameObject);
        }
    }
}
