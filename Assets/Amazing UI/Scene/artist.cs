using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class artist
{



    public string artistName;
    public string artistDesc;
    public string artistCost; 
    public int artistRating;
    public string artistGenre;
    public string artistOpinion;
    public int artistOpRate;
    void Start()
    {

    }

    public artist(string name, string description, string genre, string opinion, int rating, int opRate, string aCost)

    {

        artistName = name;
        artistDesc = description;
        artistRating = rating;
        artistCost = aCost;
        artistGenre = genre;
        artistOpinion = opinion;
        artistOpRate = opRate;
      

    }







}

