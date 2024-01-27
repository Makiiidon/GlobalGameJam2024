using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleManager : MonoBehaviour
{
    [SerializeField] private int m_CandleCount = 5;
    [SerializeField] private GameObject m_PrefabSpawned;

    [SerializeField] private GameObject m_Ghost;

    [SerializeField]private List<CandleScript> m_CandleList = new List<CandleScript>();

    private int m_WaveLevel = 1;
    private int m_GhostSpeedMultiplier = 1;

    Vector3 m_Position;

    private void OnEnable()
    {
        //StartCoroutine(this.CandleWave());
        CandleScript[] m_Candles = FindObjectsOfType<CandleScript>();

        foreach (CandleScript candle in m_Candles)
            this.m_CandleList.Add(candle);
    }

    private void Update()
    {
        if (this.m_WaveLevel >= 10)
            SceneManager.LoadScene(2);
    }

    public IEnumerator CandleWave()
    {
        //yield return new WaitForSeconds(2);

        //for (int i = 0; i < this.m_CandleCount * this.m_WaveLevel; i++)
        //{
        //    this.m_Position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-3.0f, 3.0f), 0.0f);

        //    GameObject Candle = Instantiate(this.m_PrefabSpawned, this.m_Position, Quaternion.identity, this.transform);
        //    this.m_SpawnedCandles.Add(Candle);
        //}

        yield return new WaitForSeconds(5.0f);

        //foreach (GameObject candle in this.m_SpawnedCandles)
        //    Destroy(candle);
        this.m_Ghost.SetActive(true);

        this.m_Position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-3.0f, 3.0f), 0.0f);
        this.m_Ghost.transform.position = this.m_Position;

        foreach (CandleScript candle in this.m_CandleList)
        {
            int m_Random = Random.Range(0,10);

            if (m_Random > 5)
                candle.LitState = false;
        }

        this.m_GhostSpeedMultiplier = 1;

        foreach (CandleScript candle in this.m_CandleList)
            if (candle.LitState == false)
                this.m_GhostSpeedMultiplier++;

        this.m_Ghost.GetComponent<GhostScript>().SpeedMultiplier = this.m_GhostSpeedMultiplier;

        if (this.m_WaveLevel < 10)
            this.m_WaveLevel++;

        this.StartCoroutine(this.CandleWave());
    }
}
