using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScrollBackGround : MonoBehaviour
{
    public float speed;
    public int topPosition;
    public float turnPosition;
    public Image BGImageUp;
    public Image BGImageMiddle;
    public Image BGImageDown;


    void Update()
    {
        Transform BGObjTransform = this.transform;

        BGObjTransform.position -= new Vector3(0, Time.deltaTime * speed);
        

        if (BGObjTransform.position.y <= topPosition)
        {
            BGObjTransform.position = new Vector3(0, turnPosition);
        }
    }
}
