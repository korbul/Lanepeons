using UnityEngine;
using System.Collections;
using System;

public class GameplayState : MonoBehaviour {

    public AudioClip defeatAudio;
    public AudioClip victoryAudio;
    public AudioClip welcomeAudio;
    public AudioClip friendlyTurretDestroyAudio;
    public AudioClip enemyTurretDestroyAudio;

    public Hand playerHand;

    public void TurretDestroyed(int side)
    {
        if(side == (int)PlayerSide.Friendly)
        {
            audioSource.PlayOneShot(friendlyTurretDestroyAudio);
        }
        else
        {
            audioSource.PlayOneShot(enemyTurretDestroyAudio);
        }
    }

    public void EndGame(int winner)
    {
        if(winner == (int)PlayerSide.Friendly)
        {
            Victory();
        }
        else
        {
            Defeat();
        }

        playerHand.enabled = false;
    }

    AudioSource audioSource;

    private void Defeat()
    {
        //such chain, much wow
        GameWorld.Instance.FriendlyNexus.GetComponentInChildren<Strongpoint>().DestroyStrongpoint();
        audioSource.PlayOneShot(defeatAudio);
    }

    private void Victory()
    {
        GameWorld.Instance.EnemyNexus.GetComponentInChildren<Strongpoint>().DestroyStrongpoint();
        audioSource.PlayOneShot(victoryAudio);
    }

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        audioSource.PlayOneShot(welcomeAudio);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
