using System;
using System.Collections;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
	private Transform firePoint;

	public event EventHandler OnPlaneRotate;
    /*
	public class OnPlaneRotateEventArgs : EventArgs
	{
		public bool rotateToRight;
	}
    */
	private ObjectPooler objectPooler;
	
	private bool isRotating = false;

    private Transform graphics;
    private Animator anim;
	[HideInInspector]
    public bool rotateToLeft = false;
	[HideInInspector]
    public bool rotateToRight = true;

	private int rotDir = 1;

    [Header("Movement")]
    private float finalRotZ = 0;
    public float speed;

	[HideInInspector]
    public Rigidbody2D rb;
    public float maxSpeed = 500;
    public float minSpeed = 300;
    private Vector3 _playerPos;
	public float rotationSpeed = 5;
	[HideInInspector]
	public float finalSpeed;

	[Header("Two Blades Design")]
	public bool twoBlades = false;
	private Transform blade;

	//private Transform target;
	//private Enemy potentialTarget;

	//-----Change-------
	private Transform jetFire;
	private Transform smokeTrail;
	private Transform blackSmokeTrail;
	private Transform fireFromBrokenPlane;
	private BoxCollider2D col;

	protected virtual void Start()
    {
		firePoint = transform.Find("FirePoint").transform;

		objectPooler = ObjectPooler.instance;

		if(transform.Find("JetFire") != null)
			jetFire = transform.Find("JetFire").transform;

		smokeTrail = transform.Find("SmokeTrail").transform;
		blackSmokeTrail = transform.Find("BlackSmokeTrail").transform;
		fireFromBrokenPlane = transform.Find("FireFromBrokenPlane").transform;
		col = GetComponent<BoxCollider2D>();

		if (twoBlades)
			blade = transform.Find("Graphics").transform.Find("Blade").transform;

        graphics = transform.Find("Graphics").transform;
        anim = transform.Find("Graphics").transform.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        //OnPlaneRotate?.Invoke(this, EventArgs.Empty);//new OnPlaneRotateEventArgs { rotateToRight = rotateToRight });
	}

    protected virtual void Update()
    {
		/*
		if (shootType == BulletType.GuidedRocketSystem && Enemy.Pool.Count != 0)
		{
			potentialTarget = FindClosestEnemy(transform.position);
			
			distanceFromPlayer = Vector2.Distance(transform.position, potentialTarget.transform.position);
			Vector2 direction = potentialTarget.transform.position - transform.position;
			direction.Normalize();
			q = Quaternion.FromToRotation(transform.right, direction);

			if ((q.eulerAngles.z <= 80 || q.eulerAngles.z >= 280) && distanceFromPlayer < 60f)
			{
				potentialTarget.transform.Find("EnemyWorldSpaceCanvas").transform.Find("LockOnShower").transform.GetComponent<LockedOnShowerScript>().lockedOn = true;
				if (lockOnTimer > 2f)
					target = potentialTarget.transform;
				lockOnTimer += Time.deltaTime;
			}
			else
			{
				target = null;
				potentialTarget.transform.Find("EnemyWorldSpaceCanvas").transform.Find("LockOnShower").transform.GetComponent<LockedOnShowerScript>().lockedOn = false;
				lockOnTimer = 0f;
			}
		}
		*/	
			/*
			if (StaticVariables.playerHealth > 0)
			{
				_playerPos = FindObjectOfType<PlayerController>().transform.position;
			}
            //float distance = Vector3.Distance(_playerPos, transform.position);
            if(distance > 5)
            {
                rb.velocity = transform.right * (StaticVariables.playerSpeed + 150) * Time.deltaTime;
            }

            if(distance <= 4)
            {
                rb.velocity = transform.right * (StaticVariables.playerSpeed + 150) * Time.deltaTime;
            }
			*/
       // }
	   /*
        if(transform.tag == "Player")
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
		*/


        speed -= transform.right.y * Time.deltaTime * 100;

        if(speed <= minSpeed)
        {
            speed = minSpeed;
        }

        if(speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        ChangeScale();
	}

    public virtual void Rotate(float xDir, float yDir)
    {
        if (xDir != 0 || yDir != 0)
        {
            finalRotZ = (Mathf.Atan2(xDir, yDir * -1) * Mathf.Rad2Deg) - 90;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed * Time.deltaTime * 55);
        }
	}

    private void ChangeScale()
    {
        if ((transform.rotation.eulerAngles.z > 270 || transform.rotation.eulerAngles.z < 90) && rotateToRight == false && isRotating == false)
        {
            StartCoroutine(rotateAnimToLeft());
        }
        else if(transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270 && rotateToLeft == false && isRotating == false)
        {
            StartCoroutine(rotateAnimToRight());
        }
    }

	private IEnumerator rotateAnimToLeft()
    {
		isRotating = true;
        anim.SetBool("shouldRotate", true);

		if (twoBlades == true)
			blade.gameObject.SetActive(false);

		firePoint.localPosition = new Vector3(firePoint.localPosition.x, -firePoint.localPosition.y, 0);
		yield return new WaitForSeconds(0.3f * 100 / 60);

        graphics.localScale = new Vector3(1, 1, transform.localScale.z);

		if (jetFire != null)
			jetFire.localPosition = new Vector3(jetFire.localPosition.x, -jetFire.localPosition.y, 0);

		smokeTrail.localPosition = new Vector3(smokeTrail.localPosition.x, -smokeTrail.localPosition.y, 0);
		blackSmokeTrail.localPosition = new Vector3(blackSmokeTrail.localPosition.x, -blackSmokeTrail.localPosition.y, 0);
		fireFromBrokenPlane.localPosition = new Vector3(fireFromBrokenPlane.localPosition.x, -fireFromBrokenPlane.localPosition.y, 0);
		
		col.offset = new Vector2(col.offset.x, -col.offset.y);

		yield return new WaitForSeconds(0.1f * 100 / 60);

		anim.SetBool("shouldRotate", false);

		if (twoBlades == true)
			blade.gameObject.SetActive(true);

		rotateToLeft = false;
		rotateToRight = true;
		isRotating = false;

        OnPlaneRotate?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator rotateAnimToRight()
    {
		isRotating = true;
		anim.SetBool("shouldRotate", true);

		if(twoBlades == true)
			blade.gameObject.SetActive(false);

		firePoint.localPosition = new Vector3(firePoint.localPosition.x, -firePoint.localPosition.y, 0);
		yield return new WaitForSeconds(0.3f * 100 / 60);
        graphics.localScale = new Vector3(1, -1, transform.localScale.z);

		if (jetFire != null)
			jetFire.localPosition = new Vector3(jetFire.localPosition.x, -jetFire.localPosition.y, 0);

		smokeTrail.localPosition = new Vector3(smokeTrail.localPosition.x, -smokeTrail.localPosition.y, 0);
		blackSmokeTrail.localPosition = new Vector3(blackSmokeTrail.localPosition.x, -blackSmokeTrail.localPosition.y, 0);
		fireFromBrokenPlane.localPosition = new Vector3(fireFromBrokenPlane.localPosition.x, -fireFromBrokenPlane.localPosition.y, 0);
		
		col.offset = new Vector2(col.offset.x, -col.offset.y);

		yield return new WaitForSeconds(0.1f * 100 / 60);
        anim.SetBool("shouldRotate", false);

		if (twoBlades == true)
			blade.gameObject.SetActive(true);

		rotateToRight = false;
		rotateToLeft = true;
		isRotating = false;

        OnPlaneRotate?.Invoke(this, EventArgs.Empty);
    }





	
	//-------Experimental-------//
	
	/*
	private void GuidedRocketSystem()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		GameObject instance = Instantiate(bullet, firePoint.position, transform.rotation);
		instance.GetComponent<GuidedRocketScript>().target = target;
		if (transform.tag == "Player")
			instance.gameObject.layer = 10;
		else
			instance.gameObject.layer = 11;
	}
	/*
	private void ExplosiveBulletShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
		GameObject instance = Instantiate(bullet, firePoint.position, firePoint.rotation);
		instance.GetComponent<ExplosiveBulletScript>().firePoint = firePoint;
		instance.GetComponent<ExplosiveBulletScript>().damage = damage;
		if (transform.tag == "Player")
			instance.gameObject.layer = 10;
		else
			instance.gameObject.layer = 11;
		Instantiate(muzzleFlash, firePoint.position, transform.rotation);
	}

	public static Enemy FindClosestEnemy(Vector3 pos)
	{
		Enemy result = null;
		float dist = float.PositiveInfinity;
		var e = Enemy.Pool.GetEnumerator();
		while (e.MoveNext())
		{
			float d = (e.Current.transform.position - pos).sqrMagnitude;
			if (d < dist)
			{
				result = e.Current;
				dist = d;
			}
		}
		return result;
	}
	*/
}
