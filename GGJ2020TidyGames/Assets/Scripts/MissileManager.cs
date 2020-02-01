using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public static MissileManager instance;
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
            blueMissiles.Add(Instantiate(blueMissile, transform));
        }
    }

    public void FireMissile(string colour)
    {
        if (colour == "red")
        {
            foreach (GameObject _missile in redMissiles)
            {
                if (!_missile.activeSelf)
                {
                    //_missile.transform.position;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
