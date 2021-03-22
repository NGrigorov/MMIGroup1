using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionObject : MonoBehaviour
{
    
    public TextMeshProUGUI textUI;

    public string question = "Who dis new fone?";
    public int correctAnswer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textUI.text = question;
    }
}
