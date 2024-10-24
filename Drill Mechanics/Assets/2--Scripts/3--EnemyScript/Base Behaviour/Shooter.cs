using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooter : EnemyBase
{
    ShootingWeapon weapon;
    protected override void Start()
    {
        base.Start();
        weapon = GetComponentInChildren<ShootingWeapon>();
    }
    protected override void Update()
    {
        base.Update();
        if(anim.GetBool(attackAnimTag) == true)
        {
            weapon.TimerFunction();
        }
    }
}
