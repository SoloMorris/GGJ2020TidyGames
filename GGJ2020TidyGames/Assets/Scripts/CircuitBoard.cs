﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum button
{
    DEFAULT = -1,
    MOVEMENT = 0,
    RELOAD = 1,
    SHOOT = 2,
    DASH = 3
}

public class CircuitBoard : MonoBehaviour
{

    public int[] maxCounts = new int[4];

    private int[] buttonHealth = new int[4];

    private bool[] buttonAlive = new bool[4];

    private float[] lastDamageTick = new float[4];
    [SerializeField]
    private float[] damageDelay = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonHealth[i] = maxCounts[i];
            //buttonHealth[i] = 0;
            lastDamageTick[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AliveUpdate();
    }

    private void FixedUpdate()
    {
        DamageOverTime();
    }

    public void AliveUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            if (buttonHealth[i] < 0)
            {
                buttonHealth[i] = 0;
            }

            if (buttonHealth[i] == 0)
            {
                buttonAlive[i] = false;
            }
            else
            {
                buttonAlive[i] = true;
            }
        }
    }

    public void DamageButton(button target, int damage)
    {
        if (buttonAlive[(int)target])
        {
            buttonHealth[(int)target] -= damage;
            Debug.Log(target + " takes damage");
        }
    }

    public bool RepairButton(button target)
    {
        if (buttonHealth[(int)target] != maxCounts[(int)target])
        {
            buttonHealth[(int)target]++;
            lastDamageTick[(int)target] = Time.time;
            return true;
        }
        return false;
    }

    private void DamageOverTime()
    {
        for (int i = 0; i < 4; i++)
        {
            if (buttonAlive[i] && lastDamageTick[i]+damageDelay[i] < Time.time)
            {
                DamageButton((button)i, 1);
                lastDamageTick[i] = Time.time;
            }
        }
    }
}
