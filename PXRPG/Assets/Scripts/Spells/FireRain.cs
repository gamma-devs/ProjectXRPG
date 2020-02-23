using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRain : Spell
{
    public FireRain()
    {
        name = "Fire Rain";
        description = "Summon a cloud that will burn the ground below it";

        GameObject iconPrefab = null;
        string path = "UIElements/SpellPrefabs/FireRainImage";
        iconPrefab = Resources.Load<GameObject>(path);
        icon = iconPrefab;
    }

    /*
     *  Need 2 fire and 1 cloud pikmin to perform.
     */
    public override bool requirements(Dictionary<FlowerName, int> flowers)
    {
        int fireOutValue = 0;
        int cloudOutValue = 0;
        flowers.TryGetValue(FlowerName.Fire, out fireOutValue);
        flowers.TryGetValue(FlowerName.Cloud, out cloudOutValue);
        if (fireOutValue >= 1 && cloudOutValue >= 1)
            return true;
        return false;
    }
}
