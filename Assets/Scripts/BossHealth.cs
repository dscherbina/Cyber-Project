using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Image healthbar;
    public Image healthBarEffect;

    float maxhealth = 12f;
    public static float health;

    private float healthspeed = 0.002f;
    void Start()    
    {
        healthbar = GetComponent<Image>();
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health / maxhealth;
        if(healthBarEffect.fillAmount > healthbar.fillAmount)
        {
            healthBarEffect.fillAmount -= healthspeed;
        }
        else
        {
            healthBarEffect.fillAmount = healthbar.fillAmount;
        }
    }
}
