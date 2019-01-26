using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTruck : MonoBehaviour
{
    public float truckWaitingTime = 60.0f;

    private bool aboutToLeave;
    private bool startTimer;
    private float timer;

    public List<PickUpableObject> pickedUpObjects = new List<PickUpableObject>();
    
	void Start ()
    {
        pickedUpObjects = new List<PickUpableObject>();
        timer = 0;
	}
	
	void Update ()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer > truckWaitingTime)
            {
                // truck leaves
            }
        }
	}

    public void StartTimer()
    {
        startTimer = true;
    }

    public void AddItem(PickUpableObject obj)
    {
        pickedUpObjects.Add(obj);
    }
}
