using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    protected Animator _animator;
    protected Dictionary<int, string> _animName;
    protected delegate void StateMethod();  // 状態ごとの処理を行うデリゲート
    protected StateMethod State;
    [SerializeField]
    protected float _speed;     // キャラごとの速度 継承先で初期化

    private Vector3 _dir;       // 動く向き
    private Camera _camera;
    private Vector3 _targetPos;

    protected void Start(Animator anim)
    {
        _camera = FindObjectOfType<Camera>();
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
        if (_dir.magnitude != 0)
        {
            State = Move;
        }
        if (Input.GetButtonDown("HA"))
        {
            State = Action;
        }
        if (State == null)
        {
            State = Idle;
            _animator.SetBool(_animName[0], _dir.magnitude != 0);
        }
        State();
    }

    //////ここから状態ごとのメソッド
    private void Idle()
    {
        // いまのところ何もしない
        Debug.Log(State.Method);
        State = null;
    }
    // 範囲内の敵へ攻撃
    private void Attack()
    {

    }

    // 移動
    private void Move()
    {
        _targetPos = _camera.transform.forward;
        var targetRot = Quaternion.LookRotation(_dir) * Quaternion.LookRotation(_targetPos);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 10);
        transform.position += transform.forward * Time.deltaTime * _speed;
        _animator.SetBool(_animName[0], true);
        Debug.Log(State.Method);
        State = null;
    }
    // 各キャラごとの実装
    public abstract void Action();  // キャラ固有アクション
    public abstract void Skill();   // カードスキル
}
