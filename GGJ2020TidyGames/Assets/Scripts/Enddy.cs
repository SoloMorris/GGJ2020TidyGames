using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enddy : MonoBehaviour
{

    private GameManager gm;

    public GameObject[] screens = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.scores.x > gm.scores.y)
        {
            screens[0].SetActive(true);
            screens[1].SetActive(false);
            screens[2].SetActive(false);
        }
        else if (gm.scores.x < gm.scores.y)
        {
            screens[0].SetActive(false);
            screens[1].SetActive(true);
            screens[2].SetActive(false);
        }
        else
        {
            screens[0].SetActive(false);
            screens[1].SetActive(false);
            screens[2].SetActive(true);
        }
    }
}
