using UnityEngine;

public class BGAIHealth : Health
{
    private void Update()
	{
		if(badState == true)
		{
			health = Mathf.Lerp(health, -1, 0.01f);

			if(health <= 0)
			{
				Die();
			}
		}
	}

	protected override void Die()
	{
		planeFinalSpeed = transform.GetComponent<BGAIController>().speed;

		base.Die();
	}

	protected void Dissable()
	{
		transform.GetComponent<BGAIController>().Dissable();
		health = Mathf.Lerp(health, -1, 0.01f);
	}

	public override void TakeDamage(float damage)
	{
		health -= damage;

		if(health <= 10 && badState == false)
		{
			badState = true;
		}

		base.TakeDamage(damage);
	}
}
