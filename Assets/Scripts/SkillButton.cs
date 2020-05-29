using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Skill skill;

    [SerializeField] Image coverImage;
    [SerializeField] Text nameText;
    [SerializeField] Text costText;

    public void Initialize(Skill s)
    {
        skill = s;

        coverImage.sprite = skill.cover;
        nameText.text = skill.nameOfSkill;
        costText.text = "COST: " + skill.cost.ToString();
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void Clicked()
    {
        SkillTreeShop.Instance.selected = this;
        SkillTreeShop.Instance.dialogueText.text = "Description: (should we put cost here?)\n" + skill.descriptionOfSkill;
        SkillTreeShop.Instance.buyPanel.SetActive(true);
        SkillTreeShop.Instance.DeactivateButtons();
    }
}
