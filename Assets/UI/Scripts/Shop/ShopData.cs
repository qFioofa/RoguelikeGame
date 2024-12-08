using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Shop/Shop Data")]
public class ShopData : ScriptableObject {
    [Header("Player")]
    public int playerHPIncreased = 10;
    public int hpUpgradeCost = 100;

    [Header("p228")]
    public int p288DmgIncreased = 5;
    public int gunDamageUpgradeCost = 100;
    public int gunMagazineUpgradeCost = 100;

    [Header("Galil")]
    public int galilDmgIncreased = 5;
    public int galilMagazineIncreased = 5;
    public int galilDamageUpgradeCost = 100;
    public int galilMagazineUpgradeCost = 100;

    [Header("other")]
    public float priceMultiplier = 1.5f;

    public static void Copy(ShopData from, ShopData other){
        from.playerHPIncreased = other.playerHPIncreased;
        from.hpUpgradeCost = other.hpUpgradeCost;
        from.p288DmgIncreased = other.p288DmgIncreased;
        from.gunDamageUpgradeCost = other.gunDamageUpgradeCost;
        from.gunMagazineUpgradeCost = other.gunMagazineUpgradeCost;
        from.galilMagazineIncreased = other.galilMagazineIncreased;
        from.galilDamageUpgradeCost = other.galilDamageUpgradeCost;
        from.galilMagazineUpgradeCost = other.galilMagazineUpgradeCost;
        from.priceMultiplier = other.priceMultiplier;
    }
}