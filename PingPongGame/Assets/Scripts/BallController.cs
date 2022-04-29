using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosY, endPosY;
    public bool inWindZone = false;
    public bool racket = false;
    [SerializeField] GameObject balls;
    [SerializeField] GameObject obsDryer;


    void Start()
    {
        balls = GameObject.FindWithTag("Balls");
        obsDryer = GameObject.FindWithTag("obsDryer");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        startPosY = transform.position;
        endPosY = new Vector3(transform.position.x, transform.position.y + ObsDryer.Current.deadBallPos, transform.position.z);
    }

    

    void FixedUpdate()
    {
        if (inWindZone == true)
        {
            Debug.Log("space");
            rb.AddForce(transform.right * 150);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "obsDryer")
        {
            rb.isKinematic = false;
            gameObject.GetComponent<Animator>().enabled = false;
            StartCoroutine(Timer());
            inWindZone = true;
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        obsDryer.SetActive(false);
    }
}
