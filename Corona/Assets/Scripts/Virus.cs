using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    protected override void Attack()
    {
        WayShot();
    }
}