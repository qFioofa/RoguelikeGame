using UnityEngine;

public class EnemyLife : MonoBehaviour //tmp script for enemy to kill player + take damage and die
{
    private float health = 50f;
    
    public PlayerInfo playerInfo;

    protected virtual void Start()
    {
        playerInfo = GameObject.FindWithTag("Player")?.GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other) // kill player - change to raycast of the enemy's weapon
    {
        if (other.CompareTag("Player"))
        {
            if (playerInfo != null)
            {
                playerInfo.Damage(100);
            }
        }
    }
    public void TakeDamage(float received_damage)
    {
        health -= received_damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
