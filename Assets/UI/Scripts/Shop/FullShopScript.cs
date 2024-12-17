using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Transactions;
using UnityEngine.Rendering;

public class FullShopScript : MonoBehaviour {
    [Header("Reset properties")]
    public PlayerData playerDataReset;
    public WeaponData galilWeaponDataReset;
    public WeaponData p228WeaponDataReset;
    public ShopData shopDataReset;

    [Header("Shop")]
    public ShopData shopData;

    [Header("Canvases")]
    public GameObject shopCanvas;
    public GameObject menuCanvas;
    public GameObject settingsCanvas;

    [Header("Player")]
    public PlayerData playerData;

    [Header("Galil")]
    public WeaponData galilWeaponData;

    [Header("p288")]
    public WeaponData p228WeaponData;

    [Header("Currency")]
    public AudioClip Perchesed;
    public AudioClip NoMoney;

    [Header("Other")]
    public AudioClip ambient;
    [Range(0f,1f)] public float Volume = 1f;
    public SoundHandler returnAmbient;
    private AudioClip lastAmbient;
    
    [Header("UI elements")]
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI galilStatsText;
    public TextMeshProUGUI gunStatsText;
    public TextMeshProUGUI coinsText;

    public TextMeshProUGUI hpButtonText;
    public TextMeshProUGUI galilDamageButtonText;
    public TextMeshProUGUI galilMagazineButtonText;
    public TextMeshProUGUI gunDamageButtonText;
    public TextMeshProUGUI gunMagazineButtonText;

    void Start() {
        UpdateUI();
    }

    public void ResetProgress(){
        PlayerData.Copy(playerData, playerDataReset);
        WeaponData.Copy(galilWeaponData, galilWeaponDataReset);
        WeaponData.Copy(p228WeaponData, p228WeaponDataReset);
        ShopData.Copy(shopData, shopDataReset);
        UpdateUI();
    }

    public void ShowShopUI(){
        shopCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        PlayAmbient();
    }

    private void PlayAmbient(){
        returnAmbient.Reset();
        lastAmbient = returnAmbient.Ambient;
        returnAmbient.Ambient = ambient;
    }

    public void HideShopUI(){
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        returnAmbient.Reset();
        returnAmbient.Ambient = lastAmbient;
    }
    public void UpgradePlayerHP()
    {
        if (TrySpendCoins(shopData.hpUpgradeCost))
        {
            playerData.HealthLimit += shopData.playerHPIncreased;
            shopData.hpUpgradeCost = Mathf.CeilToInt(shopData.hpUpgradeCost * shopData.priceMultiplier);
            UpdateUI();
        }
    }

    public void UpgradeGalilDamage()
    {
        if (TrySpendCoins(shopData.galilDamageUpgradeCost))
        {
            galilWeaponData.damage += shopData.galilDmgIncreased;
            shopData.galilDamageUpgradeCost = Mathf.CeilToInt(shopData.galilDamageUpgradeCost * shopData.priceMultiplier);
            UpdateUI();
        }
    }

    public void UpgradeGalilMagazine()
    {
        if (TrySpendCoins(shopData.galilMagazineUpgradeCost))
        {
            galilWeaponData.magCapacity += shopData.galilDmgIncreased;
            shopData.galilMagazineUpgradeCost = Mathf.CeilToInt(shopData.galilMagazineUpgradeCost * shopData.priceMultiplier);
            UpdateUI();
        }
    }

    public void UpgradeGunDamage()
    {
        if (TrySpendCoins(shopData.gunDamageUpgradeCost))
        {
            p228WeaponData.damage += shopData.p288DmgIncreased;
            shopData.gunDamageUpgradeCost = Mathf.CeilToInt(shopData.gunDamageUpgradeCost * shopData.priceMultiplier);
            UpdateUI();
        }
    }

    public void UpgradeGunMagazine()
    {
        if (TrySpendCoins(shopData.gunMagazineUpgradeCost))
        {
            p228WeaponData.magCapacity += shopData.galilMagazineIncreased;
            shopData.gunMagazineUpgradeCost = Mathf.CeilToInt(shopData.gunMagazineUpgradeCost * shopData.priceMultiplier);
            UpdateUI();
        }
    }
    private bool TrySpendCoins(int cost)
    {
        if (playerData.Money >= cost)
        {
            playerData.Money -= cost;
            SoundFXManager.PlaySoundClipForce(Perchesed,transform);
            return true;
        }
        else
        {
            SoundFXManager.PlaySoundClipForce(NoMoney, transform);
            return false;
        }
    }
    private void UpdateUI()
    {
        // Игрок
        playerHPText.text = "HP: " + playerData.HealthLimit;
        hpButtonText.text = "Upgrade HP\nCost: " + shopData.hpUpgradeCost;

        // Galil
        galilStatsText.text = "Damage: " + galilWeaponData.damage + "\nMagazine: " + galilWeaponData.magCapacity;
        galilDamageButtonText.text = "Upgrade Damage\nCost: " + shopData.galilDamageUpgradeCost;
        galilMagazineButtonText.text = "Upgrade Magazine\nCost: " + shopData.galilMagazineUpgradeCost;

        // Gun
        gunStatsText.text = "Damage: " + p228WeaponData.damage + "\nMagazine: " + p228WeaponData.magCapacity;
        gunDamageButtonText.text = "Upgrade Damage\nCost: " + shopData.gunDamageUpgradeCost;
        gunMagazineButtonText.text = "Upgrade Magazine\nCost: " + shopData.gunMagazineUpgradeCost;

        // Монеты
        coinsText.text = "$ " + playerData.Money;
    }
}