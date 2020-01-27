﻿using System.Collections;
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
    private Spell[] availableSpells;
    private SpellMenu spellMenu;
    //Create a dictionary that keeps track of how many of each type you have.
    Dictionary<FlowerName, int> flowerDictionary = new Dictionary<string, int>();
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

    }

    public Spell[] getAvailableSpells()
    {
        return availableSpells;
    }

    public void bringUpSpellMenu()
    {
        //Apply some blur shader on camera.

    }

    public void quitSpellMenu()
    {

    }
}
