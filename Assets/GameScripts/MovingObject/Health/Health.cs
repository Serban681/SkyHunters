using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    public GameObject objectToReplace;
    public GameObject explosion;
	protected float planeFinalSpeed;

	protected ParticleSystem blackSmokeTrail;
	protected GameObject fireFromBrokenPlane;

	private float camShakeValue;
	private float camShakeTime;

	public enum PlaneType
	{
		FighterPlane,
		AttackPlane,
		BomberPlane
	}

	public PlaneType planeType = PlaneType.FighterPlane;

	private Transform smokeEffect;

	protected float smokePoint;
	protected float firePoint;

	protected bool playedFire = false;
	protected bool playedSmoke = false;
	protected bool badState = false;

	protected virtual void Start()
    {	
		if(planeType == PlaneType.FighterPlane)
		{
			smokePoint = 30;
			firePoint = 10;
		}
		else if(planeType == PlaneType.AttackPlane)
		{
			smokePoint = 70;
			firePoint = 35;
		}
		else if(planeType == PlaneType.BomberPlane)
		{
			smokePoint = 200;
			firePoint = 100;
		}

		blackSmokeTrail = transform.Find("BlackSmokeTrail").transform.GetComponent<ParticleSystem>();
		blackSmokeTrail.Stop();

		fireFromBrokenPlane = transform.Find("FireFromBrokenPlane").gameObject;
		
		fireFromBrokenPlane.SetActive(false);

		smokeEffect = transform.Find("SmokeTrail").transform;
	}

	protected virtual void Die()
	{
		smokeEffect.parent = null;
		smokeEffect.GetComponent<SmokeTrailScript>().PlaneDestroyed();

		blackSmokeTrail.transform.parent = null;
		blackSmokeTrail.transform.GetComponent<SmokeTrailScript>().PlaneDestroyed();

		AudioManagerScript.instance.Play("ExplosionSound");

		GameObject instance = Instantiate(objectToReplace, transform.position, transform.rotation);
		Rigidbody2D[] rbs = instance.GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rb in rbs)
		{
			Vector2 dir = new Vector2(rb.position.x - transform.position.x, rb.position.y - transform.position.y);
			dir.Normalize();
			rb.AddForce(dir * 2, ForceMode2D.Impulse);
			rb.AddForce(transform.right * (planeFinalSpeed * 1.75f / 100), ForceMode2D.Impulse);
		}

        Instantiate(explosion, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

	protected void StartSmoke()
	{
		blackSmokeTrail.Play();
	}

	protected virtual void StartFire()
	{
		fireFromBrokenPlane.gameObject.SetActive(true);
	}

	public virtual void TakeDamage(float damage)
	{
		if (health <= smokePoint && playedSmoke == false)
		{
			StartSmoke();
			playedSmoke = true;
		}

		if (health <= firePoint && playedFire == false)
		{
			StartFire();
			playedFire = true;
		}

		if (health <= 0)
			Die();	
	}
}
