using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool Top = false;
    public string Suit;
    public int Value;
    public int Row;
    public bool FaceUp = false;
    public bool InDeckPile = false;

    private string ValueString;
    // Start is called before the first frame update
    void Start()
    {
        if(CompareTag("Card")){
            Suit = transform.name[0].ToString();

            for (int i = 1; i < transform.name.Length; i++)
            {
                char c = transform.name[i];
                ValueString += c.ToString(); 
            }

            if (ValueString == "A")
            {
                Value = 1;
            }
            if (ValueString == "2")
            {
                Value = 2;
            }
            if (ValueString == "3")
            {
                Value = 3;
            }
            if (ValueString == "4")
            {
                Value = 4;
            }
            if (ValueString == "5")
            {
                Value = 5;
            }
            if (ValueString == "6")
            {
                Value = 6;
            }
            if (ValueString == "7")
            {
                Value = 7;
            }
            if (ValueString == "8")
            {
                Value = 8;
            }
            if (ValueString == "9")
            {
                Value = 9;
            }
            if (ValueString == "10")
            {
                Value = 10;
            }
            if (ValueString == "J")
            {
                Value = 11;
            }
            if (ValueString == "Q")
            {
                Value = 12;
            }
            if (ValueString == "K")
            {
                Value = 13;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
