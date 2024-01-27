using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightCandleScript : MonoBehaviour
{
    private VisualElement root;
    private VisualElement m_TutorialScreen;

    [SerializeField] private CandleManager m_Manager;

    // Start is called before the first frame update
    void OnEnable()
    {
        this.root = this.GetComponent<UIDocument>().rootVisualElement;
        this.m_TutorialScreen = this.root.Q<VisualElement>("TutorialScreen");

        this.m_TutorialScreen.AddManipulator(new Clickable(evt => { this.m_TutorialScreen.style.display = DisplayStyle.None; 
                                                                    this.m_Manager.StartCoroutine(this.m_Manager.CandleWave()); }));
    }
}
