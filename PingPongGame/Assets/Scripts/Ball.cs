using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Ball : MonoBehaviour
{
    [SerializeField] GameObject machine;
    public GameObject ballOutParent;
    public GameObject ballParent;
    public GameObject cam;
    public int score;
    public static Ball Current;
    [SerializeField] float speed;
    public bool finished = false;
    [SerializeField] float touchSpeed;
    public int ballCount;
    public GameObject[] balls;
    public List<GameObject> ballList;
    public List<GameObject> ballInMachine;
    

    void Awake()
    {
        ballList[0].transform.parent = ballParent.transform;
    }

    private void Start()
    {
        Current = this;
        ballInMachine.Add(ballList[0]);
        StartCoroutine(Wait());
        Vector3 firstBallPos = new Vector3(0, 1, 0);
        ballList[0].transform.localPosition = firstBallPos;
        ballInMachine[0].GetComponent<Animator>().enabled = true;
    }
    private void Update()
    {
        if (LevelController.Current.gameActive == true)
        {
            Vector3 newPosition = new Vector3(machine.transform.position.x, machine.transform.position.y, machine.transform.position.z + speed * Time.deltaTime);
            machine.transform.position = newPosition;
            if (ballCount == 0)
            {
                LevelController.Current.GameOver();
            }
        }

        ballCount = ballInMachine.Count;
        Debug.Log(ballCount);
        if (Input.touchCount > 0 && LevelController.Current.gameActive == true && ballInMachine[ballCount - 1].transform.position.y < 16 && finished == false)
        {
            Touch first = Input.GetTouch(0);
            if (first.phase == TouchPhase.Stationary)
            {
                if (ballInMachine[ballCount - 1].transform.position.y < 12)
                {
                    Vector3 ballPosition = new Vector3(ballParent.transform.position.x, ballParent.transform.position.y + touchSpeed * Time.deltaTime, ballParent.transform.position.z);
                    ballParent.transform.position = ballPosition;

                    Debug.Log("yukarý çýkýyor");
                }
               
            }
        }
        else if (ballParent.transform.localPosition.y > 0.95f)
        {
            Vector3 ballPosition = new Vector3(ballParent.transform.position.x, ballParent.transform.position.y - touchSpeed * Time.deltaTime, ballParent.transform.position.z);
            ballParent.transform.position = ballPosition;
        }

        if (finished == true)
        {
            score = ballCount * 100;
            cam.GetComponent<Animator>().enabled = true;
            ballParent.transform.DOLocalMoveY(0.95f, 0.1f);
            speed = 0;
            for (int i = ballCount - 1; i >= 0; i--)
            {
                ballInMachine[i].GetComponent<Animator>().applyRootMotion = false;
            }
            for (int i = ballCount - 1; i >= 0; i--)
            {
                ballInMachine[i].GetComponent<Animator>().SetBool("finish", true);
            }
            StartCoroutine(GameFinish(3f));
        }
    }
    public void BallPull()
    {
        ballList[0].transform.parent = ballParent.transform;
        ballInMachine.Add(ballList[0]);
        StartCoroutine(Wait());
        ballList[0].GetComponent<Animator>().enabled = true;
        ballInMachine[ballCount].transform.localPosition = new Vector3(0, ballInMachine[ballCount - 1].transform.localPosition.y + 0.5f, 0);
    }


    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        ballList.RemoveAt(0);
    }

    public IEnumerator GameFinish(float second)
    {
        yield return new WaitForSeconds(second);
        LevelController.Current.FinishMenu();
    }
}
