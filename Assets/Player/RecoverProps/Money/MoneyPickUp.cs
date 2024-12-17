using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyPickUp : PickUp {
    [SerializeField] private int value = 50;
    public static UnityEvent<int> OnMoneyPickedUp = new UnityEvent<int>();
    protected override void PickedUp() {
        playerInfo.AddMoney(value);
        OnMoneyPickedUp.Invoke(value);
        base.PickedUp();
    }
}