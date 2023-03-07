using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public ItemScore itemScore;
    private List<ItemScore> itemScoreslist = new List<ItemScore>();//khởi tạo list chứa cell
    private List<Transform> hSEntrieslistTrans;

    public void  Clear(){ //xóa dữ liệu bảng khi tắt hoặc load lại bảng để k bị trùng đè dữ liệu
        for(int i =0; i < itemScoreslist.Count; i++){
           Destroy(itemScoreslist[i].gameObject);
        }
        itemScoreslist.Clear();
    }   
    

    private void Awake() {
        entryTemplate.gameObject.SetActive(true);//tạm thời ẩn bảng
        // list();
        
    }
    

    public void list(){
        
        
        // //khởi tạo danh sách thông tin người chơi
        // hSEntrieslist =  new List<HSEntry>(){
        //     new HSEntry{score = 10000, name  = "v1"},
        //     new HSEntry{score = 90000, name  = "v2"},
        //     new HSEntry{score = 130000, name  = "v3"},
        //     new HSEntry{score = 14000, name  = "v4"},
        //     new HSEntry{score = 15000, name  = "v5"},
        //     new HSEntry{score = 1000, name  = "v6"},
        //     new HSEntry{score = 200, name  = "v7"},
        //     new HSEntry{score = 1200, name  = "v8"},
        //     new HSEntry{score = 1400, name  = "v9"},
        //     new HSEntry{score = 1300, name  = "v10"},
        // };

        //lấy dữ liệu từ file json đã lưu ở PlayerPef
        string jsonString =  PlayerPrefs.GetString("highScoreTable");
        //chuyển đổi từ json sang string để trả về dữ liệu highscore của người chơi
        highscore highscore = JsonUtility.FromJson<highscore>(jsonString);

        




        //sắp xếp danh sách dựa theo điểm số khi load bảng
        if (highscore.HScoreEntriesList.Count > 5) //giới hạn 5 người được lên bảng
        {
            for (int h = highscore.HScoreEntriesList.Count; h>5; h--)
            {
                highscore.HScoreEntriesList.RemoveAt(5);//k hiển thị player từ vị trí số 5, tức là player thứ 6
            }
        
        }

        hSEntrieslistTrans = new List<Transform>(); //khởi tạo list lưu dữ liệu
        foreach (HSEntry hSEntry in highscore.HScoreEntriesList){ //đọc danh sách bằng hàm CreateHSEntryTrans

            CreateHSEntryTrans(hSEntry, entryContainer, hSEntrieslistTrans);
            //gọi hàm với các giá trị được đặt ra tương ứng, hSEntry=hSEntry, entryContainer = container, hSEntrieslistTrans = transformslist
            
        }

        
        
    }


    //hàm thêm giá trị (giá trị ở đây có để được đổi tên) vào bảng entryTemplate đặt trong container, khi đó sẽ tạo dựa theo giá trị score, name và sắp xếp theo list đề ra,
    //list được đánh vị trí dựa theo số mục được tạo
    private void CreateHSEntryTrans(HSEntry hSEntry, Transform container, List<Transform> transformslist){
            ItemScore entryTrans = Instantiate(itemScore, entryTemplate); //nhân bản cell của itemscore
            itemScoreslist.Add(entryTrans); //thêm cell vào list
            entryTrans.gameObject.SetActive(true); //hiển thị bảng
            
            //thiết lập thứ tự xếp hạng
            int score = hSEntry.score; //gán giá trị score
            string name = hSEntry.name;//gán giá trị name
            int index = itemScoreslist.IndexOf(entryTrans); //lấy index của list
            string rankString = (index +1).ToString(); //gán số thứ tự rankstring dựa theo index +1 vì base index là 0;
            switch(index){// thêm chú thích
                default:
                    rankString = rankString +" th"; break;
                case 0: rankString = rankString +" st"; break;
                case 1: rankString = rankString +" nd"; break;
                case 2: rankString = rankString  +" rd"; break;
            } 
            entryTrans.SetText(rankString, name, score); //tryuền giá trị
            Debug.Log("index: "+index);
            
    }



//dùng playerpefs để lưu highscore và jsonutility để chuyển đổi danh sách lưu
    public void AddHighScore(string name, int score){ 
        //tạo ojb lưu lại highscore
        HSEntry hSEntry = new HSEntry();
        hSEntry.name = name;
        hSEntry.score = score;
        
        //lấy dữ liệu từ file json đã lưu ở PlayerPef load ra
        string jsonString =  PlayerPrefs.GetString("highScoreTable");
        //chuyển đổi từ json sang string để trả về dữ liệu highscore của người chơi
        highscore highscore = JsonUtility.FromJson<highscore>(jsonString);
        //thêm dữ liệu mới vào highscore
        highscore.HScoreEntriesList.Add(hSEntry);
        //lưu lại và cập nhật dữ liệu trong file json
        if (highscore.HScoreEntriesList.Count > 5) //giới hạn 5 người được lên bảng
        {
            for (int h = highscore.HScoreEntriesList.Count; h>5; h--)
            {   //sắp xếp khi lưu
                for( int i = 0; i< highscore.HScoreEntriesList.Count; i++)
                { //kiểm tra trong danh sách
                    for(int j = i+1; j <highscore.HScoreEntriesList.Count;j++){ //ở mỗi giá trị tiếp theo
                        if(highscore.HScoreEntriesList[j].score >highscore.HScoreEntriesList[i].score){
                            //nếu giá trị sau lớn hơn giá trị trước
                            //thì đổi vị trị, dưới đây là hàm swap
                            HSEntry temp = highscore.HScoreEntriesList[i];
                            highscore.HScoreEntriesList[i] = highscore.HScoreEntriesList[j];
                            highscore.HScoreEntriesList[j] = temp;
                        }
                    }
                }
                
            }
        
        }
        string json = JsonUtility.ToJson(highscore); //chuyển về chuỗi(string) dữ liệu lưu trong file json //để dùng được thì phải mở truy cập của lớp HSEntry thì mới truy cập dc giá trị
        PlayerPrefs.SetString("highScoreTable", json); //cần phải có obj để chứa bảng dữ liệu
        Debug.Log("debug2" + json);
        PlayerPrefs.Save(); //lưu vào playerpref
    }


    [System.Serializable]
    private class highscore{
        public List<HSEntry> HScoreEntriesList; //tảo bảng chứa dữ liệu

    }
    //class dại diện cho giá trị nhập
    [System.Serializable] //mở truy cập lớp
    private class HSEntry{
        public int score;
        public string name;
    }    
}
