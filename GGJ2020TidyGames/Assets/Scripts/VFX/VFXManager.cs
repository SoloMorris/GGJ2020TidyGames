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
        public GameObject target;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        foreach (VFX item in vfxList)
        {
            if (!item.effect.isPlaying && item.target != item.instance)
            {
                StopParticleSystem(item.name, item.target);
            }
        }
    }
    //Use this in Start to add a particle system to the pool.
    public void AddParticleSystemToVFXList(ParticleSystem _effect, string _name)
    {
        VFX newEffect = new VFX();

        newEffect.name = _name;
        newEffect.effect = Instantiate(_effect, transform);
        newEffect.instance = newEffect.effect.gameObject;
        newEffect.target = newEffect.instance;
        newEffect.effect.Stop();

        vfxList.Add(newEffect);
        vfxList[vfxList.Count - 1].instance.SetActive(false);
        Debug.Log("Particle system " + vfxList[vfxList.Count-1].name +" added!");
    }

    public void AddParticleSystemToVFXList(ParticleSystem _effect, string _name, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            VFX newEffect = new VFX();

            newEffect.name = _name;
            newEffect.effect = Instantiate(_effect, transform);
            newEffect.instance = newEffect.effect.gameObject;
            newEffect.target = newEffect.instance;
            newEffect.effect.Stop();

            vfxList.Add(newEffect);
            vfxList[vfxList.Count - 1].instance.SetActive(false);
            Debug.Log("Particle system " + vfxList[vfxList.Count - 1].name + " added!");
        }
    }
    //Finds an inactive effect by name and attaches it to given target transform.
    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, string ignorerot)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.TransformDirection(_target.transform.position);
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfx.name, _target);
                    }
                    else
                        return false;

                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    if (ignorerot == " ")
                    {
                        _vfx.instance.transform.rotation = _vfx.target.transform.rotation;
                    }
                    _vfx.instance.transform.position = _vfx.target.transform.InverseTransformPoint(_vfx.target.transform.position);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }
    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.TransformDirection(_target.transform.position);
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfx.name, _target);
                    }
                    else
                        return false;

                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    _vfx.instance.transform.rotation = _vfx.target.transform.rotation;
                    _vfx.instance.transform.position = _vfx.target.transform.TransformDirection(_vfx.target.transform.position);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    //Overload for Vector3 offset
    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, Vector3 _offset)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.position + _offset;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfx.name, _target);
                    }
                    else
                        return false;
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    _vfx.instance.transform.rotation = new Quaternion(_vfx.instance.transform.rotation.x, _vfx.instance.transform.rotation.y, _vfx.target.transform.rotation.z, _vfx.instance.transform.rotation.w);
                    _vfx.instance.transform.position = (_vfx.target.transform.InverseTransformPoint(_vfx.target.transform.position) + _offset);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, bool ignoreActiveEffects)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.position;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfxName, _target);
                    }
                    else if (!ignoreActiveEffects)
                    {
                        return false;
                    }
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    _vfx.instance.transform.rotation = _vfx.target.transform.rotation;
                    _vfx.instance.transform.position = _vfx.target.transform.position;
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, bool ignoreActiveEffects, bool inverseVFXRotation)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.position;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfxName, _target);
                    }
                    else if (!ignoreActiveEffects)
                    {
                        return false;
                    }
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    //_vfx.instance.transform.rotation = Quaternion.Inverse(_vfx.target.transform.rotation);
                    _vfx.instance.transform.rotation = new Quaternion(_vfx.instance.transform.rotation.x, _vfx.instance.transform.rotation.y, _vfx.target.transform.rotation.z, _vfx.instance.transform.rotation.w);
                    _vfx.instance.transform.position = _vfx.target.transform.position;
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }


    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, bool ignoreActiveEffects, Vector3 _offset)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                print("Found tank " + _target.name);
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = _target.transform.position + _offset;
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfxName, _target);
                    }
                    else if (!ignoreActiveEffects)
                    {
                        return false;
                    }
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                    _vfx.instance.transform.rotation = new Quaternion(_vfx.instance.transform.rotation.x, _vfx.instance.transform.rotation.y, _vfx.target.transform.rotation.z, _vfx.instance.transform.rotation.w);
                    _vfx.instance.transform.position = _vfx.target.transform.position + _offset;
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }

    public bool PlayParticleSystemFromVFXList(GameObject _target, string _vfxName, bool ignoreActiveEffects, bool inverseVFXRotation, Vector3 offset)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _vfxName)
            {
                if (_vfx.target == _target)
                {
                    _vfx.instance.transform.position = (_vfx.target.transform.position + offset);
                    if (!_vfx.effect.isPlaying)
                    {
                        StopParticleSystem(_vfxName, _target);
                    }
                    else if (!ignoreActiveEffects)
                    {
                        return false;
                    }
                }

                //Checks if the vfx's target is itself -- i.e it's not used anywhere else
                if (_vfx.target == _vfx.instance)
                {
                    _vfx.target = _target;
                    _vfx.instance.SetActive(true);
                        //_vfx.instance.transform.rotation = Quaternion.Inverse(_vfx.target.transform.rotation);
                        _vfx.instance.transform.rotation = new Quaternion(_vfx.instance.transform.rotation.x, _vfx.instance.transform.rotation.y, _vfx.target.transform.rotation.z, _vfx.instance.transform.rotation.w);

                    _vfx.instance.transform.position = (_vfx.target.transform.position + offset);
                    _vfx.effect.Play();
                    return true;
                }
            }
        }
        return false;
    }
    public bool StopParticleSystem(string _name, GameObject _target)
    {
        foreach (VFX _vfx in vfxList)
        {
            if (_vfx.name == _name)
            {
                if (_vfx.target == _target)
                {
                    _vfx.effect.Stop();
                    _vfx.target = _vfx.instance;
                    _vfx.instance.transform.position = Vector3.zero;
                    _vfx.instance.SetActive(false);
                    return true;
                }
            }
        }
        return false;
    }
}
