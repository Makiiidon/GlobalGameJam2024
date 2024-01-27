using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    [SerializeField] private Sprite m_LitCandle;
    [SerializeField] private Sprite m_UnlitCandle;

    private bool m_LitState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        switch (this.m_LitState)
        {
            case true:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_LitCandle;
                break;

            case false:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_UnlitCandle;
                break;
        }
    }

    public bool LitState { get { return this.m_LitState; } set { this.m_LitState = value; } }
}
