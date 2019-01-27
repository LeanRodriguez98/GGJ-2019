using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public AudioClip leaveBox;
    public AudioClip pickUpBox;
    public AudioClip pickUpHeavyBox;

    public PlayerUI playerUI;

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
        HOLDING_ITEM,
        PICKING_UP
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
                if (Input.GetButton("Action") && pickUpTrigger.CanPickUpObj)
                {
                    state = PlayerState.PICKING_UP;
                }
                break;
            case PlayerState.HOLDING_ITEM:
                Movement(objectToPickUp.weight);// <------------ Change by the object weight
                DropItem();
                break;
            case PlayerState.PICKING_UP:
                PickUp();
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

                GameManager.GetInstance().PlayEffectSound(leaveBox);

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

        if (Input.GetButton("Action") && pickUpTrigger.CanPickUpObj)
        {

            Debug.Log("Pick up button");

            // ------ New pick up mechanic------ //
            objectToPickUp = pickUpTrigger.objToPickUp;

            if (!objectToPickUp.BeingPickedUp)
            {
                playerUI.EnableLoadingCircle(); 

                //objectToPickUp.BeingPickedUp = true;

                // disable item
                if (objectToPickUp.GetPickedUp())
                {
                    GameManager.GetInstance().PlayEffectSound(pickUpBox);
                    playerUI.ResetTimer();
                    objectToPickUp.gameObject.SetActive(false);
                    canPickUp = false;
                    state = PlayerState.HOLDING_ITEM;
                    pickUpTrigger.HideObjectInfo();
                    Debug.Log("Culo2");
                    animator.runtimeAnimatorController = controllerBox;
                }
            }
        }
        else if (Input.GetButtonUp("Action"))
        {
            if (objectToPickUp != null)
            {
                state = PlayerState.NOT_HOLDING_ITEM;
                objectToPickUp.BeingPickedUp = false;
                playerUI.ResetTimer();
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
                grabberPos.transform.localPosition = new Vector2(0, grabberOffsetY + .2f);

            }
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", 0);

            if (Input.GetAxis("Horizontal") < 0)
            {
                grabberPos.transform.localPosition = new Vector2(grabberOffsetX * -1, .1f);

            }
            else
            {
                grabberPos.transform.localPosition = new Vector2(grabberOffsetX, .1f);

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

