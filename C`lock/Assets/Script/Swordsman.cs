using UnityEngine;

public class Swordsman : PlayerControl
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
        _animator.SetBool(_animName[1], true);
        Debug.Log(State.Method);
    }
}
