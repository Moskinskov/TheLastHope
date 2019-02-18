using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;

public class SimpleBullet : AAmmo
{

    float path = 0;
    public override void Move(float deltaTime)
    {
        transform.position += Direction * Speed * deltaTime;
        path += Speed * deltaTime;
        if (path > Range)
        {
            IsFinifhed = true;
        }
    }
}
