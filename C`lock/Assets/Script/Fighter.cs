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

    public override void Skill()
    {

    }

    public override void Action()
    {
        Debug.Log(State.Method);
    }
}
