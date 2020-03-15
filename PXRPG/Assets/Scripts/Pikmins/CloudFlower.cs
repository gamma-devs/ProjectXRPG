using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFlower : Flower
{
    public void Start()
    {
        base.Start();
        name = FlowerName.Cloud;
    }
}
