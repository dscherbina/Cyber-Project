using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startanim;
    public DialogueManager dm;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        startanim.SetBool("startopen", true);
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        startanim.SetBool("startopen", false);
        dm.EndDialogue();
    }
}
