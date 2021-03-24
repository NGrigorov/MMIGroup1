using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] happySongs = new AudioClip[5];
    public AudioClip[] sadSongs = new AudioClip[5];
    public AudioClip[] scarySongs = new AudioClip[5];

    private List<AudioSource[]> questionSources = new List<AudioSource[]>();

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

    // Start is called before the first frame update
    void Start()
    {
        List<AudioClip> happySongList = new List<AudioClip>(happySongs);
        List<AudioClip> sadSongList = new List<AudioClip>(sadSongs);
        List<AudioClip> scarySongList = new List<AudioClip>(scarySongs);

        // Randomly shuffle songs
        ShuffleList(happySongList);
        ShuffleList(sadSongList);
        ShuffleList(scarySongList);

        // Get audio sources
        for (int i = 0; i < 5; i++)
        {
            questionSources.Add(new AudioSource[3]);
        }

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

        // Assign songs to sources
        List<List<AudioClip>> songs = new List<List<AudioClip>>() { happySongList, sadSongList, scarySongList };
        
        for (int i = 0; i < 5; i++)
        {
            List<int> moodList = new List<int>() { 0, 1, 2 };
            ShuffleList(moodList);
            
            for (int mi = 0; mi < 3; mi++)
            {
                int mood = moodList[mi];
                Debug.Log(mood);
                questionSources[i][mi].clip = songs[mood][i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
