﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private int layer;
    public int[] maxCounts = new int[4];

    [SerializeField]
    private int[] buttonHealth = new int[4];

    private bool[] buttonAlive = new bool[4];

    private float[] lastDamageTick = new float[4];
    [SerializeField]
    private float[] damageDelay = new float[4];

    public Slider[] displays = new Slider[4];

    [SerializeField] private ParticleSystem buttonDeath;
    [SerializeField] private ParticleSystem buttonDeathAmbient;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        for (int i = 0; i < 4; i++)
        {
            ResetBoard();
            VFXManager.instance.AddParticleSystemToVFXList(buttonDeath, "killButton" + layer.ToString());
            VFXManager.instance.AddParticleSystemToVFXList(buttonDeathAmbient, "deadButton" + layer.ToString());
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonHealth[i] = maxCounts[i];
            displays[i].maxValue = maxCounts[i];
            lastDamageTick[i] = 0;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.state == gameState.GAME_PLAY)
        {
            AliveUpdate();
            DisplayUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (gm.state == gameState.GAME_PLAY)
        {
            DamageOverTime();
        }
    }

    public void DisplayUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            displays[i].value = buttonHealth[i];
        }
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
                VFXManager.instance.PlayParticleSystemFromVFXList(displays[i].gameObject, "killButton" + layer.ToString(), "ye");
                VFXManager.instance.PlayParticleSystemFromVFXList(displays[i].gameObject, "deadButton" + layer.ToString(), "ye");
                buttonAlive[i] = false;
            }
            else
            {
                VFXManager.instance.StopParticleSystem("killButton" + layer.ToString(), displays[i].gameObject);
                VFXManager.instance.StopParticleSystem("deadButton" + layer.ToString(), displays[i].gameObject);
                buttonAlive[i] = true;
            }
        }
    }

    public void DamageButton(button target, int damage)
    {
        if (buttonAlive[(int)target])
        {
            buttonHealth[(int)target] -= damage;
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

    public bool CheckButton(button target)
    {
        return buttonAlive[(int)target];
    }
}
