using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text dialogueBox;
    StreamReader sr;
    //float timer;

    void Start()
    {
        if(GlobalControl.Instance.dialogue.Count == 0)
        {
            print("ERROR: not enough dialogues (Start in DialogueManager)");
            LevelEnd.LoadLevel();
        }
        else
        {
            sr = new StreamReader(GlobalControl.Instance.dialogue[0]);
            GlobalControl.Instance.dialogue.RemoveAt(0);
            dialogueBox.text = sr.ReadLine();
        }
    }

    void Update()
    {
        if (/*timer < 0 || */Input.GetMouseButtonDown(0))
        {
            string line = sr.ReadLine();
            if (line != null)
                dialogueBox.text = line;
            else
            {
                sr.Close();
                sr.Dispose();
            }
        }
        /*else
        {
            timer -= Time.deltaTime;
        }*/
    }
}
