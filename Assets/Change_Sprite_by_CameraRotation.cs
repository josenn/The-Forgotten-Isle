using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Sprite_by_CameraRotation : MonoBehaviour
{
    private Transform _currentCamRot;
    private float _rotation;
    public Sprite[] sprites;
    private SpriteRenderer _spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        _currentCamRot = GameObject.Find("RotateCamera").transform;
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rotation = _currentCamRot.rotation.y;
        //Debug.Log(_rotation);

    }
}
