using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _offset;
    }
}
