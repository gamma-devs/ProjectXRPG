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
    public int rarity; //A number from 0 to 5.
    public int id; //Each spell will have a unique id.
    protected GameObject icon; //The icon object which is basically the UI element/prefab.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
