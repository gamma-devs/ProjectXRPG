using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  The spell windslash requires 2 jump flowers.
 */
public class Windslash : Spell
{
    public override bool requirements(Dictionary<FlowerName, int> flowers)
    {
        int outValue = 0;
        flowers.TryGetValue(FlowerName.Jump, out outValue);
        if (outValue >= 2)
            return true;
        return false;
    }
}
