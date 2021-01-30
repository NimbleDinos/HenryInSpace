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
    public GameObject fishPrefab;

    public int catchPeriod;
    public float fishVelocity = 100f;
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
        particle.startLifetime = 3;
        particle.duration = 3;

        var bubbleScale = bubbleMachine.shape;
        bubbleScale.scale = new Vector3(1, 1, 0.5f);
        
        var sol = bubbleMachine.sizeOverLifetime;
        sol.xMultiplier = 1f;
        sol.yMultiplier = 1f;
        sol.zMultiplier = 1f;
    }

    private void MegaBubbles()
    {
        var particle = bubbleMachine.main;
        particle.startSpeed = 5;
        particle.maxParticles = 100;

        var bubbleScale = bubbleMachine.shape;
        bubbleScale.scale = new Vector3(1, 1, 0.5f);

        var sol = bubbleMachine.sizeOverLifetime;
        sol.xMultiplier = 2f;
        sol.yMultiplier = 2f;
        sol.zMultiplier = 2f;
    }

    private void SpawnFish()
    {
        Debug.Log(("Spawn fish"));
        GameObject obj = Instantiate(fishPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(GameObject.FindGameObjectWithTag("Player").transform.forward * fishVelocity + (new Vector3(0, 0.65f, 0) * fishVelocity));
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
                    Debug.Log("FIIIIIIIIIIISH!!!!");
                    SpawnFish();
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
