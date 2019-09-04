using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    public Sprite[] CardFronts;
    public GameObject CardPrefab;
    public GameObject DeckButton;
    public static string[] Suits = new string[] { "C", "D", "H", "S" };
    public static string[] Values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public GameObject[] TopPosition;
    public GameObject[] BottomPosition;

    public List<string> Deck;

    public List<string>[] Tops;
    public List<string>[] Bottoms;

    private List<string> Bottom0 = new List<string>();
    private List<string> Bottom1 = new List<string>();
    private List<string> Bottom2 = new List<string>();
    private List<string> Bottom3 = new List<string>();
    private List<string> Bottom4 = new List<string>();
    private List<string> Bottom5 = new List<string>();
    private List<string> Bottom6 = new List<string>();

    private int Trips;
    private int TripsRemainder;

    private int DeckLocation;

    public List<string> TripsDisplayed;

    public List<List<string>> ListOfTrips = new List<List<string>>();


    public void StartGame()
    {
        Deck = MakeDeck();

        foreach (var item in Deck)
        {
            print(item);
        }

        Shuffle(Deck);
        SolitaireSort();
        StartCoroutine(SolitaireDeal());
    }
    // Start is called before the first frame update
    void Start()
    {
        Bottoms = new List<string>[] { Bottom0, Bottom1, Bottom2, Bottom3, Bottom4, Bottom5, Bottom6 };
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string> MakeDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string suit in Suits)
        {
            foreach (string value in Values)
            {
                newDeck.Add(suit + value);
            }
        }

        return newDeck;
    }

    private static System.Random random = new System.Random();

    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    //Coroutine with IEnumator 
    public IEnumerator SolitaireDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0f;
            float zOffset = 0.03f;

            foreach (string card in Bottoms[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(CardPrefab, new Vector3(BottomPosition[i].transform.position.x, BottomPosition[i].transform.position.y - yOffset, BottomPosition[i].transform.position.z - zOffset), Quaternion.identity, BottomPosition[i].transform);
                newCard.name = card;

                if(card == Bottoms[i][Bottoms[i].Count - 1]){
                    newCard.GetComponent<Selectable>().FaceUp = true;
                }

                yOffset += .3f;
                zOffset += .03f;
            }
        }
    }

    public void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                Bottoms[j].Add(Deck.Last<string>());
                Deck.RemoveAt(Deck.Count - 1); 
            }
        }
    }

    public void SortDeckIntoTrips()
    {
        Trips = Deck.Count / 3;
        TripsRemainder = Deck.Count % 3;

        ListOfTrips.Clear();

        int index = 0;
        for (int i = 0; i < Trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++)
            {
                myTrips.Add(Deck[j + index]);
            }
            ListOfTrips.Add(myTrips);
            index += 3;
        }

        if (TripsRemainder != 0)
        {
            List<string> remainingCards = new List<string>();
            index = 0;
            for (int k = 0; k < TripsRemainder; k++)
            {
                remainingCards.Add(Deck[Deck.Count - TripsRemainder + index]);
                index++;
            }
            ListOfTrips.Add(remainingCards);
            Trips++;
        }

        DeckLocation = 0;
    }

    public void DealFromDeck()
    {
        if (DeckLocation < Trips)
        {
            TripsDisplayed.Clear();
            float xOffset = 2.5f;
            float zOffset = -0.2f;

            foreach (string card in ListOfTrips[DeckLocation])
            {
               GameObject topCard = Instantiate(CardPrefab, new Vector3(DeckButton.transform.position.x + xOffset, DeckButton.transform.position.y, DeckButton.transform.position.z + zOffset), Quaternion.identity, DeckButton.transform);
            }
        }
    }
}
