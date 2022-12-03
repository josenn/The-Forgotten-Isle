using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{

    [SerializeField] float followSpeed = 2f;
    [SerializeField] Transform target;

    public bool lerpFollow = true;
    
    
    
    
    void Update()
    {
        Vector3 newPos = target.position;

        if (lerpFollow){
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        else{
            transform.position = newPos;
        }
    }
}
