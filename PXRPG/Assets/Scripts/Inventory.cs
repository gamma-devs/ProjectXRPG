using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The players inventory.
 * When pressing the spell menu, this class will be helpful to know what spells are available.
 * The spell menu should be updated each time the player gets or loses a pikmin.
 */
public class Inventory : MonoBehaviour
{
    private Flower[] flowers; //Maybe not neccesary
    private List<Spell> availableSpells = new List<Spell>(); //Spells you can perform
    public SpellMenu spellMenu;// = new SpellMenu();
    //Create a dictionary that keeps track of how many of each type you have.
    Dictionary<FlowerName, int> flowerDictionary = new Dictionary<FlowerName, int>();
    int flowerCount;
    // Start is called before the first frame update


    void Start()
    {
        flowerCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToInventory(Flower flower)
    {
        FlowerName name = flower.getName();
        int outValue = 0;
        flowerDictionary.TryGetValue(name, out outValue);
        if (outValue == 0)
        {
            flowerDictionary.Add(name, 1);
        }
        else
        {
            flowerDictionary.Add(name, outValue + 1);
        }
        flowerCount++;
        generateSpells();
    }

    public void removeFromInventory()
    {

    }

    void incrementFlowerCount()
    {
        flowerCount++;
        Debug.Log("added");
    }

    void decrementFlowerCount()
    {
        flowerCount--;
        Debug.Log("removed");
    }

    public void generateSpells()
    {
        availableSpells.Clear();
        for(int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            if(GameVariables.SPELLS[i].requirements(flowerDictionary))
            {
                availableSpells.Add(GameVariables.SPELLS[i]);
                Debug.Log(GameVariables.SPELLS[i].getName() + " added to list");
            }
        }
        //Sort the available spells list according to some criteria.
    }

    public List<Spell> getAvailableSpells()
    {
        return availableSpells;
    }

    public void bringUpSpellMenu()
    {
        //Apply some blur shader on camera.
        spellMenu.generateMenu();

    }

    public void quitSpellMenu()
    {

    }
}
