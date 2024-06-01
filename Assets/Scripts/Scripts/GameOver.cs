using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{   
    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI leaderboardNameText;

    [SerializeField]
    private TextMeshProUGUI leaderboardScoreText;

    private int score = 0;

    private string leaderboardID = "22319";

    private int leaderboardTopCount = 10;

    public void StopGame(int score)
    {
      this.score = score;
      scoreText.text = score.ToString();
      GetLeaderboard();
    }

    public void SubmitScore()
    {
      StartCoroutine(SubmitScoreToLeaderboard());
    }

    private IEnumerator SubmitScoreToLeaderboard(){
      bool? nameSet = null;
      LootLockerSDKManager.SetPlayerName(inputField.text, (response) => {
        if (response.success){
          Debug.Log("Successfully set player's name");
          nameSet = true;
        }else{
          Debug.Log("Unable to set player's name");
          nameSet = false;
        }
      });
      yield return new WaitUntil(()=> nameSet.HasValue);
      if (!nameSet.Value) yield break;
      bool? scoreSubmitted = null;
      LootLockerSDKManager.SubmitScore(inputField.text, score, leaderboardID, (response) => {
         if (response.success){
          Debug.Log("Successfully submitted score to the leaderboard");
          scoreSubmitted = true;
         } else {
          Debug.Log("Unsuccessfully submitted score to the leaderboard");
          scoreSubmitted = false;
         }
      });
      yield return new WaitUntil(()=> scoreSubmitted.HasValue);
      if (!scoreSubmitted.Value) yield break;
      GetLeaderboard();
    }

    private void GetLeaderboard(){
      LootLockerSDKManager.GetScoreList(leaderboardID, leaderboardTopCount, (response) => {
        if (response.success){
          Debug.Log("Successfully retrived score from the leaderboard");
          string leaderboardName = "";
          string leaderboardScore = "";
          LootLockerLeaderboardMember[] members = response.items;
          if (members != null){
            for (int i = 0; i < members.Length; i++) {
              LootLockerPlayer player = members[i].player;
              if (members[i].player == null) continue;
              if (members[i].player.name != "") {
                leaderboardName += members[i].player.name + "\n";
              } else {
                leaderboardName += members[i].player.id + "\n";
              }
              leaderboardScore += members[i].score  + "\n";
          }
          }
          
          leaderboardNameText.SetText(leaderboardName);
          leaderboardScoreText.SetText(leaderboardScore);
        } else {
          Debug.Log("Unsuccessfully retrived score from the leaderboard");
        }
      });
    }
    public void AddXP(int score)
    {

    }
    public void ReloadScene(){
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
