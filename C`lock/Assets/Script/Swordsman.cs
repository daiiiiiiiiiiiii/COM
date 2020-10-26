using UnityEngine;

public class Swordsman : PlayerControl
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
        for(int i = 0; i < _animator.parameterCount; i++)
        {
            _animator.SetBool(_animName[i], false);
            if (i == num)
            {
                _animator.SetBool(_animName[i], true);
            }
        }
    }
}
