using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyHealth : Health
{
	private Transform explosionPoint;

	private TimeManagerScript timeManager;

	public GameObject smallExplosion;

	// Start is called before the first frame update
	protected override void Start()
    {
		timeManager = TimeManagerScript.instance;

		if (planeType == PlaneType.BomberPlane)
		{
			explosionPoint = transform.Find("ExplosionPoint").transform;
		}

		base.Start();
    }

	private void Update()
	{
		if(badState == true)
		{
			health = Mathf.Lerp(health, -1, 0.01f);

			if(health <= 0)
			{
				Die();
			}
		}
	}

	protected override void Die()
	{
		FindObjectOfType<CameraShake>().Shake(3.5f, 3.5f, 2.5f);

		timeManager.DoSlowmotion(0.1f, 1f);

		transform.GetComponent<EnemyController>().ArrowDestroyer();

		if (planeType == PlaneType.BomberPlane)
		{
			StaticVariables.currentScore += 3;
			EnemySpawner.instance.numberOfBombers--;
		}
		else
		{
			StaticVariables.currentScore++;
			EnemySpawner.instance.curEnemies--;
		}

		ScoreText.instance.ChangeText();

		planeFinalSpeed = transform.GetComponent<EnemyController>().speed;

		base.Die();
	}

	protected override void StartFire()
	{
		if(planeType == PlaneType.BomberPlane)
		{
			FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);

			AudioManagerScript.instance.Play("ExplosionSound", 1.2f, 0.3f);

			timeManager.DoSlowmotion(0.5f, 2);

			Instantiate(smallExplosion, explosionPoint.position, Quaternion.identity);
		}

		base.StartFire();
	}

	protected void Dissable()
	{
		transform.GetComponent<EnemyController>().Dissable();
		health = Mathf.Lerp(health, -1, 0.01f);
	}

	public override void TakeDamage(float damage)
	{
		health -= damage;

		if(health <= 10 && badState == false)
		{
			badState = true;
		}

		base.TakeDamage(damage);
	}
}
