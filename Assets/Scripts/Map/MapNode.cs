using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapNode : MonoBehaviour
{
    public List<GameObject> connectingNodes = new List<GameObject>();

    public List<MapNode> previousNode = new List<MapNode>();

    public bool canInteract = false;

    public bool finishedEncounter = false;

    public Camera myCamera;

    public GameObject selectable;
    public GameObject selected;

    public BaseEncounter encounter;

    public bool isCameraMoveToken = false;
    public Transform newCameraPoint;

    [Header("End Map Stuff")]
    public bool isEndOfMap = false;
    public bool isEndOfGame = false; //only set this to true when the player is at the final node of the game
    public MapGenerator mapGenerator;
    public GameObject nextMap;

    public void Start()
    {
        myCamera = Camera.main;
    }

    private void OnMouseDown()
    {
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

        TelemetryLogger.Log(this, $"Selected Node Encounter {encounter.name}");

        StartCoroutine(SmoothLerp(3));
    }

    private void Update()
    {
        GetComponent<BoxCollider2D>().enabled = canInteract;

        if (finishedEncounter)
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
            }
            canInteract = false;

        }

        if(canInteract)
        {
            selectable.SetActive(true);
        }
        else
        {
            selectable.SetActive(false);
        }
    }

    private IEnumerator SmoothLerp(float time)
    {
        selected.SetActive(true);
        Vector3 startingPos = GameManager.Instance.playerMoveSprite.transform.position;
        Vector3 finalPos = this.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            if (new Vector3(GameManager.Instance.playerMoveSprite.transform.position.x, GameManager.Instance.playerMoveSprite.transform.position.y) 
                == new Vector3(finalPos.x, finalPos.y))//once the camera reaches desired position break while loop
                break;
            //myCamera.transform.position = Vector3.Lerp(startingPos, new Vector3(finalPos.x, finalPos.y, -10), (elapsedTime / time));

            //the player icon moving to the next map node
            //GameManager.Instance.playerMoveSprite.transform.position = 
            //    Vector3.Lerp(new Vector3(GameManager.Instance.playerMoveSprite.transform.position.x, 0.5f, GameManager.Instance.playerMoveSprite.transform.position.z), 
            //    new Vector3(finalPos.x, 0.5f,finalPos.z), (elapsedTime / time));
            GameManager.Instance.playerMoveSprite.transform.position = Vector3.Lerp(new Vector3(startingPos.x, startingPos.y), new Vector3(finalPos.x, finalPos.y), (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            //Disable all other interactable nodes
            for (int i = 0; i < previousNode.Count; i++)
            {
                previousNode[i].finishedEncounter = false;
                foreach (var node in previousNode[i].connectingNodes)
                {
                    node.GetComponent<MapNode>().canInteract = false;
                    node.GetComponent<MapNode>().selectable.SetActive(false);
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
