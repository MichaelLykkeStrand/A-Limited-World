using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerTask : AbstractTask
{
    protected override void Awake()
    {
        base.Awake();
        MovementRequired = false;
    }
}
