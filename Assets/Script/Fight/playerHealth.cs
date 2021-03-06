﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    float fullHealth;
    //public GameObject deathFX;
    //public AudioClip playerHurt;

    public GameObject gameOverScreen = null;

    float currentHealth;

    //public AudioClip playerDeathSound;
    //AudioSource playerAS;
    
    //HUD Variables
    public Slider healthSlider;


    bool damaged = false;

	// Use this for initialization
	void Start () {
        fullHealth = SaveManager.Instance.BaseHP;
        currentHealth = fullHealth;
        
        //HUD initialisation
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        //playerAS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void addDamage(float damage)
    {
        if (damage <= 0)
            return;

        healthSlider.gameObject.SetActive(true);
        currentHealth -= damage;
        FloatingTextController.CreateFloatingText(damage.ToString(), FightManager.currPlayer.transform);
        healthSlider.value = currentHealth;
        damaged = true;

        
        // player damaged animation
        gameObject.GetComponent<Animator>().SetTrigger("isPushed");
        
        if (AutoMove.playerContact == true)
        {
            gameObject.GetComponent<Animator>().SetInteger("State", 0);
        }
        
        //playerAS.clip = playerHurt;
        //playerAS.Play();
        
 //       playerAS.PlayOneShot(playerHurt); //same

        if (currentHealth <= 0)
        {
                makeDead();
        }
    }

    public void addHealth(float heathAmount)
    {
        currentHealth += heathAmount;
        if (currentHealth > fullHealth)
            currentHealth = fullHealth;
        healthSlider.value = currentHealth;
    }

    public void makeDead()
    {
        if (currentHealth > 0)
        {
            currentHealth -= currentHealth;
            healthSlider.value = currentHealth;
        }
        GameOver();
        /*
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
        damageScreen.color = damagedColor;

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
        */
    }

    public void winGame()
    {
        /*
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
        */
    }

    void GameOver()
    {
        MusicManager.StopBGM();
        SfxManager.PlaySound("GameOver");
        StartCoroutine(playAfterTime(1f));
        
        ButtonShop.paused = false;
        ButtonShop.togglePause();
        gameOverScreen.SetActive(true);
    }

    IEnumerator playAfterTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        MusicManager.PlayBGM("BGM2");
    }
}
