using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Sprite nioHead;
    [SerializeField] private Sprite virgilHead;

    [SerializeField] private Image speakerHead;
    [SerializeField] private Text speakerName;
    [SerializeField] private Text dialogue;

    StreamReader sr;
    bool done;
    //float timer;

    void Start()
    {
        done = false;
        if(GlobalControl.dialogue.Count == 0)
        {
            dialogue.text = "ERROR: not enough dialogue options";
            // LevelEnd.LoadLevel();
        }
        else
        {
            int rndIndex = Random.Range(0, GlobalControl.dialogue.Count);
            sr = new StreamReader(GlobalControl.dialogue[rndIndex]);
            GlobalControl.dialogue.RemoveAt(rndIndex);
            ShowNextLine();
        }
    }

    void Update()
    {
        if (/*timer < 0 || */Input.GetMouseButtonDown(0) && !done)
            ShowNextLine();
        /*else
        {
            timer -= Time.deltaTime;
        }*/
    }

    void ShowNextLine()
    {
        string line = sr.ReadLine();
        if (line == null)
        {
            sr.Close();
            sr.Dispose();
            done = true;
        }
        else
        {
            if (line[0] == 'N')
            {
                speakerHead.sprite = nioHead;
                speakerName.text = "Nio";
            }
            else
            {
                speakerHead.sprite = virgilHead;
                speakerName.text = "Virgil";
            }
            dialogue.text = line.Remove(0, 3);
            print(line.Remove(0, 3));
        }
    }
}
