using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    run,
    attack,
    down,
    max
}

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    // 連想配列 
    // @key:プレイヤーの状態
    // @object:アニメーションの名前
    private Dictionary<PlayerState, string> _animName;
    protected Animator _animator;
    private GameObject _obj;        // 基底クラスのオブジェクトの情報
    private PlayerControl _player;  // プレイヤーの情報

    protected void Start(GameObject obj,PlayerControl player,Animator anim,string[] name)
    {
        _obj = obj;
        _player = player;
        _animator = anim;
        _animName = new Dictionary<PlayerState, string>();
        for (var i = (PlayerState)0;i < PlayerState.max;i++)
        {
            _animName.Add(i,name[(int)i]);
        }
    }

    protected void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if(vec.z + vec.x != 0)
        {
            Move(vec);
        }
        else
        {
            _player._animator.SetBool(_animName[PlayerState.run], false);
        }
        Debug.Log(vec);
    }

    public void Move(Vector3 speed)
    {
        var targetRot  = Quaternion.LookRotation(speed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 1099);
        _obj.transform.position += speed / 20f;
        _player._animator.SetBool(_animName[PlayerState.run], true);
        _player._animator.SetFloat("Speed", speed.z);
    }
}
