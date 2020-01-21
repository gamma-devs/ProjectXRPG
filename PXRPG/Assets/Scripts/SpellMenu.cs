using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  The menu you bring up to then perform a spell.
 */
public class SpellMenu : MonoBehaviour
{
    private Spell[] availableSpells;
    PlayerController player;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //availableSpells = generateSpells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     *  The spells are generated inside Inventory class whenever a new pikmin is
     *  collected or lost. This function retrieves the list of available spells from the Inventory,
     *  sorts them according to some criteria, and finally shows them on screen.
     */
    private void generateMenu()
    {
        availableSpells = player.getInventory().getAvailableSpells();
        //TODO: Sort the list according to some criteria, as default sort them according to rarity.

        //Show the spells on screen in a circle around the player.
        Vector3 playerPosition = player.transform.position;
        playerPosition = camera.WorldToScreenPoint(playerPosition);
        float degree = 360 / GameVariables.TOTAL_NR_OF_SPELLS;
        //Spell with id=1 is drawn on top.
        float posAbovePlayer = playerPosition.y + 0.5f;
        float startDegree = 0.0f;

        /*
         *  for i = 0 to TOTAL_NR_OF_SPELLS;      
         *  Draw the first spell at posAbovePlayer*cos(startDegree + degree*i)
         */
    }
}
