using System;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    public Text Attack;
    public Text Defense;
    public CanvasManager setupCanvas;
    public SceneManager sceneManaer;
    private int startAttack;
    private int currentAttack;
    private int startDefense;
    private int currentDefense;

    public GameObject EndPopup;
    public GameObject MessagePopup; 
    public Text MessagePopupText;
    public Text EndPopupText;

    private void OnEnable()
    {
        startAttack = setupCanvas.attackCount;
        startDefense = setupCanvas.defenseCount;
        currentAttack = startAttack;
        currentDefense = startDefense;

        Attack.text = currentAttack + "/" + startAttack;
        Defense.text = currentDefense + "/" + startDefense;
    }

    public void Init()
    {
        foreach (var defense in sceneManaer.DefenseSoldiers)
        {
            defense.DeathEvent += ReduceDefense;
        }

        foreach (var attack in sceneManaer.AttakSoldiers)
        {
            attack.DeathEvent += ReduceAttack;
        }
    }

    private void ReduceDefense()
    {
        currentDefense--;

        Defense.text = currentDefense + "/" + startDefense;

        ShowMessagePopup("Defense player died!");
    }

    private void ReduceAttack()
    {
        currentAttack--;

        Attack.text = currentAttack + "/" + startAttack;

        ShowMessagePopup("Attack player died!");

        if(currentAttack == 0){
            ShowEndPopup("Defense team win!");
        }
    }

    public void ShowMessagePopup(string message, float timeToDisplay = 5f)
    {
        MessagePopup.gameObject.SetActive(true);
        MessagePopupText.text = message;

        Invoke(nameof(HidePopup), timeToDisplay);
    }

    private void HidePopup()
    {
        MessagePopup.gameObject.SetActive(false);
    }

    public void ShowEndPopup(string message)
    {
        EndPopup.SetActive(true);
        EndPopupText.text = message;
        Time.timeScale = 0;
    }

    public void AttackWin()
    {
        ShowEndPopup("Attack team win!");
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}