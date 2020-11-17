using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] PlayerHand = new GameObject[10];
    public GameObject[] DealerHand = new GameObject[10];

    int CurrentPlayerCard = 0;
    int CurrentDealerCard = 0;

    int PlayerPoints = 0;
    int DealerPoints = 0;

    public Text PlayerScore;
    public Text DealerScore;

    bool DealerPlaying = false;
    bool DealerFinished = false;

    public Button HIT;
    public Button DEAL;

    public Text Victory;
    public Text Defeat;
    public Text Tie;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHand = GameObject.FindGameObjectsWithTag("Player");
        DealerHand = GameObject.FindGameObjectsWithTag("Dealer");

        Victory.enabled = false;
        Tie.enabled = false;
        Defeat.enabled = false;
    }

    public void Next()
    {
        if (PlayerPoints < 21 && DealerPlaying == false)
        {
            PlayerHand[CurrentPlayerCard].GetComponent<CardScript>().Toggle();
            GetPoints();
            CurrentPlayerCard++;
        }
    }

    public void GetPoints()
    {
        if(DealerPlaying == false)
        {
            if (PlayerHand[CurrentPlayerCard].GetComponent<CardScript>().Value == 11 && (PlayerPoints + PlayerHand[CurrentPlayerCard].GetComponent<CardScript>().Value) > 21)
            {
                PlayerHand[CurrentPlayerCard].GetComponent<CardScript>().Value = 1;
            }

            PlayerPoints = PlayerPoints + PlayerHand[CurrentPlayerCard].GetComponent<CardScript>().Value;
        }

        if (DealerPlaying == true)
        {
            if (DealerHand[CurrentDealerCard].GetComponent<CardScript>().Value == 11 && (PlayerPoints + DealerHand[CurrentDealerCard].GetComponent<CardScript>().Value) > 21)
            {
                DealerHand[CurrentDealerCard].GetComponent<CardScript>().Value = 1;
            }

            DealerPoints = DealerPoints + DealerHand[CurrentDealerCard].GetComponent<CardScript>().Value;
        }
    }

    public void DealerTurn()
    {
        HIT.enabled = false;
        DealerPlaying = true;

        if (Defeat.enabled == false)
        {
            StartCoroutine(Coroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScore.text = "Player puntuation: " + PlayerPoints;
        DealerScore.text = "Dealer puntuation: " + DealerPoints;

        if(PlayerPoints > 21 || (DealerPoints > PlayerPoints && DealerPoints < 22))
        {
            Defeat.enabled = true;
            HIT.enabled = false;
            DEAL.enabled = false;
        }

        if(PlayerPoints == DealerPoints && DealerFinished == true)
        {
            Tie.enabled = true;
            HIT.enabled = false;
            DEAL.enabled = false;
        }

        if(PlayerPoints < DealerPoints && DealerFinished == true)
        {
            Victory.enabled = true;
            HIT.enabled = false;
            DEAL.enabled = false;
        }
    }

    public IEnumerator Coroutine()
    {
        if (DealerPoints < 17)
        {
            DealerHand[CurrentDealerCard].GetComponent<CardScript>().Toggle();
            GetPoints();
            CurrentDealerCard++;
        }
        else
        {
            DealerFinished = true;
        }

        yield return new WaitForSeconds(1f);

        DealerTurn();
    }
}
