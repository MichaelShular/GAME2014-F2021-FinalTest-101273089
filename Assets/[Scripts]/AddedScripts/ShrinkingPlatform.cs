//ShrinkingPlatform
//Michael Shular 101273089
//12/16/2021 
//Summary: Controls aspects of shrinking platforms.
//Revision History
//-Added bobbing cycle
//-Added shrinking
//-Added sounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ShrinkingPlatform : MonoBehaviour
{
    [Header("Bobbing Movement")]
    [SerializeField] private float bobbingDistance;
    private Vector3 bobbingPosition;
    [Header("Shrinking")]
    private bool isShrinking;
    [SerializeField] private float shrinkingSpeed;
    [Header("Sound")]
    [SerializeField] private AudioSource shrinkingSFX;
    [SerializeField] private AudioSource growingSFX;



    // Start is called before the first frame update
    void Start()
    {
        bobbingPosition = transform.position;
        isShrinking = false;
    }

    // Update is called once per frame
    void Update()
    {
        _bobbingCycle();
        _shinrking();
    }
    private void _bobbingCycle()
    {
        transform.position = new Vector3(bobbingPosition.x,
                bobbingPosition.y + Mathf.PingPong(Time.time, bobbingDistance), 0.0f);
    }

    private void _shinrking()
    {
        if (isShrinking)
        {
            //shrinking
            if (transform.localScale.x >= 0.01)
            {
                transform.localScale -= Vector3.one * shrinkingSpeed * Time.deltaTime;
                
            }
            else
            {
                //Turn collision off and on so play fall off
                StartCoroutine(turnBackOnCollision());
            }
        }
        else
        {
            //growing
            if (transform.localScale.x <= 1)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                transform.localScale += Vector3.one * shrinkingSpeed * Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player lands on platform
        if (collision.gameObject.CompareTag("Player"))
        {
            isShrinking = true;
            shrinkingSFX.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //player leaves platform
        if (collision.gameObject.CompareTag("Player"))
        {
            isShrinking = false;
            growingSFX.Play();
        }
    }

    IEnumerator turnBackOnCollision()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
