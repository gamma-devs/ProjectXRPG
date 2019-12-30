using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(-1*Input.GetAxis("Horizontal") * moveSpeed, 0f, -1*Input.GetAxis("Vertical") * moveSpeed);

        controller.Move(moveDirection);
    }
}
