using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyFab;
    public float timeBetweenWaves = 5f;
    public Transform spwanPoint;
    private float countDown = 0f;

    private int waveNumber = 1;

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpwanWave());
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
    }

    private IEnumerator SpwanWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpwanEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        ++waveNumber;
    }

    private void SpwanEnemy()
    {
        Instantiate(enemyFab, spwanPoint.position, spwanPoint.rotation);
    }
}
