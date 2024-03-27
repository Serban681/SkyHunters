using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraFollow : MonoBehaviour
{
	private bool executed = false;
	private Animator anim;
	private Animator comeBackTextAnim;
	private Text comeBackText;

	private float timer = 10.5f;

	private Vector3 targetPosition;

	public Vector2 xConstraints;
	public Vector2 yConstraints;
	private Transform playerTransform;
	public float lerpSpeed = 1f;
	private Camera cam;
	private float initialSize;
	public float maxDistanceOnZ = 40;
	private float zIncrementer;
	private float initialZValue;
	private float zValue;
	public float directionMultiplier = 4f;
	private Vector2 curDirection;
	private Vector2 finalDirection;

	private bool playerAlive = true;
	private bool onPlayArea = true;

	void Start()
	{
		initialZValue = transform.position.z;
		anim = GetComponentInChildren<Animator>();
		comeBackTextAnim = GameObject.Find("ComeBackText").transform.GetComponent<Animator>();
		comeBackText = GameObject.Find("ComeBackText").transform.GetComponent<Text>();

		PlayerHealth.instance.OnPlayerDeath += _OnPlayerDeath;
		playerTransform = PlayerController.instance.transform;
	}

	void FixedUpdate()
	{
		if (playerAlive)
			OnPlayerActive();
	}

	private void zDistanceSetter()
	{
		zIncrementer = (StaticVariables.playerSpeed - StaticVariables.playerMinSpeed) / (StaticVariables.playerMaxSpeed - StaticVariables.playerMinSpeed) * maxDistanceOnZ;
	}

	private IEnumerator textOut()
	{
		yield return new WaitForSeconds(1f);
		timer = 10.5f;
	}

	private void _OnPlayerDeath(object sender, EventArgs e)
	{
		anim.SetBool("FadeGrayScaleIn", true);

		playerAlive = false;
	}

	void OnPlayerActive()
	{
		finalDirection = StaticVariables.playerDir;
		finalDirection.Normalize();
		curDirection = Vector2.Lerp(curDirection, finalDirection, 2f * Time.deltaTime);
		curDirection.Normalize();

		zDistanceSetter();

		if (playerTransform.position.x <= xConstraints.x || playerTransform.position.x >= xConstraints.y || playerTransform.position.y <= yConstraints.x || playerTransform.position.y >= yConstraints.y)
		{
			if (onPlayArea == true)
			{
				targetPosition = transform.position;
				comeBackTextAnim.SetBool("Appear", true);
				anim.SetBool("FadeGrayScaleIn", true);

				executed = false;

				onPlayArea = false;
			}

			comeBackText.text = "Come Back : " + (int)timer + "s";
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				PlayerHealth.instance.TakeDamage(PlayerHealth.instance.health);
			}
		}
		else
		{
			if (executed == false)
			{		
				comeBackTextAnim.SetBool("Appear", false);
				anim.SetBool("FadeGrayScaleIn", false);
				StartCoroutine(textOut());	

				executed = true;

				onPlayArea = true;
			}

			targetPosition = new Vector3(playerTransform.position.x + curDirection.x * directionMultiplier, playerTransform.position.y + curDirection.y * directionMultiplier, zValue);
		}
	
		zValue = initialZValue - zIncrementer;
		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
	}
}
