using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityStandardAssets.Utility.WaypointProgressTracker tracker;
    private DefenseScript building;
    private GameScript gameScript;
    private int waypointPos;
    private Slider healthBarVal;


    public int health, maxHealth;
    public GameObject healthBar;
    public GameObject explosion;

    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameScript>();

        // init enemy health bar
        health = maxHealth;
        healthBar = Instantiate(healthBar);
        healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        healthBarVal = healthBar.GetComponent<Slider>();

        building = GameObject.FindGameObjectWithTag("MainBase").GetComponent<DefenseScript>();
        tracker = gameObject.GetComponent<UnityStandardAssets.Utility.WaypointProgressTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        // enemy health bars always follow and update accordingly
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        healthBarVal.value = (float)health / maxHealth;

        waypointPos = tracker.progressNum;

        if(waypointPos >= 7 || health <= 0){
            if(waypointPos >= 7){ // WP 7 is the building
                building.health -= 25;
            }

            gameScript.numEnemies++;

            if(gameObject.transform.tag == "BossEnemy"){
                if(gameScript.numEnemies >= 20){
                    SceneManager.LoadScene("VictoryScene");
                }
                gameScript.bossPresent = false;
            }

            //explosion FX on death
            explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosion, 2.0f);
            Destroy(healthBar);
            Destroy(gameObject);
        }
    }
}
