using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemAction : PlayerControl
{
    Vector3 _nextPos;
    private float _distance;
    [SerializeField]
    private float _waitMin = default;   // アクション待ち時間
    [SerializeField]
    private float _readyMin = default;  // アクション準備時間
    [SerializeField]
    private float _maxDistance = default;  // アクション距離

    [SerializeField]
    private GameObject _actRange = default; // アクション範囲
    [SerializeField]
    private GameObject _attRange = default; // 攻撃範囲
    private Vector3 _actFirst;              // アクションの初期座標

    delegate int PhaseMethod();
    void Start()
    {
        _actFirst = _actRange.transform.localPosition;
        var anim = GetComponent<Animator>();
        base.Start(anim);
    }

    public override StateMethod Skill()
    {
        return Skill;
    }

    public override StateMethod Action()
    {
        _actRange.SetActive(true);
        _attRange.SetActive(false);
        _actRange.transform.localPosition = transform.forward * _distance * _speed;
        //_camera.GetComponent<CameraTrack>().IsAvailable(false);
        _distance = Mathf.Clamp(_distance * _speed + Time.deltaTime, 0, _maxDistance);
        return Action;
    }

    public override StateMethod ActionTrigger()
    {
        if (Run() == 0)
        {
            _distance = 0;
            _camera.GetComponent<CameraTrack>().IsAvailable(true);
            Reset();
            _actRange.SetActive(false);
            _attRange.SetActive(true);
            return Idle;
        }
        return ActionTrigger;
    }

    private int Run()
    {
        transform.position = _actRange.transform.position;
        Debug.Log("プレイヤー移動");
        return 0;
    }

    private void Reset()
    {
        _actRange.transform.localPosition = _actFirst;
    }
}
