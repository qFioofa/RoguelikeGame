using UnityEngine;
using UnityEngine.AI;
public class IDamageable : MonoBehaviour {
    public virtual void TakeDamage(float damage){}
    public virtual void Die(){}
}