using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public GameObject NextLevel;
    public GameObject PauseMenu;
    public GamestateHandler game;
    public TMP_Text bestScore;
    public bool wordlist = true;
    public TMP_Text words;

    

    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void openNextLevel(){
        NextLevel.SetActive(!NextLevel.active);
        game.gameRunning = !game.gameRunning;
        StartCoroutine(game.PlaySFX(game.chest));
    }
    public void openPauseMenu(){
        PauseMenu.SetActive(!PauseMenu.active);
        game.gameRunning = !game.gameRunning;
        StartCoroutine(game.PlaySFX(game.chest));
    }
    public void openHome(){
        StartCoroutine(game.PlaySFX(game.chest));
        SceneManager.LoadScene(0);
    }
    public void reloadGame(){
        game.score = 0;
        StartCoroutine(game.PlaySFX(game.chest));
        SceneManager.LoadScene(1);  
    }
    public void flip(){
        wordlist = !wordlist;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !NextLevel.active && !game.retryMenu.active)
        {
            openPauseMenu();

        }
        bestScore.text = game.bestScore + "";
        text.text = game.score + "";
        if(game.score2 != 0) { text.text += " + " + game.score2; }

        if(wordlist){
            words.text = "";
            for(int  i = game.pastWords.Count-1; i>=0 ;i--){
                words.text += (game.pastWords[i] +  "\n");
            }
            if(game.pastWords.Count == 0){
                words.text = ("NO WORDS YET");
            }
        }else{
            words.text = ("TAP TO EXPAND");
        }
    }
}
