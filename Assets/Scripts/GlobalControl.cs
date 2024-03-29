using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public float speed;
    public int jumpProcs;
    public int atkDamage;
    public int atkLag;
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float dodgeLength;
    public float accelerationRate;
    public float decelerationRate;
    public float InvincibleFrames;
    public GameObject[] passives;
    public GameObject[] actives;
    public static float GravityScale;
    //Variables that we want to preserve between scenes

    public bool canDodge;
    public bool canDodgeRoll;
    public bool canAccelerate;
    //The abilities

    private FileInfo[] easyFileInfo; //array of all scenes with easy difficulty
    public static List<string> easyScenes; //list of unvisited scenes with easy difficulty
    private FileInfo[] medFileInfo; //etc.
    public static List<string> medScenes;
    private FileInfo[] hardFileInfo;
    public static List<string> hardScenes;

    private FileInfo[] dialogueFileInfo;
    public static List<string> dialogue;
    public static List<string> nioFT;
    public static List<string> virgilFT;

    public readonly static int MAIN_SCENE_NUM = 0;
    public readonly static int VSHOP_SCENE_NUM = 1;
    public readonly static int NSHOP_SCENE_NUM = 2;
    public readonly static int REST_SCENE_NUM = 3;
    public readonly static int BOSS_SCENE_NUM = 4;

    public int difficulty; //0: easy, 1: medium, 2: difficult
    public int level; //how many levels the player has completed so far
    public int levelAt;

    public int blueSouls, redSouls = 0; //total num of blue & red souls
    public List<Skill> blueSkills, redSkills; //this might become a list or dictionary at some point

    //save file variable
    public int saveFile;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        speed = 4f;
        jumpProcs = 1;
        atkDamage = 10;
        atkLag = 20;
        maxHealth = 100;
        health = 100;
        GravityScale = 3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo easyDirInfo = new DirectoryInfo("Assets/Scenes/Easy");
        DirectoryInfo medDirInfo = new DirectoryInfo("Assets/Scenes/Medium");
        DirectoryInfo hardDirInfo = new DirectoryInfo("Assets/Scenes/Hard");
        DirectoryInfo dialogueDirInfo = new DirectoryInfo("Assets/Dialogue/Quiet Area");
        easyFileInfo = easyDirInfo.GetFiles("*.unity");
        medFileInfo = medDirInfo.GetFiles("*.unity");
        hardFileInfo = hardDirInfo.GetFiles("*.unity");
        dialogueFileInfo = dialogueDirInfo.GetFiles("*.txt");

        nioFT = new List<string>();
        StreamReader sr = new StreamReader("Assets/Dialogue/Flavor Text/Nio.txt");
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine())
            nioFT.Add(line);
        sr.Close();
        sr.Dispose();

        virgilFT = new List<string>();
        sr = new StreamReader("Assets/Dialogue/Flavor Text/Virgil.txt");
        for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
            virgilFT.Add(line);
        sr.Close();
        sr.Dispose();

        ResetSceneLists();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetSceneLists() //call this after the player dies
    {
        difficulty = 0;
        level = 0;

        easyScenes = new List<string>();
        foreach (FileInfo fileInfo in easyFileInfo)
            easyScenes.Add(fileInfo.Name);
        medScenes = new List<string>();
        foreach (FileInfo fileInfo in medFileInfo)
            medScenes.Add(fileInfo.Name);
        hardScenes = new List<string>();
        foreach (FileInfo fileInfo in hardFileInfo)
            hardScenes.Add(fileInfo.Name);
        dialogue = new List<string>();
        foreach (FileInfo fileInfo in dialogueFileInfo)
            dialogue.Add("Assets/Dialogue/Quiet Area/" + fileInfo.Name);
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(2); //for now
        health = maxHealth;
        LevelEnd.LoadMain();
    }

    public void ActivateAbility(string nameOfSkill)
    {
        print("activate");
        //Invoke
    }

    //Save file stuff

    public void OpenSaveFile(int fileNum)
    {
        SaveFile saved = SaveSystem.LoadPlayer(fileNum);
        speed = saved.speed;
        jumpProcs = saved.jumpProcs;
        atkDamage = saved.atkDamage;
        maxHealth = saved.maxHealth;
        health = saved.health;
        dodgeLength = saved.dodgeLength;
        maxSpeed = saved.maxSpeed;
        accelerationRate = saved.accelerationRate;
        decelerationRate = saved.decelerationRate;
        InvincibleFrames = saved.InvincibleFrames;
        /*    for (int x = 0; x < saved.passives.Length; x++)
            {
                passives[x] = saved.passives[x];
            }
            for (int y = 0; y < saved.actives.Length; y++)
            {
                actives[y] = saved.actives[y];
            } */

        canDodge = saved.canDodge;
        canDodgeRoll = saved.canDodgeRoll;
        canAccelerate = saved.canAccelerate;
        blueSouls = saved.blueSouls;
        redSouls = saved.redSouls;
        level = saved.level;

        difficulty = saved.difficulty;
        /*  blueSkillName = new string[GlobalControl.Instance.blueSkills.Capacity];
          redSkillName = new string[GlobalControl.Instance.redSkills.Capacity];
          for (int x = 0; x < GlobalControl.Instance.blueSkills.Capacity; x++)
          {
              blueSkills[x] = GlobalControl.Instance.blueSkills[x];
              blueSkillName[x] = GlobalControl.Instance.blueSkills[x].nameOfSkill;

          }
          for (int x = 0; x < GlobalControl.Instance.redSkills.Capacity; x++)
          {
              redSkills[x] = GlobalControl.Instance.redSkills[x];
              blueSkillName[x] = GlobalControl.Instance.blueSkills[x].nameOfSkill;
          }*/

        saveFile = saved.fileNum;
    }
}
