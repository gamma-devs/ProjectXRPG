using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCloudController : MonoBehaviour
{
    const float MAX_HEIGHT = 2.0f;
    Vector3 ogPos;
    float offset;
    PlayerController player;
    Rigidbody rb;
    float speed;
    Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0.0f;
        speed = 2.0f;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y-0.6f, player.transform.position.z);
        ogPos = transform.position;
        moveDirection = new Vector3(0.0f, speed, 0.0f);
    }

    // Update is called once per frame
    /*
     *  When moving the cloud, don't use transform.position = ... because that teleports the cloud up a few steps
     *  and makes the playerController shake. Use the rigidbody velocity instead.
     */
    void Update()
    {
        if (transform.position.y - ogPos.y < MAX_HEIGHT)
        {
            rb.MovePosition(transform.position + new Vector3(0, 0.01f, 0)*speed);
            //offset += 0.1f*Time.deltaTime;
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, ogYPos + MAX_HEIGHT, transform.position.z);
            rb.MovePosition(ogPos + new Vector3(0, MAX_HEIGHT, 0));
            enabled = false;
        }
    }
}
