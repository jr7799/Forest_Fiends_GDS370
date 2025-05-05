using ChristinaCreatesGames.EyeMovement;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject eyePrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public Transform cameraPosition;
    PupilTracking pupilTracking;
    [Header("Timer")]
    public float timer;
    public float timerReset;
    public float difficultyTimer;
    public float difficultyTimerReset;
    [Header("Spawn Vars")]
    private Vector3[] spawnSides = new Vector3[16];
    private int spawnChoice = 0;
    private int previousChoice = 0;

    private GameManager manager;
    private int totalSpawned;
    private void Start()
    {
        pupilTracking = GameObject.Find("Viewcone").GetComponent<PupilTracking>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = timerReset;
        difficultyTimer = difficultyTimerReset;
        spawnChoice = previousChoice;
    }
    private void Update()
    {
        difficultyTimer -= Time.deltaTime;
        if(difficultyTimer <= 0)
        {
            timerReset -= 0.1f;
            if(timerReset <= 0.1f)
            {
                timerReset = 0.05f;
            }
            difficultyTimer = difficultyTimerReset;
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(totalSpawned < 500)
            {
                Spawn(eyePrefab, enemy1Prefab, enemy2Prefab, enemy3Prefab, enemy4Prefab);             
            }
            timer = timerReset;
        }
        
    }
    private void Spawn(GameObject eye, GameObject enemy1, GameObject enemy2, GameObject enemy3, GameObject enemy4)
    {
        manager.increaseTotalSpawned();
        totalSpawned++;
        GameObject enemyEye = Instantiate(eye, getSpawnLocation(), Quaternion.Euler(0,180,0));
        GameObject enemy_1 = Instantiate(enemy1, getSpawnLocation(), Quaternion.Euler(0,180,0));
        GameObject enemy_2 = Instantiate(enemy2, getSpawnLocation(), Quaternion.Euler(0,180,0));
        GameObject enemy_3 = Instantiate(enemy3, getSpawnLocation(), Quaternion.Euler(0,180,0));
        GameObject enemy_4 = Instantiate(enemy4, getSpawnLocation(), Quaternion.Euler(0,180,0));
        if(enemyEye.GetComponent<Eye>() != null)
            pupilTracking.AddToEyes(enemyEye.GetComponent<Eye>());
        if (enemy_1.GetComponent<Eye>() != null)
            pupilTracking.AddToEyes(enemy_1.GetComponent<Eye>());
        if (enemy_2.GetComponent<Eye>() != null)
            pupilTracking.AddToEyes(enemy_2.GetComponent<Eye>());
        if (enemy_3.GetComponent<Eye>() != null)
            pupilTracking.AddToEyes(enemy_3.GetComponent<Eye>());
        if (enemy_4.GetComponent<Eye>() != null)
            pupilTracking.AddToEyes(enemy_4.GetComponent<Eye>());

        Debug.Log("Enemy Spawned");
    }
    private Vector3 getSpawnLocation()
    {
        //l sides
        spawnSides[0] = new Vector3(cameraPosition.position.x + Random.Range(-26, -20), cameraPosition.position.y + Random.Range(-14, -6), 0);
        spawnSides[1] = new Vector3(cameraPosition.position.x + Random.Range(-26, -20), cameraPosition.position.y + Random.Range(-11, 0), 0);
        spawnSides[2] = new Vector3(cameraPosition.position.x + Random.Range(-26, -20), cameraPosition.position.y + Random.Range(0, 11), 0);
        spawnSides[3] = new Vector3(cameraPosition.position.x + Random.Range(-26, -20), cameraPosition.position.y + Random.Range(6, 14), 0);
        //r sides
        spawnSides[4] = new Vector3(cameraPosition.position.x + Random.Range(20, 26), cameraPosition.position.y + Random.Range(-12, -4), 0);
        spawnSides[5] = new Vector3(cameraPosition.position.x + Random.Range(20, 26), cameraPosition.position.y + Random.Range(-7, 0), 0);
        spawnSides[6] = new Vector3(cameraPosition.position.x + Random.Range(20, 26), cameraPosition.position.y + Random.Range(0, 7), 0);
        spawnSides[7] = new Vector3(cameraPosition.position.x + Random.Range(20, 26), cameraPosition.position.y + Random.Range(4, 12), 0);
        //t sides
        spawnSides[8] = new Vector3(cameraPosition.position.x + Random.Range(-25, -14), cameraPosition.position.y + Random.Range(13, 17), 0);
        spawnSides[9] = new Vector3(cameraPosition.position.x + Random.Range(-14, 0), cameraPosition.position.y + Random.Range(13, 17), 0);
        spawnSides[10] = new Vector3(cameraPosition.position.x + Random.Range(0, 14), cameraPosition.position.y + Random.Range(13, 17), 0);
        spawnSides[11] = new Vector3(cameraPosition.position.x + Random.Range(14, 25), cameraPosition.position.y + Random.Range(13, 17), 0);
        //b sides
        spawnSides[12] = new Vector3(cameraPosition.position.x + Random.Range(-25, -14), cameraPosition.position.y + Random.Range(-17, -13), 0);
        spawnSides[13] = new Vector3(cameraPosition.position.x + Random.Range(-14, 0), cameraPosition.position.y + Random.Range(-17, -13), 0);
        spawnSides[14] = new Vector3(cameraPosition.position.x + Random.Range(0, 14), cameraPosition.position.y + Random.Range(-17, -13), 0);
        spawnSides[15] = new Vector3(cameraPosition.position.x + Random.Range(14, 25), cameraPosition.position.y + Random.Range(-17, -13), 0);

        while (spawnChoice == previousChoice)
            spawnChoice = Random.Range(0, spawnSides.Length - 1);
        previousChoice = spawnChoice;

        return spawnSides[spawnChoice];
    }
}
