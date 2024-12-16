using UnityEngine;
using UnityEngine.AI;

public class Runner : IDamageable
{
    [Header("Настройки здоровья")]
    [SerializeField] private float HP = 100;

    [Header("Настройки стрельбы")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootDistance = 50f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] [Range(0f, 1f)] private float hitProbability = 0.7f;
    [SerializeField] private int shotsBeforeReload = 5;

    [Header("Souds")]

    [SerializeField] private AudioClip shotSound;

    [SerializeField] private AudioClip reloadSound;

    [SerializeField] private AudioClip[] takeDamageSound;
    [SerializeField] private AudioClip[] dieSound;

    private Animator animator;
    private NavMeshAgent navAgent;
    private Transform target;
    private float nextFireTime = 0f;
    private int shotsFired = 0;
    private bool isReloading = false;

    private bool dead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0) Die();
        else {
            SoundFXManager.PlaySoundClipForce(takeDamageSound[Random.Range(0,takeDamageSound.Length-1)],transform);
            animator.SetTrigger("DAMAGE");
        }
    }

    public override void Die() {
        if(dead) return;
        dead = true;
        SoundFXManager.PlaySoundClipForce(dieSound[Random.Range(0,dieSound.Length-1)],transform);
        animator.SetTrigger("DIE");
        navAgent.isStopped = true;
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        if (HP <= 0 || isReloading) return;

        navAgent.SetDestination(target.position);

        animator.SetBool("isWalking", navAgent.velocity.magnitude > 0.1f);

        TryShoot();
    }

    private void TryShoot()
    {
        if(!CanSeePlayer()) return;
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            animator.SetTrigger("Shoot");

            shotsFired++;

            if (shotsFired >= shotsBeforeReload)
            {
                StartReloading();
                return;
            }

            if (Random.value > hitProbability) return;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > shootDistance) return;

            PlaySoundShot();

            Vector3 directionToTarget = (target.position - shootPoint.position).normalized;
            Ray ray = new Ray(shootPoint.position, directionToTarget);
            RaycastHit hitInfo;

            Debug.DrawRay(shootPoint.position, directionToTarget * shootDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out hitInfo, shootDistance))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    PlayerInfo playerInfo = hitInfo.collider.GetComponent<PlayerInfo>();
                    if (playerInfo != null)
                    {
                        playerInfo.Damage(damage);
                    }
                }
            }
        }
    }

    private void StartReloading()
    {
        isReloading = true;
        animator.SetTrigger("Reload");

        Invoke(nameof(PlaySoundReload), 2f);
        Invoke(nameof(FinishReloading), 2f);
    }

    private bool CanSeePlayer() 
    { 
        Vector3 directionToPlayer = target.position - transform.position; 
        float distanceToPlayer = directionToPlayer.magnitude; 
 
        if (distanceToPlayer <= shootDistance) 
        { 
            Ray ray = new Ray(shootPoint.position, directionToPlayer.normalized); 
            RaycastHit hitInfo; 
 
            if (Physics.Raycast(ray, out hitInfo, distanceToPlayer)) 
            { 
                return hitInfo.collider.CompareTag("Player"); 
            } 
        } 
 
        return false;
    }


    private void FinishReloading()
    {
        isReloading = false;
        shotsFired = 0;
    }

    public void PlaySoundShot(){
        SoundFXManager.PlaySoundClipForce(shotSound, transform, 0.5f);
    }

    public void PlaySoundReload(){
        SoundFXManager.PlaySoundClipForce(reloadSound, transform, 0.5f);
    }
}
