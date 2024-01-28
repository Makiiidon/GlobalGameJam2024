using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightCandleScript : MonoBehaviour
{
    private VisualElement root;
    private VisualElement m_TutorialScreen;
    private Label m_Scream;

    [SerializeField] private CandleManager m_Manager;
    [SerializeField] private GameObject m_Ghost;
    [SerializeField] private MicLoudnessDetector m_Detector;

    // Start is called before the first frame update
    void OnEnable()
    {
        this.root = this.GetComponent<UIDocument>().rootVisualElement;
        this.m_TutorialScreen = this.root.Q<VisualElement>("TutorialScreen");
        this.m_Scream = this.root.Q<Label>("Scream");

        this.StartCoroutine(this.VoiceChecker());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.m_TutorialScreen.style.display = DisplayStyle.None;
            this.m_Manager.StartCoroutine(this.m_Manager.CandleWave());
        }

        if (this.m_Ghost.activeSelf)
            this.m_Scream.style.display = DisplayStyle.Flex;

        else
            this.m_Scream.style.display = DisplayStyle.None;
    }

    private IEnumerator VoiceChecker()
    {
        yield return new WaitForSeconds(2);

        if (this.m_Detector.GetSound() < 0.5f)
            this.m_Scream.text = "Scream at the Ghost!!";

        else if (this.m_Detector.GetSound() > 0.5f && this.m_Detector.GetSound() < 1.0f)
            this.m_Scream.text = "LOUDER!!!!";

        else if (this.m_Detector.GetSound() > 1.0f && this.m_Detector.GetSound() < 1.5f)
            this.m_Scream.text = "EVEN LOUDER!!";

        else
            this.m_Scream.text = "ok chill out...";

        this.StartCoroutine(this.VoiceChecker());
    }
}
