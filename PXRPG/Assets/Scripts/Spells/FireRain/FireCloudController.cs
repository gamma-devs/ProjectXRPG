using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Add a reference to this object inside the FireRain spell.
 */
public class FireCloudController : MonoBehaviour
{
    private PlayerController player;
    private GameObject fireParticles;
    public float height;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z);
    }

    // Move the cloud in player start position.
    void Update()
    {
        
    }
}
