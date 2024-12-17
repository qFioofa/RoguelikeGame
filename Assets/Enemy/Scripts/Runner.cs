using UnityEngine;
using UnityEngine.AI;

public class Runner : IDamageable {
    [Header("Настройки стрельбы")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int shotsBeforeReload = 5;
    private float nextFireTime = 0f;

    private void Update()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        if (enemyInfo.HP <= 0 || isReloading) return;

        if(navAgent != null) navAgent.SetDestination(target.position);

        animator.SetBool("isWalking", navAgent.velocity.magnitude > 0.1f);

        TryShoot();
    }

    private void TryShoot() {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + enemyInfo.fireRate;

            animator.SetTrigger("Shoot");

            shotsFired++;

            if (shotsFired >= shotsBeforeReload)
            {
                StartReloading();
                return;
            }

            if (Random.value < enemyInfo.hitProbability) return;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > enemyInfo.shootDistance) return;

            PlaySoundShot();

            Vector3 directionToTarget = (target.position - shootPoint.position).normalized;
            Ray ray = new Ray(shootPoint.position, directionToTarget);
            RaycastHit hitInfo;

            Debug.DrawRay(shootPoint.position, directionToTarget * enemyInfo.shootDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out hitInfo, enemyInfo.shootDistance))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    PlayerInfo playerInfo = hitInfo.collider.GetComponent<PlayerInfo>();
                    if (playerInfo != null)
                    {
                        playerInfo.Damage(enemyInfo.damage);
                    }
                }
            }
        }
    }

    private void StartReloading()
    {
        isReloading = true;
        animator.SetTrigger("Reload");

        Invoke(nameof(PlaySoundReload), 0.1f);
        Invoke(nameof(PlaySoundReload), 1.5f);
        Invoke(nameof(PlaySoundReload), 1.7f);
        Invoke(nameof(PlaySoundReload), 2f);
        Invoke(nameof(FinishReloading), 2.3f);
    }

    public void PlaySoundShot(){
        SoundFXManager.PlaySoundClipForce(shotSound, transform, 0.5f);
    }

    public void PlaySoundReload(){
        SoundFXManager.PlaySoundClipForce(reloadSound[getReloadSoundPtr()], transform, 0.5f);
    }
}
