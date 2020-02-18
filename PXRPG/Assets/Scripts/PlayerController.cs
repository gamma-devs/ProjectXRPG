﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    Camera sceneCamera;

    private Inventory inventory; //static?

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (inventory == null) {
            Debug.Log("Inventory created");
            inventory = new Inventory();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(-1*Input.GetAxis("Horizontal") * moveSpeed, 0f, -1*Input.GetAxis("Vertical") * moveSpeed);

        //Gravity
        moveDirection.y = moveDirection.y + (gravityScale * Physics.gravity.y);
        controller.Move(moveDirection * Time.deltaTime);

        // Z brings up the spell menu.
        if(Input.GetKeyDown("z"))
        {
            Time.timeScale = 0.05f;
            inventory.bringUpSpellMenu();
        }
        if (Input.GetKeyUp("z")) {
            Time.timeScale = 1;
            inventory.quitSpellMenu();
        }
        transform.rotation = Camera.main.transform.rotation;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

        //Vector3 forward = Camera.main.transform.forward;
        //forward.y = 0;
        //forward.Normalize();
        //Vector3 right = Camera.main.transform.right;
        //right.y = 0;
        //right.Normalize();
        //transform.position += Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * right + Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed * forward;

    }

    public Inventory getInventory()
    {
        return inventory;
    }
}
