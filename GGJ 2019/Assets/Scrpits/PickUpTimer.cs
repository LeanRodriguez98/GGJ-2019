using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTimer : MonoBehaviour
{
    public Image pickingUpTimer;

    private float pickUpTime;
    private bool startTimer;

    private float timerTest = 0;

    private PickUpableObject currentObj;
    public void SetCurrentObj(PickUpableObject obj) { currentObj = obj; }

    private void Start()
    {
        pickingUpTimer = GetComponent<Image>();
        pickingUpTimer.fillAmount = 0;
        pickUpTime = 0;
        startTimer = false;
    }

    private void Update()
    {
        if (currentObj != null)
        {
            if (!currentObj.beingPickedUp)
            {
                pickingUpTimer.fillAmount = 0;
            }
        }
    }


    public void EnableLoadingCircle()
    {
        if (pickingUpTimer.fillAmount != 1)
        {
            //Debug.Log("Time: " + timerTest);
            timerTest += Time.deltaTime;

            pickingUpTimer.fillAmount += Time.deltaTime / currentObj.timeToPickUp;
        }
        else
        {
            Debug.Log("Object Picked Up!");
        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }
}

