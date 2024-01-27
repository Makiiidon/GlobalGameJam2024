using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleScript : MonoBehaviour
{
    [SerializeField] private Sprite m_LitCandle;
    [SerializeField] private Sprite m_UnlitCandle;

    [SerializeField] private Light2D m_Candlelight;

    private bool m_LitState = false;

    // Update is called once per frame
    private void Update()
    {
        switch (this.m_LitState)
        {
            case true:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_LitCandle;
                this.m_Candlelight.intensity = 1.0f;
                break;

            case false:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_UnlitCandle;
                this.m_Candlelight.intensity = 0.05f;
                break;
        }
    }

    public bool LitState { get { return this.m_LitState; } set { this.m_LitState = value; } }
}
