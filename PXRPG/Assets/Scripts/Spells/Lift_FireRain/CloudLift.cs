using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLift : Spell
{

    public GameObject cloud;

    public override void init()
    {
        name = "Lift";
        description = "Let the cloud lift you up to hard to reach places";

        GameObject iconPrefab = null;
        string path = "UIElements/SpellPrefabs/LiftImage";
        iconPrefab = Resources.Load<GameObject>(path);
        icon = iconPrefab;

    }

    public override bool requirements(Dictionary<FlowerName, int> flowers)
    {
        int cloudOutValue = 0;
        flowers.TryGetValue(FlowerName.Cloud, out cloudOutValue);
        if (cloudOutValue >= 1)
            return true;
        return false;
    }

    public override void castSpell()
    {
        //Summon the cloud prefab that has an update script inside it.
        Instantiate(cloud);
    }

}
