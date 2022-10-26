using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cursor : MonoBehaviour
{
    public bool responsePosOrNeg = true;

    public RectTransform m_RectTransform;

    
    
    private void Awake() {
        m_RectTransform.anchoredPosition = new Vector2(68, 68);
            responsePosOrNeg = true;
    }

    private void Update() 
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && responsePosOrNeg == false)
        {
            m_RectTransform.anchoredPosition = new Vector2(68, 68);
            responsePosOrNeg = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && responsePosOrNeg == true)
        {
            m_RectTransform.anchoredPosition = new Vector2(68, 25);
            responsePosOrNeg = false;
        }

    }

    
}
