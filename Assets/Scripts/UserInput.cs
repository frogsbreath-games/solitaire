using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject CardSlot1;
    private Solitaire solitaire;
    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        CardSlot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                //HIT STUFF Deck/Card/Empty
                if (hit.collider.CompareTag("Deck"))
                {
                    //Deck
                    Deck();
                    solitaire.DealFromDeck();
                }
                if (hit.collider.CompareTag("Card"))
                {
                    //Card
                    Card(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Top"))
                {
                    //Top
                    Top();
                }
                if (hit.collider.CompareTag("Bottom"))
                {
                    //Bottom
                    Bottom();
                }
            }
        }
    }

    void Deck()
    {
        print("Deck");
    }
    void Card(GameObject selectedCard)
    {
        print("Card");
        //If card is facedown & uncovered
            // flip the card

        //If card is in the deck pile of trips select it
            //select the card

        //If card is faceup and no card is selected 
            //select the card
        if(CardSlot1 == this.gameObject) // not null because this passed to avoid reference error (investigate)
        {
            CardSlot1 = selectedCard;
        }

        else if (CardSlot1 != selectedCard)
        {
            //If a card is already selected and the new card can be stacked
            if (Stackable(selectedCard))
            {
                //stack the card

            }
            else
            {
                //else select the new card
                CardSlot1 = selectedCard;

            }
        }

        //else if the card is already selected and it is the same card
        //short time it is a double click and if stackable the card should fly up to stack in the top row
    }
    void Top()
    {
        print("Top");
    }
    void Bottom()
    {
        print("Bottom");
    }

    bool Stackable(GameObject selectedCard)
    {
        Selectable slotCard = CardSlot1.GetComponent<Selectable>();
        Selectable selectCard = selectedCard.GetComponent<Selectable>();
        //Compare!

        //Top pile must stack suited Ace to King
        if (selectCard.Top)
        {
            if(slotCard.Suit == selectCard.Suit || (slotCard.Value == 1 && selectCard.Suit == null))
            {
                if(slotCard.Value == selectCard.Value + 1)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
        else
        {
            if (slotCard.Value == selectCard.Value - 1)
            {
                bool slotRed = true;
                bool selectedRed = true;

                if(slotCard.Suit == "C" || slotCard.Suit == "S")
                {
                    slotRed = false;
                }

                if (selectCard.Suit == "C" || selectCard.Suit == "S")
                {
                    selectedRed = false;
                }

                if (selectedRed == slotRed) {
                    print("Not Stackable");
                    return false;
                }
                else
                {
                    print("Stackable");
                    return true;
                }
            }
        }

        //Bottom pile must be alternate colors King to Ace
        return false;
    }

    void Stack(GameObject card)
    {
        //if on top of king or empty bottom stack in place
        //else stack with y offset
        Selectable slotCard = CardSlot1.GetComponent<Selectable>();
    }
}
