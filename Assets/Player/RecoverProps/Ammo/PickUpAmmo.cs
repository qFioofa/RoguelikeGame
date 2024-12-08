using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : PickUp {
    protected override void PickedUp() {
        playerInfo.AddOneMag();
        base.PickedUp();
    }
}
