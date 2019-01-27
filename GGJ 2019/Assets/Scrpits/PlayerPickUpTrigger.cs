using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpTrigger : MonoBehaviour
{
    public bool CanPickUpObj    { get; set; }
    public bool ObjPickedUp     { get; set; }
    public bool CanDropOnTruck  { get; set; }

    [HideInInspector]
    public PickUpableObject objToPickUp;

    void Start ()
    {
        CanPickUpObj = false;
        ObjPickedUp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Truck"))
        {
            Debug.Log("CAN drop on truck");
            CanDropOnTruck = true;
            CanPickUpObj = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("CAN pick up");
            CanPickUpObj = true;

            objToPickUp = collision.gameObject.GetComponent<PickUpableObject>();

			objToPickUp.SetObjectForPlayerUI(objToPickUp);
			DisplayObjectInfo();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("CANT pick up");
            CanPickUpObj = false;

			HideObjectInfo();
			objToPickUp = null;
        }

        if (collision.gameObject.CompareTag("Truck"))
        {
            Debug.Log("CANT drop on truck");
            CanDropOnTruck = false;
        }
    }

	void DisplayObjectInfo()
	{
		if (objToPickUp != null)
			objToPickUp.DisplayObjectInfo();
	}

	void HideObjectInfo()
	{
		if (objToPickUp != null)
			objToPickUp.HideObjectInfo();
	}
}
