using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObsDryer : MonoBehaviour
{
    public static ObsDryer Current;
    public  float deadBallPos;
    public int deadBallIndex;
    GameObject deadBall;
    public float topBallPos;



    // Start is called before the first frame update
    void Start()
    {
        
        Current = this;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        deadBall = other.gameObject;
        deadBallIndex = Ball.Current.ballInMachine.IndexOf(deadBall);

        
        Debug.Log("Deadball index= "+deadBallIndex);
        for (int i = deadBallIndex; i < Ball.Current.ballInMachine.Count; i++)
        {
            topBallPos = Ball.Current.ballInMachine[i].transform.localPosition.y;
            Ball.Current.ballInMachine[i].transform.DOLocalMoveY(topBallPos-0.5f, 1);
            Debug.Log("For döngü inme" + i);
        }
        
        Ball.Current.ballInMachine.Remove(deadBall);
       
        deadBallPos = other.transform.localPosition.y;
        other.transform.parent = Ball.Current.ballOutParent.transform;
        gameObject.GetComponent<Collider>().enabled = false;

    }

}
