using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PartyManager : NetworkBehaviour {

    public enum StateFSM
    {
        WaitForPlayers,
        Battle,
        End,
        Restart
    }

    [SerializeField] private TMPro.TMP_Text timerText;

    [SerializeField] private int maxPlayer;
    [SerializeField] private int minPlayer;

    [SyncVar] private int playerAlive = 0;
    [SyncVar (hook = "OnTimeLeftChange")] private int timeLeft = 31;

    [SyncVar] public StateFSM state = StateFSM.WaitForPlayers;

    private bool coroutineRunning = false;

    void Start()
    {
        StartCoroutine(FSMCorutine());
    }

    IEnumerator FSMCorutine()
    {
        while (true)
        {
            UpdateFSM();
            yield return new WaitForSeconds(2.0f);
        }
    }

    void UpdateFSM()
    {
        var playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        switch (state)
        {
            case StateFSM.WaitForPlayers:
                if (playerCount == maxPlayer)
                {
                    state = StateFSM.Battle;
                    playerAlive = playerCount;
                    if (coroutineRunning)
                    {
                        StopCoroutine("StartWithMinPlayer");
                        timerText.gameObject.SetActive(false);
                    }
                }
                if (playerCount == minPlayer && !coroutineRunning) {
                    StartCoroutine("StartWithMinPlayer");
                }
                break;
            case StateFSM.Battle:
                if (playerAlive == 1)
                {
                    state = StateFSM.End;
                }
                break;
            case StateFSM.End:
                //End
                break;
            case StateFSM.Restart:
                break;
        }
    }

    IEnumerator StartWithMinPlayer()
    {
        coroutineRunning = true;
        timerText.gameObject.SetActive(true);
        if (isServer)
        {
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(1f);
                timeLeft--;
            }
        }
        timerText.gameObject.SetActive(false);
        coroutineRunning = false;
        state = StateFSM.Battle;
        playerAlive = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    void OnTimeLeftChange(int time)
    {
        timerText.text = time + (time <= 1 ? " seconde" : " secondes");
    }
}
