using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector2 position;
    public float movementSpeed;

	void Start () {
        position = transform.position;
	}

    void Update () {
        Movement();

    }

    private void Movement()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            position.y += movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            position.x += movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        }

        if (transform.position.x != position.x || transform.position.y != position.y)
        {
            transform.position = position;
        }

        if (Input.GetAxis("RightVertical") > 0)
        {

        }

        if (Input.GetAxis("RightVertical") < 0)
        {

        }

        if (Input.GetAxis("RightHorizontal") > 0)
        {

        }

        if (Input.GetAxis("RightHorizontal") < 0)
        {

        }
    }
}
