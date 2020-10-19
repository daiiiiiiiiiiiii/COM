using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : PlayerControl
{
    PlayerControl _fighter;
    void Start()
    {
        _fighter = this;
        var anim = GetComponent<Animator>();

        base.Start(this.gameObject, _fighter, anim);
    }

    public override void Skill()
    {

    }
}
