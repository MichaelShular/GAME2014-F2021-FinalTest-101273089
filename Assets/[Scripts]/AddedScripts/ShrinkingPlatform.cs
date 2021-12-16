//ShrinkingPlatform
//Michael Shular 101273089
//12/16/2021 
//Summary: Controls aspects of shrinking platforms.
//Revision History
//-Added bobbing cycle


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{

    [SerializeField] private float bobbingDistance;
    private Vector3 bobbingPosition;

    // Start is called before the first frame update
    void Start()
    {
        bobbingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _bobbingCycle();

    }

    private void _bobbingCycle()
    {
        transform.position = new Vector3(bobbingPosition.x,
                bobbingPosition.y + Mathf.PingPong(Time.time, bobbingDistance), 0.0f);
    }
}
