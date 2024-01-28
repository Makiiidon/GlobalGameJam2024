using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private bool isInstructionsFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TriggerFadeIn()
    {
        animator.SetTrigger("didFadeIn");
        yield return new WaitForSeconds(3.1f);
    }

    public void DisableInstructionPanel()
    {
        instructionPanel.SetActive(false);
        isInstructionsFinished = true;
    }

    public bool GetIsInstructionsFinished()
    {
        return isInstructionsFinished;
    }
}
