using UnityEngine;
using DG.Tweening;
using System;

public class GameplayState : MonoBehaviour {

    public AudioClip defeatAudio;
    public AudioClip victoryAudio;
    public AudioClip welcomeAudio;

    public AudioClip friendlyTurretDestroyAudio;
    public AudioClip enemyTurretDestroyAudio;

    public Hand playerHand;
    public Hand enemyHand;

    public CanvasGroup introCanvasGroup;
    public CanvasGroup endCanvasGroup;
    public GameObject victoryImage;

    public GameObject defeatImage;

    public void OnPlayerMove(string data)
    {
        NetworkAction na = JsonUtility.FromJson<NetworkAction>(data);
        switch(na.messageType)
        {
            case NetworkCardPlay.ID:
                enemyHand.PlayNetworkCard(data);
                playerHand.enabled = true;
                break;
            case NetworkRollFirst.ID:
                CheckRoll(data);
                break;
        }
    }

    public void OnGameStart(string data)
    {
        DOTween.To(() => introCanvasGroup.alpha, x => introCanvasGroup.alpha = x, 0, 1).OnComplete(OnIntroCanvasComplete);
    }

    public void OnOpponentDisconnected(string data)
    {
        EndGame((int)PlayerSide.Friendly);
    }

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

        DOTween.To(() => endCanvasGroup.alpha, x => endCanvasGroup.alpha = x, 1, 1);

        playerHand.enabled = false;
    }

    AudioSource audioSource;
    int myRoll = -1;

    private void OnIntroCanvasComplete()
    {
        playerHand.Init();

        RollDiceForFirst();

        audioSource.PlayOneShot(welcomeAudio);
    }

    private void RollDiceForFirst()
    {
        if (myRoll != -1)
            return;

        //roll numbers between 0-1000 to decide who goes first
        NetworkRollFirst roll = new NetworkRollFirst();
        roll.rollAmount = UnityEngine.Random.Range(0, 1000);
        myRoll = roll.rollAmount;
        SocketIOClient.Send(JsonUtility.ToJson(roll));
    }

    private void CheckRoll(string data)
    {
        //if have not rolled yet, do it now
        if (myRoll == -1)
            RollDiceForFirst();

        NetworkRollFirst roll = JsonUtility.FromJson<NetworkRollFirst>(data);
        if (myRoll > roll.rollAmount)
        {
            //go first
            playerHand.enabled = true;
        }
        else if (myRoll < roll.rollAmount)
        {
            //go second
        }
        else
        {
            myRoll = -1;
            //roll again :|
            RollDiceForFirst();
        }
    }

    private void Defeat()
    {
        //such chain, much wow
        GameWorld.Instance.FriendlyNexus.GetComponentInChildren<Strongpoint>().DestroyStrongpoint();
        audioSource.PlayOneShot(defeatAudio);
        victoryImage.SetActive(false);
    }

    private void Victory()
    {
        GameWorld.Instance.EnemyNexus.GetComponentInChildren<Strongpoint>().DestroyStrongpoint();
        audioSource.PlayOneShot(victoryAudio);
        defeatImage.SetActive(false);
    }

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }
}
