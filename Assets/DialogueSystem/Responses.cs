using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Responses 
{   
    [SerializeField] private string responseTextPos;
    [SerializeField] private string responseTextNeg;
    
    [SerializeField] private DialogObject dialogObject;
    
    public string ResponseTextPos => responseTextPos;
    public string ResponseTextNeg => responseTextNeg;
    

    public DialogObject DialogObject => dialogObject;
}
