using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CoverShooter : IDamageable { 
    [Header("Настройки стрельбы")]
    [SerializeField] private Transform shootPoint;

    [Header("Настройки укрытий")]
    [SerializeField] private Transform[] coverPoints;
    [SerializeField] private float changeCoverDistance = 10f;
    [SerializeField] private float timeInCover = 3f;
    private Transform currentCover;
    private float nextFireTime = 0f;
    private float coverExitTime = 0f;

    protected override void Start() {
        base.Start();
        FindCoverPoints(transform);
        coverExitTime = Time.time + timeInCover;
    }

    public void FindCoverPoints(Transform where) {
        Transform propsParent = where.Find("Props");
        
        if (propsParent != null) {
            coverPoints = propsParent.GetComponentsInChildren<Transform>();
            Debug.Log(coverPoints.Length);
        }
        else coverPoints = new Transform[0];
    }

    private void Update()
    {
        if (enemyInfo.HP <= 0 || target == null || isReloading) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < changeCoverDistance)
        {
            FindNewCover();
        }


        if (Time.time >= coverExitTime)
        {
            ExitCoverAndShoot();
        }

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

        foreach (Transform cover in coverPoints)
        {
            if (cover == currentCover) continue;

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

    private void ExitCoverAndShoot(){
        navAgent.isStopped = true;

        RotateTowardsTarget();
        TryShoot();

        coverExitTime = Time.time + timeInCover;
		navAgent.isStopped = false;
        if(currentCover == null) return;
		navAgent.SetDestination(currentCover.position);
	}

	private void TryShoot()
	{
		if (Time.time < nextFireTime || target == null) return;

		nextFireTime = Time.time + enemyInfo.fireRate;

		animator.SetTrigger("Shoot");

		shotsFired++;

		if (shotsFired >= enemyInfo.shotsBeforeReload)
		{
			StartReloading();
			return;
		}

		if (Random.value < enemyInfo. hitProbability) return;

		float distanceToTarget = Vector3.Distance(transform.position, target.position);
		if (distanceToTarget > enemyInfo.shootDistance) return;

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

	private void StartReloading() {
	    isReloading = true;
	    animator.SetTrigger("Reload");

	    Invoke(nameof(FinishReloading), 2f);
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
