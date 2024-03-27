using UnityEngine;

public class PlayerController : PlaneMovement
{
	public float additionalSpeed = 0;
	private float additionalSpeedValue = 0;

	public static PlayerController instance;

    private Joystick movementJoystick;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	protected override void Start()
    {
		StaticVariables.playerMaxSpeed = maxSpeed;
		StaticVariables.playerMinSpeed = minSpeed;
		StaticVariables.playerDir.x = 1;
		StaticVariables.playerDir.y = 0;

		movementJoystick = FindObjectOfType<Joystick>();
        base.Start();
    }

    protected override void Update()
    {
        Rotate(movementJoystick.Direction.x, movementJoystick.Direction.y);

		finalSpeed = speed + additionalSpeedValue;

		rb.velocity = transform.right * finalSpeed * Time.deltaTime;

		StaticVariables.playerSpeed = speed;

		base.Update();
	}

	public override void Rotate(float xDir, float yDir)
	{
		base.Rotate(xDir, yDir);

		additionalSpeedValue = Mathf.Lerp(additionalSpeedValue, additionalSpeed * Vector2.SqrMagnitude(new Vector2(xDir, yDir)), 0.1f);

		if (xDir != 0 || yDir != 0)
		{	
			StaticVariables.playerDir.x = xDir;
			StaticVariables.playerDir.y = yDir;
		}
	}
}
