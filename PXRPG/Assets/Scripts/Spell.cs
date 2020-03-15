using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * The Spell base class.
 * Each unique spell is created in the GameVariable class and put in a spell list.
 * If you can perform them, they will show up in the spellMenu when brought up.
 */
public class Spell : MonoBehaviour
{
    enum SpellType { Atk, Def, Buff, Debuff };

    protected string name, description;
    
    private SpellType type;
    protected int rarity; //A number from 0 to 5.
    protected int id; //Each spell will have a unique id.
    protected GameObject icon; //The icon object which is basically the UI element/prefab.
    protected PlayerController player;
    //Refernce to inventory and player needed so we can call inventory.removePikmins() and player.performSpell()?

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------- VIRTUAL FUNCTIONS ----------------
    public virtual void init(){}

    //Gets called when you press the spell button
    //Should remove the pikmin from your inventory and remove the menu.
    public virtual void castSpell() {}

    //True if you fullfill the requirements for the spell.
    public virtual bool requirements(Dictionary<FlowerName, int> flowers)
    {
        return false;
    }

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public GameObject getIconPrefab()
    {
        return icon;
    }


}
