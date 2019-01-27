using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTimeTest : MonoBehaviour
{
    private PickUpableObject currentObj;
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            PickUpObject();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (currentObj != null)
            {
                currentObj.BeingPickedUp = false;
            }
        }
    }

    void PickUpObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            //Debug.Log("Object: " + hit.collider.name);

            currentObj = hit.collider.GetComponent<PickUpableObject>();
            currentObj.BeingPickedUp = true;
        }
    }
}
