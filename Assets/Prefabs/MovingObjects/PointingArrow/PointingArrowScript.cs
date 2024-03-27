using UnityEngine;

public class PointingArrowScript : MonoBehaviour
{
	public float speed = 10;

	private Vector2 targetDirection;

	public Transform target;

	private float rotZ;

	void Update()
	{
		targetDirection = target.position - transform.parent.position;
		targetDirection.Normalize();

		rotZ = Mathf.Atan2(targetDirection.x * -1, targetDirection.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotZ), 5);

	}

	public void AssignTarget(Transform _target)
	{
		target = _target;
	}
}