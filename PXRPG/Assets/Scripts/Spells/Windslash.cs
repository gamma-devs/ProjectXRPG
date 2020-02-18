using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  The spell windslash requires 2 jump flowers.
 */
public class Windslash : Spell
{

    public Windslash()
    {
        name = "Wind Slash";
        description = "Perform a powerful wind slash";

        Sprite mySprite = null;
        string path = "Sprites/Spells/Atkicon";
        //Put all spells into the SPELLS array.
        mySprite = Resources.Load<Sprite>(path);
        image = mySprite;
    }

    public override bool requirements(Dictionary<FlowerName, int> flowers)
    {
        int outValue = 0;
        flowers.TryGetValue(FlowerName.Jump, out outValue);
        if (outValue >= 1)
            return true;
        return false;
    }
}
