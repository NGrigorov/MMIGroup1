using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerVolume : MonoBehaviour
{
    public GameObject answerVolumeLeft;
    public GameObject answerVolumeMid;
    public GameObject answerVolumeRight;

    private AudioController controller;

    private bool rightAnswer = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Main Camera").GetComponent<AudioController>();
    }

    public void SetCorrectAnswer()
    {
        rightAnswer = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (rightAnswer)
            {
                controller.IncrementScore();
                Debug.Log("Correctly answered!");
            }

            // Destroy all answer volumes for this question
            Destroy(answerVolumeLeft);
            Destroy(answerVolumeMid);
            Destroy(answerVolumeRight);
        }
    }
}
