using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handManager.SetIsGameOver(true);
        Debug.Log("GAME OVER");
    }
}
