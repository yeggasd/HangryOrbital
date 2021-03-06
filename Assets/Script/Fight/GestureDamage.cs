﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDamage : MonoBehaviour {

    public GameObject line;
    public GameObject slash;
    public GameObject lightning;
    public GameObject pyro;


    Animator lineAnim;
    Animator slashAnim;
    Animator lightningAnim;
    Animator pyroAnim;

    public GameObject enemy;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void FixedUpdate () { 
        for (int i = 0; i < SaveManager.Instance.gestureProb.Length; i++)
        {
            if (Random.value <= SaveManager.calculateFixedUpdateProbability(SaveManager.Instance.gestureProb[i]))
            {

                switch (i)
                {
                    case 1:
                        damage("line", 1);
                        Debug.Log("Prob Line");
                        break;
                    case 2:
                        damage("forward slash", 1);
                        Debug.Log("Prob forward");
                        break;
                    case 3:
                        damage("back slash", 1);
                        Debug.Log("Prob back");
                        break;
                    case 4:
                        damage("Lightning", 1);
                        Debug.Log("Prob Lightning");
                        break;
                }
            }
        }
    }
    
    public void damage(string gestureClass, float gestureScore)
    {
        if (FightManager.currEnemy == null)
            return;
        // code to make enemy fly back
        AutoMove.damaged = true;
        AutoMove.playerContact = false;
        Animator enemyAnim = FightManager.currEnemy.GetComponent<Animator>();
        enemyDamage enemyDMG = FightManager.currEnemy.GetComponent<enemyDamage>();
        // enemyDMG.contact = false;
        enemyAnim.SetTrigger("IsPushed");

        // code to make player fight
        Animator playerAnim = FightManager.currPlayer.GetComponent<Animator>();
        playerAnim.SetTrigger("isFighting");

        switch (gestureClass)
        {
            case "Lightning":
                if (SaveManager.Instance.upgrades[8] != 0 && FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    SfxManager.PlaySound("Zap");

                    GameObject tempLightning = Instantiate(lightning, FightManager.currPlayer.transform.position, Quaternion.identity);
                    float lightningLength = tempLightning.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    float dist = -FightManager.currPlayer.transform.position.x + FightManager.currEnemy.transform.position.x;
                    float toScale = dist / lightningLength;
                    tempLightning.transform.localScale = new Vector3(toScale/2, tempLightning.transform.localScale.y, tempLightning.transform.localScale.z);
                    tempLightning.transform.position = FightManager.currPlayer.transform.position + new Vector3(lightningLength * tempLightning.transform.localScale.x, -1f, 0);

                    lightningAnim = tempLightning.GetComponent<Animator>();
                    lightningAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempLightning));
                    //Temp formula 
                    float damage =(float) SaveManager.Instance.gestureDMG[3];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                    FloatingTextController.CreateFloatingText(damage.ToString(), FightManager.currEnemy.transform);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "forward slash":
                if (SaveManager.Instance.upgrades[6] != 0 && FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    GameObject tempSlash = Instantiate(slash, FightManager.currEnemy.transform.position, Quaternion.identity);

                    SfxManager.PlaySound("ForwardSlash");
                    slashAnim = tempSlash.GetComponent<Animator>();
                    slashAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempSlash));
                    //Temp formula 
                    float damage = (float)SaveManager.Instance.gestureDMG[2];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                    FloatingTextController.CreateFloatingText(damage.ToString(), FightManager.currEnemy.transform);
                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "back slash":
                if (SaveManager.Instance.upgrades[4] != 0 && FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 

                    GameObject tempSlash = Instantiate(slash, FightManager.currEnemy.transform.position, Quaternion.identity);
                    tempSlash.transform.localScale = new Vector3(-tempSlash.transform.localScale.x, tempSlash.transform.localScale.y, tempSlash.transform.localScale.z);

                    SfxManager.PlaySound("BackwardSlash");
                    slashAnim = tempSlash.GetComponent<Animator>();
                    slashAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempSlash));
                    float damage = (float)SaveManager.Instance.gestureDMG[1];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                    FloatingTextController.CreateFloatingText(damage.ToString(), FightManager.currEnemy.transform);
                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "line":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    GameObject tempLine = Instantiate(line, FightManager.currEnemy.transform.position, Quaternion.identity);
                    SfxManager.PlaySound("Line");

                    lineAnim = tempLine.GetComponent<Animator>();
                    lineAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.2f, tempLine));
                    float damage = (float)SaveManager.Instance.gestureDMG[0];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                    FloatingTextController.CreateFloatingText(damage.ToString(), FightManager.currEnemy.transform);
                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "Fireball":
                if (SaveManager.Instance.upgrades[10] != 0 && FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    Vector3 offset = new Vector3(0.1f, -0.5f, 0);
                    Vector3 spawnPos = FightManager.currPlayer.transform.position + offset;
                    GameObject tempPyro = Instantiate(pyro, spawnPos, Quaternion.identity);
                    SfxManager.PlaySound("FireBall");

                    tempPyro.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 0), ForceMode2D.Impulse);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;
        }
            
    }
    
    IEnumerator destroyAfterTime(float waitTime, GameObject temp)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Destroy(temp);
    }
}
