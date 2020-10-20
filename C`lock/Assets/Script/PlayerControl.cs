using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    private Dictionary<int, string> _animName;
    protected Animator _animator;
    private GameObject _obj;                // 基底クラスのオブジェクトの情報
    private PlayerControl _player;          // プレイヤーの情報
    private delegate void StateMethod();    // 状態ごとの処理を行うデリゲート
    StateMethod State;
    Animator _anim;
    private Vector3 _dir;       // 動く向き



    protected void Start(GameObject obj,PlayerControl player,Animator anim)
    {
        State = Idle;
        _anim = GetComponent<Animator>();
        
        _obj = obj;
        _player = player;
        _animator = anim;
        _animName = new Dictionary<int, string>();
        // 状態ごとのアニメーション遷移に使う名前
        var name = new string[_anim.parameters.Length];
        for (var i = 0;i < _anim.parameters.Length;i++)
        {
            _animName.Add(i, _anim.parameters[(int)i].name);
        }
    }

    private void Update()
    {
        _dir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if (Input.GetButtonUp("Skill"))
        {
            _player.Skill();
        }
        if (_dir.magnitude != 0)
        {
            Move();
        }
        else
        {
        }
        State();
        _player._animator.SetBool(_animName[0], _dir.magnitude != 0);       
    }

    //////ここから状態ごとのメソッド
    private void Idle()
    {

    }

    private void Attack()
    {
        int a = 0;
    }

    // プレイヤーのアクションメソッド
    // 移動
    private void Move()
    {
        var targetRot  = Quaternion.LookRotation(_dir);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 10);
        _obj.transform.position += _dir / 20f;
        _player._animator.SetBool(_animName[0], true);
        _player._animator.SetFloat("Speed", _dir.z);
    }
    // 固有スキル使用
    public abstract void Skill();
}
