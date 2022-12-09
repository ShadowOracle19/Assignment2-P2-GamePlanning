using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public List<GameObject> connectingNodes = new List<GameObject>();

    public List<MapNode> previousNode = new List<MapNode>();

    public GameObject combat;
    public GameObject map;

    public EnemyScriptable encounteredEnemy;

    public bool canInteract = false;

    public bool finishedEncounter = false;

    public Camera myCamera;

    public GameObject selectable;
    public GameObject selected;

    public void Start()
    {
        myCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        StartCoroutine(SmoothLerp(3));
    }

    private void Update()
    {
        GetComponent<BoxCollider2D>().enabled = canInteract;

        if (finishedEncounter)
        {
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
        Vector3 startingPos = myCamera.transform.position;
        Vector3 finalPos = this.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            if (new Vector3(myCamera.transform.position.x, myCamera.transform.position.y) == new Vector3(finalPos.x, finalPos.y))//once the camera reaches desired position break while loop
                break;
            myCamera.transform.position = Vector3.Lerp(startingPos, new Vector3(finalPos.x, finalPos.y, -10), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

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


        //once they reach the position run this code to start the encounter
        //note when we add more encounters run a bit of code to see what kind of encounter to run instead
        combat.SetActive(true);
        map.SetActive(false);

        //if the encounter is an enemy run this line to start encounter
        combat.GetComponent<TurnBasedManager>().StartEncounter(encounteredEnemy, this);
        yield return null;
    }
}
