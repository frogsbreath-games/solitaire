using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    Top(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Bottom"))
                {
                    //Bottom
                    Bottom(hit.collider.gameObject);
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
        if (!selectedCard.GetComponent<Selectable>().FaceUp)
        {
            if (!Blocked(selectedCard))
            {
                // flip the card
                selectedCard.GetComponent<Selectable>().FaceUp = true;
                CardSlot1 = this.gameObject;
            }
        }
        //If card is in the deck pile of trips select it
        //select the card
        else if (selectedCard.GetComponent<Selectable>().InDeckPile)
        {
            if (!Blocked(selectedCard))
            {
                // Select the card
                CardSlot1 = selectedCard;
            }
        }

        //If card is faceup and no card is selected 
        //select the card
        if (CardSlot1 == this.gameObject) // not null because this passed to avoid reference error (investigate)
        {
            CardSlot1 = selectedCard;
        }

        else if (CardSlot1 != selectedCard)
        {
            //If a card is already selected and the new card can be stacked
            if (Stackable(selectedCard))
            {
                //stack the card
                Stack(selectedCard);

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
    void Top(GameObject card)
    {
        print("Top");
        if (CardSlot1.CompareTag("Card"))
        {
            if(CardSlot1.GetComponent<Selectable>().Value == 1){
                Stack(card);
            }
        }
    }
    void Bottom(GameObject card)
    {
        if (CardSlot1.CompareTag("Card"))
        {
            if (CardSlot1.GetComponent<Selectable>().Value == 13)
            {
                Stack(card);
            }
        }
    }

    bool Stackable(GameObject selectedCard)
    {
        Selectable slotCard = CardSlot1.GetComponent<Selectable>();
        Selectable selectCard = selectedCard.GetComponent<Selectable>();
        //Compare!
        if (!selectCard.InDeckPile)
        {
            //Top pile must stack suited Ace to King
            if (selectCard.Top)
            {
                if (slotCard.Suit == selectCard.Suit || (slotCard.Value == 1 && selectCard.Suit == null))
                {
                    if (slotCard.Value == selectCard.Value + 1)
                    {
                        return true;
                    }
                    else
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

                    if (slotCard.Suit == "C" || slotCard.Suit == "S")
                    {
                        slotRed = false;
                    }

                    if (selectCard.Suit == "C" || selectCard.Suit == "S")
                    {
                        selectedRed = false;
                    }

                    if (selectedRed == slotRed)
                    {
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
        }
        //Bottom pile must be alternate colors King to Ace
        return false;
    }

    void Stack(GameObject card)
    {
        //if on top of king or empty bottom stack in place
        //else stack with y offset
        Selectable slotCard = CardSlot1.GetComponent<Selectable>();
        Selectable selectedCard = card.GetComponent<Selectable>();
        float yOffset = 0.3f;

        if(selectedCard.Top || ( !selectedCard.Top && slotCard.Value == 13))
        {
            yOffset = 0;
        }

        slotCard.transform.position = new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y - yOffset, selectedCard.transform.position.z - 0.01f);
        slotCard.transform.parent = selectedCard.transform; //children move with parents

        if (slotCard.InDeckPile)
        {
            solitaire.TripsDisplayed.Remove(slotCard.name);
        }
        else if(slotCard.Top && selectedCard.Top && slotCard.Value == 1)
        {
            //Allows movement between top spots
            solitaire.TopPosition[slotCard.Row].GetComponent<Selectable>().Value = 0;
            solitaire.TopPosition[slotCard.Row].GetComponent<Selectable>().Suit = null;
        } else if(slotCard.Top)
        {
            //keeps track of top decks value if card is removed
            solitaire.TopPosition[slotCard.Row].GetComponent<Selectable>().Value = slotCard.Value -1;
        }
        else
        {
            //removes card from bottom list
            solitaire.Bottoms[slotCard.Row].Remove(slotCard.name);
        }

        //cards can't go to trips pile
        slotCard.InDeckPile = false;
        slotCard.Row = selectedCard.Row;

        if (selectedCard.Top)
        {
            solitaire.TopPosition[slotCard.Row].GetComponent<Selectable>().Value = slotCard.Value;
            solitaire.TopPosition[slotCard.Row].GetComponent<Selectable>().Suit = slotCard.Suit;
            slotCard.Top = true;
        }
        else
        {
            slotCard.Top = false;
        }

        //set slot card to null
        CardSlot1 = this.gameObject;
    }

    bool Blocked(GameObject card)
    {
        Selectable selected = card.GetComponent<Selectable>();
        if(selected.InDeckPile == true)
        {
            if(selected.name == solitaire.TripsDisplayed.Last())
            {
                return false;
            }
            else
            {
                print(selected.name + " is block by " + solitaire.TripsDisplayed.Last());
                return true;
            }
        } else
        {
            if(selected.name == solitaire.Bottoms[selected.Row].Last())
            {
                return false;
            }
            else
            {
                print(selected.name + " is block by " + solitaire.Bottoms[selected.Row].Last());
                return true;
            }
        }
    }
}
