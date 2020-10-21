using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    private Dictionary<int, string> _animName;
    protected Animator _animator;
    protected delegate void StateMethod();    // 状態ごとの処理を行うデリゲート
    protected StateMethod State;                      
    private Vector3 _dir;       // 動く向き
    private Camera _camera;
    private bool _onStartMove = false;   // 動き始めの１フレーム用フラグ

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
        var beforeState = State;
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

    void AdjustmentDirection()
    {
        var rot = _camera.transform.rotation;
        rot.x = 0;
        transform.forward = rot * Vector3.forward;
        _onStartMove = false;
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

    // プレイヤーのアクションメソッド
    // 移動
    private void Move()
    {
        //var targetRot  = Quaternion.LookRotation(_dir);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 10);
        //transform.position += _dir / 60f;
        //_animator.SetBool(_animName[0], true);
        //_animator.SetFloat("Speed", _dir.z);
        Debug.Log(State.Method);
        State = null;
    }
    // 固有スキル使用
    public abstract void Skill();
}
