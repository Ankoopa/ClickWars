using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    private Ray mouseRay;
    private RaycastHit hit;
    private float spawnTimer;

    public GameObject groundSpawn, airSpawn;
    public GameObject[] enemies;
    public GameObject bossEnemy;
    public bool bossPresent;
    public int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        spawnTimer = 0f;
        bossPresent = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();

        if (Input.GetMouseButtonDown(0)){
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit)){
                if(hit.transform.tag == "Enemy" || hit.transform.tag == "BossEnemy"){
                    hit.transform.GetComponent<EnemyBehaviour>().health -= Random.Range(3, 7);
                }
            }
        }
    }

    void SpawnEnemy(){
        if(spawnTimer <= 0f && !bossPresent){
            if(numEnemies % 10 == 0 && numEnemies >= 10){
                bossPresent = true;
                Instantiate(bossEnemy, groundSpawn.transform.position, groundSpawn.transform.rotation);
            }
            else{
                int randEnemy = Random.Range(0, enemies.Length);
                GameObject spawn;

                if(randEnemy == 0){
                    spawn = groundSpawn;
                }
                else{
                    spawn = airSpawn;
                }

                Instantiate(enemies[randEnemy], spawn.transform.position, spawn.transform.rotation);
            }
            spawnTimer = Random.Range(3f, 5f);
        }
        else{
            spawnTimer -= Time.deltaTime;
        }
    }
}
