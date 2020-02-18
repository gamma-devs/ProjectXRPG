using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVariables : MonoBehaviour
{
    public static int TOTAL_NR_OF_SPELLS = 1;
    public static int NR_LEVELS = 1;
    public static int NR_FLOWER_TYPES = 1;
    public static Spell[] SPELLS = new Spell[1];
    static bool setup = false;

    //Windslash s1 = new Windslash();
    private void Start()
    {
        if (!setup)
        {
            SPELLS[0] = new Windslash();
        }
        setup = true;
    }

}
