using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject gamePauseUI;
    public GameObject PauseButton;
    public GameObject scoreBoard;
    public GameObject inputUI;
    public int pointStart = 10000;
    public Text pointUI;
    public Text yourScore;
    public Text Anou;
    
    public GameObject Continuebtn;
    private bool isEnd = false;
    private bool isWin = false;
    private bool isPause = false;
    public PlayerMov playerMov;
    public InputField playerNameInput;
    public Button btnAdd;
    public Button btnClose;
    private string getName;
    private int getScore;
    public HighScoreTable highScoreTable;
   


    private void Start() {
        
        //ẩn và khóa di chuyển con trỏ chuột khi bắt đầu game
        Cursor.visible= false;
        PointDown();
        
    }

    private void Update() {
        
        if(gamePauseUI.activeInHierarchy){ //kiểm tra trong thanh hier có bật ui này k
        //nếu có thì mở lại con trỏ chuột
        Cursor.visible= true;
        
        }
        if(Input.GetKeyDown(KeyCode.Escape)){ //kiểm tra nếu nhân esc khi game vẫn chạy thì dừng game, còn k ngược lại
            if(isPause == false){
                Pause();
            } else{
                Continue();
            }
        }
    }
    public void PointDown(){
        if(pointStart >= 0){
            pointUI.text = "Point Left: " + pointStart; //đặt hiển thị
            pointStart--;
            Invoke("PointDown", 0.01f); //giảm 1 điểm mỗi 0.1s
            getScore = pointStart;
        } else{
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void GameOver(){
        if(isEnd == false){ //khi đó kiểm tra lần đầu dc gọi
            isEnd = true; //sẽ đặt lại kiểm tra = đúng, khi đó game sẽ chỉ kết thúc 1 lần duy nh
            PauseButton.SetActive(false);
            gamePauseUI.SetActive(false);
            scoreBoard.SetActive(true);
            Debug.Log("gameOver");
            playerMov.enabled = false;
            Time.timeScale = 0f;
            Cursor.visible= true;
            Continuebtn.SetActive(false);
            pointStart = 0;
            getScore = pointStart;
            Anou.text = "Try Harder Next Time";
            yourScore.text = "Your Score is: " + getScore;
            playerNameInput.gameObject.SetActive(false);
            btnAdd.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(true);
            
            
            // Invoke("Restart", 3f); //trì hoãn bắt đầu lại
        }
       
    }
    public void Restart(){
        //load lại sceen đang hoạt động - sceen hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        
    }

    public void HomeMenu(){ // quay lại trang menu
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause(){ // tạm dừng trò chơi
        gamePauseUI.SetActive(true);
        PauseButton.SetActive(false);
        isPause = true;
        Time.timeScale = 0f;
        
        
    }
    public void Continue(){ //tiếp tục trò chơi
        gamePauseUI.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1f;
        isPause = false;
        
    }

    public void ButtonCloseHScore(){
        scoreBoard.SetActive(false);
        gamePauseUI.SetActive(true);
        highScoreTable.Clear();
    }

    public void Win(){
        if(isWin ==false){
        isWin = true;
        PauseButton.SetActive(false);
        gamePauseUI.SetActive(false);
        highScoreTable.list();
        scoreBoard.SetActive(true);
        Debug.Log("YouWin");
        playerMov.enabled = false;
        Time.timeScale = 0f;
        Cursor.visible= true;
        Continuebtn.SetActive(false);
        Anou.text = "You Have Achieved A HighScore \nPlease Enter Your Name";
        yourScore.text = "Your Score is: " + getScore;
        btnAdd.gameObject.SetActive(true);
        btnClose.gameObject.SetActive(false);
        }
    }
    public void BtnAdd(){
        inputUI.SetActive(false);
        getName = playerNameInput.text;
        highScoreTable.AddHighScore(getName,getScore);
        highScoreTable.Clear();
        highScoreTable.list();
        Debug.Log("score is: "+getScore +" name is: "+ getName);


    }
    public void CloseAnou(){
        inputUI.SetActive(false);
        
    }
}
