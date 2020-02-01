using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVFXScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        VFXManager.instance.AddParticleSystemToVFXList(effect, "test");
    }

    // Update is called once per frame
    void Update()
    {
        VFXManager.instance.PlayParticleSystemFromVFXList(transform, "test");
    }
}
