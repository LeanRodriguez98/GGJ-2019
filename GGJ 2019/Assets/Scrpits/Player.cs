using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour {

    public RuntimeAnimatorController controller;
    public RuntimeAnimatorController controllerBox;

    public GameObject grabberPos;
    public float grabberOffsetX = 0.15f;
    public float grabberOffsetY = 0.1f;

    private PickUpTruck truck;

    private Animator animator;

    public float movementSpeed;
    private PickUpableObject objectToPickUp;

    private PlayerPickUpTrigger pickUpTrigger;
    private Vector2 movement;
    private bool canPickUp;

    private Rigidbody2D rb;

    public enum PlayerState
    {
        NOT_HOLDING_ITEM,
        HOLDING_ITEM
    }
    private PlayerState state;


    void Start ()
    {
        truck = FindObjectOfType<PickUpTruck>();

        grabberPos.transform.localPosition = new Vector2(0, grabberOffsetY * -1);

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;

        objectToPickUp = null;

        pickUpTrigger = GetComponentInChildren<PlayerPickUpTrigger>();
        movement = transform.position;

        canPickUp = pickUpTrigger.CanPickUpObj;
        rb = GetComponent<Rigidbody2D>();

        // Set default state
        state = PlayerState.NOT_HOLDING_ITEM;
    }

    void FixedUpdate ()
    {
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        switch(state)
        {
            case PlayerState.NOT_HOLDING_ITEM:
                Movement(1);
                PickUp();
                break;
            case PlayerState.HOLDING_ITEM:
                Movement(1);// <------------ Change by the object weight
                DropItem();
                break;
        }
    }

    private void DropItem()
    {
        if (pickUpTrigger.CanDropOnTruck)
        {
            if (Input.GetButtonDown("Action"))
            {
                Debug.Log("Item dropped on truck");

                // push item into truck
                truck.AddItem(objectToPickUp);
                objectToPickUp = null;

                state = PlayerState.NOT_HOLDING_ITEM;
                animator.runtimeAnimatorController = controller;
            }
        }
    }

    private void PickUp()
    {
        canPickUp = pickUpTrigger.CanPickUpObj;

        if (Input.GetButton("Action") && canPickUp)
        {
            Debug.Log("Pick up button");

            // ------ New pick up mechanic------ //
            objectToPickUp = pickUpTrigger.objToPickUp;
            objectToPickUp.beingPickedUp = true;

            // disable item
            if (objectToPickUp.GetPickedUp())
            {
                objectToPickUp.gameObject.SetActive(false);
                canPickUp = false;
                state = PlayerState.HOLDING_ITEM;
                Debug.Log("Culo");
                animator.runtimeAnimatorController = controllerBox;
            }
        }
        else if (Input.GetButtonUp("Action"))
        {
            if (objectToPickUp != null)
            {
                objectToPickUp.beingPickedUp = false;
            }
        }
    }
    
    private void Movement(float weight)
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            animator.SetFloat("Horizontal", 0);
            if (Input.GetAxis("Vertical") < 0)
            {
                grabberPos.transform.localPosition = new Vector2(0, grabberOffsetY * -1);
            }
            else
            {
                grabberPos.transform.localPosition = new Vector2(0, grabberOffsetY);

            }
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", 0);

            if (Input.GetAxis("Horizontal") < 0)
            {
                grabberPos.transform.localPosition = new Vector2(grabberOffsetX * -1, 0);

            }
            else
            {
                grabberPos.transform.localPosition = new Vector2(grabberOffsetX, 0);

            }
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("NoMovement", true);
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        }
        else
        {
            animator.SetBool("NoMovement", false);
        }


        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(x, y);
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
        rb.velocity = (movement * movementSpeed * Time.fixedDeltaTime) / weight;
    

        /*  if (Mathf.Abs(Input.GetAxis("RightHorizontal")) < Mathf.Abs(Input.GetAxis("RightVertical")))
          {
              if (Input.GetAxis("RightVertical") < 0)
              {
                  Debug.Log("up");
                  animator.SetFloat("Vertical", Input.GetAxis("RightVertical"));
              }
              else if (Input.GetAxis("RightVertical") > 0)
              {
                  Debug.Log("Down");
                  animator.SetFloat("Vertical", Input.GetAxis("RightVertical"));

              }
          }
          else if (Input.GetAxis("RightHorizontal") > 0)
          {
                  Debug.Log("right");
                  animator.SetFloat("Horizontal", Input.GetAxis("RightHorizontal"));

          }
          else if (Input.GetAxis("RightHorizontal") < 0)
          {
                  Debug.Log("left");
              animator.SetFloat("Horizontal", Input.GetAxis("RightHorizontal"));

          }*/
    }
}

