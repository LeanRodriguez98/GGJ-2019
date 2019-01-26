using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpTrigger : MonoBehaviour
{
    public bool CanPickUpObj    { get; set; }
    public bool ObjPickedUp     { get; set; }

    [HideInInspector]
    public GameObject objToPickUp;

    void Start ()
    {
        CanPickUpObj = false;
        ObjPickedUp = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("CAN pick up");
            CanPickUpObj = true;

            objToPickUp = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("CANT pick up");
            CanPickUpObj = false;

            objToPickUp = null;
        }
    }
}
