using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaEvents : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _runEff;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    // 攻撃時のエフェクト
    void HitEffect()
    {

    }

    // 走っているときのエフェクト
    void RunEffect()
    {
        _runEff.transform.position = transform.position;
        _runEff.transform.rotation = transform.rotation;
        _runEff.Play();
    }
}
