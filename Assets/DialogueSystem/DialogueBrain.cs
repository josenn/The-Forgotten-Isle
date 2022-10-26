using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    public NPC_DialogueTrigger setAshDialogue;
    public NPC_DialogueTrigger setNoahDialogue;
    public bool youKnowAboutBike = false;
    public bool ashKnowsAboutBike = false;
    public bool haveTalkedToAshOnce = false;
    public DialogObject noOneKnowsBikeAsh;
    public DialogObject noOneKnowsBikeNoah;
    public DialogObject youKnowBikeAsh;
    public DialogObject youKnowBikeNoah;
    public DialogObject ashKnowsBikeAsh;
    public DialogObject ashKnowsBikeNoah;
    public DialogObject ashHasTalked;

    public void SetNPCDialogue()
    {
        if (youKnowAboutBike == false && ashKnowsAboutBike == false)
        {
            setAshDialogue.assignedDialogue = noOneKnowsBikeAsh;
            setNoahDialogue.assignedDialogue = noOneKnowsBikeNoah;
        }
        else if (youKnowAboutBike == true && ashKnowsAboutBike == false)
        {
            setAshDialogue.assignedDialogue = youKnowBikeAsh;
            setNoahDialogue.assignedDialogue = youKnowBikeNoah;
        }
        else if (youKnowAboutBike == true && ashKnowsAboutBike == true)
        {
            setAshDialogue.assignedDialogue = ashKnowsBikeAsh;
            setNoahDialogue.assignedDialogue = ashKnowsBikeNoah;
        }

        if (haveTalkedToAshOnce)
        {
            setAshDialogue.assignedDialogue = ashHasTalked;
        }
    }

    public void FindOutAboutBike()
    {
        youKnowAboutBike = true;
    }

    public void TellAshAboutBike()
    {
        ashKnowsAboutBike = true;
    }

    public void TalkedToAshOnce()
    {
        haveTalkedToAshOnce = true;
    }





}

