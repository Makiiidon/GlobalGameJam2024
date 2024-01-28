using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private bool isLeft = true;
    [SerializeField] private GameObject handPrefab;
    private Rigidbody2D rb;
    [SerializeField] private float swerveForce;
    [SerializeField] private HandManager handManager;

    // Flags
    [SerializeField] private bool clicked = false;


    private void OnEnable()
    {
        if(rb == null)
        {
            rb = handPrefab.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if(handManager.GetIsInstructionFinished())
        {
            if (!handManager.GetIsGameOver())
            {
                if (isLeft)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log("Left Area Click");
                        //rb.AddForce(new Vector2(-swerveForce * Time.deltaTime, 0));
                        clicked = true;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log("Right Area Click");
                       // rb.AddForce(new Vector2(swerveForce * Time.deltaTime, 0));
                        clicked = true;
                    }
                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (clicked)
        {
            if (isLeft)
            {
                //Debug.Log("Left Area Click");
                rb.AddForce(new Vector2(-swerveForce * Time.deltaTime, 0));
                clicked = false;
            }
            else 
            {
                //Debug.Log("Right Area Click");
                rb.AddForce(new Vector2(swerveForce * Time.deltaTime, 0));
                clicked = false;
            }
        }
    }
}
