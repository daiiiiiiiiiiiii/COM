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
        State = Action;
        Debug.Log(State.Method);
    }
}
