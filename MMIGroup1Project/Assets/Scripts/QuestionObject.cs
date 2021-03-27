using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionObject : MonoBehaviour
{
    
    public TextMeshProUGUI textUI;

    public string question = "Question";
    //public int correctAnswer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textUI.text = question;
    }

    public void SetText(string text)
    {
        textUI.text = text;
    }
}
