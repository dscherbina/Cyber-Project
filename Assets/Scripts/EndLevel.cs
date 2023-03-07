using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameObject ShowTip;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        ShowTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (MoneyText.Coin == 6)
        {
            if (FadeInOut.sceneStarting == false)
            {
                FadeInOut.sceneEnd = true;
                ShowTip.SetActive(false);
            }

        }
        else ShowTip.SetActive(true);



    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        ShowTip.SetActive(false);
    }



}
