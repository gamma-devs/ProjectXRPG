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

        GameObject iconPrefab = null;
        string path = "SpellPrefabs/WindSlashImage";
        iconPrefab = Resources.Load<GameObject>(path);
        icon = iconPrefab;
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
