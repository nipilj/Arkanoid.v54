using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Singleton

    private static Platform _instance;

    public static Platform Instance => _instance;

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

   
    private Camera mainCamera;
    private float platformInitialY;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        platformInitialY = this.transform.position.y;
    }

    private void Update()
    {
        PlatformMove();
    }

    //Управление платформой мышкой.
    private void PlatformMove()
    {
        float leftClamp = 113;
        float rightClamp = 427;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePositionWorldX, platformInitialY, 0);
    }


    //Управление мячом. За основу берется центр платформы. 
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 platformCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;

            if (hitPoint.x < platformCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallManager.Instance.InitialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallManager.Instance.InitialBallSpeed));
            }
        }
    }

}
