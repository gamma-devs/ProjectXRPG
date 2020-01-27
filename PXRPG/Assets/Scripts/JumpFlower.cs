using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Jump Flower creates a stream which you can use to reach higher up places, maybe
 * you can use them to do super jumps like in banjo kazooie.
 * 
 */
public class JumpFlower : Flower
{
    public void Start()
    {
        base.Start();
        name = FlowerName.Jump;
    }
}
