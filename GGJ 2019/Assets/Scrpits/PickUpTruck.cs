using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTruck : MonoBehaviour
{
    public float truckWaitingTime = 60.0f;

    private bool aboutToLeave;
    private bool startTimer;
    private float timer;

    private Queue<PickUpableObject> pickedUpObjects;
    
	void Start ()
    {
        pickedUpObjects = new Queue<PickUpableObject>();
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

    void AddItem(PickUpableObject obj)
    {
        pickedUpObjects.Enqueue(obj);
    }

    PickUpableObject GetItem()
    {
        return pickedUpObjects.Dequeue();
    }
}
