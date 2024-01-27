using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketInstructionManager : MonoBehaviour
{
    [SerializeField] private HandManager handManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishInstructions()
    {
        gameObject.SetActive(false);
        handManager.SetIsInstructionFinished(true); 
    }
}
