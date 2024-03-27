using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float angleToRotateTo = 0f;
	public float rotationSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angleToRotateTo), rotationSpeed);
    }
}
