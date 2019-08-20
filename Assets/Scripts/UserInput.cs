﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                }
                if (hit.collider.CompareTag("Card"))
                {
                    //Card
                    Card();
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
    void Card()
    {
        print("Card");
    }
    void Top()
    {
        print("Top");
    }
    void Bottom()
    {
        print("Bottom");
    }
}
