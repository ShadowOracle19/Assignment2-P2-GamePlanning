using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dropper : MonoBehaviour
{
    public ReadTokenValue currentToken;
    public ConveyorManager conveyorManager;

    public GameObject square;
    public Sprite noToken;
    public Sprite hasToken;

    public bool tokenAnimation = false;


    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform spawnPoint;

    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentToken != null)
        {
            square.GetComponent<SpriteRenderer>().sprite = hasToken;
            if ((TurnBasedManager.Instance.currentOnScreenCharacter != currentToken.currentToken.character))
            {
                TurnBasedManager.Instance.nextPlayerSprite.sprite = currentToken.currentToken.character.neutral;
                TurnBasedManager.Instance.nextPlayerNameplate.sprite = currentToken.currentToken.character.namePlate;
                TurnBasedManager.Instance.nextScreenCharacter = currentToken.currentToken.character;
                TurnBasedManager.Instance.combatAnim.SetTrigger("SwapSprite");//SwapSprite
            }

            TurnBasedManager.Instance.player.ATBSlider.value += Time.deltaTime * TurnBasedManager.Instance.player.ATBSpeed;

        }
        else
        {
            square.GetComponent<SpriteRenderer>().sprite = noToken;
        }


        if (TurnBasedManager.Instance.player.ATBSlider.value == TurnBasedManager.Instance.player.ATBSlider.maxValue)
        {
            if (currentToken.currentToken.sfx != null)
            {
                SoundEffectManager.Instance.weaponSFX.clip = currentToken.currentToken.sfx;
                SoundEffectManager.Instance.weaponSFX.Play();
            }
            //if a utility token this will play
            TurnBasedManager.Instance.player.Defend(currentToken.currentToken.defendAmount, TurnBasedManager.Instance.player);
            TurnBasedManager.Instance.player.Heal(TurnBasedManager.Instance.player, currentToken.currentToken.healingAmount);

            //if the token is stance change this will play
            if (currentToken.currentToken.isChangeStance)
            {
                conveyorManager.DestroyTokens();
                conveyorManager.isPiercing = !conveyorManager.isPiercing;

                //when the stance changes play the correct stance change sfx
                if (conveyorManager.isPiercing)
                {
                    SoundEffectManager.Instance.stanceChangeRangedSFX.Play();
                }
                else
                {
                    SoundEffectManager.Instance.stanceChangeMeleeSFX.Play();
                }
            }

            //When a token with attack is used this will play
            if ((TurnBasedManager.Instance.targetedEnemy != null) && currentToken.currentToken.damageAmount > 0)
            {
                TurnBasedManager.Instance.combatAnim.SetTrigger("Attacking");
                tokenAnimation = true;

            }

            TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;

            if (!tokenAnimation) UseToken();
        }
        MoveToken();
    }

    void MoveToken()
    {
        switch (TurnBasedManager.Instance.targetedEnemy.name)
        {
            case "Enemy 1":
                currentTarget = target1;
                break;
            case "Enemy 2":
                currentTarget = target2;
                break;
            case "Enemy 3":
                currentTarget = target3;
                break;
            default:
                break;
        }

        if (tokenAnimation)
        {
            currentToken.gameObject.transform.position = Vector2.Lerp(currentToken.gameObject.transform.position, currentTarget.position, Time.deltaTime * 5.0f);

            if (Vector2.Distance(currentToken.gameObject.transform.position, currentTarget.position) < 1)
            {
                TurnBasedManager.Instance.player.Attack(currentToken.currentToken.damageAmount, TurnBasedManager.Instance.targetedEnemy, currentToken.currentToken.isAoe);

                if (currentToken.currentToken.isAoe)
                {
                    TurnBasedManager.Instance.enemies[0].GetComponent<Animator>().SetTrigger("Attacked");
                    TurnBasedManager.Instance.enemies[1].GetComponent<Animator>().SetTrigger("Attacked");
                    TurnBasedManager.Instance.enemies[2].GetComponent<Animator>().SetTrigger("Attacked");

                }
                else
                {
                    TurnBasedManager.Instance.targetedEnemy.GetComponent<Animator>().SetTrigger("Attacked");

                }
                UseToken();
            }
        }
    }

    public void UseToken()
    {
        tokenAnimation = false;
        //Telemetry stuff
        switch (currentToken.currentToken.tokenName)
        {
            case "Bandage":
                TurnBasedManager.Instance.tokenUsage.bandage += 1;
                break;

            case "Chainsaw":
                TurnBasedManager.Instance.tokenUsage.chainsaw += 1;
                break;

            case "ChangeStance":
                TurnBasedManager.Instance.tokenUsage.changeStance += 1;
                break;

            case "Knife":
                TurnBasedManager.Instance.tokenUsage.knife += 1;
                break;

            case "Pistol":
                TurnBasedManager.Instance.tokenUsage.pistol += 1;
                break;

            case "Shield":
                TurnBasedManager.Instance.tokenUsage.shield += 1;
                break;

            case "Sledgehammer":
                TurnBasedManager.Instance.tokenUsage.sledgehamer += 1;
                break;

            case "Shotgun":
                TurnBasedManager.Instance.tokenUsage.shotgun += 1;
                break;

            default:
                Debug.Log("Token doesn't exist");
                break;
        }

        DestroyToken();
    }


    public void DestroyToken()
    {
        Destroy(currentToken.gameObject);
        currentToken = null;
        TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;


    }

    public void DropToken(ReadTokenValue token)
    {
        if (currentToken != null)
        {
            currentToken.gameObject.GetComponent<ClickOnActionToken>().enabled = false;
            currentToken.gameObject.GetComponent<ReadTokenValue>().icon.color = Color.gray;
            currentToken.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            currentToken.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-4, 4), Random.Range(2, 4)), ForceMode2D.Impulse);
        }
        currentToken = token;
        TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;
        //TurnBasedManager.Instance.combatAnim.SetBool("SwapSprite", false);//SwapSprite
    }
}
