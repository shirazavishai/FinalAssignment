using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneManager : MonoBehaviour
{
    public Camera PreviewCamera;
    public CanvasManager ChooseCnavas;
    public GameObject AttackPrefab;
    public GameObject DefensePrefab;
    public Transform Player;
    public Transform EndPosition;
    public Transform BaseDEefensePosition;
    public Transform BaseAttckPosition;
    public Transform[] DefenseLocations;
    public Transform[] AttackLocations;
    public LayerMask PlayerMask;
    public GameCanvasController GCC;
    public List<AtackAI> AttakSoldiers = new List<AtackAI>();
    public List<DefenseSoldier> DefenseSoldiers = new List<DefenseSoldier>();
     
    public static bool isBuildingDefense = false;
    public static int defenseCount = 0;
    public static bool IsAttack;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(11, 12);
        Time.timeScale = 0;

        ChooseCnavas.InitEvent += OnInit;

    }

    private void Update()
    {
        if (isBuildingDefense && Input.GetKeyDown(KeyCode.Space) && defenseCount > 0)
        {
            defenseCount--;
            var defenseSoldier = Instantiate(DefensePrefab, Player.position, Quaternion.identity).GetComponent<DefenseSoldier>();
            DefenseSoldiers.Add(defenseSoldier);
        }
    }

    private void OnInit()
    {
        Player.transform.GetComponentInChildren<Camera>().enabled = true;
        PreviewCamera.enabled = false;
        IsAttack = ChooseCnavas.IsAttack;

        if (ChooseCnavas.IsAttack)
        {
            Player.gameObject.tag = "Attack";
            Player.position = BaseAttckPosition.position;
            InitAttackWhenAttack(ChooseCnavas.attackCount);
            InitDefenseWhenAttack(ChooseCnavas.defenseCount);
            GCC.Init();
        }
        else
        {
            Player.gameObject.tag = "Defense";
            Player.position = BaseDEefensePosition.position;
            InitDefenseWhenDefense(ChooseCnavas.defenseCount);
            StartCoroutine(InitAttackWhenDefense(ChooseCnavas.attackCount));
        }

        Time.timeScale = 1;
    }

    private void InitAttackWhenAttack(int attackCount)
    {
        for (int i = 0; i < attackCount; i++)
        {
            var attakAI = Instantiate(AttackPrefab, AttackLocations[i].position, Quaternion.identity).GetComponent<AtackAI>();
            attakAI._leader = Player;
            attakAI._isStaticLeader = false;
            AttakSoldiers.Add(attakAI);
        }
    }

    private void InitDefenseWhenAttack(int dfenseCount)
    {
        for (int i = 0; i < dfenseCount; i++)
        {
            var defenseSoldier = Instantiate(DefensePrefab, DefenseLocations[i].position, Quaternion.identity).GetComponent<DefenseSoldier>();
            defenseSoldier.whatToAttack += PlayerMask;
            DefenseSoldiers.Add(defenseSoldier);
        }
    }

    private void InitDefenseWhenDefense(int dfenseCount)
    {
        Time.timeScale = 1;

        isBuildingDefense = true;
        defenseCount = dfenseCount;
    }

    private IEnumerator InitAttackWhenDefense(int attackCount)
    {
        GCC.ShowMessagePopup("You have 30 seconds to setup your soldiers their places!\n" +
            " To do so - navigate int the Arena and press SPACE in the wished location",
            10);

        yield return new WaitForSeconds(30);

        GCC.ShowMessagePopup("Start!!!", 3);
        GCC.Init();
        isBuildingDefense = false;

        for (int i = 0; i < attackCount; i++)
        {
            var attakAI = Instantiate(AttackPrefab, BaseAttckPosition.position, Quaternion.identity).GetComponent<AtackAI>();
            attakAI._leader = EndPosition.transform;
            attakAI._isStaticLeader = true;
            attakAI.whatToAttack += PlayerMask;
            PickKey.playerHoldsKey = true;

            AttakSoldiers.Add(attakAI);
        }

        yield break;
    }
}
