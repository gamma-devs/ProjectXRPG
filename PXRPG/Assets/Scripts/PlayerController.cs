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

    private Inventory inventory; //static?

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (inventory == null) {
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

    }

    public Inventory getInventory()
    {
        return inventory;
    }
}
