using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScore : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;
    [SerializeField] private Text _rank;
    
    public void SetText(string rank, string name, int score){ //khởi tạo cell với các giá trị
        _name.text = name;
        _score.text = score.ToString();
        _rank.text = rank;
    }
}
