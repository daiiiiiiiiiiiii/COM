using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : PlayerControl
{
    PlayerControl _sword;
    void Start()
    {
        var name = new string[(int)PlayerState.max];
        name[0] = "Idle";
        name[1] = "Run";
        name[2] = "Jab";
        name[3] = "DamageDown";
        _sword = this;
        var anim = GetComponent<Animator>();

        base.Start(this.gameObject, _sword, anim, name);
    }
}
