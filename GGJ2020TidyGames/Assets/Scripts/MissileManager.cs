using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public static MissileManager instance;

    [SerializeField] private Transform redTankBarrel;
    [SerializeField] private Transform redSpawner;

    [SerializeField] private Transform blueTankBarrel;
    [SerializeField] private Transform blueSpawner;

    [SerializeField] private GameObject redMissile;
    [SerializeField] private GameObject blueMissile;

    private List<GameObject> redMissiles = new List<GameObject>();
    private List<GameObject> blueMissiles = new List<GameObject>();
    public int maxMissiles;
    [SerializeField] private float missileSpeed;

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
            //blueMissiles.Add(Instantiate(blueMissile, transform));
            //blueMissiles[i].SetActive(false);

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
                    _missile.transform.rotation = redTankBarrel.rotation;
                    _missile.transform.position = redSpawner.position;
                    _missile.GetComponent<MissileController>().target = "blue";
                    _missile.GetComponent<MissileController>().direction = redTankBarrel.up;
                    _missile.SetActive(true);
                    VFXManager.instance.PlayParticleSystemFromVFXList(redSpawner, "bulletLaunch");
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
                    _missile.transform.rotation = blueTankBarrel.rotation;
                    _missile.transform.position = blueSpawner.position;
                    _missile.GetComponent<MissileController>().target = "red";
                    _missile.SetActive(true);
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
