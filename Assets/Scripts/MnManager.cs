using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MnManager : MonoBehaviour
{
     public GameObject scoreBoard;
     public GameObject MenuUI;
     public HighScoreTable highScoreTable;
     

     public void Play(){
        SceneManager.LoadScene("GamePlay");
   }

     public void Quit(){
        Application.Quit();
        Debug.Log("Close Game");
   }

     public void ShowHighScores(){
     scoreBoard.SetActive(true);
     MenuUI.SetActive(false);
     highScoreTable.list();
   }
     public void ButtonCloseHScore(){
        scoreBoard.SetActive(false);
        MenuUI.SetActive(true);
        highScoreTable.Clear();
    }
}
