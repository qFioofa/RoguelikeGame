using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealPickUp : PickUp {
    [SerializeField] private int healthRecover = 45;
    public static UnityEvent<int> OnHealthPickedUp = new UnityEvent<int>();
    protected override void PickedUp() {
        playerInfo.AddHealth(healthRecover);
        OnHealthPickedUp.Invoke(healthRecover);
        base.PickedUp();
    }
}