using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerHealth : Health
{
	public static PlayerHealth instance;

	public event EventHandler OnPlayerDeath;

	private float maxHealth;

	private Animator repairedText;

	private HealthSliderScript healthSliderScript;
	private bool tookDmg = false;

	private Slider healthSlider;

	private void Awake()
	{
		instance = this;

		StaticVariables.playerHealth = health;
	}

	protected override void Start()
	{
		maxHealth = health;

		repairedText = transform.Find("WorldSpaceCanvas").Find("RepairedText").GetComponent<Animator>();

		healthSliderScript = transform.Find("WorldSpaceCanvas").Find("HealthSlider").GetComponent<HealthSliderScript>();

		healthSlider = transform.Find("WorldSpaceCanvas").Find("HealthSlider").GetComponent<Slider>();

		base.Start();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.I))
		{
			TakeDamage(500);
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			TakeDamage(20);
		}
	}
	/*
	public void RestoreHealth()
	{
		///------ Not Working Properly And Not needed -----------//
		health = maxHealth;

		healthSlider.value = maxHealth;

		StartCoroutine(RepairTextAppear(repairedText));

		fireFromBrokenPlane.gameObject.SetActive(false);

		blackSmokeTrail.gameObject.SetActive(false);

		playedSmoke = false;
		playedFire = false;
	}
	*/
	public void RestoreHealth(int healAmount)
	{
		health += healAmount;
		StaticVariables.playerHealth = health;//healthSlider.value = health + healAmount;

		if (health >= smokePoint && playedSmoke == true)
		{
			blackSmokeTrail.Stop();
			playedSmoke = false;
		}

		if (health >= firePoint && playedFire == true)
		{
			fireFromBrokenPlane.SetActive(false);
			playedFire = false;
		}
	}

	protected override void Die()
	{
		OnPlayerDeath?.Invoke(this, EventArgs.Empty);

		FindObjectOfType<CameraShake>().Shake(4f, 4f, 2.5f);

		StartCoroutine(PauseButtonScript.instance.Dissappear());

		planeFinalSpeed = transform.GetComponent<PlaneMovement>().finalSpeed;

		base.Die();
	}

	private IEnumerator RepairTextAppear(Animator anim)
	{
		anim.SetBool("Appear", true);
		yield return new WaitForSeconds(1.30f * 100 / 60);
		anim.SetBool("Appear", false);
	}

	public override void TakeDamage(float damage)
	{
		if(tookDmg == false)
		{
			healthSliderScript.StartAnimator();
			tookDmg = true;
		}

		health -= damage;
		StaticVariables.playerHealth = health;

		base.TakeDamage(damage);
	}
}
