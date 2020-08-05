using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthController
{
    public Image damagePanel;

    [SerializeField]
    public List<Image> heartImages;
    public Sprite[] heartSprites;

    int[] heartIndices = new int[] { 4, 4, 4 };
    int heartImageIndex = 0;

    private void Awake()
    {
        maxHealth = 12;
    }

    public override void TakeDamage(int dmg, Interactables tag)
    {
        base.TakeDamage(dmg, tag);
        UpdateHearts();
        ShowDamagePanel();
    }

    public void HealDamage(int dmg)
    {
        currentHealth += dmg;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHearts();
    }

    void ShowDamagePanel()
    {
        damagePanel.color = new Color32(255, 73, 71, 100);
        StartCoroutine(HideDamagePanel());
    }

    IEnumerator HideDamagePanel()
    {
        yield return new WaitForSeconds(0.05f);
        damagePanel.color = new Color32(255, 73, 71, 0);
    }

    void UpdateHearts()
    {
        heartImageIndex = 2;
        int cHealthCounter = currentHealth;
        while(cHealthCounter >= 0 && heartImageIndex >= 0)
        {
            if(cHealthCounter > 4)
            {
                heartIndices[heartImageIndex] = 4;
            }
            else
            {
                heartIndices[heartImageIndex] = cHealthCounter;

            }
            heartImageIndex--;
            cHealthCounter -= 4;
        }
        for(int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = heartSprites[heartIndices[i]];
        }
    }
    /*
    void UpdateHearts(int dmg)
    {
        if (heartIndices[heartImageIndex] - dmg < 0)
        {
            int nextDmg = Math.Abs(heartIndices[heartImageIndex] - dmg);
            heartIndices[heartImageIndex] = 0;
            if (heartImageIndex + 1 < heartImages.Count)
            {
                heartImageIndex++;
            }
            heartIndices[heartImageIndex] = heartIndices[heartImageIndex] - nextDmg;
        }
        else
        {
            heartIndices[heartImageIndex] = heartIndices[heartImageIndex] - dmg;
        }
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = heartSprites[heartIndices[i]];
        }
    }
    */
}
