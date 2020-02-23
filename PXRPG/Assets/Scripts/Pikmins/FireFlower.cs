using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : Flower
{
    public void Start()
    {
        base.Start();
        name = FlowerName.Fire;
    }
}
