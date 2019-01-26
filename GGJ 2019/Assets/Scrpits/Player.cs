﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public Transform holdItemPos;

    private PlayerPickUpTrigger pickUpTrigger;
    private Vector2 position;
    private Animator animator;
    private bool canPickUp;

    private GameObject objectToPickUp;
    public void SetCurrentObj(GameObject obj)
    {
        objectToPickUp = obj;
    }

    void Start ()
    {
        objectToPickUp = null;
        pickUpTrigger = GetComponentInChildren<PlayerPickUpTrigger>();
        position = transform.position;
        animator = GetComponent<Animator>();
        canPickUp = pickUpTrigger.CanPickUpObj;
    }

    void Update ()
    {
        Movement();
        PickUp();

    }

    private void PickUp()
    {
        canPickUp = pickUpTrigger.CanPickUpObj;

        if (Input.GetButtonDown("Action") && canPickUp)
        {
            Debug.Log("Pick up button");
            objectToPickUp = pickUpTrigger.objToPickUp;
            objectToPickUp.transform.SetParent(holdItemPos);
            objectToPickUp.transform.localPosition = Vector3.zero;
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            position.y += movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            animator.SetFloat("Horizontal", 0);

        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            position.x += movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical",0);

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

        if (transform.position.x != position.x || transform.position.y != position.y)
        {
            transform.position = position;
        }

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

