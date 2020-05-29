using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] skills;
    //The skills that are available on this screen

    [SerializeField]
    public GameObject shadow;
    //The highlight that tells you which skill you've selected

    [SerializeField]
    public GameObject[] equippedSkills;

    [SerializeField]
    Text descriptionBox;

    [SerializeField]
    Text askToEquip;

    int currentSkill;
    GameObject[] globalEquippedSkills;

    [SerializeField]
    bool isPassiveScreen;
    //Will be true if this is the passive equip screen
    //Will be false if this is the active skill screen

    [SerializeField]
    GameObject nullSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        currentSkill = 0;
        askToEquip.enabled = false;
        if (isPassiveScreen)
        {
            globalEquippedSkills = GlobalControl.Instance.passives;
        }
        else
        {
            globalEquippedSkills = GlobalControl.Instance.passives;
        }
        for (int i = 0; i < globalEquippedSkills.Length; i++)
        {
            if (globalEquippedSkills[i] != null)
            {
                equippedSkills[i] = globalEquippedSkills[i];
            }
            else
                equippedSkills[i] = nullSkill;
        }
    }

    // Update is called once per frame
    void Update()
    {
        shadow.transform.position = skills[currentSkill].transform.position;
        //descriptionBox.transform.position = skills[currentSkill].transform.position;
        //The above makes the box move with what skill you're on, we can factor it in once we have an idea
        //of what the actual page will look like
        descriptionBox.text = skills[currentSkill].GetComponent<Skill>().descriptionOfSkill;
        

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentSkill == skills.Length - 1)
            {
                currentSkill = 0;
            }
            else
                currentSkill++;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            askToEquip.enabled = true;
            if (Input.GetKeyDown(KeyCode.B))
            {
                askToEquip.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                selectcurrentSkill();
            }
            
        }
    }

    void selectcurrentSkill()
    {
        bool done = false;
        for (int i = 0; i < globalEquippedSkills.Length; i++)
        {
            if (globalEquippedSkills[i] == false && !done)
            {
                globalEquippedSkills[i] = skills[currentSkill];
                equippedSkills[i] = skills[currentSkill];
                done = true;
            }
            if (done == false)
            {

            }
            
        }
    }
}
