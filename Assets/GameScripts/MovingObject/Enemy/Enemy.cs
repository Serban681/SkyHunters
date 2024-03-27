using UnityEngine;
using System.Collections.Generic;
 
public class Enemy : MonoBehaviour
{
	public readonly static HashSet<Enemy> Pool = new HashSet<Enemy>();

	private void OnEnable()
	{
		Enemy.Pool.Add(this);
	}

	private void OnDisable()
	{
		Enemy.Pool.Remove(this);
	}

}