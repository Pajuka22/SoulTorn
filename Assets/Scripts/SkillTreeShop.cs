using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SkillTreeShop : MonoBehaviour
{
    public static SkillTreeShop Instance;

    [SerializeField] public bool red;

    [SerializeField] public GameObject skillButtonPrefab;

    [SerializeField] public Text soulsText;
    [SerializeField] public Text dialogueText;
    [SerializeField] public GameObject scrollContent;
    [SerializeField] public GameObject buyPanel;
    [SerializeField] public GameObject messagePanel;

    public SkillButton selected;

    //The skill tree logic works like this, it's basically one class which holds an array of Skill classes
    //each skill tree consists of buttons, which the player clicks to indicate which ability he wants

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        buyPanel.SetActive(false);
        messagePanel.SetActive(false);

        if (red)
        {
            soulsText.text = "Red Souls: " + GlobalControl.Instance.redSouls.ToString();
            dialogueText.text = GlobalControl.nioFT[Random.Range(0, GlobalControl.nioFT.Count)];
            foreach (Skill skill in GlobalControl.Instance.redSkills)
                DisplaySkillButton(skill);
        }
        else
        {
            soulsText.text = "Blue Souls: " + GlobalControl.Instance.blueSouls.ToString();
            dialogueText.text = GlobalControl.virgilFT[Random.Range(0, GlobalControl.virgilFT.Count)];
            foreach (Skill skill in GlobalControl.Instance.blueSkills)
                DisplaySkillButton(skill);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            WithdrawSouls(1);
        if (Input.GetKeyDown(KeyCode.W))
            WithdrawSouls(-1);
    }

    public void DisplaySkillButton(Skill skill)
    {
        GameObject skillButton = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity);
        skillButton.GetComponent<SkillButton>().Initialize(skill);
        skillButton.transform.SetParent(scrollContent.transform, false);
    }

    public bool WithdrawSouls(int cost)
    {
        if (red)
        {
            if (cost > GlobalControl.Instance.redSouls)
                return false;
            GlobalControl.Instance.redSouls -= cost;
            soulsText.text = "Red Souls: " + GlobalControl.Instance.redSouls.ToString();
        }
        else
        {
            if (cost > GlobalControl.Instance.blueSouls)
                return false;
            GlobalControl.Instance.blueSouls -= cost;
            soulsText.text = "Blue Souls: " + GlobalControl.Instance.blueSouls.ToString();
        }
        return true;
    }

    public void BackButton()
    {
        LevelEnd.LoadMain();
    }

    public void BuyButton()
    {
        if (WithdrawSouls(selected.skill.cost))
        {
            GlobalControl.Instance.ActivateAbility(selected.skill.nameOfSkill);
            if(red)
                GlobalControl.Instance.redSkills.Remove(selected.skill);
            else
                GlobalControl.Instance.blueSkills.Remove(selected.skill);

            foreach (Skill branch in selected.skill.branches)
            {
                if (branch == null)
                {
                    print("branch not added to " + selected.skill.nameOfSkill);
                    continue;
                }
                if (red)
                    GlobalControl.Instance.redSkills.Add(branch);
                else
                    GlobalControl.Instance.blueSkills.Add(branch);
                DisplaySkillButton(branch);
            }
            Destroy(selected.gameObject);
            ActivateButtons();
        }
        else
        {
            messagePanel.SetActive(true);
            buyPanel.SetActive(false);
        }
    }

    public void CancelButton()
    {
        ActivateButtons();
    }

    void ActivateButtons()
    {
        buyPanel.SetActive(false);
        messagePanel.SetActive(false);
        selected = null;
        dialogueText.text = "";
        foreach (Button button in scrollContent.GetComponentsInChildren<Button>())
            button.enabled = true;
    }

    public void DeactivateButtons()
    {
        foreach (Button button in SkillTreeShop.Instance.scrollContent.GetComponentsInChildren<Button>())
            button.enabled = false;
    }
}
