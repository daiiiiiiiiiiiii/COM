using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    idle,
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
        for (var i = PlayerState.idle;i < PlayerState.max;i++)
        {
            _animName.Add(i,name[(int)i]);
        }
    }

    protected void Update()
    {
        Vector3 vec = new Vector3();
        vec.z = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") * Input.GetAxis("Horizontal");
        if(vec.z !=0)
        {
            Move(vec.normalized);
        }
        else
        {
            _player._animator.SetBool(_animName[PlayerState.run], false);
        }
        Debug.Log(vec.normalized);
    }

    public void Move(Vector3 speed)
    {
        _obj.transform.position += speed / 20f;
        _player._animator.SetBool(_animName[PlayerState.run], true);
        _player._animator.SetFloat("Speed", speed.z);
    }
}
