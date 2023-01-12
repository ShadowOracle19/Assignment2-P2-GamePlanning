using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region GameManager instance
    public static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }
    #endregion


    [Header("Map")]
    public GameObject playerMoveSprite;//this sprite is only shown on the map to move to each node
    public GameObject map;
    public MapNode node;

    [Header("Dialogue")]
    public Dialogue dialogueManager;
    public GameObject dialogueUI;

    [Header("Combat")]
    public TurnBasedManager combatManager;
    public GameObject combatUI;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCombatEncounter(CombatEncounter encounter)
    {
        combatManager.StartEncounter(encounter.encounteredEnemy, node);

        combatUI.SetActive(true);
        map.SetActive(false);
    }
    public void StartDialogueEncounter(DialogueEncounter encounter)
    {
        dialogueManager.currentConversation = encounter.desiredConversation;
        dialogueManager.currentNode = node;

        dialogueUI.SetActive(true);
        map.SetActive(false);
    }

}
