using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * The Spell base class.
 */
public class Spell : MonoBehaviour
{
    enum SpellType { Atk, Def, Buff, Debuff };

    protected string name;
    
    private SpellType type;
    public int rarity; //A number from 0 to 5.
    public int id; //Each spell will have a unique id.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
