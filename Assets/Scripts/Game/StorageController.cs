using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StorageController : MonoBehaviour
{
    [SerializeField] private GameObject highScoreTable;
    private int nicknameCounter = 0;

    private void Start()
    {
        nicknameCounter = PlayerPrefs.GetInt(Constants.Nickname + Constants.Count);
        SetHighScores();
    }

    public static void SaveFloat(string key, float data) {
        PlayerPrefs.SetFloat(key, data);
    }

    public static float GetFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }

        return 0.5f;
    }

    public static void SaveInteger(string key, int data)
    {
        PlayerPrefs.SetInt(key, data);
    }

    public static int GetInteger(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }

        return 0;
    }

    public static void SaveString(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
    }

    public static string GetString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }

        return "";
    }

    private void SetRecord(string nickname, int coin, int star) {
        GameObject record = Resources.Load("Record") as GameObject;
        record.GetComponentsInChildren<Text>()[0].text = nickname;
        record.GetComponentsInChildren<Text>()[1].text = coin+"";
        record.GetComponentsInChildren<Text>()[2].text = star + "";
        Instantiate(record, highScoreTable.transform);
    }

    public void SetHighScores() {
        HashSet<string> nicknames = GetAllNicknames();
        int coin, star;
        foreach (string nickname in nicknames) {
            coin = PlayerPrefs.GetInt(nickname + Constants.Coin);
            star = PlayerPrefs.GetInt(nickname + Constants.Star);
            if (coin != 0 && star != 0) {
                SetRecord(nickname, coin, star);
            }
        }
    }

    private HashSet<string> GetAllNicknames() {
        HashSet<string> nicknames = new HashSet<string>();

        for (int i = 0; i < PlayerPrefs.GetInt(Constants.Nickname + Constants.Count); i++) {
            nicknames.Add(PlayerPrefs.GetString(Constants.Nickname + i));
        }

        return nicknames;
    }
}
