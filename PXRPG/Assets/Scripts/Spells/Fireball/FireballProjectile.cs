using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.6f, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
