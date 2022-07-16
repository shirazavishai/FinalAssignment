using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public int attackCount = 1;
    public int defenseCount = 1;

    public event Action InitEvent; 
    public bool IsAttack = true;

    public Button AtatckButton;
    public Button DefenseButton;
    public Text AttackCount;
    public Text DefenseCount;

    public GameObject GameCnavas;

    public void Init()
    {
        if(InitEvent != null)
        {
            InitEvent.Invoke();
        }

        gameObject.SetActive(false);
        GameCnavas.SetActive(true);
    }

    public void AttackPluse()
    {
        attackCount = Mathf.Clamp(attackCount + 1, 1, 10);

        AttackCount.text = attackCount.ToString();
    }

    public void AttackMinus()
    {
        attackCount = Mathf.Clamp(attackCount - 1, 1, 10);

        AttackCount.text = attackCount.ToString();
    }

    public void DefensePluse()
    {
        defenseCount = Mathf.Clamp(defenseCount + 1, 1, 10);

        DefenseCount.text = defenseCount.ToString();
    }

    public void DefenseMinus()
    {
        defenseCount = Mathf.Clamp(defenseCount - 1, 1, 10);

        DefenseCount.text = defenseCount.ToString();
    }

    public void SetAttack()
    {
        AtatckButton.interactable = false;
        DefenseButton.interactable = true;

        IsAttack = true;
    }

    public void SetDefense()
    {
        AtatckButton.interactable = true;
        DefenseButton.interactable = false;

        IsAttack = false;
    }
}
