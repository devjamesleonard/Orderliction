using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenuManager : MonoBehaviour
{
    public GameObject levels;
    public GameObject options;
    public GameObject menu;
    public Text text;
    public Text title;
    private int count = 0;
    public AudioSource SFX, BG;
    public Text a, b;
    public AudioMixer AudioBoard;
    public AudioClip chest, ding, background, door;
    public bool SVolOn, BVolOn;

    // Start is called before the first frame update
    void Start()
    {
        //Color newColor = new Color(0.3f, 0.4f, 0.6f, 0.9f);
        Color newColor2 = new Color((float)((float)Random.Range(0, 256)/256f), (float)((float)Random.Range(0, 256) / 256f), (float)((float)Random.Range(0, 256) / 256f), 1f);
        text.color = newColor2;
       // Debug.Log(text.color);
        bool[] VolOn = BinaryFormatt.loadVolumeData();
        BVolOn= VolOn[0];
        SVolOn = VolOn[1];
        StartCoroutine(GrowTitle());
        StartCoroutine(PlayBG(background));
        StartCoroutine(spawnletters());
    }
    public void openOptions()
    {
        StartCoroutine(PlaySFX(chest));
        options.SetActive(!options.active);
        menu.SetActive(!menu.active);
    }
    public void openLevels()
    {
        StartCoroutine(PlaySFX(chest));
        levels.SetActive(!levels.active);
        menu.SetActive(!menu.active);

    }
    public void LoadGame(){
    // make menu oscilate and geet brighter between gray and white
        SceneManager.LoadScene(1);  
    }
    public void ExitGame()
    {
        StartCoroutine(PlaySFX(chest));
        Application.Quit();
    }
    public static char GetLetter()
    {
        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

        int num = Random.Range(0, chars.Length);
        return chars[num];
    }
    public void BVolumeChange()
    {
        StartCoroutine(PlaySFX(ding));
        if (BVolOn)
        {

            BVolOn = false;
        }
        else if (!BVolOn)
        {
            BVolOn = true;

        }
      BinaryFormatt.saveVolumeData(this);
    }
    public void SVolumeChange()
    {
        StartCoroutine(PlaySFX(ding));
        if (SVolOn)
        {

            SVolOn = false;
        }
        else if (!SVolOn)
        {
            SVolOn = true;

        }
         BinaryFormatt.saveVolumeData(this);
    }
    IEnumerator spawnletters()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            
            if (count >= 115)
            {
                string x = text.text;
                text.text = "";
                for (int i = 1; i < x.Length; i++)
                {
                    text.text += x[i];
                }
                if(x.Length <= 2)
                {
                    text.text = "";
                    count = 0;
                    Color newColor2 = new Color((float)((float)Random.Range(0, 256) / 256f), (float)((float)Random.Range(0, 256) / 256f), (float)((float)Random.Range(0, 256) / 256f), 1f);
                    text.color = newColor2;

                }



            }
            else
            {
                count++;
                string x = text.text;

                text.text = GetLetter() + "";
                for (int i = 0; i < x.Length; i++)
                {
                    text.text += x[i];
                }
            }
            

        }

    }
    IEnumerator GrowTitle()
    {
        while (true) {

            for (int i = 0; i < 35; i++)
            {
                title.fontSize += 1;
                yield return new WaitForSeconds(.02f);
            }

            for (int i = 0; i < 35; i++)
            {
                title.fontSize -= 1;
                yield return new WaitForSeconds(.015f);
            }

        }

    }

    IEnumerator PlayBG(AudioClip Clip)
    {
        if (1 == 1)
        {

            BG.clip = Clip;
            BG.Play();
            yield return new WaitForSeconds(BG.clip.length);


        }
    }

    IEnumerator PlaySFX(AudioClip Clip)
    {

        SFX.clip = Clip;
        SFX.Play();
        yield return new WaitForSeconds(SFX.clip.length * 2);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            if (levels.active)
            {
                openLevels();

            }
            else
            {
                openOptions();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            ExitGame();
        }
        if (BVolOn)
        {

            //AudioBoard.SetFloat("SFX", 0);
            //AudioBoard.SetFloat("BG", -30f);
          
            BG.volume = .5f;
            a.text = "Background Music ON";


        }
        else
        {
          
            BG.volume = 0;
            //AudioBoard.SetFloat("SFX", -80f);
            //AudioBoard.SetFloat("BG", -80f);
            a.text = "Background Music OFF";
        }
        if (SVolOn)
        {

            
            //AudioBoard.SetFloat("SFX", 0);
            //AudioBoard.SetFloat("BG", -30f);
            SFX.volume = .7f;
            b.text = "Sound Effects ON";


        }
        else
        {
            
            SFX.volume = 0;
            b.text = "Sound Effects OFF";
            //AudioBoard.SetFloat("SFX", -80f);
            //AudioBoard.SetFloat("BG", -80f);
        }
    }
}
