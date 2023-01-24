using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
public class GamestateHandler : MonoBehaviour
{
    public Vector2[] words;
    public char[] choices;
    public int letters;
    public Tile[][] tiles;
  
    
    public ArrayList pastWords = new ArrayList();

    public GridManager grid;
    private Vector3 startingpos;
    public GameObject[] Levels;
    public int level = 0;
    public bool deleteLetterBoxes = false;
    public bool deleteLetterBoxes2 = false;
    public bool gameRunning = false;
    public GameObject letterbox, token;
    public GameObject retryMenu;
    public HealthSystem healthSystem;
    public float timing = 90;
    public int count;
    public int possiblescore = 0;
    public int iterations = 0;
    public AudioClip chest, ding, background, door, loss, laser, startup, refresh;
    //DOOR CAN BE DOOR SOUND OR HIT19 FROM SOUNDS
    public AudioSource SFX, BG;
    public bool SVolOn, BVolOn;
    public int bestScore = 0;
    public int score = 0;
    public int score2 = 0;
    public GameObject newscore;
    private string D;
    private FileInfo Dictionarys = new FileInfo("./Assets/Resource/Dictionary.txt");
    //public Transform camera;
    //laser, loss, and restart, startup

    private char[] shuffle(char[] charArray)
    {
        char[] shuffledArray = new char[charArray.Length];
        int rndNo;


        for (int i = charArray.Length; i >= 1; i--)
        {
            rndNo = Random.Range(1, i + 1) - 1;
            shuffledArray[i - 1] = charArray[rndNo];
            charArray[rndNo] = charArray[i - 1];
        }
        return shuffledArray;
    }

    public void openRetryMenu()
    {
        
        retryMenu.SetActive(true);
        if (bestScore < score)
        {
            bestScore = score;
            newscore.SetActive(true);
            BinaryFormatt.saveBestScoreData(this);

        }

        gameRunning = !gameRunning;
    }

    public void startLevel(int level)
    {

        FileInfo RegularExpressions = new FileInfo("./Assets/Resources/RegularExpressions.txt");
        FileInfo Vector2 = new FileInfo("./Assets/Resources/Vector2.txt");
        FileInfo Count = new FileInfo("./Assets/Resources/Count.txt");
        TextAsset mytxtData = (TextAsset)Resources.Load("Dictionary");
        D = mytxtData.text;
        mytxtData = (TextAsset)Resources.Load("Vector2");
        string V = mytxtData.text;
        mytxtData = (TextAsset)Resources.Load("RegularExpressions");
        string R = mytxtData.text;
        mytxtData = (TextAsset)Resources.Load("Count");
        string C = mytxtData.text;

       
        /*
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");


        FileInfo RegularExpressions = new FileInfo("./Assets/Dictionary/RegularExpressions.txt"); 
        FileInfo Vector2 = new FileInfo("./Assets/Dictionary/Vector2.txt");
        FileInfo Count = new FileInfo("./Assets/Dictionary/Count.txt");
        //Dictionary = new FileInfo("./Assets/Dictionary/Dictionary.txt");
        foreach (FileInfo file in allFiles)
        {
            if (file.Name.Contains("Dictionary"))
            {
                Dictionarys = file;
            }
            if (file.Name.Contains("Vector2"))
            {
                Vector2 = file;
            }
            if (file.Name.Contains("RegularExpressions"))
            {
                RegularExpressions = file;
            }
            if (file.Name.Contains("Count"))
            {
                Count = file;
            }
        }
        */
        /*
        string filePath = Application.streamingAssetsPath + "/Dictionary.txt";
        Dictionarys = new FileInfo(filePath);
        filePath = Application.streamingAssetsPath + "/RegularExpressions.txt";
             RegularExpressions = new FileInfo(filePath);
        filePath = Application.streamingAssetsPath + "/Vector2.txt";
        Vector2 = new FileInfo(filePath);
        filePath = Application.streamingAssetsPath + "/Count.txt";
        
        Count = new FileInfo(filePath);
        */

        gameRunning = true;
        startingpos = letterbox.transform.position;
        count = 0;
        if (level < 1)
        {


            int z = (int)Random.Range(1, 799);



           
            //string text;
            int first = 0;
            int second = 0;
            int third = 0;
            int fourth = 0;

            int find = 0;
            string[] strlist = V.Split("\n");
            //Debug.Log(strlist[z]);
            string s = strlist[z];
            first = s[0] - '0';
            second = s[2] - '0';
            third = s[4] - '0';
            fourth = s[6] - '0';
        
            /*
            FileInfo theSourceFile = Vector2;


            StreamReader reader = theSourceFile.OpenText();
            string text;

            int first = 0;
            int second = 0;
            int third = 0;
            int fourth = 0;

            int find = 0;

            do
            {
                text = reader.ReadLine();
                find++;
                if (z == find)
                {
                    first = text[0] - '0';
                    second = text[2] - '0';
                    third = text[4] - '0';
                    fourth = text[6] - '0';
                }




            } while (text != null && find < z + 1);
            */
            //string fifth = "";
            strlist = R.Split("\n");
            //Debug.Log(strlist[z]);
            string fifth = strlist[z];
            //Debug.Log(fifth);

            /*
            theSourceFile = RegularExpressions;
            reader = theSourceFile.OpenText();
            
            string fifth = "";
            find = 0;

            do
            {

                text = reader.ReadLine();

                find++;
                if (z == find)
                {
                    fifth = text;
                }


            } while (text != null && find < z + 1);
            */

            strlist = C.Split("\n");
            string sixth = strlist[z];
            /*
            theSourceFile = Count;
            reader = theSourceFile.OpenText();

            string sixth = "";
            find = 0;

            do
            {

                text = reader.ReadLine();

                find++;
                if (z == find)
                {
                    sixth = text;
                }


            } while (text != null && find < z + 1);
            */
            //Vector2[] wds = {new Vector2(11, 6-first),new Vector2(11, 6),new Vector2(11-third, 5-fourth),new Vector2(11+second-third, 5-fourth)};
            Vector2[] wds = { new Vector2(11, 1), new Vector2(11, 1 + first), new Vector2(11 - fourth, 1 + third), new Vector2(11 + second - fourth, 1 + third) };
            int amountoftiles = (first) + (second);
            timing = (amountoftiles * 8);
            char[] choi = new char[7];
            choi = fifth.ToUpper().Trim().ToCharArray();
            //Debug.Log(choi[7]);
            /*
            for (int i = 0; i < fifth.Length-1; i++)
            {
                choi[i] = fifth[i];
            }
            */
            choices = shuffle(choi);
            /*
            string animal = "";
            for(int i = 0; i < choi.Length; i++)
            {
                if (choi[i] + "" != " ")
                {
                   // Debug.Log(choi[i]);
                   animal += choi[i];
                }
            }
            */
            //Debug.Log(animal.ToCharArray().Length);
            words = wds;
            //choices = animal.ToCharArray();
            healthSystem.startGame();

        }
        else if (level == 1)
        {
            //First set is up, next set is down
            Vector2[] wds = { new Vector2(11, 1), new Vector2(11, 6), new Vector2(8, 5), new Vector2(12, 5) };
            char[] chs = { 'P', 'A', 'C', 'E', 'D', 'F', 'G' };
            choices = shuffle(chs);
            words = wds;
        }

        StartCoroutine(spawnletters());
        grid.StartGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 pos = new Vector3(10.5,1.5,-10)

        bool[] Vol = BinaryFormatt.loadVolumeData();

        if (Vol[0])
        {
            // AudioBoard.SetFloat("SFX", 0);

            BG.volume = .05f;
        }
        else
        {

            BG.volume = 0;
            //AudioBoard.SetFloat("SFX", -80);
            //AudioBoard.SetFloat("BG", -80);
        }
        if (Vol[1])
        {
            // AudioBoard.SetFloat("SFX", 0);
            //AudioBoard.SetFloat("BG", -80);
            SFX.volume = .7f;

        }
        else
        {
            SFX.volume = 0;

            //AudioBoard.SetFloat("SFX", -80);
            //AudioBoard.SetFloat("BG", -80);
        }

        bestScore = BinaryFormatt.loadBestScoreData();
        StartCoroutine(PlaySFX(startup));
        //use chiptune as alternative track

        StartCoroutine(PlayBG(background));
        startLevel(level);
    }

    public void checkWords()
    {
        if (gameRunning)
        {
            StartCoroutine(PlaySFX(door));
            string[] wordarray = new string[tiles.Length];
            bool[] correctwords = new bool[tiles.Length];
            string[] answers = new string[tiles.Length];
            int count = 0;
            score2 = 0;
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].getLetter() == ' ')
                    {
                        wordarray[i] += ' ';
                    }
                    else
                    {
                        wordarray[i] += char.ToLower(tiles[i][j].getLetter());
                        int[] scoreArray = { 2, 30, 20, 10, 1, 21, 28, 9, 6, 40, 35, 12, 14, 7, 4, 29, 40, 8, 9, 2, 18, 32, 26, 38, 27, 40 };
                        score2 += scoreArray[((int)(char.ToLower(tiles[i][j].letter))) - 97];
                        score2 += (int)((healthSystem.hitPoint / healthSystem.maxHitPoint) * 10);
                    }

                }



                //Debug.Log(wordarray[i] + "-");
            }

            
            string[] strlist = D.Split("\n");
            foreach (string s in strlist)
            {
                
                if(s.Length < 8 && s.Length > 1){
                for (int i = 0; i < wordarray.Length; i++)
                {
                    
                    //string s = d.Substring(0,d.Length-1);
                    //correctwords[i] = false;
                    if (wordarray[i] == s.Trim())
                    {
                        answers[i] = s.Trim();
                        correctwords[i] = true;
                        //Debug.Log(text);
                        count++;
                        break;
                    }
                }
                }
            }
            /*
            FileInfo theSourceFile = Dictionarys;
            StreamReader reader = theSourceFile.OpenText();

            string text;

            do
            {
                text = reader.ReadLine();
                //Console.WriteLine(text);
                for (int i = 0; i < wordarray.Length; i++)
                {
                    //correctwords[i] = false;
                    if (wordarray[i] == text)
                    {
                        answers[i] = text;
                        correctwords[i] = true;
                        //Debug.Log(text);
                        count++;
                        break;
                    }
                }
            } while (text != null);

            */

            for (int i = 0; i < correctwords.Length; i++)
            {
                if (correctwords[i])
                {
                    correctwords[i] = (answers[i] == wordarray[i]);
                }

                if (!correctwords[i])
                {
                    for (int j = 0; j < tiles[i].Length; j++)
                    {
                        // Color x = black
                        tiles[i][j].piece.color = Color.black;

                    }

                }
            }
            for (int i = 0; i < correctwords.Length; i++)
            {
                if (correctwords[i])
                {
                    for (int j = 0; j < tiles[i].Length; j++)
                    {
                        // Color x = green
                        tiles[i][j].piece.color = Color.green;

                    }

                }
            }


            if (count >= tiles.Length && gameRunning)
            {
                gameRunning = false;
                if (level < 1)
                {   int[] scorex = new int[tiles.Length];
                    int timebonus = 0;
                    int[] scoreArray = { 2, 30, 20, 10, 1, 21, 28, 9, 6, 40, 35, 12, 14, 7, 4, 29, 40, 8, 9, 2, 18, 32, 26, 38, 27, 40 };
                    for (int i = 0; i < tiles.Length; i++)
                    {

                        for (int j = 0; j < tiles[i].Length; j++)
                        {

                            score2 = 0;
                            // Color x = green
                            //score +=scoreArray[((int)tiles[i][j].letter.ToLower())-97];
                           
                            scorex[i] += scoreArray[((int)(char.ToLower(tiles[i][j].letter))) - 97];
                            
                            //score += scoreArray[((int)tiles[i][j].letter.ToLower()) - 97];
                        }


                    }
                    timebonus += (int)((healthSystem.hitPoint / healthSystem.maxHitPoint) * 15);
                    for(int i = 0; i<answers.Length;i++){
                        if(i == answers.Length-1){
                                pastWords.Add(answers[i] + " (" + scorex[i] + ")");
                            score += scorex[i];
                        }else{
                                pastWords.Add(answers[i] + " (" + scorex[i] + ")" + "+ (" + timebonus + ")");
                                 
                            score += scorex[i]; 
                        }
                    }
                    score += timebonus;

                    startLevel(0);

                    iterations++;
                    //timing =
                  
                }

                StartCoroutine(PlaySFX(laser));
            }
        }

    }
    public void rearrange()
    {

        StartCoroutine(PlaySFX(refresh));
        if (!deleteLetterBoxes2)
        {
            choices = shuffle(choices);

            StartCoroutine(spawnletters());
        }
        //  myArray.OrderBy(x => r.Next()).ToArray();
    }
    IEnumerator spawnletters()
    {
        count = 0;
        deleteLetterBoxes2 = true;
        deleteLetterBoxes = true;
        yield return new WaitForSeconds(.1f);
        deleteLetterBoxes = false;
        letterbox.transform.position = startingpos;
        Transform t = letterbox.transform;
        t.position = new Vector3(t.position.x + (float)(10 + (5 - letters)), t.position.y + (1.5f), t.position.z);
        for (int i = 0; i < letters; i++)
        {



            t.position = new Vector3(t.position.x + 1.5f, t.position.y, t.position.z);
            Instantiate(letterbox, gameObject.transform).SetActive(true);


            yield return new WaitForSeconds(.05f);
        }
        t.position = startingpos;
        deleteLetterBoxes2 = false;

    }
    // Update is called once per frame

    public IEnumerator PlayBG(AudioClip Clip)
    {
        if (1 == 1)
        {

            BG.clip = Clip;
            BG.Play();
            yield return new WaitForSeconds(BG.clip.length);


        }
    }

    public IEnumerator PlaySFX(AudioClip Clip)
    {

        SFX.clip = Clip;
        SFX.Play();
        yield return new WaitForSeconds(SFX.clip.length * 2);

    }
}










///the actual script for finding random words

/*

    FileInfo theSourceFile = new FileInfo("./Assets/Dictionary/Dictionary.txt");
    StreamReader reader = theSourceFile.OpenText();
    //look into closing text and reader

    //100 is the bar
         int amount = 0;
         int first = (int)( Random.Range(2,5));
    int second = (int)( Random.Range(2,5));
    int third = (int) ( Random.Range(0,first-1));
    int fourth = (int)(  Random.Range(0,second-1));
      string letters = "[";
while(amount < 100){
amount = 0;
first = (int)( Random.Range(2,5));
    second = (int)( Random.Range(2,5));
    third = (int) ( Random.Range(0,first-1));
    fourth = (int)(  Random.Range(0,second-1));
    ArrayList array = new ArrayList();


letters = "[";
//don't find a word, just instead randomize letters and setups, write the code using java thing and then preload it into here
    for(int i = 0; i<7;i++){
        letters += GetLetter();
    }
    letters+= "]+";
    Regex regex = new Regex(letters, RegexOptions.IgnoreCase);
        string text;

        do
        {
            text = reader.ReadLine();
            if(text != null){
            if( regex.IsMatch(text) && text.Length == first) {

                  array.Add(text);

            }  
        }


        } while (text != null);

        do
        {
            text = reader.ReadLine();
            if(text != null){
            if( regex.IsMatch(text) && text.Length == second) {
                for(int i = 0; i < array.Count; i++) {
                            string x = array[i].ToString();
                          if(x[third] == text[fourth]) {

                             amount++;
                             //System.out.println(array.get(i).charAt(2) + " - " + data2.charAt(4));
                            //System.out.println(array.get(i) + " - " + data2);

                          }
                      }
                  //array.Add(text);

            }  
            }


        } while (text != null);

        if(amount>=100){
            break;
        }

}
Vector2[] wds = {new Vector2(11, 1),new Vector2(11, first),new Vector2(8, second),new Vector2(second, 1 + third)};
words = wds;
string lets = letters.Substring(1,letters.Length-2);
choices = lets.ToCharArray();
*/