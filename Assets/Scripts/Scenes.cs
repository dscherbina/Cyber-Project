using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeScenes (int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
        MoneyText.Coin = 0;
    }
    public void MainMenu(string Menu)
    {
        SceneManager.LoadScene(Menu);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
