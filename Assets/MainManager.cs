using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    // ????????????????????/????
    public string PlayerName;
    public string PlayTime;
    public bool Died;

    public TMP_InputField text_field;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
        Debug.Log($"MainManager Awake. PlayerName='{PlayerName}', PlayTime={PlayTime}, Died={Died}");
    }

    // ???????????? Start (????????? Scene ????????????????? Build Settings)
    public void StartGame()
    {
        // ??????????????????? PlayerName ??????????? ??????????? LoadScene (??????? SavePlayerName() ????????????? UI)
        Debug.Log("Starting game. PlayerName = " + PlayerName);
        text_field = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        SavePlayerName(text_field.text);
        SceneManager.LoadScene("Game");
    }

    // ???????? UI InputField (OnEndEdit) ????????????????????
    public void SavePlayerName(string name)
    {
        PlayerName = name;
        Debug.Log("PlayerName set to: " + PlayerName);
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public string PlayTime;
        public bool Died;
    }

    public void SaveDataToFile()
    {
        SaveData data = new SaveData
        {
            PlayerName = PlayerName,
            PlayTime = PlayTime,
            Died = Died
        };

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        try
        {
            File.WriteAllText(path, json);
            Debug.Log("Saved data to: " + path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save data: " + ex);
        }
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                PlayerName = data.PlayerName;
                PlayTime = data.PlayTime;
                Died = data.Died;

                Debug.Log("Loaded data: " + json);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to load data: " + ex);
            }
        }
        else
        {
            Debug.Log("No save file found at: " + path);
        }
    }

    private void OnApplicationQuit()
    {
        SaveDataToFile();
    }

    // --- (????????????? PlayTime ????) ---
    // ???????????????????????????????????????????:
    // private void Update()
    // {
    //     // ????????: ?????????????????????????????????????????
    //     PlayTime += Time.deltaTime;
    // }
}
