using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LighterScript : MonoBehaviour
{
    private Collider2D m_Collider;
    private float m_ScrollValue;

    private bool m_LitState;

    [SerializeField] private Sprite m_LitLighter;
    [SerializeField] private Sprite m_UnlitLighter;

    [SerializeField] private GameObject m_LighterLight;

    [Space]
    [SerializeField] private AudioClip m_LighterFlicker;
    [SerializeField] private AudioClip m_LitSound;

    private void OnEnable()
    {
        this.m_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 m_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Position.z += Camera.main.nearClipPlane;
        this.gameObject.transform.position = m_Position;

        //if (Mathf.Abs(Input.mouseScrollDelta.y) == 1.0f)
        //    this.m_ScrollValue += 0.1f;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AudioManager.Instance.PlaySFX(this.m_LighterFlicker);
            this.StartCoroutine(this.Flicker(0.5f));
        }
        //if (this.m_ScrollValue > 1.0f)
        //{
        //    this.m_LitState = true;
        //    this.m_Collider.enabled = true;

        //    this.m_ScrollValue = 0.0f;
        //    AudioManager.Instance.PlaySFX(this.m_LighterFlicker);
        //    this.StartCoroutine(this.Flicker(0.5f));
        //}

        switch (this.m_LitState)
        {
            case true:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_LitLighter;
                this.m_LighterLight.GetComponent<Light2D>().enabled = true;
                break;

            case false:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.m_UnlitLighter;
                this.m_LighterLight.GetComponent<Light2D>().enabled = false;
                break;
        }
    }

    private IEnumerator Flicker(float delay)
    {
        int m_Rand = 0;

        while (m_Rand <= 3)
        {
            yield return new WaitForSeconds(delay);
            m_Rand = Random.Range(0, 10);
        }

        this.m_LitState = false;
        this.m_Collider.enabled = false;
        this.StopCoroutine(this.Flicker(delay));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Candle"))
        {
            if (collision.gameObject.GetComponent<CandleScript>().LitState == false)
                AudioManager.Instance.PlaySFX(this.m_LitSound);

            collision.gameObject.GetComponent<CandleScript>().LitState = true;
        }
    }
}
