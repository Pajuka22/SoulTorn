using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    //Buttons (To hide)
    [SerializeField]
    Button Continue;
    [SerializeField]
    Button NewGame;

    //Text elements
    [SerializeField]
    Text title;

    //Panel Elements
    [SerializeField]
    GameObject FilePanel;
    [SerializeField]
    Button SaveFile1;
    [SerializeField]
    Button SaveFile2;
    [SerializeField]
    Button SaveFile3;

    //System variables
    bool pressedContinue;

    // Start is called before the first frame update
    void Start()
    {
        FilePanel.SetActive(false);
    }


    public void continueButton()
    {
        pressedContinue = true;
        FilePanel.SetActive(true);
        Continue.GetComponent<Image>().enabled = false;
        Continue.enabled = false;
        NewGame.GetComponent<Image>().enabled = false;
        NewGame.enabled = false;
        title.transform.localScale = new Vector3(0, 0, 0);


    }

    public void newGameButton()
    {
        pressedContinue = false;
        FilePanel.SetActive(true);
        Continue.GetComponent<Image>().enabled = false;
        Continue.enabled = false;
        NewGame.GetComponent<Image>().enabled = false;
        NewGame.enabled = false;
        title.transform.localScale = new Vector3(0, 0, 0);
    }

    public void backButton()
    {
        FilePanel.SetActive(false);
        Continue.GetComponent<Image>().enabled = true;
        Continue.enabled = true;
        NewGame.GetComponent<Image>().enabled = true;
        NewGame.enabled = true;
        title.transform.localScale = new Vector3(5, 5, 1);
    }


}
