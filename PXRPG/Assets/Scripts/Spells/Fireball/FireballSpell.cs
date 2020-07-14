using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : Spell
{
    public GameObject fireball;
    float timer = 5.0f; //The time this spell is active

    public override void init()
    {
        name = "Fireball";
        description = "Shoot fire projectiles at your enemies";

        GameObject iconPrefab = null;
        string path = "UIElements/SpellPrefabs/FireBallImage";
        iconPrefab = Resources.Load<GameObject>(path);
        icon = iconPrefab;

    }

    public override bool requirements(Dictionary<FlowerName, int> flowers)
    {
        int fireOutValue = 0;
        flowers.TryGetValue(FlowerName.Fire, out fireOutValue);
        if (fireOutValue >= 1)
            return true;
        return false;
    }

    //Called once when spell is clicked on
    public override void castSpell()
    {
        //Instantiate(fireball);
        //Set the corresponding variables inside the player
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //player.setCurrentSpell(fireball);
        player.setCurrentProjectile(fireball);
        player.setSpellTimer(timer);
    }

}
