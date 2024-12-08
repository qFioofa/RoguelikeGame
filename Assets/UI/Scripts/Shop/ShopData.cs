using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Shop Data")]
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

    public static ShopDataAttributes FromScriptableObject(ShopData saveData) {
        return new ShopDataAttributes {
            playerHPIncreased = saveData.playerHPIncreased,
            hpUpgradeCost = saveData.hpUpgradeCost,
            p288DmgIncreased = saveData.p288DmgIncreased,
            gunDamageUpgradeCost = saveData.gunDamageUpgradeCost,
            gunMagazineUpgradeCost = saveData.gunMagazineUpgradeCost,
            galilDmgIncreased = saveData.galilDmgIncreased,
            galilMagazineIncreased = saveData.galilMagazineIncreased,
            galilDamageUpgradeCost = saveData.galilDamageUpgradeCost,
            galilMagazineUpgradeCost = saveData.galilMagazineUpgradeCost,
            priceMultiplier = saveData.priceMultiplier
        };
    }

    public static ShopData FromAttributes(ShopDataAttributes attributes) {
        ShopData shopData = ScriptableObject.CreateInstance<ShopData>();
        shopData.playerHPIncreased = attributes.playerHPIncreased;
        shopData.hpUpgradeCost = attributes.hpUpgradeCost;
        shopData.p288DmgIncreased = attributes.p288DmgIncreased;
        shopData.gunDamageUpgradeCost = attributes.gunDamageUpgradeCost;
        shopData.gunMagazineUpgradeCost = attributes.gunMagazineUpgradeCost;
        shopData.galilDmgIncreased = attributes.galilDmgIncreased;
        shopData.galilMagazineIncreased = attributes.galilMagazineIncreased;
        shopData.galilDamageUpgradeCost = attributes.galilDamageUpgradeCost;
        shopData.galilMagazineUpgradeCost = attributes.galilMagazineUpgradeCost;
        shopData.priceMultiplier = attributes.priceMultiplier;
        return shopData;
    }

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

[System.Serializable]
public class ShopDataAttributes {
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

    public static ShopDataAttributes FromScriptableObject(ShopData saveData) {
        return new ShopDataAttributes {
            playerHPIncreased = saveData.playerHPIncreased,
            hpUpgradeCost = saveData.hpUpgradeCost,
            p288DmgIncreased = saveData.p288DmgIncreased,
            gunDamageUpgradeCost = saveData.gunDamageUpgradeCost,
            gunMagazineUpgradeCost = saveData.gunMagazineUpgradeCost,
            galilDmgIncreased = saveData.galilDmgIncreased,
            galilMagazineIncreased = saveData.galilMagazineIncreased,
            galilDamageUpgradeCost = saveData.galilDamageUpgradeCost,
            galilMagazineUpgradeCost = saveData.galilMagazineUpgradeCost,
            priceMultiplier = saveData.priceMultiplier
        };
    }

    public static ShopData FromAttributes(ShopDataAttributes attributes) {
        ShopData shopData = ScriptableObject.CreateInstance<ShopData>();
        shopData.playerHPIncreased = attributes.playerHPIncreased;
        shopData.hpUpgradeCost = attributes.hpUpgradeCost;
        shopData.p288DmgIncreased = attributes.p288DmgIncreased;
        shopData.gunDamageUpgradeCost = attributes.gunDamageUpgradeCost;
        shopData.gunMagazineUpgradeCost = attributes.gunMagazineUpgradeCost;
        shopData.galilDmgIncreased = attributes.galilDmgIncreased;
        shopData.galilMagazineIncreased = attributes.galilMagazineIncreased;
        shopData.galilDamageUpgradeCost = attributes.galilDamageUpgradeCost;
        shopData.galilMagazineUpgradeCost = attributes.galilMagazineUpgradeCost;
        shopData.priceMultiplier = attributes.priceMultiplier;
        return shopData;
    }

    public static void Copy(ShopDataAttributes from, ShopDataAttributes to) {
        to.playerHPIncreased = from.playerHPIncreased;
        to.hpUpgradeCost = from.hpUpgradeCost;
        to.p288DmgIncreased = from.p288DmgIncreased;
        to.gunDamageUpgradeCost = from.gunDamageUpgradeCost;
        to.gunMagazineUpgradeCost = from.gunMagazineUpgradeCost;
        to.galilDmgIncreased = from.galilDmgIncreased;
        to.galilMagazineIncreased = from.galilMagazineIncreased;
        to.galilDamageUpgradeCost = from.galilDamageUpgradeCost;
        to.galilMagazineUpgradeCost = from.galilMagazineUpgradeCost;
        to.priceMultiplier = from.priceMultiplier;
    }

}