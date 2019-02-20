﻿//Player stats manager
//by Bartek
//
//Actual stats:
// - health
// - stamina

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Health stat")]
    public int maxHealth = 1000;
    public int actualHealth = 1000;
    [Space]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;

    [Header("Stamina stat")]
    public int maxStamina = 1000;
    public int actualStamina = 1000;
    [Space]
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Image staminaBar;
    [Space]
    public float startRegenStaminaAfter = 10;
    public float regenStaminaInterval = 0.25f;

    private bool isRegenerating = false;
    private IEnumerator regenStaminaCor;

    private void Start()
    {
        regenStaminaCor = RegenStamina();
    }

    private void Update()
    {
        SetUI();
        CheckValues();

        if(actualStamina < maxStamina && !isRegenerating)
        {
            Debug.Log("start");
            StartCoroutine(regenStaminaCor);
        }
    }

    #region Health manager functions
    public void AddHealth (int healthToAdd)
    {
        if (healthToAdd + actualHealth <= maxHealth)
            actualHealth += healthToAdd;
        else if (healthToAdd + actualHealth > maxHealth)
            actualHealth = maxHealth;

        Debug.Log("Stamina added = " + healthToAdd);
    }

    public void MinusHealth(int healthToMinus)
    {
        if (actualHealth - healthToMinus > 0)
            actualHealth -= healthToMinus;
        else if (healthToMinus - actualHealth < 0)
            actualHealth = 0;

        Debug.Log("Stamina minus = " + healthToMinus);
    }
    #endregion

    #region Stamina manager functions
    public void AddStamina(int staminaToAdd)
    {
        if (staminaToAdd + actualStamina <= maxStamina)
            actualStamina += staminaToAdd;
        else if (staminaToAdd + actualStamina > maxStamina)
            actualStamina = maxStamina;

        Debug.Log("Stamina added = " + staminaToAdd);
    }

    public void MinusStamina(int staminaToMinus)
    {
        if (actualStamina - staminaToMinus > 0)
            actualStamina -= staminaToMinus;
        else if (staminaToMinus - actualStamina < 0)
            actualStamina = 0;

        Debug.Log("Stamina minus = " + staminaToMinus);
    }

    private IEnumerator RegenStamina ()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(startRegenStaminaAfter);
        while(actualStamina < maxStamina)
        {
            actualStamina += 5;
            yield return new WaitForSeconds(regenStaminaInterval);
        }
        isRegenerating = false;
    }
    #endregion

    private void CheckValues ()
    {
        if (actualHealth > maxHealth)
            actualHealth = maxHealth;

        if (actualStamina > maxStamina)
            actualStamina = maxStamina;

        if (actualHealth < 0)
            actualHealth = 0;

        if (actualStamina < 0)
            actualStamina = 0;
    }

    private void SetUI ()
    {
        healthBar.fillAmount = (float)actualHealth / maxHealth;
        staminaBar.fillAmount = (float)actualStamina / maxStamina;
        healthText.text = actualHealth + " / " + maxHealth;
        staminaText.text = actualStamina + " / " + maxStamina;
    }
}
