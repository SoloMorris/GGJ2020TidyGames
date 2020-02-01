using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    //Singleton instance
    public static VFXManager instance;

    //Lists
    public List<VFX> vfxList = new List<VFX>();

    //Class to hold VFX
    public class VFX
    {
        public string name;
        public ParticleSystem effect;
        public GameObject instance;
        public Transform target;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Use this in Start to add a particle system to the pool.
    public void AddParticleSystemToVFXList(ParticleSystem _effect, string _name)
    {
        VFX newEffect = new VFX();

        newEffect.name = _name;
        newEffect.effect = Instantiate(_effect, transform);
        newEffect.instance = _effect.gameObject;
        newEffect.target = newEffect.instance.transform;
        newEffect.effect.Stop();
        newEffect.instance.SetActive(false);

        vfxList.Add(newEffect);
        Debug.Log("Particle system " + newEffect.name +" added!");
    }

    //Finds an inactive effect by name and attaches it to given target transform.
    public bool PlayParticleSystemFromVFXList(Transform _target, string _vfxName)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.position;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfx.name, _target);
                    }
                    return false;
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance.transform)
                {
                    _vfx.target = _target;
                    _vfx.instance.transform.position = _vfx.target.position;
                    _vfx.instance.SetActive(true);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    //Overload for Vector3 offset
    public bool PlayParticleSystemFromVFXList(Transform _target, string _vfxName, Vector3 _offset)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.position + _offset;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfx.name, _target);
                    }
                    return false;
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance.transform)
                {
                    _vfx.target = _target;
                    _vfx.instance.transform.position = _vfx.target.position + _offset;
                    _vfx.instance.SetActive(true);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    public void StopParticleSystem(string _name, Transform _target)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _name)
            {
                if (_vfx.target == _target)
                {
                    _vfx.effect.Stop();
                    _vfx.target = _vfx.instance.transform;
                    _vfx.instance.transform.position = Vector3.zero;
                    _vfx.instance.SetActive(false);
                }
            }
        }
    }
}
