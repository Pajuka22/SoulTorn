using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    RectTransform rt;
    readonly float width = 287.7f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SlowDeath());
        rt = GetComponent<RectTransform>();
        UpdateHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbar();
    }

    void UpdateHealthbar()
    {
        if (Player.Instance.health < 0)
            Player.Instance.health = 0;

        rt.sizeDelta = new Vector2(width * Player.Instance.health / 100, 49.8f);
        /*if (Player.Instance.health < 50)
            GetComponent<Image>().color = new Color(1, Player.Instance.health / 50f, 0);
        else
            GetComponent<Image>().color = new Color(-Player.Instance.health / 50f + 2, 1, 0);*/
    }

    IEnumerator SlowDeath()
    {
        for (int i = 100; i >= 0; i--)
        {
            Player.Instance.health = i;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
