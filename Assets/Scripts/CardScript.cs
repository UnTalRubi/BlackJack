using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Sprite Front;
    public Sprite Back;

    public int Value;
    public string Suit;
    public int Rank;

    SpriteRenderer CardSprite;

    bool FaceUp = false;

    private void Awake()
    {
        CardSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Toggle()
    {
        if(FaceUp == false)
        {
            CardSprite.sprite = Front;
            FaceUp = true;
        }else if(FaceUp == true)
        {
            CardSprite.sprite = Back;
            FaceUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
