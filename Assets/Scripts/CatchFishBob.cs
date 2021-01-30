using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatchFishBob : MonoBehaviour
{
    public bool isPlayerFishing = false;
    public ParticleSystem bubbleMachine;
    public GoFish goFish;

    public int catchPeriod;
    private float startTime;
    private int waitPeriod;
    private bool timeWaited;
    private bool isFishReady = false;
    
    // Start is called before the first frame update
    void Start()
    {
        bubbleMachine.Stop();
    }

    public void PlayBubbles()
    {
        bubbleMachine.Play();
    }

    public void StopBubbles()
    {
        bubbleMachine.Stop();
        bubbleMachine.Clear();
    }

    private void NormalBubbles()
    {
        var particle = bubbleMachine.main;
        particle.startSpeed = 2;
        particle.maxParticles = 25;

        var bubbleScale = bubbleMachine.shape;
        bubbleScale.scale = new Vector3(1, 1, 0.5f);
    }

    private void MegaBubbles()
    {
        var particle = bubbleMachine.main;
        particle.startSpeed = 10;
        particle.maxParticles = 200;
        
        var bubbleScale = bubbleMachine.shape;
        bubbleScale.scale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        startTime = Time.realtimeSinceStartup;
        waitPeriod = Random.Range(1, 10);
        timeWaited = false;
        NormalBubbles();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("FishingSpot") || !isPlayerFishing) return;
        
        if (!isFishReady)
        {
            if ((!(Time.realtimeSinceStartup >= startTime + waitPeriod)) || timeWaited) return;
            
            timeWaited = true;
            isFishReady = true;
            MegaBubbles();
        }
        else
        {
            if (Time.realtimeSinceStartup <= startTime + waitPeriod + catchPeriod)
            {
                if (Input.GetKeyDown(goFish.fishKey)) 
                {
                    //TODO: Spawn fish
                    Debug.Log("FIIIIIIIIIIISH!!!!");
                    isFishReady = false;
                }
            }
            else
            {
                StopBubbles();
                goFish.ResetLine();
                isFishReady = false;
            }
        }
    }
}
