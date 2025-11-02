using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

//game data class
[System.Serializable]
public class GameData
{
    public List<float> score;
}

//High score class
public class HighScore : MonoBehaviour
{
    GameData gameData = new GameData();
    public List<float> score = new List<float>();

    string path = Path.Combine(Application.dataPath, "Dot_Battle", "Assets");
    string filename = "savedata";

    public TMP_Text Time_1;
    public TMP_Text Time_2;
    public TMP_Text Time_3;
    public TMP_Text Time_4;
    public TMP_Text Time_5;

    private Animator myAnim;
    public bool animationTerminated;

    private Scene load;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.UnloadSceneAsync(1);
        myAnim = GetComponent<Animator>();
        animationTerminated = false;

        LoadData();
    }


    // Update is called once per frame
    void Update()
    {
        if (animationTerminated)
        {
            load = SceneManager.GetSceneByName("Menu");
            if (load.name == null)
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(3);
        }
    }

    public void ReturnToMenu()
    {
        myAnim.Play("FadeOut");
    }

    void LoadData()
    {
        //load the value from the file into a list/array
        Load();

        //add timing value to score array
        score.Add(1);

        //sort the list
        score.Reverse();

        //if score.lenght > 5, erase the last element
        if (score.Count > 5)
            score.RemoveAt(5);

        //save data into file
        Save();

        //write the list into the scene
        WriteInScene();
    }

    void Save()
    {
        gameData.score = score;
        string fullPath = Path.Combine(path, filename);
        string dataToStore = JsonUtility.ToJson(gameData, true);
        using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }


    void Load()
    {
        int count = 0;
        string fullPath = Path.Combine(path, filename);
        if (File.Exists(fullPath))
        {
            //load the serialized data from file
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            if (dataToLoad != "")
            {
                foreach (char c in dataToLoad)
                    if (c == '}' || c == '{')
                        count++;

                if (count % 2 != 0)
                    dataToLoad = dataToLoad.Remove(dataToLoad.Length - 1);

                //convert the serialized data into unity object
                GameData gamedata = JsonUtility.FromJson<GameData>(dataToLoad);

                score = gamedata.score;

            } 
        }
        else
            Debug.Log("The file don't exsist");
    }

    void WriteInScene()
    {
        if (score.Count > 0)
            Time_1.text = score[0].ToString();
        if (score.Count > 1)
            Time_2.text = score[1].ToString();
        if (score.Count > 2)
            Time_3.text = score[2].ToString();
        if (score.Count > 3)
            Time_4.text = score[3].ToString();
        if (score.Count > 4)
            Time_5.text = score[4].ToString();
    }
}
