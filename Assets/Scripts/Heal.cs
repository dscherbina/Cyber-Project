using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private Character character;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.gameObject.GetComponent<Character>();
        if ((character) && character.Lives < 5)
        {
            character.Lives += 1;
            Destroy(gameObject);
           
        }
        

    }
}
