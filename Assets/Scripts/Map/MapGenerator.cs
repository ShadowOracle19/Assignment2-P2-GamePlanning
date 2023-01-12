using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<EnemyScriptable> enemiesToEncounter = new List<EnemyScriptable>();

    public List<GameObject> nodes = new List<GameObject>();

    public int maxLevelCount;

    public Transform mapParent;

    public EnemyScriptable[] enemyList;

    public GameObject mapPrefab;


    // Start is called before the first frame update
    void Start()
    {
        var map = Instantiate(mapPrefab, this.transform);
        mapParent = map.transform;
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        //enemyList = Resources.LoadAll<EnemyScriptable>("Enemies");//get list of enemies

        //foreach(EnemyScriptable enemy in enemyList)//add list of enemies from resource folder to enemiesToEncounter
        //{
        //    enemiesToEncounter.Add(enemy);
        //}

        foreach (Transform child in mapParent)
        {
            nodes.Add(child.gameObject);
        }

        //foreach(GameObject node in nodes)
        //{
        //    node.GetComponent<MapNode>().encounteredEnemy = enemiesToEncounter[Random.Range(0, enemiesToEncounter.Count)];

        //}
        nodes[0].GetComponent<MapNode>().canInteract = true;
    }

}

