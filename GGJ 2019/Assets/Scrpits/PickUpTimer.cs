using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTimer : MonoBehaviour
{
    private Image loadingBarImage;
    private float pickUpTime;
    private bool startTimer;

    private float timerTest = 0;

    private PickUpableObject currentObj;
    public void SetCurrentObj(PickUpableObject obj) { currentObj = obj; }

    private void Start()
    {
        loadingBarImage = GetComponent<Image>();
        loadingBarImage.fillAmount = 0;
        pickUpTime = 0;
        startTimer = false;
    }

    private void Update()
    {
        if (currentObj != null)
        {
            if (!currentObj.beingPickedUp)
            {
                loadingBarImage.fillAmount = 0;
            }
        }
    }

    public void EnableLoadingCircle()
    {
        if (loadingBarImage.fillAmount != 1)
        {
            //Debug.Log("Time: " + timerTest);
            timerTest += Time.deltaTime;

            loadingBarImage.fillAmount += Time.deltaTime / currentObj.timeToPickUp;
        }
        else
        {
            Debug.Log("Object Picked Up!");
            currentObj.PickedUp();
        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }
}

