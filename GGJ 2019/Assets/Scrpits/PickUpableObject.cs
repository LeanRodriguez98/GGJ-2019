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

    [HideInInspector]
    public bool canPutDown;

    private PickUpTimer pickUpTimer;
    private bool pickedUp;
    private SpriteRenderer spriteR;

    private void Start()
    {
        pickUpTimer = FindObjectOfType<PickUpTimer>();
        pickedUp = false;
        canPutDown = true;
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!pickedUp && beingPickedUp)
        {
            //Debug.Log("Someone is picking me up!!!!!!");

            // start pick up timer based on timeToPickUp
            pickUpTimer.SetCurrentObj(this);
            pickUpTimer.EnableLoadingCircle();
        }

        if (canPutDown)
        {
            Debug.Log("YES");
            spriteR.color = Color.green;
        }
        else
        {
            Debug.Log("NO");
            spriteR.color = Color.red;
        }
    }

    public void PickedUp()
    {
        // Item is now being carried by the player
        pickedUp = true;
    }

    // -------------- PUT DOWN OBJECTS LEVEL ----------------- //
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") ||
            collision.gameObject.CompareTag("Object"))
        {
            canPutDown = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") ||
            collision.gameObject.CompareTag("Object"))
        {
            canPutDown = true;
        }
    }
}
