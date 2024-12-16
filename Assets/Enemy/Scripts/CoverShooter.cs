using UnityEngine;
using UnityEngine.AI;

public class CoverShooter : MonoBehaviour, IDamageable
{
    [Header("Настройки здоровья")]
    [SerializeField] private float HP = 100;

    [Header("Настройки стрельбы")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootDistance = 50f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] [Range(0f, 1f)] private float hitProbability = 0.7f;
    [SerializeField] private int shotsBeforeReload = 5; // Количество выстрелов до перезарядки

    [Header("Настройки укрытий")]
    [SerializeField] private Transform[] coverPoints; // Массив укрытий
    [SerializeField] private float changeCoverDistance = 10f; // Расстояние до игрока для смены укрытия
    [SerializeField] private float timeInCover = 3f; // Время, которое враг будет проводить в укрытии

    private Animator animator;
    private NavMeshAgent navAgent;
    private Transform currentCover; // Текущее укрытие
    private Transform target;
    private float nextFireTime = 0f;
    private float coverExitTime = 0f;

    private int shotsFired = 0;
    private bool isReloading = false; // Флаг перезарядки

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();


        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }

        FindNewCover();
        coverExitTime = Time.time + timeInCover;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("DIE");
            navAgent.isStopped = true;
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

    private void Update()
    {
        if (HP <= 0 || target == null || isReloading) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < changeCoverDistance)
        {
            FindNewCover(); // Сменить укрытие
        }

        // Если пришло время выходить из укрытия
        if (Time.time >= coverExitTime)
        {
            ExitCoverAndShoot();
        }

        // Если укрытие найдено, враг движется к нему
        if (currentCover != null)
        {
            navAgent.SetDestination(currentCover.position);
            animator.SetBool("isWalking", navAgent.velocity.magnitude > 0.1f);
        }
    }

    private void FindNewCover()
    {
        float minDistance = Mathf.Infinity;
        Transform bestCover = null;

        // Поиск ближайшего укрытия
        foreach (Transform cover in coverPoints)
        {
            if (cover == currentCover) continue; // Пропускаем текущее укрытие

            float distance = Vector3.Distance(cover.position, target.position);
            if (distance > changeCoverDistance && distance < minDistance)
            {
                minDistance = distance;
                bestCover = cover;
            }
        }

        if (bestCover != null)
        {
            currentCover = bestCover;
            navAgent.SetDestination(currentCover.position);
            coverExitTime = Time.time + timeInCover; 
        }
    }

    private void ExitCoverAndShoot()
    {

        // Останавливаемся на месте для стрельбы
        navAgent.isStopped = true;

        // Поворачиваемся к игроку
        RotateTowardsTarget();

        // Стреляем
        TryShoot();

        // Возвращаемся в укрытие после выстрела
        coverExitTime = Time.time + timeInCover; // Устанавливаем время для следующего выхода
		navAgent.isStopped = false;
		navAgent.SetDestination(currentCover.position);
	}

	private void TryShoot()
	{
		if (Time.time < nextFireTime || target == null) return;

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

	private void StartReloading()
	{
	    isReloading = true;
	    animator.SetTrigger("Reload");


	    Invoke(nameof(FinishReloading), 2f);
	}

	private void FinishReloading()
	{
	    isReloading = false;
	    shotsFired = 0;
	}

	private void RotateTowardsTarget()
	{
	    if (target == null) return;

	    Vector3 directionToPlayer = target.position - transform.position;
	    directionToPlayer.y = 0f;
	    Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
	    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
	}
}
