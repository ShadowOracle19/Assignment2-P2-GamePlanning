using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapNode;

    public List<GameObject> nodes = new List<GameObject>();
    public List<GameObject> activeNodes = new List<GameObject>();

    public int maxLevelCount;

    public Transform mapParent;

    private float nodeOffsetx = 2.5f; //change this offset by 3 
    private float nodeOffsety = 3.5f; //change this offset by 3 
    private int currentNodesGeneratedInColumn = 0;

    [Header("Grid Settings")]
    public int rows = 6; 
    public int columns = 7;
    public int nodesPerColumn = 3;

    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        for (int i = 0; i < columns; i++)//iterate through the columns
        {

            for(int j = 0; j < rows; j++)//iterate through the row in that column
            {
                var node = Instantiate(mapNode, new Vector2((nodeOffsetx * j),(nodeOffsety * i)), Quaternion.identity, mapParent);
                node.GetComponent<MapNode>().row = j + 1;
                node.GetComponent<MapNode>().column = i + 1;

                nodes.Add(node);
            }
        }

        GeneratePath();
    }

    public void GeneratePath()
    {
        for (int i = 0; i < nodesPerColumn; i++)
        {
            GameObject currentNode = nodes[Random.Range(0, columns)];

            nodes.Remove(currentNode);

            activeNodes.Add(currentNode);
            
            currentNode.GetComponent<SpriteRenderer>().color = Color.red;
            

            
        }
        
    }
}

