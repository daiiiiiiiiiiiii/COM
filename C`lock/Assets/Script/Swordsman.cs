using UnityEngine;

public class Swordsman : PlayerControl
{
    Vector3 _nextPos;
    private float _time;
    [SerializeField]
    private float _waitMin = default;   // アクション待ち時間
    [SerializeField]
    private float _readyMin = default;  // アクション準備時間

    delegate int PhaseMethod();
    PhaseMethod Phase;
    void Start()
    {        
        var anim = GetComponent<Animator>();
        base.Start(anim);
    }

    public override StateMethod Skill()
    {
        return Skill;
    }

    public override StateMethod Action()
    {
        _camera.GetComponent<CameraTrack>().IsAvailable(false);
        // レム
        if(_time < _waitMin)
        {
            Phase = Wait;// 待機時間
        }
        else if(_time < _readyMin + _waitMin)
        {
            Phase = Ready;
        }
        Phase();
        _time += Time.deltaTime;
        return Action;
    }

    public override StateMethod ActionTrigger()
    {       
        if (Run() == 0)
        {
            _time = 0;
            _camera.GetComponent<CameraTrack>().IsAvailable(true);
            return Idle;
        }      
        return ActionTrigger;
    }

    private int Wait()
    {
        Debug.Log("Wait");
        return 0;
    }

    private int Ready()
    {
        Debug.Log("サークル移動");
        return 0;
    }

    private int Run()
    {
        transform.position += transform.forward * _time;       
        Debug.Log("プレイヤー移動");
        return 0;
    }
}
