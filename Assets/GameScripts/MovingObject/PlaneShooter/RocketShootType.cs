using UnityEngine;

public class RocketShootType : PlaneShooter
{
	private int curNrOfRockets;
	public float maxIncrementation;
	public int maxNrOfRockets;
	private float timer;

	// Start is called before the first frame update
	protected override void Start()
	{
		if (transform.tag == "Player")
			transform.Find("WorldSpaceCanvas").Find("BarrageReloadUI").gameObject.SetActive(true);

		curNrOfRockets = maxNrOfRockets;
		StaticVariables.maxNrOfRockets = maxNrOfRockets;

		base.Start();
	}

	public override void Shoot()
	{
		if (Time.time >= nextTimeToFire)
		{
			if (transform.CompareTag("Player"))
			{
				if (curNrOfRockets > 0)
				{
					FindObjectOfType<AudioManagerScript>().Play("ShootSound");

					FindObjectOfType<FancyShootButtonScript>().Flash();

					ShootType();
				}
			}
			else
			{
				if (curNrOfRockets > 0)
				{
					FindObjectOfType<AudioManagerScript>().Play("ShootSound");

					ShootType();
				}
			}
		}	
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.P) && transform.tag == "Player")
			Shoot();

		ReloadBarageSystem();
		if(transform.tag == "Player")
		{
			StaticVariables.curNrOfRockets = curNrOfRockets;
		}
    }

	protected override void ShootType()
	{
		//if (transform.tag == "Player")
		//{
			if (curNrOfRockets > 0)
			{
				nextTimeToFire = Time.time + 1 / fireRate;
				GameObject instance = objectPooler.SpawnFromPool("Rocket", firePoint.position, transform.rotation);
				RocketScript script = instance.GetComponent<RocketScript>();

				script.damage = damage;
				script.maxIncrementation = maxIncrementation;
				script.finalSpeed = 60;
				script.maxDistance = 100;

				instance.gameObject.layer = layer;

				script.OnObjectSpawn();

				curNrOfRockets--;

				//StaticVariables.curNrOfRockets--;
			}
			/*
		}
		else
		{
			nextTimeToFire = Time.time + 1 / fireRate;
			GameObject instance = objectPooler.SpawnFromPool("Rocket", firePoint.position, transform.rotation);
			RocketScript script = instance.GetComponent<RocketScript>();
			script.damage = damage;
			script.maxIncrementation = maxIncrementation;
			script.finalSpeed = 60;
			script.maxDistance = 100;
			instance.gameObject.layer = layer;

			script.OnObjectSpawn();
		}
		*/
	}

	private void ReloadBarageSystem()
	{
		if (curNrOfRockets < maxNrOfRockets)
			timer += Time.deltaTime;

		if (timer >= 2f && curNrOfRockets < maxNrOfRockets)
		{
			curNrOfRockets++;
			//StaticVariables.curNrOfRockets++;
			timer = 0f;
		}
	}
}
