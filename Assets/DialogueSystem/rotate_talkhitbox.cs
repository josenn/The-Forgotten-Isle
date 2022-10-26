using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_talkhitbox : MonoBehaviour
{
    public bool canTalk = false;
    private CircleCollider2D circleCol;
    public GameObject npcObject;
    
    private void Start() 
    {
        circleCol = this.GetComponent<CircleCollider2D>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log(other.name);
        if (other.gameObject.tag == "NPC")
        {
            canTalk = true;
            npcObject = other.gameObject;

            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            canTalk = false;
        }
    }


}
