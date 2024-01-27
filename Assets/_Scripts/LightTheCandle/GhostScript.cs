using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private MicLoudnessDetector m_Detector;

    [SerializeField] private float m_SoundMinThreshold = 0.0f;

    private float m_GhostSpeed = 0.001f;
    private float m_SpeedMultiplier = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (this.m_Detector.GetSound() > this.m_SoundMinThreshold && this.gameObject.transform.localScale.x >= 0 && this.gameObject.transform.localScale.y >= 0)
        {
            Vector3 m_NewVector = this.gameObject.transform.localScale;

            m_NewVector.x -= 0.002f;
            m_NewVector.y -= 0.002f;

            this.gameObject.transform.localScale = m_NewVector;
        }

        else if (this.gameObject.transform.localScale.x <= 0 && this.gameObject.transform.localScale.y <= 0)
        {
            Vector3 m_NewVector = this.gameObject.transform.localScale;

            m_NewVector.x = 0.5f;
            m_NewVector.y = 0.5f;

            this.gameObject.transform.localScale = m_NewVector;
            this.gameObject.SetActive(false);
        }

        else
        {
            Vector3 m_NewVector = this.gameObject.transform.localScale;

            m_NewVector.x += this.m_GhostSpeed * this.m_SpeedMultiplier / 4.0f;
            m_NewVector.y += this.m_GhostSpeed * this.m_SpeedMultiplier / 4.0f;

            this.gameObject.transform.localScale = m_NewVector;
        }
    }

    public float GhostSpeed { get { return this.m_GhostSpeed; } set { this.m_GhostSpeed = value; } }
    public float SpeedMultiplier { get { return this.m_SpeedMultiplier; } set { this.m_SpeedMultiplier = value; } }
}
