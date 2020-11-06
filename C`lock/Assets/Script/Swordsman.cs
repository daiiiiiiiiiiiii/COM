using UnityEngine;

public class Swordsman : PlayerControl
{
    Vector3 _nextPos;
    private float _time;
    [SerializeField]
    private float _waitmin = default;   // アクション待ち時間
    [SerializeField]
    private float _readyMin = default;  // アクション準備時間

    delegate int PhaseMethod();
    PhaseMethod Phase;
    void Start()
    {        
        var anim = GetComponent<Animator>();
        base.Start(anim);
    }

    public override void Skill()
    {
    }

    public override void Action()
    {
        // レム
        if(_time < _waitmin)
        {
            Phase = Wait;// 待機時間
        }
        else if(_time < _readyMin)
        {
            Phase = Ready;
        }
        Phase();
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
        _time += Time.deltaTime;
        Debug.Log("プレイヤー移動");
        return 0;
    }
}
