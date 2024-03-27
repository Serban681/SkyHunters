using System.Collections.Generic;
using UnityEngine;

public class EnemySide : MonoBehaviour
{
	public readonly static HashSet<EnemySide> Pool = new HashSet<EnemySide>();

	private void OnEnable()
	{
		EnemySide.Pool.Add(this);
	}

	private void OnDisable()
	{
		EnemySide.Pool.Remove(this);
	}
}
