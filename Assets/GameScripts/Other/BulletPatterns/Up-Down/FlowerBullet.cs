using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour, IPooledObject
{
	public Animator anim;

	private float damage;

	private Transform[] otherBullets;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	public void OnObjectSpawn()
	{
		gameObject.SetActive(true);
		gameObject.layer = transform.parent.transform.gameObject.layer;
		damage = transform.parent.GetComponent<BulletFormationScript>().damage;

		otherBullets = transform.parent.transform.GetComponentsInChildren<Transform>();
	}

	public void StartFirstUp()
	{
		anim.SetBool("Up", true);
		anim.SetBool("HasStarted", true);
	}

	public void StartFirstDown()
	{
		anim.SetBool("Up", false);
		anim.SetBool("HasStarted", true);
	}

	public void GoDown()
	{
		anim.SetBool("Up", false);
	}

	public void GoUp()
	{
		anim.SetBool("Up", true);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		objectPooler.SpawnFromPool("Spark", other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		other.GetComponent<Health>().TakeDamage(damage);
		/*
		for (int i = 0; i < otherBullets.Length; i++)
		{
			if(otherBullets[i] != this.transform && otherBullets[i].gameObject.active == false)
			{
				otherBullets[0].gameObject.SetActive(false);
			}
		}
		*/
		transform.gameObject.SetActive(false);
	}
}
