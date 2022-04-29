using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class ObstacleBox : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer gloveMesh;
    [SerializeField] private Transform startTransform = null, endTransform = null;
    [SerializeField] [Range(0f, 1f)] private float lerpValue;
    float blendWeight;
    public float deadBallPoss;
    public int deadBallIndexx;
    Vector3 startPosY, endPosY;
    GameObject deadBalll;
    float topBallPoss;


    // Update is called once per frame
    void Update()
    {
        blendWeight = gloveMesh.GetBlendShapeWeight(0);
        lerpValue = 1 - (blendWeight / 100);
        
        transform.position = Vector3.Lerp(startTransform.position, endTransform.position, lerpValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        deadBalll = other.gameObject;
        deadBallIndexx = Ball.Current.ballInMachine.IndexOf(deadBalll);

        //if (transform.position.y > deadBallPos)
        //{

        //    Debug.Log("top aþaðýya iniyor");
        //    // transform.localPosition = Vector3.Lerp(startPosY, endPosY, 0.1f);

        //}
        for (int i = deadBallIndexx; i < Ball.Current.ballInMachine.Count; i++)
        {
            topBallPoss = Ball.Current.ballInMachine[i].transform.localPosition.y;
            Ball.Current.ballInMachine[i].transform.DOLocalMoveY(topBallPoss - 0.5f, 1);
            Debug.Log("For döngü inme" + i);
        }

        Ball.Current.ballInMachine.Remove(deadBalll);

        deadBallPoss = other.transform.localPosition.y;
        other.transform.parent = Ball.Current.ballOutParent.transform;
        gameObject.GetComponent<Collider>().enabled = false;

    }
}
