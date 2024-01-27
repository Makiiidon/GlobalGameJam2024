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

    private List<GameObject> m_SpawnedCandles = new List<GameObject>();
    private Vector3 m_Position;

    private int m_WaveLevel = 1;
    private int m_GhostLevel = 0;

    private void OnEnable()
    {
        //StartCoroutine(this.CandleWave());
    }

    private void Update()
    {
        switch (this.m_GhostLevel)
        {
            case 0:
                this.m_Ghost.SetActive(false);
                break;

            case 1:
                this.m_Ghost.SetActive(true);
                break;

            case 2:
                this.m_Ghost.transform.localScale = new Vector3(3.0f, 3.0f);
                break;

            case 3:
                SceneManager.LoadScene(1);
                break;

            default:
                break;
        }
    }

    public IEnumerator CandleWave()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < this.m_CandleCount * this.m_WaveLevel; i++)
        {
            this.m_Position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-3.0f, 3.0f), 0.0f);

            GameObject Candle = Instantiate(this.m_PrefabSpawned, this.m_Position, Quaternion.identity, this.transform);
            this.m_SpawnedCandles.Add(Candle);
        }

        yield return new WaitForSeconds(5 * this.m_WaveLevel + 5);

        foreach (GameObject candle in this.m_SpawnedCandles)
        {
            if (candle.gameObject.GetComponent<CandleScript>().LitState == false)
            {
                this.m_GhostLevel++;
                break;
            }
        }

        foreach (GameObject candle in this.m_SpawnedCandles)
            Destroy(candle);

        if (this.m_WaveLevel < 5)
            this.m_WaveLevel++;

        this.m_SpawnedCandles.Clear();
        this.StartCoroutine(this.CandleWave());
    }
}
