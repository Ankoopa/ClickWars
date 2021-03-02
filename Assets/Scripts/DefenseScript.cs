using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefenseScript : MonoBehaviour
{
    public int health;
    public GameObject healthBar, explosion;

    private Slider healthBarVal;
    private bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        isDestroyed = false;

        health = 100;
        healthBar = Instantiate(healthBar);
        healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        healthBarVal = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarVal.value = (float)health / 100;
        if(health <= 0 && !isDestroyed){ // game over
            isDestroyed = true; //ensures that explosions won't instantiate every frame

            StartCoroutine(SceneLoad());
            
            explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosion, 2.0f);
            Destroy(healthBar);
            gameObject.GetComponent<MeshRenderer>().enabled = false; // hides the building mesh
        }
    }

    private IEnumerator SceneLoad(){
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoseScene");
    }
}
