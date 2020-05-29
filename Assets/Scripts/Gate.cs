using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] int nextScene; //  0: Main, 1: Virgil, 2: Neo, 3: Levels (maybe 4: tutorial?)

    private bool nextToPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && nextToPlayer)
        {
            switch (nextScene)
            {
                case 0:
                    GlobalControl.Instance.ResetSceneLists();
                    SceneManager.LoadScene(GlobalControl.MAIN_SCENE_NUM);
                    break;
                case 1:
                    SceneManager.LoadScene(GlobalControl.VSHOP_SCENE_NUM);
                    break;
                case 2:
                    SceneManager.LoadScene(GlobalControl.NSHOP_SCENE_NUM);
                    break;
                case 3:
                    LevelEnd.LoadLevel();
                    break;
                default:
                    print("ERROR: shouldn't be here (Update in Gate)");
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            nextToPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextToPlayer = false;
        }
    }
}
