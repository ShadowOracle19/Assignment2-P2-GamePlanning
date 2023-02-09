using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public List<ActionTokens> availablePiercingActionTokens = new List<ActionTokens>();
    public List<ActionTokens> availableSlashingActionTokens = new List<ActionTokens>();

    public bool isPiercing = false;//if false set slashing, if true set piercing

    public GameObject baseToken;

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform parent;

    public List<GameObject> spawnedActionTokens = new List<GameObject>();

    [SerializeField]
    private float spawnSpeed = 1.0f;

    public Dropper drop;

    // Start is called before the first frame update
    void Start()
    {
    }


    public IEnumerator SpawnActionTokens()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnSpeed);
            if (spawnedActionTokens.Count == 4) continue;

            var token = Instantiate(baseToken, startPoint.position, Quaternion.identity, parent);

            token.GetComponent<ReadTokenValue>().currentToken = GenerateRandomToken();
            token.gameObject.name = token.GetComponent<ReadTokenValue>().currentToken.name;

            token.GetComponent<ClickOnActionToken>().manager = this;
            token.GetComponent<ClickOnActionToken>().drop = drop;
            spawnedActionTokens.Add(token);

            yield return null;
        }
        
    }

    private ActionTokens GenerateRandomToken()
    {
        if(isPiercing)
        {
            var token = availablePiercingActionTokens[Random.Range(0, availablePiercingActionTokens.Count)];
            return token;
        }
        else
        {
            var token = availableSlashingActionTokens[Random.Range(0, availableSlashingActionTokens.Count)];
            return token;
        }
    }

    public void DestroyTokens()
    {
        spawnedActionTokens.Clear();

        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        StopCoroutine(SpawnActionTokens());
    }

}
