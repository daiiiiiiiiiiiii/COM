using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    protected Animator _animator;
    protected Dictionary<int, string> _animName;
    protected delegate int StateMethod();   // 状態ごとの処理を行うデリゲート
    protected StateMethod State;
    [SerializeField]
    protected float _speed;     // キャラごとの速度 継承先で初期化

    private Vector3 _dir;       // 動く向き
    private Camera _camera;
    private Vector3 _targetPos;
    Circle _range;              // 攻撃範囲

    protected void Start(Animator anim)
    {
        _camera = FindObjectOfType<Camera>();
        _range = transform.Find("Range").GetComponent<Circle>();
        State = Idle;     
        // 状態ごとのアニメーション遷移に使う名前
        _animName = new Dictionary<int, string>();
        _animator = anim;
        for (var i = 0;i < _animator.parameters.Length;i++)
        {
            _animName.Add(i, _animator.parameters[(int)i].name);
        }
    }

    private void Update()
    {
        _dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("HA"))
        {
            State = Action;
        }
        else if (_dir.magnitude != 0)
        {
            State = Move;
        }       
        else
        {
            if (_range._serch != null)
            {
                State = Attack;
            }
            else
            {
                State = Idle;
            }
        }
        Debug.Log(State.Method);     
        SetAnim(State());
    }

    //////ここから状態ごとのメソッド
    private int Idle()
    {
        // いまのところ何もしない
        return 99;
    }
    // 範囲内の敵へ攻撃
    private int Attack()
    {
        _animator.SetBool(_animName[1], true);
        return 1;
    }

    // 移動
    private int Move()
    {
        _targetPos = _camera.transform.forward;
        var targetRot = Quaternion.LookRotation(_dir) * Quaternion.LookRotation(_targetPos);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 5);
        transform.position += transform.forward * Time.deltaTime * _speed;
        _animator.SetBool(_animName[0], true);
        return 0;
    }
    // 各キャラごとの実装
    public abstract int Action();  // キャラ固有アクション
    public abstract int Skill();   // カードスキル

    public abstract void SetAnim(int num);
}
