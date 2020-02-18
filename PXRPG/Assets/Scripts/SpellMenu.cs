﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 *  The menu you bring up to then perform a spell.
 */
public class SpellMenu : MonoBehaviour
{
    PlayerController player;
    Camera camera;
    Canvas menuCanvas; //The canvas you draw on.
    GameObject spellMenu;
    /*
     *  The reason we save an availableSpells list in Spell menu is because
     *  we want this when navigating through the UI in order to show the
     *  current selected spell information such as name and description.
     */
    List<Spell> currAvailableSpells;
    public GameObject title, description; //The texts in canvas.

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spellMenu = GameObject.FindGameObjectWithTag("SpellMenu");

    }

    /*
     *  Use the Update function to control the UI.
     */
    void Update()
    {
        
    }

    /*
     *  The spells are generated inside Inventory class whenever a new pikmin is
     *  collected or lost. This function retrieves the list of available spells from the Inventory,
     *  sorts them according to some criteria, and finally shows them on screen.
     */
    public void generateMenu()
    {
        List<Spell> availableSpells = player.getInventory().getAvailableSpells();
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
         * 
         *  UPDATE: We instead draw the spells breath of the wild style.
         */
        for (int i = 0; i < availableSpells.Count; i++)
        {
            //menuCanvas.Children.Add()
            GameObject icon = Instantiate(availableSpells[i].getIconPrefab(), new Vector3(0,-5.0f,0) ,Quaternion.identity);
            icon.transform.SetParent(spellMenu.transform, false);
        }

        //Set the spell title and description.
        GameObject spellTitle, spellDescription;
        spellTitle = Instantiate(title, new Vector3(title.transform.position.x, title.transform.position.y, title.transform.position.z) ,Quaternion.identity);;
        spellDescription = Instantiate(description, new Vector3(title.transform.position.x, title.transform.position.y, title.transform.position.z), Quaternion.identity); ;

        if (availableSpells.Count > 0)
        {
            //Show the title and description of the first spell in the list.
            spellTitle.GetComponent<Text>().text = availableSpells[0].getName();
            spellDescription.GetComponent<Text>().text = availableSpells[0].getDescription();
        }
        else
        {
            //The default title and description.
            spellTitle.GetComponent<Text>().text = "Spell Menu";
            spellDescription.GetComponent<Text>().text = "You have no spells right now";
        }
        spellTitle.transform.SetParent(spellMenu.transform, false);
        spellDescription.transform.SetParent(spellMenu.transform, false);

        //Change canvas alpha to some value 0.5f.
        spellMenu.GetComponent<Image>().color = new Vector4(0.3867925f, 0.3411802f, 0.3411802f, 0.57f);
    }

    /*
     *  Remove all gameobjects in the spellMenu and make it invisible.
     */
    public void removeMenu()
    {
        foreach (Transform child in spellMenu.transform)
        {
            Destroy(child.gameObject);
        }
        spellMenu.GetComponent<Image>().color = new Vector4(0,0,0,0);
    }
}
