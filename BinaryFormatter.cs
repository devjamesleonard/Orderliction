using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class BinaryFormatt
{

    public static void saveVolumeData(MainMenuManager manager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Volume.sav", FileMode.Create);
        VolumeData Data = new VolumeData(manager);
        bf.Serialize(stream, Data);
        stream.Close();
    }
    public static bool[] loadVolumeData()
    {

        if (File.Exists(Application.persistentDataPath + "/Volume.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Volume.sav", FileMode.Open);
            VolumeData data = bf.Deserialize(stream) as VolumeData;
            stream.Close();

            return data.Vol;
        }
        else
        {
            bool[] Vol = { true, true };
            return Vol;
        }
    }
    public static void saveBestScoreData(GamestateHandler Game)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/BestScore.sav", FileMode.Create);
        bestScoreData Data = new bestScoreData(Game);
        bf.Serialize(stream, Data);
        stream.Close();
    }
    public static int loadBestScoreData()
    {

        if (File.Exists(Application.persistentDataPath + "/BestScore.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/BestScore.sav", FileMode.Open);
            bestScoreData data = bf.Deserialize(stream) as bestScoreData;
            stream.Close();

            return data.bestScore;
        }
        else
        {
            return 0;
        }
    }
    [Serializable]
    public class VolumeData
    {
        public bool[] Vol = new bool[2];
        public VolumeData(MainMenuManager Volume)
        {
            Vol[0] = Volume.BVolOn;
            Vol[1] = Volume.SVolOn;
        }

    }

    [Serializable]
    public class bestScoreData
    {
        public int bestScore;
        public bestScoreData(GamestateHandler Game)
        {
            bestScore = Game.score;


        }

    }



}
