using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{

    public Sprite CardFace;
    public Sprite CardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Solitaire solitaire;
    private UserInput userInput;

    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Solitaire.MakeDeck();
        solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<UserInput>();

        int i = 0;

        foreach (string cardName in deck)
        {
            if(this.name == cardName)
            {
                CardFace = solitaire.CardFronts[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable.FaceUp == true)
        {
            spriteRenderer.sprite = CardFace;
        }
        else
        {
            spriteRenderer.sprite = CardBack;
        }

        //runs all time not efficient only on click
        if (userInput.CardSlot1)
        {
            if (name == userInput.CardSlot1.name)
            {
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = Color.white;

            }
        }
    }
}
