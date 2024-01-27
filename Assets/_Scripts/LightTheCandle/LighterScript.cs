using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterScript : MonoBehaviour
{
    private Collider2D m_Collider;

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

        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.m_Collider.enabled = true;
            this.StartCoroutine(this.Flicker(0.5f));
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

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.m_Collider.enabled = false;
        this.StopCoroutine(this.Flicker(delay));
        Debug.Log("ENDED");
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Candle"))
        {
            Debug.Log("Hit");
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true); //Change when assets are done
        }
    }
}
