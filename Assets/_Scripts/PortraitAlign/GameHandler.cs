using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Transform Painting;
    [SerializeField] Transform Hands;
    [SerializeField] GameObject Sparkles;

    bool hasStarted = true;

    [SerializeField] float paintingStrength;
    [SerializeField] float armStrength;
    float timer = 4.0f;
    [SerializeField] float initialAngle = 0.0f;

    [SerializeField] float elapsedTime = 0.0f;
    float totalTime = 60.0f;
    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] GameObject Panel;
    bool firstTime = true;


    // Start is called before the first frame update
    void OnEnable ()
    {
        paintingStrength = Random.Range(-15.0f, 15.0f);
        initialAngle = Painting.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if(totalTime > 0.0f)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                armStrength = mousePos.x;

                if (elapsedTime > timer)
                {
                    
                    paintingStrength = Random.Range(-10.0f, 10.0f);
                    elapsedTime = 0.0f;
                    if (firstTime)
                    {
                        firstTime = false;
                        timer = 3.0f;
                    }
                }
                if (firstTime)
                {
                    Image pic = Panel.transform.GetChild(0).GetComponent<Image>();
                    TextMeshProUGUI text = Panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    float a1 = Mathf.Lerp(pic.color.a, 0, Time.deltaTime);
                    Color output = new Color(pic.color.r, pic.color.g, pic.color.b, a1);
                    pic.color = output;
                    float a2 = Mathf.Lerp(text.color.a, 0, Time.deltaTime);
                    Color output2 = new Color(text.color.r, text.color.g, text.color.b, a2);
                    text.color = output2;
                    

                }
                if (!firstTime)
                {
                    Debug.Log("Arm Strength: " + armStrength + " Painting Strength: " + paintingStrength);
                    if (Mathf.Abs(armStrength) > Mathf.Abs(paintingStrength))
                    {

                        if (Hands.transform.rotation.z <= 60.0 && Hands.transform.rotation.z >= -60.0f) //if the arm strength is stronger than the painting
                        {
                            Hands.transform.RotateAround(Hands.transform.position, Vector3.forward, armStrength * Time.deltaTime);
                            Painting.transform.RotateAround(Painting.transform.position, Vector3.back, (armStrength + paintingStrength) * Time.deltaTime);

                        }
                    }
                    else if (Mathf.Abs(armStrength) < Mathf.Abs(paintingStrength))  //If painting rotates more than the hand strength
                    {
                        if (Painting.transform.rotation.z <= 60.0 && Painting.transform.rotation.z >= -60.0f)
                        {
                            Painting.transform.RotateAround(Painting.transform.position, Vector3.forward, paintingStrength * Time.deltaTime);
                        }

                    }


                    totalTime -= Time.deltaTime;
                    float minutes = Mathf.FloorToInt(totalTime / 60);
                    float seconds = Mathf.FloorToInt(totalTime % 60);
                    string mins = minutes.ToString();
                    string secs = seconds.ToString();
                    if (minutes < 10)
                    {
                        mins = "0" + minutes.ToString();
                    }
                    if (seconds < 10)
                    {
                        secs = "0" + seconds.ToString();
                    }
                    clockText.SetText(mins + ":" + secs);
                }
               

                elapsedTime += Time.deltaTime;
                float currentAngle = Painting.transform.rotation.eulerAngles.z;
                if (currentAngle > 180)
                {
                    currentAngle -= 360.0f;

                }
                //Debug.Log("Current Angle: " + currentAngle);

                if (currentAngle >= -5.0f && currentAngle <= 5.0f)
                {
                    Sparkles.SetActive(true);
                    //Debug.Log("Within of Range");
                }
                else
                {
                   // Debug.Log("Outside of Range");
                    Sparkles.SetActive(false);
                }
            }
            else
            {
                float currentAngle = Painting.transform.rotation.eulerAngles.z;
                if (currentAngle > 180)
                {
                    currentAngle -= 360.0f;
                   
                }
                if (currentAngle >= -5.0f && currentAngle <= 5.0f) 
                {
                    GameManager.Instance.SetWinState(3, true);
                    //Insert Winning Code
                }
                else
                {
                    GameManager.Instance.SetWinState(3, false);
                    //Insert Losing Code
                }
                clockText.gameObject.SetActive(false);
            }
        }
    }
}
