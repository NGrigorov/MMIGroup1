using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionObject : MonoBehaviour
{
    
    public TextMeshProUGUI textUI;
    public List<AnswerObject> answers;

    public string question = "Who dis new fone?";
    public int correctAnswer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textUI.text = question;
        for (int i = 1; i < 4; i++)
        {
            answers.Add(this.transform.GetChild(i).GetComponent<AnswerObject>());
        }
    }

    public void questionAnswer(AnswerObject caller)
    {
        if(answers.IndexOf(caller) == correctAnswer)
        {
            //Correct answer
            Debug.Log("Correct answer");
        }
        else
        {
            //incorrect answer
            Debug.Log("Incorrect answer");
        }
    }

    public void disableAnswers()
    {
        foreach (AnswerObject item in answers)
        {
            item.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
