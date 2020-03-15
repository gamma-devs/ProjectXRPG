using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVariables : MonoBehaviour
{
    public static int TOTAL_NR_OF_SPELLS = 3; //Change this when adding spells
    public static int NR_LEVELS = 1;
    public static int NR_FLOWER_TYPES = 3; //The different "race" types of flowers
    public static Spell[] SPELLS = new Spell[3]; //Change this when adding spells
    static bool setup = false;

    //Windslash s1 = new Windslash();
    private void Start()
    {
        if (!setup)
        {
            SPELLS[0] = new Windslash();
            SPELLS[1] = new FireRain();

            SPELLS[2] = new CloudLift();

            for(int i = 0; i < TOTAL_NR_OF_SPELLS; i++)
            {
                SPELLS[i].init();
            }
        }
        setup = true;
    }

}
