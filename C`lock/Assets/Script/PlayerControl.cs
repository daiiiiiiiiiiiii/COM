using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    run,
    attack,
    down,
    idle,
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
    private PlayerState _state;     // プレイヤーの状態

    protected void Start(GameObject obj,PlayerControl player,Animator anim)
    {
        _obj = obj;
        _player = player;
        _animator = anim;
        _animName = new Dictionary<PlayerState, string>();
        var name = new string[(int)PlayerState.max];
        name[0] = "IsRun";
        name[1] = "Jab";
        name[2] = "DamageDown";
        name[2] = "DamageDown"; 
        for (var i = (PlayerState)0;i < PlayerState.max;i++)
        {
            _animName.Add(i,name[(int)i]);
        }
    }

    private void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if (Input.GetButtonUp("Skill"))
        {
            _player.Skill();
        }
        if (vec.magnitude != 0)
        {
            Move(vec);
        }
        else
        {
            // 現在は止まっている状態で範囲内に敵がいれば攻撃する
            Attack();
        }
        _player._animator.SetBool(_animName[PlayerState.run], vec.magnitude != 0);       
    }

    private void CameraMove()
    {

    }

    private void Attack()
    {

    }

    // プレイヤーのアクションメソッド
    // 移動
    public void Move(Vector3 speed)
    {
        var targetRot  = Quaternion.LookRotation(speed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 10);
        _obj.transform.position += speed / 20f;
        _player._animator.SetBool(_animName[PlayerState.run], true);
        _player._animator.SetFloat("Speed", speed.z);
    }
    // 固有スキル使用
    public abstract void Skill();
}
