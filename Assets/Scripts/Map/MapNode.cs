using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapNode : MonoBehaviour
{
    public List<GameObject> connectingNodes = new List<GameObject>();

    public List<MapNode> previousNode = new List<MapNode>();

    public bool canInteract = false;

    public bool finishedEncounter = false;

    private bool endOfEncounterSetup = false;

    public Camera myCamera;


    public BaseEncounter encounter;

    public bool isCameraMoveToken = false;
    public Transform newCameraPoint;

    [Header("End Map Stuff")]
    public bool isEndOfMap = false;
    public bool isEndOfGame = false; //only set this to true when the player is at the final node of the game
    public MapGenerator mapGenerator;
    public GameObject nextMap;

    public Sprite notInteractable;
    public Sprite interactable;
    public Sprite interactableHover;
    public Sprite used;

    public void Start()
    {
        myCamera = Camera.main;
        if(canInteract)
        {
            GetComponent<SpriteRenderer>().sprite = interactable;
        }
    }

    private void OnMouseDown()
    {

        if (GameManager.Instance.isGamePaused) return;

        GameManager.Instance.amountOfRations -= 1;
        GameManager.Instance.amountOfRations = Mathf.Clamp(GameManager.Instance.amountOfRations, 0 , 999);
        if (GameManager.Instance.amountOfRations <= 0)
        {
            GameManager.Instance.amountOfTimeWithoutRations += 1;
            if(GameManager.Instance.amountOfTimeWithoutRations >= 4)
            {
                //hunger check
                if(Random.Range(0, 100) <= 50)//if the random value is below or equal to 50 take damage
                {
                    GameManager.Instance.player.currentHealth -= 2;
                }
            }
        }

        //TelemetryLogger.Log(this, $"Selected Node Encounter", encounter.name);

        StartCoroutine(SmoothLerp(3));
    }

    private void OnMouseOver()
    {
        if(canInteract)
        {
            GetComponent<SpriteRenderer>().sprite = interactableHover;
        }
    }

    private void OnMouseExit()
    {
        if (canInteract)
        {
            GetComponent<SpriteRenderer>().sprite = interactable;
        }
    }

    private void Update()
    {
        if(finishedEncounter)
            GetComponent<SpriteRenderer>().sprite = used;

        GetComponent<BoxCollider2D>().enabled = canInteract;

        if (finishedEncounter && !endOfEncounterSetup)
        {
            //check if there are no connecting nodes
            if (isEndOfMap)
            {
                if(isEndOfGame)
                {
                    GameManager.Instance.EndGame();
                    return;
                }
                Destroy(mapGenerator.mapParent.gameObject);
                mapGenerator.GenerateMap(nextMap);
            }

            foreach (var node in connectingNodes)
            {
                node.GetComponent<MapNode>().canInteract = true;
                node.GetComponent<MapNode>().GetComponent<SpriteRenderer>().sprite = node.GetComponent<MapNode>().interactable;
            }
            canInteract = false;
            endOfEncounterSetup = true;
        }

        if(!canInteract && !finishedEncounter)
        {
            GetComponent<SpriteRenderer>().sprite = notInteractable;
        }
    }

    private IEnumerator SmoothLerp(float time)
    {
        canInteract = false;
        Vector3 startingPos = GameManager.Instance.playerMoveSprite.transform.position;
        Vector3 finalPos = this.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            if (new Vector3(GameManager.Instance.playerMoveSprite.transform.position.x, GameManager.Instance.playerMoveSprite.transform.position.y) 
                == new Vector3(finalPos.x, finalPos.y))//once the camera reaches desired position break while loop
                break;

            GameManager.Instance.playerMoveSprite.transform.position = Vector3.Lerp(new Vector3(startingPos.x, startingPos.y), new Vector3(finalPos.x, finalPos.y), (elapsedTime / time));
            elapsedTime += Time.deltaTime * GameManager.Instance.mapMoveSpeed;

            //Disable all other interactable nodes
            for (int i = 0; i < previousNode.Count; i++)
            {
                //previousNode[i].finishedEncounter = false;
                foreach (var node in previousNode[i].connectingNodes)
                {
                    node.GetComponent<MapNode>().canInteract = false;
                }
            }

            yield return null;
        }

        


        //once they reach the position run this code to start the encounter
        //note when we add more encounters run a bit of code to see what kind of encounter to run instead
        //if the encounter is an enemy run this line to start encounter
        GameManager.Instance.node = this;

        encounter.StartEncounter();
        if(isCameraMoveToken)
        {
            myCamera.transform.position = newCameraPoint.position;
        }
        yield return null;
    }
}
