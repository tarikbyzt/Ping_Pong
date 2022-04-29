using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Ball")
        {
            Ball.Current.BallPull();
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }


}
