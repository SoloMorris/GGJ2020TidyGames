using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public static MissileManager instance;

    [SerializeField] private GameObject redTankBarrel;
    [SerializeField] private GameObject redSpawner;

    [SerializeField] private GameObject blueTankBarrel;
    [SerializeField] private GameObject blueSpawner;

    [SerializeField] private GameObject redMissile;
    [SerializeField] private GameObject blueMissile;

    private List<GameObject> redMissiles = new List<GameObject>();
    private List<GameObject> blueMissiles = new List<GameObject>();
    public int maxMissiles;
    
    [SerializeField]
    [Range (0.0f,10f)]
    private float missileSpeed;

    [SerializeField] private ParticleSystem firePuff;
    [SerializeField] private ParticleSystem bulletExplosion;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (maxMissiles / 2); i++)
        {
            redMissiles.Add(Instantiate(redMissile, transform));
            redMissiles[i].SetActive(false);
            blueMissiles.Add(Instantiate(blueMissile, transform));
            blueMissiles[i].SetActive(false);
            VFXManager.instance.AddParticleSystemToVFXList(firePuff, "fireBullet", 4);
            VFXManager.instance.AddParticleSystemToVFXList(bulletExplosion, "bulletExplode", 4);
            Debug.Log("1");
        }

    }

    public bool FireMissile(string colour)
    {
        if (colour == "red")
        {
            foreach (GameObject _missile in redMissiles)
            {
                if (!_missile.activeSelf)
                {
                    _missile.GetComponent<MissileController>().travelSpeed = missileSpeed;
                    _missile.transform.rotation = redTankBarrel.transform.rotation;
                    _missile.transform.position = redSpawner.transform.position;

                    _missile.GetComponent<MissileController>().target = "TankBlue";
                    _missile.GetComponent<MissileController>().direction = redTankBarrel.transform.up;
                    _missile.SetActive(true);

                    VFXManager.instance.PlayParticleSystemFromVFXList(redSpawner, "fireBullet", true, (redTankBarrel.transform.up * 0.75f));
                    return true;
                }
            }
        }
        else if (colour == "blue")
        {
            foreach (GameObject _missile in blueMissiles)
            {
                if (!_missile.activeSelf)
                {
                    _missile.GetComponent<MissileController>().travelSpeed = missileSpeed;
                    _missile.transform.rotation = blueTankBarrel.transform.rotation;
                    _missile.transform.position = blueSpawner.transform.position;

                    _missile.GetComponent<MissileController>().target = "TankRed";
                    _missile.GetComponent<MissileController>().direction = blueTankBarrel.transform.up;
                    _missile.SetActive(true);
                    VFXManager.instance.PlayParticleSystemFromVFXList(blueSpawner, "fireBullet", true, (blueTankBarrel.transform.up * 0.75f));
                    return true;
                }
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
