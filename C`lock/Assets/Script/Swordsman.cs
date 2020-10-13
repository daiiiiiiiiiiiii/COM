using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : PlayerControl
{
    PlayerControl _sword;
    void Start()
    {
        var name = new string[(int)PlayerState.max];
        name[0] = "Run";
        name[1] = "Jab";
        name[2] = "DamageDown";
        _sword = this;
        var anim = GetComponent<Animator>();

        base.Start(this.gameObject, _sword, anim, name);
    }
}
