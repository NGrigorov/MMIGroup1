using UnityEngine;

public class AnswerObject : MonoBehaviour
{
    private QuestionObject parentQuestion;
    private void Start()
    {
        parentQuestion = this.transform.parent.GetComponent<QuestionObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            parentQuestion.questionAnswer(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            parentQuestion.disableAnswers();
        }
    }
}
