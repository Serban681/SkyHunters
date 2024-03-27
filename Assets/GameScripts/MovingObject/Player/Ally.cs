using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
	public readonly static HashSet<Ally> Pool = new HashSet<Ally>();

	private void OnEnable()
	{
		Ally.Pool.Add(this);
	}

	private void OnDisable()
	{
		Ally.Pool.Remove(this);
	}
}
