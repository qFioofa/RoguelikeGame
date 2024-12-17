using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyInfo {
    public float shootDistance = 50f;
    public float fireRate = 1f;
    public int damage = 10;
    public int shotsBeforeReload = 5;
    public float hitProbability = 0.7f;
    public float HP = 20f;
    public static void Copy(EnemyInfo from, EnemyInfo to) {
        to.shootDistance = from.shootDistance;
        to.fireRate = from.fireRate;
        to.damage = from.damage;
        to.shotsBeforeReload = from.shotsBeforeReload;
        to.hitProbability = from.hitProbability;
    }
}