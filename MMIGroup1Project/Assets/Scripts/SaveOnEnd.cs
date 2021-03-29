using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveOnEnd : MonoBehaviour
{
    private AudioController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Main Camera").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WriteDataToFile()
    {
        int score = controller.GetScore();
        SongConfiguration songConfig = controller.GetSongConfiguration();
        List<string> choices = controller.GetChoices();

        // Create unique file name
        DateTime dt = DateTime.Now;
        string fileName = "Report" + dt.ToString("HH-mm-ss") + ".txt";
        if (File.Exists(fileName))
        {
            // Fallback for bugs
            fileName = "Report1.txt";
            Debug.Log("File exists already?");
            return;
        }

        // Open and write to file
        var sr = File.CreateText(fileName);

        // Song configuration
        sr.WriteLine("Song configuration:");
        for (int i = 0; i < 6; i++)
            sr.WriteLine(songConfig.songs[i, 0] + " " + songConfig.songs[i, 1] + " " + songConfig.songs[i, 2]);

        sr.WriteLine("\nPlayer choices:");
        for (int i = 0; i < choices.Count; i++)
            sr.WriteLine(choices[i]);

        sr.WriteLine("\nFinal score:");
        sr.WriteLine("Score: {0}", score);

        sr.Close();
        Debug.Log("Wrote to file");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WriteDataToFile();
            Destroy(this);
        }
    }
}
