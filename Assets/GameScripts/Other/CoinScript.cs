using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	private bool touched = false;
	private Vector3 playerPos;

	private float distance = 0f;
	private Vector3 subtract;

	private float delay = 1f;
	private float delayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
		playerPos = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (delayTimer > delay)
		{
			subtract = transform.position - playerPos;

			distance = Vector3.Magnitude(subtract);

			if (distance > 80)
			{
				EnemySpawner.instance.curCoins--;
				Destroy(gameObject);
			}

			delayTimer = 0f;
		}

		delayTimer += Time.deltaTime;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        AudioManagerScript.instance.Play("CoinPickSound");

		if(touched == false)
			StartCoroutine(FindObjectOfType<CoinSystemScript>().MovingCoin(transform.gameObject));

		touched = true;
	}
}
