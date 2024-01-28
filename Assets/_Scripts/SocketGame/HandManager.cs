using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] GameObject handPrefab;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float ticks = 0.0f;
    [SerializeField] private float swerveForce;
    [SerializeField] private float minSwerveForce;
    [SerializeField] private float maxSwerveForce;

    [SerializeField] private float minSwerveInterval;
    [SerializeField] private float maxSwerveInterval;
    [SerializeField] private float SWERVE_INTERVAL;

    [SerializeField] private float pullForce;

    [SerializeField] private bool isGameOver = false;
    [SerializeField] private bool isInstructionFinished = false;


    private void OnEnable()
    {
        if (rb == null )
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstructionFinished)
        {
            if (!isGameOver)
            {
                // Bounds of screen
                if (handPrefab.transform.position.x < -7.35f)
                {
                    handPrefab.transform.position = new Vector3(-7.35f, handPrefab.transform.position.y, handPrefab.transform.position.z);
                }
                else if(handPrefab.transform.position.x > 7.08f)
                {
                    handPrefab.transform.position = new Vector3(7.08f, handPrefab.transform.position.y, handPrefab.transform.position.z);
                }

                // Slowly move towards the outlet
                handPrefab.transform.position += new Vector3(0, speed * Time.deltaTime, 0);

                // Randomly swerve after certain intervals
                ticks += Time.deltaTime;
                if (ticks > SWERVE_INTERVAL)
                {
                    ticks = 0.0f;
                    RandomSwerve();
                    SWERVE_INTERVAL = Random.Range(minSwerveInterval, maxSwerveInterval);
                }

                // Bring back hand using scroll wheel down
                if (Input.mouseScrollDelta.y < 0)
                {
                    rb.AddForce(new Vector2(0, -1.0f * pullForce * Time.deltaTime));
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void RandomSwerve()
    {
        int randX = Random.Range(1, 3);
        if (randX == 2)
            randX = -1;
        //Debug.Log("Rand X" + randX);

        int randY = Random.Range(1, 3);
        if (randY == 2)
            randY = 0;

        swerveForce = Random.Range(minSwerveForce, maxSwerveForce);

        rb.AddForce(new Vector2(randX * swerveForce * Time.deltaTime, randY * swerveForce / 2.0f * Time.deltaTime));
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public bool GetIsInstructionFinished()
    {
        return isInstructionFinished;
    }

    public void SetIsGameOver(bool flag)
    {
        isGameOver = flag;
    }

    public void SetIsInstructionFinished(bool flag)
    {
        isInstructionFinished = flag;
    }
}
