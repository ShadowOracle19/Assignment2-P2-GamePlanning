using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public List<GameObject> actionPrefabList = new List<GameObject>();

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform parent;

    public List<GameObject> spawnedActionTokens = new List<GameObject>();

    [SerializeField]
    private float spawnSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnActionTokens());
    }


    IEnumerator SpawnActionTokens()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnSpeed);
            if (spawnedActionTokens.Count == 6) continue;

            var token = Instantiate(GenerateRandomToken(), startPoint.position, Quaternion.identity, parent);
            token.GetComponent<DragAndDrop>().manager = this;
            spawnedActionTokens.Add(token);

            yield return null;
        }
        
    }

    private GameObject GenerateRandomToken()
    {
        var token = actionPrefabList[Random.Range(0, actionPrefabList.Count)];

        return token;
    }
}
