using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Singleton

    private static BallManager _instance;

    public static BallManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [SerializeField]
    private Ball BallPrefab;

    private Ball InitialBall;

    private Rigidbody2D InitialBallRb;

    public float InitialBallSpeed = 250;
    public List<Ball> Ball { get; set; }

    private void Start()
    {
        InitBall();
    }
        
    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            
            Vector3 paddlePosition = Platform.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .31f, 0);
            InitialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                InitialBallRb.isKinematic = false;
                InitialBallRb.AddForce(new Vector2(0, InitialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    public void ResetBall()
    {
        foreach (Ball ball in Ball.ToList())
        {
            Destroy(ball.gameObject);
        }
        InitBall();
    }

    //Спавнит мячик на платформе.
    private void InitBall()
    {
        Vector3 paddlePosition = Platform.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .31f, 0);
        InitialBall = Instantiate(BallPrefab, startingPosition, Quaternion.identity);
        InitialBallRb = InitialBall.GetComponent<Rigidbody2D>();

        this.Ball = new List<Ball>
        {
            InitialBall
        };
    }
}
