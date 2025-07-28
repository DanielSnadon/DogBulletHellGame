using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.TextCore.Text;

public class Spawner : MonoBehaviour
{
    public int waveCredits = 1;
    public int enemyCount;
    public int hardCount;
    public int midCount;
    public int easyCount;
    public GameObject hardEnemy;
    public GameObject midEnemy;
    public GameObject easyEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0)
        {
            SpawnNewWave(waveCredits);
            waveCredits++;
        }
    }

    void SpawnNewWave(int credits)
    {
        int points = credits;
        hardCount = Random.Range(0, (points / 5));
        points -= hardCount * 5;
        midCount = Random.Range(0, (points / 3));
        points -= midCount * 3;
        easyCount = points;
        for (int i = 0; i < hardCount; i++)
        {
            Instantiate(hardEnemy, GenerateSpawnPosition(), hardEnemy.transform.rotation);
        }
        for (int i = 0; i < midCount; i++)
        {
            Instantiate(midEnemy, GenerateSpawnPosition(), midEnemy.transform.rotation);
        }
        for (int i = 0; i < easyCount; i++)
        {
            Instantiate(easyEnemy, GenerateSpawnPosition(), easyEnemy.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        return new Vector3(Random.Range(-20, 20), 2, Random.Range(-20, 20));
    }
}
