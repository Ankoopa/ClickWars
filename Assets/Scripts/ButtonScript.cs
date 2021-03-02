using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button playAgainBtn, quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        playAgainBtn.onClick.AddListener(PlayAgain);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void PlayAgain(){
        SceneManager.LoadScene("GameScene");
	}

    void QuitGame(){
        Application.Quit();
    }
}
