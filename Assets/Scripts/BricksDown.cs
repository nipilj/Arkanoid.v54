using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BricksDown : MonoBehaviour
{
    //private Vector3 startPosition; 
    //private float startTime = 3f;
    //float distance = 0.53f;
    //private bool moving;

    //public float timer =3f;
    //public float speed = 0.25f;
    //public int newtarget;
    //public float yPos;
    //public Vector3 desiredPos;



    //void Update()
    //{
    
    //    timer += Time.deltaTime;
    //    if (timer >= newtarget && !moving)
    //    {
    //        NewTarget();
    //        timer = 3;
    //    }
    //    if (moving) 
    //    {
    //        float timePast = Time.time - startTime;
    //        float distanceCovered = timePast * speed;

    //        transform.position = Vector3.Lerp(startPosition, desiredPos, distanceCovered);  
    //        if (distanceCovered >= 1)
    //        {
    //            moving = false;
    //        }
    //    }
    //}

    //void NewTarget()
    //{

    //    yPos += -0.2f;
    //    desiredPos = new Vector3(transform.position.x, yPos, transform.position.z);

    //    moving = true;
        
        
    //    startPosition = transform.position;
    //    startTime = Time.time; 
    //    distance = Vector3.Distance(startPosition, desiredPos);
    //}
}

