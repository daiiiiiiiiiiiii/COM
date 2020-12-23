using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField]
    public float _hp = default;
    private Slider _slider;
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _hp;
        _slider.value = _hp;
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        _slider.value = _hp;
    }

    public void HitDamage()
    {
        _hp--;
        if(_hp <= 0)
        {

        }
    }
}