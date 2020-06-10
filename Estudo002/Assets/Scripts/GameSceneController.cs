using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TextOutputHandler(string text);
public class GameSceneController : MonoBehaviour
{
    [Header("Player Configs")]
    [Range(5, 20)]
    public float playerSpeed;

    [Header("Tela Configs")]
    [Space]
    public Vector3 screenBounds;

    [Header("Prefab")]
    [Space]
    [SerializeField]
    public EnemyController enemyPrefab; //Quem vai spawnar os 

    private HUDController hUDController;
    private int totalPoints;

    private PlayerController player;

    void Start()
    {
        hUDController = FindObjectOfType<HUDController>();
        playerSpeed = 10;
        screenBounds = GetScreenBounds();
        
        //Chamada via Coroutine os inimigos
        StartCoroutine(SpawnEnemies());

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetColor(Color.red);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.SetColor(Color.yellow);
        }
    }

    //Método para 
    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(2);

        while(true)
        {
            float horizontalPosition = Random.Range(-screenBounds.x, screenBounds.x);
            Vector2 spawnPosition = new Vector2(horizontalPosition, screenBounds.y);

            //Instanciar => Prefab, posição, //Quaternion.identity = "(0.0, 0.0, 0.0, 1.0)
            //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            enemy.EnemyEscaped += EnemyAtBottom;
            enemy.EnemyKilled += EnemyKilled;

            yield return wait; 
        }
         
    }

    void EnemyKilled(int pointValue)
    {
        totalPoints += pointValue;
        hUDController.scoreText.text = totalPoints.ToString();
    }

    private void EnemyAtBottom(EnemyController enemy)
    {
        Destroy(enemy.gameObject);
        Debug.Log("Enemy Escaped");
    }

    private Vector3 GetScreenBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenVector = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);

        return mainCamera.ScreenToWorldPoint(screenVector); 
    }

    public void KillObject(IKillable killable)
    {
        Debug.LogWarningFormat("{0} objeto morto por Game Scene Controller", killable.GetName());
        killable.Kill(); 
    }

    public void OutputText(string output)
    {
        Debug.LogFormat("{0} output by GameSceneController", output);
    }
}
