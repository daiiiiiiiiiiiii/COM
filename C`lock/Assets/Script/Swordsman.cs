using UnityEngine;

public class Swordsman : PlayerControl
{
    PlayerControl _sword;
    void Start()
    {
        
        _sword = this;
        var anim = GetComponent<Animator>();

        base.Start(this.gameObject, _sword, anim);
    }

    public override void Skill()
    {
    }
}
