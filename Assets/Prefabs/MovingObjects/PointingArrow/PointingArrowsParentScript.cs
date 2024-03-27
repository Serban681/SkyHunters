using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingArrowsParentScript : MonoBehaviour
{
    private Vector3 player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>().transform.position;
        transform.position = player;
    }
}
