using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class UIManager : FlyingMonster
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    


    public void PauseOn()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Win()
    {
        panelWin.SetActive(true);

    }

    public void Lose()
    {
        panelLose.SetActive(true);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(1);
        MoneyText.Coin = 0;
    }
    public void DieTime()
    {
        Invoke("Lose", 2f);
    }
    public void WinTime()
    {
        Invoke("Win", 2f);
    }






}
