using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTruck : MonoBehaviour
{
    public Text scoreText;
    public float truckWaitingTime = 60.0f;

    private bool aboutToLeave;
    private bool startTimer;
    private float timer;
    public int score = 0;
    public List<PickUpableObject> pickedUpObjects = new List<PickUpableObject>();
    
	void Start ()
    {
        score = 0;
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
        score += obj.happiness;
        scoreText.text = "Happiness: " + score.ToString("0");
    }
}
