using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    protected Animator _animator;
    protected delegate void StateMethod();  // 状態ごとの処理を行うデリゲート
    protected StateMethod State;
    [SerializeField]
    protected float _speed;     // キャラごとの速度 継承先で初期化

    private Dictionary<int, string> _animName;
    private Vector3 _dir;       // 動く向き
    private Camera _camera;
    private bool _onStartMove = false;   // 動き始めの１フレーム用フラグ
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
            if (_onStartMove)
            {
                AdjustmentDirection();
            }
            State = Move;
        }
        if(State == null)
        {            
            State = Idle;
            _animator.SetBool(_animName[0], _dir.magnitude != 0);
        }
        if (Input.GetButtonDown("HA"))
        {
            State = Action;
        }
        if(State != Move)
        {
            _onStartMove = true;
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

    // 各キャラごとの実装
    public abstract void Action();

    // 移動
    void AdjustmentDirection()
    {
        _targetPos = _camera.transform.forward;
        _onStartMove = false;
    }
    private void Move()
    {        
        var targetRot = Quaternion.LookRotation(_dir) * Quaternion.LookRotation(_targetPos);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 10);
        transform.position += transform.forward * Time.deltaTime * _speed;
        _animator.SetBool(_animName[0], true);
        Debug.Log(State.Method);
        State = null;
    }
    // 固有スキル使用
    public abstract void Skill();
}
