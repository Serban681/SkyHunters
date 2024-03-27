using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	/*
	public GameObject[] ww1Balloons;
	public GameObject[] ww2Balloons;
	*/
	private Transform[] balloonSpawnPoints;

	private GameObject[] balloons;

	public static EnemySpawner instance;

	private int playerPlaneType = 1;

	struct PlaneArmy
	{
		public GameObject[] fighterPlanes;
		public GameObject[] attackPlanes;
		public GameObject[] lightBombers;
		public GameObject[] heavyBombers;
	}

	PlaneArmy planeArmy;

	[Header("WW1 Planes - Enemies")]
	public GameObject[] ww1fighterPlanes;
	public GameObject[] ww1attackPlanes;
	public GameObject[] ww1lightBombers;
	public GameObject[] ww1heavyBombers;

	[Header("WW1 Planes - Players")]
	public GameObject[] ww1Players;

	[Header("WW2 Planes - Enemies")]
	public GameObject[] ww2fighterPlanes;
	public GameObject[] ww2attackPlanes;
	public GameObject[] ww2lightBombers;
	public GameObject[] ww2heavyBombers;

	[Header("WW2 Planes - Players")]
	public GameObject[] ww2Players;

	private float bomberTimer = 0f;
	[HideInInspector]
	public int numberOfBombers = 0;
	private float timeToSpawnBomber = 30f;

	private bool enemyRotating;

	[Header("Other")]

	private int maxEnemies = 4;
	[HideInInspector]
	public int curEnemies = 0;
	public float distanceFromPlayer = 20f;
	private Vector2 direction;
	private float rotZ;

	private float timer = 0f;
	private float timeBetweenSpawns = 1f;

	public GameObject bird;
	public GameObject flamingo;
	private float birdTimer = 0f;
	public float timeBetweenBirdSpawns = 3f;
	public int maxNumberOfBirds = 8;
	[HideInInspector]
	public int currentNumberOfBirds = 0;
	private float birdSpawnerRotZ;
	private int xScale;

	private EnemyController[] enemyPlanes = new EnemyController[100];
	private Transform player;

	public GameObject coin;
	[HideInInspector]
	public int curCoins = 0;
	private int maxCoins = 4;
	private float coinRotZ;
	private float coinTimer = 0;
	private float timeBetCoinSpawns = 1f;

	public GameObject heart;
	[HideInInspector]
	public int curHearts = 0;
	private int maxHearts = 3;
	private float heartRotZ;
	private float heartTimer = 0f;
	private float timeBetHeartSpawns = 2f;

	private void Awake()
	{
		instance = this;

		playerPlaneType = StaticVariables.playerPlaneType;
		
		for (int i = 0; i < ww1Players.Length; i++)
		{
			if (i == StaticVariables.focussedPlane)
			{
				if(playerPlaneType == 0)
					Instantiate(ww1Players[i], Vector3.zero, Quaternion.identity);
				else if(playerPlaneType == 1)
					Instantiate(ww2Players[i], Vector3.zero, Quaternion.identity);
			}
		}

		if(playerPlaneType == 0)
		{
			planeArmy.fighterPlanes = ww1fighterPlanes;
			planeArmy.attackPlanes = ww1attackPlanes;
			planeArmy.lightBombers = ww1lightBombers;
			planeArmy.heavyBombers = ww1heavyBombers;
		}
		else if(playerPlaneType == 1)
		{
			planeArmy.fighterPlanes = ww2fighterPlanes;
			planeArmy.attackPlanes = ww2attackPlanes;
			planeArmy.lightBombers = ww2lightBombers;
			planeArmy.heavyBombers = ww2heavyBombers;
		}

		player = PlayerController.instance.transform;
	}

	void Update()
    {
		if (StaticVariables.playerHealth > 0)
        {
			if (timer >= timeBetweenSpawns && curEnemies < maxEnemies)
            {
               SpawnEnemy();
            }

            if (curEnemies < maxEnemies)
            {
                timer += Time.deltaTime;
            }

            if (bomberTimer >= timeToSpawnBomber)
            {
                rotZ = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                GameObject instance = Instantiate(planeArmy.heavyBombers[Random.Range(0, planeArmy.heavyBombers.Length)], player.position + transform.right * (200 + Random.Range(0, 10)), Quaternion.identity);
                numberOfBombers++;
                timeToSpawnBomber = Random.Range(25f, 45f);

                if (transform.right.x > 0)
                {
                    instance.transform.rotation = Quaternion.Euler(0, 0, 180);
                }

                bomberTimer = 0f;
            }
            if (numberOfBombers < 1)
            {
                bomberTimer += Time.deltaTime;
            }

            if (birdTimer >= timeBetweenBirdSpawns)
            {
				GameObject instance;
				int x = Random.Range(0, 100);
                birdSpawnerRotZ = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(0, 0, birdSpawnerRotZ);
				if (x < 95)
				{
					instance = Instantiate(bird, player.position + transform.right * (distanceFromPlayer + Random.Range(0, 10)), Quaternion.identity);
				}
				else
				{
					instance = Instantiate(flamingo, player.position + transform.right * (distanceFromPlayer + Random.Range(0, 10)), Quaternion.identity);
				}
                instance.transform.rotation = Quaternion.identity;
                xScale = Random.Range(-1, 1);
                if (xScale < 0)
                {
                    xScale = -1;
                }
                else
                {
                    xScale = 1;
                }
                instance.transform.localScale = new Vector3(xScale, 1, 1);
                timeBetweenBirdSpawns = Random.Range(3f, 5f);
                currentNumberOfBirds++;
                birdTimer = 0f;
            }

			if(currentNumberOfBirds < maxNumberOfBirds)
				birdTimer += Time.deltaTime;

			if (coinTimer >= timeBetCoinSpawns)
            {
                timeBetCoinSpawns = Random.Range(3f, 5f);
                coinRotZ = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(0, 0, coinRotZ);
                GameObject instance = Instantiate(coin, player.position + transform.right * (50 + Random.Range(0, 30)), Quaternion.identity);
                instance.transform.rotation = Quaternion.identity;
                curCoins++;
                coinRotZ += 80;
                coinTimer = 0f;
            }

			if(curCoins < maxCoins)
				coinTimer += Time.deltaTime;

			if (heartTimer >= timeBetHeartSpawns)
            {
				timeBetHeartSpawns = Random.Range(4f, 6f);
                heartRotZ = Random.Range(0, 359);
                transform.rotation = Quaternion.Euler(0, 0, heartRotZ);
                GameObject instance = Instantiate(heart, player.position + transform.right * (50 + Random.Range(0, 30)), Quaternion.identity);
                instance.transform.rotation = Quaternion.identity;
                curHearts++;
				heartTimer = 0f;
            }

            if (curHearts < maxHearts)
				heartTimer += Time.deltaTime;
        }
    }

	private void SpawnEnemy()
	{
		int x = Random.Range(1, 100);
		if(x <= 60)
		{
			EnemySpawnProcedure(planeArmy.fighterPlanes, distanceFromPlayer);
		}
		else if(x > 60 && x <= 85)
		{
			EnemySpawnProcedure(planeArmy.attackPlanes, distanceFromPlayer);
		}
		else if(x > 85 && x <= 100)
		{
			EnemySpawnProcedure(planeArmy.lightBombers, distanceFromPlayer);
		}
	}

	private void EnemySpawnProcedure(GameObject[] planeType, float distance)
	{
        rotZ = Random.Range(0, 359);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        Instantiate(planeType[Random.Range(0, planeType.Length)], player.position + transform.right * (distance + Random.Range(0, 10)), Quaternion.identity);
        curEnemies++;
        timeBetweenSpawns = Random.Range(3f, 5f);
        timer = 0f;
	}
}
