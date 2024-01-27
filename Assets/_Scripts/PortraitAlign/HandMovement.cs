using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HandMovement : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = mousePos.x;
        this.transform.RotateAround(this.transform.position, Vector3.forward, x * Time.deltaTime);
        //Vector3 dir = mousePos - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //if (angle < -60.0)
        //{
        //    angle = -60;
        //}
        //else if (angle > 60.0)
        //{
        //    angle = 60;
        //}
        //Debug.Log(angle);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
