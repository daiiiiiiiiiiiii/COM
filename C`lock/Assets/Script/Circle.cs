using System.Collections;
using System;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private LineRenderer _renderer;     // 円を描画するための LineRenderer
    [SerializeField]
    private float _radius = 1;          // 円の半径
    [SerializeField]
    private float _lineWidth = 0.3f;    // 円の線の太さ
    [SerializeField]
    private Color _color;               // 線の色
    
    readonly int _count = 10;
    private Vector3[] _points;          // 円の座標
    private Transform _player;          // 攻撃範囲を表示しているプレイヤー

    public GameObject _serch { private set; get; }  // 

    void Start()
    {
        _renderer = GetComponent<LineRenderer>();
        _player = transform.parent;
        Init();
    }

    private void Init()
    {
        _renderer.startWidth = _lineWidth;
        _renderer.endWidth = _lineWidth;
        _renderer.positionCount = _count;
        _renderer.loop = true;
        _renderer.startColor = _color;
        _renderer.endColor = _color;
        _renderer.material.color = _color;
        _renderer.receiveShadows = false;
        _points = new Vector3[_count];
        var pos = _player.position;
        for (float i = 0; i < _count; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / _count);
            var x = Mathf.Sin(rad) * _radius;
            var z = Mathf.Cos(rad) * _radius;
            _points[(int)i] = new Vector3(x + pos.x, pos.y, z + pos.z + 1);
        }
        _renderer.SetPositions(_points);
    }

    void Update()
    {
        SetPosition();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col != _player && col.tag == "Player")
        {
            _serch = col.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        _serch = null;
    }

    private void SetPosition()
    {
        var pos = _player.position;
        for (float i = 0; i < _count; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / _count);
            var x = Mathf.Sin(rad) * _radius;
            var z = Mathf.Cos(rad) * _radius;
            _points[(int)i] = new Vector3(pos.x + x, pos.y, pos.z + z + 1);
        }
        _renderer.SetPositions(_points);
    }
}
