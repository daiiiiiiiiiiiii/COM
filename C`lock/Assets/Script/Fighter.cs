using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : PlayerControl
{
    PlayerControl _fighter;
    void Start()
    {
        var name = new string[(int)PlayerState.max];
        name[0] = "IsRun";
        name[1] = "_jab";
        name[2] = "_down";
        _fighter = this;
        var anim = GetComponent<Animator>();

        base.Start(this.gameObject, _fighter, anim, name);
    }

    public override void Skill()
    {

    }
}
