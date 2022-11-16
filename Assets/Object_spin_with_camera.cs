using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_spin_with_camera : MonoBehaviour
{
    private void LateUpdate()
     {
         transform.forward = new Vector3(-Camera.main.transform.forward.x, transform.forward.y, -Camera.main.transform.forward.z);
     }
}
