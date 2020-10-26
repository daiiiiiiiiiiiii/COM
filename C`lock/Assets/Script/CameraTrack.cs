using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    Vector3 _offset;
    float _rotate;
    void Start()
    {
        _offset = new Vector3(0, 2.8f, -6.4f);
    }

    void Update()
    {
        _rotate = Input.GetAxis("Horizontal_R");
        AdjustmentPosition();
        transform.position = _player.transform.position + _offset;
    }
    void AdjustmentPosition()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, _rotate);
        var rot = Quaternion.Euler(new Vector3(0,_rotate,0));
        _offset = rot * _offset;
    }
}
