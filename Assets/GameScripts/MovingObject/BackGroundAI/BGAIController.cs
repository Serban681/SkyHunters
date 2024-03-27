using System.Collections;
using UnityEngine;

public class BGAIController : PlaneMovement
{
	private float distanceFromPlayer = 0f;

	public enum AIState
	{
		Patrolling,
		Attacking
	}

	public enum Side
	{
		Enemy,
		Player
	}

	private AIState currentState;
	private Side side;

	[HideInInspector]
	public bool isChasingEnemy = false;

	private Vector3 enemyDir;
	private float _timer = 0f;
	public float timeToRotate = 3.5f;
	public float rotationTime = 6f;
	private Transform _firePoint;
	private Transform target;

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

	public float dirDelay = 0.3f;
	private float dirDelayTimer = 0f;

	public bool heavyPlane;

	private bool areEnemies
	{
		get
		{
			if(side == Side.Player)
			{
				if (EnemySide.Pool.Count == 0)
					return false;

				return true;
			}
			else
			{
				if (PlayerSide.Pool.Count == 0)
					return false;

				return true;
			}
		}
	}

	protected override void Start()
	{
		shooter = GetComponent<PlaneShooter>();

		InitializeSide();
		InitializeTargetLayer();

		rotZ = Random.Range(-30, 30);

		currentState = AIState.Patrolling;

		initialRotationZ = transform.rotation.eulerAngles.z;
		
		_firePoint = transform.Find("FirePoint").transform;

		base.Start();
	}

	protected override void Update()
	{
		rb.velocity = transform.right * speed * Time.deltaTime;

		if (heavyPlane == false)
		{
			switch (currentState)
			{
				case AIState.Patrolling:
					Patrolling();
					break;

				case AIState.Attacking:
					Attacking();
					break;
			}

			if (Physics2D.Raycast(_firePoint.position, transform.right, 40, layerMask) && (shootTimer > timerBetweenShots))
			{
				StartCoroutine(Shoot());
			}

			if (_timer >= timeToRotate)
			{
				currentState = AIState.Attacking;
			}
			else
			{
				currentState = AIState.Patrolling;
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

		dirDelayTimer += Time.deltaTime;

		base.Update();
	}

	private IEnumerator Shoot()
	{
		shooter.Shoot();
		yield return new WaitForSeconds(0.5f);
		timerBetweenShots = Random.Range(0.5f, 1f);
		shootTimer = 0f;
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
			currentState = AIState.Patrolling;
			timeToRotate = Random.Range(2, 3.5f);
			attackTimer = 0f;
			_timer = 0f;
		}
		else
		{
			if (areEnemies && dirDelayTimer >= dirDelay)
			{
				var enemy = AssignTarget(transform.position);
				enemyDir = enemy.position - transform.position;
				enemyDir.Normalize();

				dirDelayTimer = 0;
			}		

			if (dissabled == false)
			{
				isChasingEnemy = true;
				Rotate(enemyDir.x, enemyDir.y);
			}

			attackTimer += Time.deltaTime;
		}
	}

	private void Patrolling()
	{
		isChasingEnemy = false;

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

	void InitializeSide()
	{
		if (gameObject.GetComponent<PlayerSide>() != null)
		{
			side = Side.Player;
		}
		else if (gameObject.GetComponent<EnemySide>() != null)
		{
			side = Side.Enemy;
		}
	}

	void InitializeTargetLayer()
	{
		if (side == Side.Player)
			layerMask = (1 << 15);
		else
			layerMask = (1 << 14);
	}

	Transform AssignTarget(Vector3 pos)
	{
		if(side == Side.Player)
		{
			EnemySide result = null;
			float dist = float.PositiveInfinity;
			var e = EnemySide.Pool.GetEnumerator();
			while (e.MoveNext())
			{
				float d = (e.Current.transform.position - pos).sqrMagnitude;
				if (d < dist)
				{
					result = e.Current;
					dist = d;
				}
			}
			return result.transform;
		}
		else
		{
			PlayerSide result = null;
			float dist = float.PositiveInfinity;
			var e = PlayerSide.Pool.GetEnumerator();
			while (e.MoveNext())
			{
				float d = (e.Current.transform.position - pos).sqrMagnitude;
				if (d < dist)
				{
					result = e.Current;
					dist = d;
				}
			}
			return result.transform;
		}		
	}
}
