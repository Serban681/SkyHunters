using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlaneMovement
{
	private float distanceFromPlayer = 0f;

	public enum EnemyState
	{
		Patrolling,
		Attacking
	}

	public EnemyState currentState;

	[HideInInspector]
	public bool isChasingPlayer = false;
	public GameObject arrow;
    private Vector3 playerDir;
	//[HideInInspector]
    private float _timer = 0f;
    public float timeToRotate = 3.5f;
    public float rotationTime = 6f;
    private Transform _firePoint;
	private GameObject player;
	[HideInInspector]
	public GameObject arrowInstance;

	private float shootTimer = 0f;
	private float timerBetweenShots = 1f;

	private LayerMask layerMask;
	private bool dissabled = false;

	//Heavy Planes
	private Vector2 dir;
	private float initialRotationZ;
	private float incrementer;

	private float patrolTimer = 0f;
	private float attackTimer = 0f;

	private float rotZ;

	private PlaneShooter shooter;

	public float delay = 2f;
	private float delayTimer = 0f;

	public float dirDelay = 0.1f;
	private float dirDelayTimer = 0f;

	public bool heavyPlane;

	protected override void Start()
    {
		shooter = GetComponent<PlaneShooter>();

		player = FindObjectOfType<PlayerController>().transform.gameObject;
		rotZ = Random.Range(-30, 30);

		currentState = EnemyState.Patrolling;

		initialRotationZ = transform.rotation.eulerAngles.z;
		
		arrowInstance = Instantiate(arrow, player.transform);
		arrowInstance.transform.GetComponent<PointingArrowScript>().AssignTarget(transform);
        base.Start();
        _firePoint = transform.Find("FirePoint").transform;

		layerMask = ~(1 << 9);
	}

	protected override void Update()
    {
		if (heavyPlane == true)
		{
			if (transform.position.x < -820 || transform.position.x > 820 || transform.position.y < -820 || transform.position.y > 820)
			{
				transform.GetComponent<EnemyController>().ArrowDestroyer();
				FindObjectOfType<EnemySpawner>().numberOfBombers--;
				Destroy(gameObject);
			}

			if (StaticVariables.playerHealth > 0 && delayTimer >= delay)
			{
				distanceFromPlayer = Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position);
				delayTimer = 0f;
			}

			if (distanceFromPlayer <= 250)
			{
				rb.velocity = transform.right * speed * Time.deltaTime;
			}
			else
			{
				rb.velocity = transform.right * 0 * Time.deltaTime;
			}

			delayTimer += Time.deltaTime;
		}
		else
		{
			rb.velocity = transform.right * speed * Time.deltaTime;

			dirDelayTimer += Time.deltaTime;
		}

		if (heavyPlane == false)
		{
			switch (currentState)
			{
				case EnemyState.Patrolling:
					Patrolling();
					break;

				case EnemyState.Attacking:
					Attacking();
					break;
			}

			if (Physics2D.Raycast(_firePoint.position, transform.right, 40, layerMask))
			{
				RaycastHit2D ray = Physics2D.Raycast(_firePoint.position, transform.right, 40, layerMask);
				if (ray.transform.tag == "Player")
				{
					if (shootTimer > timerBetweenShots)
						StartCoroutine(EnemyShoot());
					//currentState = EnemyState.Attacking;
				}
			}

			if (_timer >= timeToRotate)
			{
				currentState = EnemyState.Attacking;
			}
			else
			{
				currentState = EnemyState.Patrolling;
			}

			_timer += Time.deltaTime;
			shootTimer += Time.deltaTime;
		}
		else
		{
			if (_timer >= timeToRotate)
			{
				if (dissabled == false)
				{
					incrementer = Random.Range(-5, 5);
					_timer = 0f;
				}
				timeToRotate = Random.Range(2, 2.5f);
			}

			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, initialRotationZ + incrementer), rotationSpeed);
			_timer += Time.deltaTime;
		}

		base.Update();
    }
	/*
    private IEnumerator Rotate()
    {
		if (dissabled == false)
		{
			
			yield return new WaitForSeconds(rotationTime);
			isRotating = false;
			timer = 0f;
		}
    }
	*/
	private IEnumerator EnemyShoot()
	{
		shooter.Shoot();
		yield return new WaitForSeconds(0.5f);
		timerBetweenShots = Random.Range(0.5f, 1f);
		shootTimer = 0f;
	}

	public void ArrowDestroyer()
	{
		Destroy(arrowInstance);
	}

	public void Dissable()
	{
		rotationSpeed = 0.1f;
		Rotate(0, -1);
		dissabled = true;
	}

	private void Attacking()
	{
		if (attackTimer >= rotationTime)
		{
			currentState = EnemyState.Patrolling;
			timeToRotate = Random.Range(2, 3.5f);
			attackTimer = 0f;
			_timer = 0f;
		}
		else
		{
			if (StaticVariables.playerHealth > 0 && dirDelayTimer >= dirDelay)
			{
				playerDir = PlayerController.instance.transform.position - transform.position;
				playerDir.Normalize();

				dirDelayTimer = 0f;
			}

			if (dissabled == false)
			{
				isChasingPlayer = true;
				Rotate(playerDir.x, playerDir.y);
			}

			attackTimer += Time.deltaTime;
		}
	}

	private void Patrolling()
	{
		isChasingPlayer = false;

		if (patrolTimer >= 4f)
		{
			initialRotationZ = transform.rotation.eulerAngles.z;
			rotZ = Random.Range(-30, 30);
			patrolTimer = 0f;
		}
		else
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.z + rotZ), 0.5f);

		patrolTimer += Time.deltaTime;
	}
}
