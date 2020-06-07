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

    //Save file panel elements
    [SerializeField]
    GameObject SavePanel1;
    [SerializeField]
    GameObject SavePanel2;
    [SerializeField]
    GameObject SavePanel3;
    [SerializeField]
    Text saveText;
    [SerializeField]
    Text locationText;
    [SerializeField]
    Text learnedText;
    [SerializeField]
    Text newGameText;
    [SerializeField]
    Text saveText2;
    [SerializeField]
    Text locationText2;
    [SerializeField]
    Text learnedText2;
    [SerializeField]
    Text newGameText2;
    [SerializeField]
    Text saveText3;
    [SerializeField]
    Text locationText3;
    [SerializeField]
    Text learnedText3;
    [SerializeField]
    Text newGameText3;

    //System variables
    bool pressedContinue;
    bool[] existence = new bool[3];
    int[] locations = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        FilePanel.SetActive(false);

        for (int x = 0; x <= 2; x++)
        {
            existence[x] = SaveSystem.checkExistence(x);
            print(x + " " + existence[x]);
            locations[x] = 0;
            //Just to make sure no null exceptions are created^
            if (existence[x])
            {
                locations[x] = SaveSystem.LoadPlayer(x).levelAt;

            }

        }


        SavePanel1.SetActive(false);
        SavePanel2.SetActive(false);
        SavePanel3.SetActive(false);

        //This for loop is to set the LocationTexts
        for (int y = 0; y < 2; y++)
        {
            switch (y)
            {
                case 0:
                    switch (locations[y])
                    {
                        case 12:
                            locationText.text = "Path to the Grand Hall";
                            break;
                    }
                    break;
                case 1:
                    switch (locations[y])
                    {
                        case 12:
                            locationText2.text = "Path to the Grand Hall";
                            break;
                    }
                    break;
                case 2:
                    switch (locations[y])
                    {
                        case 12:
                            locationText3.text = "Path to the Grand Hall";
                            break;
                    }
                    break;
            }
        }
        //Locations will be upgraded as time goes on

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
        SavePanel1.SetActive(true);
        SavePanel2.SetActive(true);
        SavePanel3.SetActive(true);
        if (!existence[0])
        {

            saveText.transform.localScale = new Vector3(0, 0, 0);
            locationText.transform.localScale = new Vector3(0, 0, 0);
            learnedText.transform.localScale = new Vector3(0, 0, 0);
            newGameText.transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            saveText.transform.localScale = new Vector3(1, 1, 1);
            locationText.transform.localScale = new Vector3(1, 1, 1);
            learnedText.transform.localScale = new Vector3(1, 1, 1);
            newGameText.transform.localScale = new Vector3(0, 0, 0);

        }
        if (!existence[1])
        {
            saveText2.transform.localScale = new Vector3(0, 0, 0);
            locationText2.transform.localScale = new Vector3(0, 0, 0);
            learnedText2.transform.localScale = new Vector3(0, 0, 0);
            newGameText2.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            saveText2.transform.localScale = new Vector3(1, 1, 1);
            locationText2.transform.localScale = new Vector3(1, 1, 1);
            learnedText2.transform.localScale = new Vector3(1, 1, 1);
            newGameText2.transform.localScale = new Vector3(0, 0, 0);
        }
        if (!existence[2])
        {
            saveText3.transform.localScale = new Vector3(0, 0, 0);
            locationText3.transform.localScale = new Vector3(0, 0, 0);
            learnedText3.transform.localScale = new Vector3(0, 0, 0);
            newGameText3.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            saveText3.transform.localScale = new Vector3(1, 1, 1);
            locationText3.transform.localScale = new Vector3(1, 1, 1);
            learnedText3.transform.localScale = new Vector3(1, 1, 1);
            newGameText3.transform.localScale = new Vector3(0, 0, 0);
        }
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
        SavePanel1.SetActive(true);
        SavePanel2.SetActive(true);
        SavePanel3.SetActive(true);
        saveText.transform.localScale = new Vector3(0, 0, 0);
        locationText.transform.localScale = new Vector3(0, 0, 0);
        learnedText.transform.localScale = new Vector3(0, 0, 0);
        newGameText.transform.localScale = new Vector3(1, 1, 1);
        saveText2.transform.localScale = new Vector3(0, 0, 0);
        locationText2.transform.localScale = new Vector3(0, 0, 0);
        learnedText2.transform.localScale = new Vector3(0, 0, 0);
        newGameText2.transform.localScale = new Vector3(1, 1, 1);
        saveText3.transform.localScale = new Vector3(0, 0, 0);
        locationText3.transform.localScale = new Vector3(0, 0, 0);
        learnedText3.transform.localScale = new Vector3(0, 0, 0);
        newGameText3.transform.localScale = new Vector3(1, 1, 1);
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

    public void saveFile(int fileNum)
    {
        int sceneNum;
        bool[] existence = new bool[3];
        for (int x = 0; x < 2; x++)
        {
            existence[x] = SaveSystem.checkExistence(x);
        }
        if (pressedContinue)
        {

            if (existence[fileNum])
            {
                //int sceneNum;

                GlobalControl.Instance.OpenSaveFile(fileNum);
                sceneNum = SaveSystem.LoadPlayer(fileNum).levelAt;
                SceneManager.LoadScene(sceneNum);

            }
            else
            {
                Debug.Log(fileNum);
                SaveSystem.CreateFile(fileNum);
                sceneNum = SaveSystem.LoadPlayer(fileNum).levelAt;
                SceneManager.LoadScene(sceneNum);
            }
        }
        else
        {
            SaveSystem.CreateFile(fileNum);
            sceneNum = SaveSystem.LoadPlayer(fileNum).levelAt;
            SceneManager.LoadScene(sceneNum);

        }
    }


}
