using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : PickUp {
    [SerializeField] private int value = 50;
    protected override void PickedUp() {
        playerInfo.AddMoney(value);
        base.Destroy();
    }
}