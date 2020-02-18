using System.Collections;
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
    public GameObject[] spellIconPrefabs;

    public SpellMenu()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //menuCanvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //spellIconPrefabs = new GameObject[GameVariables.TOTAL_NR_OF_SPELLS];
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
            GameObject icon = Instantiate(availableSpells[i].getIconPrefab(), new Vector3(0,0,0) ,Quaternion.identity);
            icon.transform.SetParent(GameObject.FindGameObjectWithTag("SpellMenu").transform, false);
        }
    }

    /*
     *  Remove all gameobjects in the spellMenu and make it invisible.
     */
    public void removeMenu()
    {
        GameObject spellMenu = GameObject.FindGameObjectWithTag("SpellMenu");
        foreach (Transform child in spellMenu.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
