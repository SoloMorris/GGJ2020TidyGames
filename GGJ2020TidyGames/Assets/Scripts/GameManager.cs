using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ParticleSystem firePuff;
    void Start()
    {
        VFXManager.instance.AddParticleSystemToVFXList(firePuff, "bulletLaunch");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            MissileManager.instance.FireMissile("red");
        }
    }
    private void FixedUpdate()
    {
        
    }
}
