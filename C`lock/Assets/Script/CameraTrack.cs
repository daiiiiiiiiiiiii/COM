using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = default;
    Vector3 _offset;
    float _rotate;
    bool _available = true;
    void Start()
    {
        // ここは要調整
        _offset = new Vector3(0, 2f,-5.8f);
    }

    void Update()
    {
        if (_available)
        {
            _rotate = Input.GetAxis("Horizontal_R");
            AdjustmentPosition();
        }
        //else
        //{
        //    _rotate = _player.transform.rotation;
        //    AdjustmentFront();
        //}
        transform.position = _player.transform.position + _offset;
    }

    void AdjustmentPosition()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, _rotate);
        var rot = Quaternion.Euler(new Vector3(0,_rotate,0));
        _offset = rot * _offset;
    }

    void AdjustmentFront()
    {

    }

    // プレイヤーがカメラの操作ができるかどうか設定するメソッド
    public void IsAvailable(bool flag)
    {
        _available = flag;
    }
}
