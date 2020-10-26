using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : PlayerControl
{
    void Start()
    {
        var anim = GetComponent<Animator>();

        base.Start(anim);
    }

    public override int Skill()
    {
        return 3;
    }

    public override int Action()
    {
        return 2;
    }

    public override void SetAnim(int num)
    {
    }
}
