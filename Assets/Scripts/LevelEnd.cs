using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    readonly static int LEVELS_PER_DIFF = 5;
    readonly static int LEVELS_PER_QR = 1; //I forgot what this was supposed to be (oops) so i put 1

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == GlobalControl.REST_SCENE_NUM)
                LoadLevel();
            else
            {
                Player.Instance.SavePlayer();

                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach (Enemy e in enemies)
                {
                    e.DeathByRunning();
                }

                GameObject panel = FindObjectOfType<Canvas>().transform.Find("Complete Panel").gameObject;

                GlobalControl.Instance.blueSouls += enemies.Length;
                panel.transform.Find("Blue Souls").Find("Blue Number").GetComponent<Text>().text = enemies.Length.ToString();
                GlobalControl.Instance.redSouls += Player.Instance.enemiesKilled;
                panel.transform.Find("Red Souls").Find("Red Number").GetComponent<Text>().text = Player.Instance.enemiesKilled.ToString();

                panel.GetComponentInChildren<Button>().onClick.AddListener(LoadNextScene);
                panel.SetActive(true);
            }
        }
    }

    public static void LoadNextScene()
    {
        GlobalControl.Instance.level++;
        if (GlobalControl.Instance.level == LEVELS_PER_DIFF)
        {
            GlobalControl.Instance.level = 0;
            GlobalControl.Instance.difficulty += 1;
            GlobalControl.Instance.health += 1;
            if (GlobalControl.Instance.difficulty == 4)
            {
                //Load empty rest area
            }
            else
                LoadRestStop();
        }
        else if (GlobalControl.Instance.level % LEVELS_PER_QR == 0)
            LoadRestStop();
        else
            LoadLevel();
    }

    public static void LoadLevel()
    {
        switch (GlobalControl.Instance.difficulty)
        {
            case 0:
                LoadEasyLevel();
                break;
            case 1:
                LoadMediumLevel();
                break;
            case 2:
                LoadHardLevel();
                break;
            default:
                LoadFinalLevel();
                break;
        }
    }

    static void LoadEasyLevel()
    {
        int rndIndex = Random.Range(0, GlobalControl.easyScenes.Count);
        string sceneName = GlobalControl.easyScenes[rndIndex];
        GlobalControl.easyScenes.RemoveAt(rndIndex);
        SceneManager.LoadScene("Assets/Scenes/Easy/" + sceneName);
    }

    static void LoadMediumLevel()
    {
        int rndIndex = Random.Range(0, GlobalControl.medScenes.Count);
        string sceneName = GlobalControl.medScenes[rndIndex];
        GlobalControl.medScenes.RemoveAt(rndIndex);
        SceneManager.LoadScene("Assets/Scenes/Medium/" + sceneName);
    }

    static void LoadHardLevel()
    {
        int rndIndex = Random.Range(0, GlobalControl.hardScenes.Count);
        string sceneName = GlobalControl.hardScenes[rndIndex];
        GlobalControl.hardScenes.RemoveAt(rndIndex);
        SceneManager.LoadScene("Assets/Scenes/Hard/" + sceneName);
    }

    public static void LoadMain()
    {
        SceneManager.LoadScene(GlobalControl.MAIN_SCENE_NUM);
    }

    static void LoadRestStop()
    {
        SceneManager.LoadScene(GlobalControl.REST_SCENE_NUM);
    }

    static void LoadFinalLevel()
    {
        SceneManager.LoadScene(GlobalControl.BOSS_SCENE_NUM);
    }
}
