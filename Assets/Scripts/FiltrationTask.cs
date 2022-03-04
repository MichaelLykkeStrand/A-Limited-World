using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiltrationTask : AbstractTask
{
    protected override void Awake()
    {
        base.Awake();
        MovementRequired = true;
    }
}
