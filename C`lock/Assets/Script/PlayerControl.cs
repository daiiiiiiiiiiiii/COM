using System.Collections.Generic;
using UnityEngine;

// プレイヤーの基底クラス
// Moveなどはここで
public abstract class PlayerControl : MonoBehaviour
{
    protected Animator _animator;
    protected delegate void  StateMethod();   // 状態ごとの処理を行うデリゲート
    protected StateMethod State;
    [SerializeField]
    protected float _speed = 1;     // キャラごとの速度 継承先で初期化
    protected Camera _camera;   // 追従してるカメラ

    private Vector3 _dir;       // 動く向き
    private Vector3 _targetPos; // 
    private bool _serchFlag;    // 敵が攻撃範囲内にいるかどうか
    private Transform _enemy;   // 範囲内の敵の座標情報

    protected void Start(Animator anim)
    {
        _camera = FindObjectOfType<Camera>();
        State = Idle;     
        //_animator = anim;
        _serchFlag = false;
    }

    private void Update()
    {
        _dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetAxis("HA") >= 0.8f)
        {
            State = Action;
        }
        else if (_dir.magnitude != 0)
        {
            State = Move;
        }       
        else
        {
            if (_serchFlag)
            {
                State = Attack;
            }
            else
            {
                State = Idle;
            }
        }
        //for (var i = 0; i < _animator.parameterCount; i++)
        //{
        //    _animator.SetBool(_animator.parameters[i].name, false);
        //}
        State();

    }

    //////ここから状態ごとのメソッド
    private void Idle()
    {
        // いまのところ何もしない
        
    }
    // 範囲内の敵へ攻撃
    private void Attack()
    {
        // 敵へのベクトル
        _targetPos = _enemy.position - transform.position;
        transform.localRotation = Quaternion.LookRotation(_targetPos);
        // _animator.SetBool("IsAction", true);
    }

    // 移動
    private void Move()
    {
        _targetPos = _camera.transform.forward;
        _targetPos.y = 0;
        var targetRot = Quaternion.LookRotation(_dir) * Quaternion.LookRotation(_targetPos);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * 5);
        transform.position += transform.forward * Time.deltaTime * _speed;
        // _animator.SetBool("IsRun", true);
    }
    // 各キャラごとの実装
    public abstract void Action();  // キャラ固有アクション
    public abstract void Skill();   // カードスキル

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && _enemy == null)
        {
            _enemy = col.transform;
            _serchFlag = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _enemy = null;
            _serchFlag = false;
        }
    }
}
