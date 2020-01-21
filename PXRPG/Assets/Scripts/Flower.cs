using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerName
{
    Fire,
    Ice,
    Jump
};

/*
 * The Flower parent class which all flower types will inherit from.
 * Contains an AI for the flowers so they will follow the player when player is close enough etc.
 */
public class Flower : MonoBehaviour
{
    static protected GameObject player;
    protected bool sleep; //sleep = true if player still hasn't found pikmin.
    //Radius that player needs to be into to wake pikmin up.
    public float radius;
    private float moveRadius;


    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sleep = true;
        radius = 3f;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!sleep)
        {
            //follow player
            transform.position = new Vector3 (player.transform.position.x-0.5f, transform.position.y, player.transform.position.z - 0.3f);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("HEj");
            Debug.Log(sleep);
            this.sleep = false;
        }
    }


    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
