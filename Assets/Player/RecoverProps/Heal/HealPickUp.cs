using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUp : PickUp {
    [SerializeField] private int healthRecover = 45;
    protected override void PickedUp() {
        playerInfo.AddHealth(healthRecover);
        base.Destroy();
    }
}