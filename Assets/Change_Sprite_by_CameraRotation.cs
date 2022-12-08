using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Sprite_by_CameraRotation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Transform plane;
    public Camera cam;

    private const float step = 22.5f;

    public Sprite N, NE, E, SE, S, SW, W, NW;
    public void Start() => spriteRenderer = GetComponent<SpriteRenderer>();
    public void Update()
    {
        var projected = Vector3.ProjectOnPlane(cam.transform.forward, plane.up);
        var angle = Vector3.SignedAngle(projected, plane.forward, plane.up);
        
        var AbsAngle = Mathf.Abs(angle);
        
        if (AbsAngle < step) spriteRenderer.sprite = N;
        else if (AbsAngle < step*3) spriteRenderer.sprite = Mathf.Sign(angle) < 0 ? NW : NE;
        else if (AbsAngle < step*5) spriteRenderer.sprite = Mathf.Sign(angle) < 0 ? W : E;
        else if (AbsAngle < step*7) spriteRenderer.sprite = Mathf.Sign(angle) < 0 ? SW : SE;
        else spriteRenderer.sprite = S;
        
        //Billboard(spriteRenderer.transform, cam);
    }
}
