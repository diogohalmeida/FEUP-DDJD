using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class MyList {
	public List<int> scores = new List<int>();
    public List<string> names = new List<string>();
}

public class LeaderboardController : MonoBehaviour
{

    [SerializeField]
    private Text nameInputField;

    [SerializeField]
    private GameObject leaderboardEntryPrefab;

    [SerializeField]
    private GameObject leaderboardEntriesHolder;

    private MyList leaderboardEntries = new MyList();

    private int score = 0;

    public int maxLeaderboardEntries;

    void Start()
    {
        LoadLeaderboard();
        Debug.Log("Leaderboard Loaded");
        PrintLeaderboard();
    }

    public void SaveScore()
    {
        string name = nameInputField.text;
        InsertScoreOnLeaderboard(score, name);
        SaveLeaderboard();
        Debug.Log("Leaderboard Saved");
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    private void InsertScoreOnLeaderboard(int score, string name)
    {
        if (leaderboardEntries.scores.Count == 0){
            leaderboardEntries.scores.Add(score);
            leaderboardEntries.names.Add(name);
            return;
        }

        int idx = -1;
        for (int i = 0; i < leaderboardEntries.scores.Count; i++){
            if (score > leaderboardEntries.scores[i]){
                idx = i;
                break;
            }
        }

        if (idx != -1){
            leaderboardEntries.scores.Insert(idx, score);
            leaderboardEntries.names.Insert(idx, name);
            if (leaderboardEntries.scores.Count > maxLeaderboardEntries){
                leaderboardEntries.scores.RemoveAt(maxLeaderboardEntries);
                leaderboardEntries.names.RemoveAt(maxLeaderboardEntries);
            }
        }

    }

    private void SaveLeaderboard()
    {
        string toWrite = JsonUtility.ToJson(leaderboardEntries);
        Debug.Log(toWrite);
        File.WriteAllText("Data/leaderboard.json", toWrite);
    }

    private void LoadLeaderboard()
    {
        string leaderboard = File.ReadAllText("Data/leaderboard.json");
        leaderboardEntries = JsonUtility.FromJson<MyList>(leaderboard);
    }

    private void PrintLeaderboard()
    {
        for (int i = 0; i < leaderboardEntries.names.Count; i++) {
            Debug.Log(leaderboardEntries.names[i] + " " + leaderboardEntries.scores[i].ToString());
        }
    }

    public void ShowLeaderboardEntries()
    {
        for (int i = 0; i < maxLeaderboardEntries; i++){
            string name, scoreStr;
            if (i >= leaderboardEntries.names.Count){
                name = "----------";
                scoreStr = "-----";
            } else {
                name = leaderboardEntries.names[i];
                scoreStr = leaderboardEntries.scores[i].ToString();
            }
            GameObject entry = Instantiate(leaderboardEntryPrefab, new Vector3(3, -10 - i * 60, 0), new Quaternion(0, 0, 0, 0));
            entry.transform.Find("Name").GetComponent<Text>().text = name;
            entry.transform.Find("Score").GetComponent<Text>().text = scoreStr;
            entry.transform.SetParent(leaderboardEntriesHolder.transform, false);
        }
        
    }

}
