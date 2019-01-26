using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : MonoBehaviour
{
    public int happiness = 5;
    public float timeToPickUp = 5f;
    public Sprite objectImage;

    [HideInInspector]
    public bool beingPickedUp;

    private PickUpTimer pickUpTimer;

    private void Start()
    {
        pickUpTimer = FindObjectOfType<PickUpTimer>();
    }

    private void Update()
    {
        if (beingPickedUp)
        {
            //Debug.Log("Someone is picking me up!!!!!!");

            // start pick up timer based on timeToPickUp
            pickUpTimer.SetCurrentObj(this);
            pickUpTimer.EnableLoadingCircle();
        }
    }
}
