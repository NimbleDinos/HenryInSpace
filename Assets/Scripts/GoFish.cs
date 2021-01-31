using System;
using System.Text;
using UnityEngine;

public class GoFish : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject bob;
    private CatchFishBob CatchFishBob;
    public GameObject defaultBobPosition;
    private bool isCast = false;

    public string fishKey = "f";

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        CatchFishBob = bob.GetComponent<CatchFishBob>();
    }

    void Update()
    {
        if (Input.GetKeyDown(fishKey) && !isCast)
        {
            isCast = true;

            Vector3 bobPosition = defaultBobPosition.transform.position;
            
            bob.transform.position = bobPosition;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, bobPosition);

            CatchFishBob.isPlayerFishing = true;
            CatchFishBob.PlayBubbles();
            
            return;
        }

        if (Input.anyKeyDown && isCast)
        {
            ResetLine();
            CatchFishBob.isPlayerFishing = false;
            CatchFishBob.StopBubbles();
        }
    }
    
    public void ResetLine()
    {
        isCast = false;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
        bob.transform.position = transform.position;
    }
}