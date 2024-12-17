using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class IDamageable : MonoBehaviour {
    [Header("Sounds")]
    [SerializeField] protected AudioClip shotSound;
    [SerializeField] protected AudioClip[] reloadSound;
    private int reloadPtr = 0;
    [SerializeField] protected AudioClip[] takeDamageSound;
    [SerializeField] protected AudioClip[] dieSound;
    protected Transform target;
    protected Animator animator;
    protected NavMeshAgent navAgent;

    [Header("Settings")]
    [SerializeField] protected EnemyInfo enemyInfo = new EnemyInfo();

    public EnemyInfo EnemyInfo{
        set { EnemyInfo.Copy(value, enemyInfo); }
    }

    [Header("Player")]    
    protected int shotsFired = 0;
    protected bool isReloading = false;
    protected bool dead = false;
    [Header("Stats")]
    [SerializeField] private GameObject MoneyPrefab;
    [SerializeField] private GameObject HealthPrefab;
    [SerializeField] private GameObject AmmoPrefab;

    [Header("Chanses")]
    [Range(0,1)] private float MoneyChanse = 1f;
    [Range(0,1)] private float HealthChanse = 0.3f;
    [Range(0,1)] private float AmmoChanse = 0.4f;
    public static UnityEvent<int> DeadCount = new UnityEvent<int>();
    protected virtual void Start(){
        enemyInfo = new EnemyInfo();
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        FindPlayer();
    }
    protected virtual void FindPlayer() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) {
            target = playerObject.transform;
        }
    }
    public virtual void TakeDamage(float damageAmount) {
        enemyInfo.HP -= damageAmount;
        if (enemyInfo.HP <= 0) Die();
        else {
            SoundFXManager.PlaySoundClipForce(takeDamageSound[Random.Range(0,takeDamageSound.Length-1)],transform);
            animator.SetTrigger("DAMAGE");
        }
    }
    public virtual void Die() {
        if(dead) return;
        dead = true;
        SpawnRecoverProps();
        DeadCount.Invoke(1);
        SoundFXManager.PlaySoundClipForce(dieSound[Random.Range(0,dieSound.Length-1)],transform);
        animator.SetTrigger("DIE");
        navAgent.isStopped = true;
        Destroy(gameObject, 2f);
    }

    protected virtual void SpawnRecoverProps(){
        if(Random.value < MoneyChanse) Instantiate(MoneyPrefab, transform.position + RandomVector(), MoneyPrefab.transform.localRotation);
        if(Random.value < HealthChanse) Instantiate(HealthPrefab, transform.position + RandomVector(), HealthPrefab.transform.localRotation);
        if(Random.value < AmmoChanse) Instantiate(AmmoPrefab, transform.position + RandomVector(), AmmoPrefab.transform.localRotation);
    }

    protected virtual Vector3 RandomVector(){
        return new Vector3(Random.Range(0f,2f),Random.Range(-0.2f,0.1f),Random.Range(0f,2f));
    }
    protected virtual void FinishReloading() {
        isReloading = false;
        shotsFired = 0;
    }

    protected int getReloadSoundPtr(){
        ++reloadPtr;
        if(reloadPtr>=reloadSound.Length) reloadPtr = 0;
        return reloadPtr;
    }
}