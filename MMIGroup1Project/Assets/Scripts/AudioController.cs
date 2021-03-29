using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongConfiguration
{
    public string[,] songs = new string[6, 3];
}

public class AudioController : MonoBehaviour
{
    private const int NUM_QUESTIONS = 6;

    private int score = 0;

    public AudioClip[] happySongs = new AudioClip[NUM_QUESTIONS];
    public AudioClip[] sadSongs = new AudioClip[NUM_QUESTIONS];
    public AudioClip[] scarySongs = new AudioClip[NUM_QUESTIONS];

    private QuestionObject scoreSign;

    private List<AudioSource[]> questionSources = new List<AudioSource[]>();
    private List<AnswerVolume[]> answerVolumes = new List<AnswerVolume[]>();

    private int[] correctAnswers = new int[6] { 0, 1, 2, 0, 1, 2 };

    private SongConfiguration songConfiguration = new SongConfiguration();

    private List<string> moods = new List<string>() { "Happy", "Sad", "Scary", "Happy", "Sad", "Scary" };
    private List<string> choices = new List<string>();

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public SongConfiguration GetSongConfiguration()
    {
        return songConfiguration;
    }

    public List<string> GetChoices()
    {
        return choices;
    }

    public int GetScore()
    {
        return score;
    }

    public void LogChoice(string mood)
    {
        choices.Add(mood);

        string text = "Thank you for your time and participation!\n";
        //text += "Mood | Choice\n";
        int choiceCount = Mathf.Min(moods.Count, choices.Count);
        Debug.Log(choiceCount);
        for (int i = 0; i < choiceCount; i++)
        {
            text += "Q" + (i+1).ToString() + " Mood: " + moods[i] + "  |  Your choice: " + choices[i] + "\n";
        }
        text += "Correct answers: " + score + "/6";

        scoreSign.SetText(text);
    }

    public void IncrementScore()
    {
        score++;
    }

    private string IndexToMood(int index)
    {
        switch(index)
        {
            case 0: return "Happy";
            case 1: return "Sad";
            case 2: return "Scary";
            default: return "Unknown";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreSign = GameObject.Find("ThankYouSign").GetComponent<QuestionObject>();

        List<AudioClip> happySongList = new List<AudioClip>(happySongs);
        List<AudioClip> sadSongList = new List<AudioClip>(sadSongs);
        List<AudioClip> scarySongList = new List<AudioClip>(scarySongs);

        // Randomly shuffle songs
        ShuffleList(happySongList);
        ShuffleList(sadSongList);
        ShuffleList(scarySongList);

        // Get audio sources
        for (int i = 0; i < NUM_QUESTIONS; i++)
        {
            questionSources.Add(new AudioSource[3]);
            answerVolumes.Add(new AnswerVolume[3]);
        }

        {
            questionSources[0][0] = GameObject.Find("Q1Left").GetComponent<AudioSource>();
            questionSources[0][1] = GameObject.Find("Q1Mid").GetComponent<AudioSource>();
            questionSources[0][2] = GameObject.Find("Q1Right").GetComponent<AudioSource>();

            questionSources[1][0] = GameObject.Find("Q2Left").GetComponent<AudioSource>();
            questionSources[1][1] = GameObject.Find("Q2Mid").GetComponent<AudioSource>();
            questionSources[1][2] = GameObject.Find("Q2Right").GetComponent<AudioSource>();

            questionSources[2][0] = GameObject.Find("Q3Left").GetComponent<AudioSource>();
            questionSources[2][1] = GameObject.Find("Q3Mid").GetComponent<AudioSource>();
            questionSources[2][2] = GameObject.Find("Q3Right").GetComponent<AudioSource>();

            questionSources[3][0] = GameObject.Find("Q4Left").GetComponent<AudioSource>();
            questionSources[3][1] = GameObject.Find("Q4Mid").GetComponent<AudioSource>();
            questionSources[3][2] = GameObject.Find("Q4Right").GetComponent<AudioSource>();

            questionSources[4][0] = GameObject.Find("Q5Left").GetComponent<AudioSource>();
            questionSources[4][1] = GameObject.Find("Q5Mid").GetComponent<AudioSource>();
            questionSources[4][2] = GameObject.Find("Q5Right").GetComponent<AudioSource>();

            questionSources[5][0] = GameObject.Find("Q6Left").GetComponent<AudioSource>();
            questionSources[5][1] = GameObject.Find("Q6Mid").GetComponent<AudioSource>();
            questionSources[5][2] = GameObject.Find("Q6Right").GetComponent<AudioSource>();
        }

        {
            answerVolumes[0][0] = GameObject.Find("Q1AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[0][1] = GameObject.Find("Q1AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[0][2] = GameObject.Find("Q1AnswerR").GetComponent<AnswerVolume>();

            answerVolumes[1][0] = GameObject.Find("Q2AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[1][1] = GameObject.Find("Q2AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[1][2] = GameObject.Find("Q2AnswerR").GetComponent<AnswerVolume>();

            answerVolumes[2][0] = GameObject.Find("Q3AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[2][1] = GameObject.Find("Q3AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[2][2] = GameObject.Find("Q3AnswerR").GetComponent<AnswerVolume>();

            answerVolumes[3][0] = GameObject.Find("Q4AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[3][1] = GameObject.Find("Q4AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[3][2] = GameObject.Find("Q4AnswerR").GetComponent<AnswerVolume>();

            answerVolumes[4][0] = GameObject.Find("Q5AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[4][1] = GameObject.Find("Q5AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[4][2] = GameObject.Find("Q5AnswerR").GetComponent<AnswerVolume>();

            answerVolumes[5][0] = GameObject.Find("Q6AnswerL").GetComponent<AnswerVolume>();
            answerVolumes[5][1] = GameObject.Find("Q6AnswerM").GetComponent<AnswerVolume>();
            answerVolumes[5][2] = GameObject.Find("Q6AnswerR").GetComponent<AnswerVolume>();
        }

        // Assign songs to sources
        List<List<AudioClip>> songs = new List<List<AudioClip>>() { happySongList, sadSongList, scarySongList };
        
        for (int i = 0; i < NUM_QUESTIONS; i++)
        {
            // Randomly select a mood for each path
            List<int> moodList = new List<int>() { 0, 1, 2 };
            ShuffleList(moodList);

            int correctAnswer = correctAnswers[i];

            // Assign songs to paths and right answers to answer volumes
            for (int mi = 0; mi < 3; mi++)
            {
                int mood = moodList[mi];
                
                questionSources[i][mi].clip = songs[mood][i];
                questionSources[i][mi].Play();
                questionSources[i][mi].volume = 0;

                songConfiguration.songs[i, mi] = songs[mood][i].name;
                
                answerVolumes[i][mi].SetMood(IndexToMood(mood));

                if (mood == correctAnswer)
                    answerVolumes[i][mi].SetCorrectAnswer();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
