using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private float perfectX = -1.2f;
    [SerializeField] private float allowance;
    [SerializeField] private GameObject shakeFX;
    private GameObject hand;
    [SerializeField] private GameObject electricity;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handManager.SetIsGameOver(true);
        shakeFX.SetActive(false);

        hand = handManager.gameObject;   
        if(hand.transform.position.x == perfectX)
        {
            Debug.Log("PERFECT");
            StartCoroutine(RevealPlug());
        }
        else if((hand.transform.position.x > perfectX && hand.transform.position.x <= perfectX + allowance) ||
                (hand.transform.position.x < perfectX && hand.transform.position.x >= perfectX - allowance))
        {
            Debug.Log("Close Enough.");
            StartCoroutine(RevealPlug());
        }
        else
        {
            Debug.Log("Where are you aiming, granpapi?");

            StartCoroutine(RevealPlug());
            StartCoroutine(TriggerElectricity());   
        }
    }

    IEnumerator RevealPlug()
    {
        Animator handAnimator = hand.GetComponent<Animator>();  
        yield return new WaitForSeconds(0.5f);
        handAnimator.SetTrigger("checkPlug");
    }

    IEnumerator TriggerElectricity()
    {
        yield return new WaitForSeconds(1.0f);
        electricity.SetActive(true);
        Animator animator = electricity.GetComponent<Animator>();
        animator.SetTrigger("didExplode");
    }
}
