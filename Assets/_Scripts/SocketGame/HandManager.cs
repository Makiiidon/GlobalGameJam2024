using GG.Infrastructure.Utils.Swipe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    // Flag
    [SerializeField] private bool scrolled = false;

    // SFX
    [SerializeField] private float groanTicks;
    [SerializeField] private float groanInterval;
    [SerializeField] private float maxGroanInterval;
    [SerializeField] private float minGroanInterval;    
    [SerializeField] private AudioClip groansSFX;
    [SerializeField] private AudioClip backSFX;

    // For Swipe 
    [SerializeField] private SwipeListener swipeListener;

    private void OnEnable()
    {
        if (rb == null )
        {
            rb = GetComponent<Rigidbody2D>();
        }

        groanInterval = minGroanInterval;

        // Swipe
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstructionFinished)
        {
            if (!isGameOver)
            {
                PlayRandomGroans();


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

                /* Temporarily disable for mobile
                // Bring back hand using scroll wheel down
                if (Input.mouseScrollDelta.y < 0)
                {
                    scrolled = true;               
                }
                */
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if(scrolled)
        {
            rb.AddForce(new Vector2(0, -1.0f * pullForce * Time.deltaTime));
            scrolled = false;
        }
    }

    private void OnSwipe(string swipe)
    {
        // If scrolling down, move hand down
        if(swipe == "Down")
        {
            scrolled = true;
        }
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
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

    void PlayRandomGroans()
    {
        groanTicks += Time.deltaTime;
        if(groanTicks > groanInterval)
        {
            groanTicks = 0; 

            int chosen = Random.Range(1, 3);
            if (chosen == 1)
                AudioManager.Instance.PlaySFX(groansSFX);
            else if (chosen == 2)
                AudioManager.Instance.PlaySFX(backSFX);

            groanInterval = Random.Range(minGroanInterval, maxGroanInterval);
        }
    }
}
