using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialoguetext;
    public Text nametext;

    public Animator boxanim;
    public Animator startanim;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        boxanim.SetBool("boxopen", true);
        startanim.SetBool("startopen", false);

        nametext.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
       
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialoguetext.text = " ";
        foreach(char letter in sentence.ToCharArray())
        {
            dialoguetext.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        boxanim.SetBool("boxopen",false);  
    }
}
