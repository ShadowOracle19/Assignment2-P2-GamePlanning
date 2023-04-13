using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject playerSprite;

    public List<GameObject> nodes = new List<GameObject>();

    public Transform mapParent;

    public GameObject beginningMapPrefab;



    // Start is called before the first frame update
    void Start()
    {
        GenerateMap(beginningMapPrefab);

        GameManager.Instance.mapTutorial.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void GenerateMap(GameObject newMap)
    {
        
        //TelemetryLogger.ChangeSection(this, newMap.name);

        var map = Instantiate(newMap, this.transform);
        mapParent = map.transform;

        playerSprite.transform.position = new Vector3(-2, 0, 0);

        foreach (Transform child in mapParent)
        {
            nodes.Add(child.gameObject);
            child.gameObject.GetComponent<MapNode>().mapGenerator = this;
        }

    }

}

