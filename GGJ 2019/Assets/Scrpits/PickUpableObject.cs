using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : MonoBehaviour
{
    public int happiness = 5;
    public float weight = 5f;
    public float timeToPickUp = 5f;
    public Sprite objectImage;

    public bool BeingPickedUp { get; set; }

    [HideInInspector]
    public bool canPutDown;

    //private PlayerUI playerUI;
    private bool pickedUp;
    private SpriteRenderer spriteR;

    private void Start()
    {
       // playerUI = FindObjectOfType<PlayerUI>();
        pickedUp = false;
        canPutDown = true;
        spriteR = GetComponent<SpriteRenderer>();
        BeingPickedUp = false;
    }

    private void Update()
    {
        if (!pickedUp && BeingPickedUp)
        {
            //Debug.Log("Someone is picking me up!!!!!!");

            // start pick up timer based on timeToPickUp
            //playerUI.SetCurrentObj(this);
            //playerUI.EnableLoadingCircle();
        }

        //if (canPutDown)
        //{
        //    Debug.Log("YES");
        //    spriteR.color = Color.green;
        //}
        //else
        //{
        //    Debug.Log("NO");
        //    spriteR.color = Color.red;
        //}
    }

	//public void SetObjectForPlayerUI(PickUpableObject obj)
	//{
	//	playerUI.SetCurrentObj(obj);
	//}

    public void PickedUp()
    {
		// Item is now being carried by the player
		//HideObjectInfo();
        //playerUI.ResetTimer();
        pickedUp = true;
    }

    public bool GetPickedUp()
    {
        return pickedUp;
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
            Invoke("OffsetExitTimer", 0.05f);
        }
    }

    public void OffsetExitTimer()
    {
        canPutDown = true;
    }

	//public void DisplayObjectInfo()
	//{
	//	playerUI.DisplayInfo();
	//}
    //
	//public void HideObjectInfo()
	//{
	//	playerUI.HideInfo();
	//}
}
