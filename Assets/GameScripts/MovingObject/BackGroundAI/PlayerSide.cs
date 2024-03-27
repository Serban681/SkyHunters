using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : MonoBehaviour
{
	public readonly static HashSet<PlayerSide> Pool = new HashSet<PlayerSide>();

	private void OnEnable()
	{
		PlayerSide.Pool.Add(this);
	}

	private void OnDisable()
	{
		PlayerSide.Pool.Remove(this);
	}
}
