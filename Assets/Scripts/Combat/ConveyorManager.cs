using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ConveyorManager : MonoBehaviour
{
    public List<ActionTokens> availableRangedActionTokens = new List<ActionTokens>();
    public List<ActionTokens> availableMeleeActionTokens = new List<ActionTokens>();

    public int maxTokens = 4;

    public bool isPiercing = false;//if false set slashing, if true set piercing
    public Image stanceIndicator;
    public Sprite rangedIcon;
    public Sprite meleeIcon;

    public GameObject baseToken;

    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform parent;

    public List<GameObject> spawnedActionTokens = new List<GameObject>();

    [SerializeField]
    private float spawnSpeed = 1.0f;

    public Dropper drop;
    public Reserve reserve;

    public Animator stanceChangerAnim;


    private void Update()
    {
        //this swaps the stance indictaor to show piercing or slashing
        if(isPiercing)
        {
            stanceIndicator.sprite = rangedIcon;
        }
        else
        {
            stanceIndicator.sprite = meleeIcon;
        }
    }

    //this ienumertator will spawn tokens on the conveyor and set them up
    public IEnumerator SpawnActionTokens()
    {
        while(true)//infinite loop :P
        {
            yield return new WaitForSeconds(spawnSpeed);//delays spawn based on spawn speed
            if (spawnedActionTokens.Count == maxTokens) continue;//if there are a certain amount of spawned tokens dont run the rest of the code

            var token = Instantiate(baseToken, startPoint.position, Quaternion.identity, parent);

            token.GetComponent<ReadTokenValue>().currentToken = GenerateRandomToken();

            

            token.gameObject.name = token.GetComponent<ReadTokenValue>().currentToken.name;

            //checks if there is already a stance change token and if there is reroll the token
            //this wont stop multiple spawning at once but should spawn less
            if (spawnedActionTokens.Count > 0 && token.GetComponent<ReadTokenValue>().currentToken.isChangeStance)
            {
                foreach (var tokenCheck in spawnedActionTokens)
                {
                    if (tokenCheck.gameObject.name == token.gameObject.name)
                    {
                        token.GetComponent<ReadTokenValue>().currentToken = GenerateRandomToken();
                        Debug.Log("Switched");
                        break;
                    }

                }
            }

            token.GetComponent<ClickOnActionToken>().manager = this;
            token.GetComponent<ClickOnActionToken>().drop = drop;
            token.GetComponent<ClickOnActionToken>().reserve = reserve;
            spawnedActionTokens.Add(token);

            yield return null;
        }
        
    }

    //this will generate a random token from a desired list
    private ActionTokens GenerateRandomToken()
    {
        //if it is piercing it will pull tokens from piercing tokens list
        if(isPiercing)
        {
            var token = availableRangedActionTokens[Random.Range(0, availableRangedActionTokens.Count)];

            return token;
        }
        //if it is not piercing it will pull tokens from slashing tokens list
        else
        {
            var token = availableMeleeActionTokens[Random.Range(0, availableMeleeActionTokens.Count)];

            return token;
        }
    }

    //destroys all tokens created
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
