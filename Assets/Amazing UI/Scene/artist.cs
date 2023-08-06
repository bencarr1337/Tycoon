using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class artist
{



    public string artistName;
    public string artistDesc;
    public decimal artistCost; 
    public int artistRating;
    public string artistGenre;
    public string artistOpinion;
    public int artistOpRate;
    void Start()
    {

    }

    public artist(string name, string description, string genre, string opinion, int rating, int opRate)

    {

        artistName = name;
        artistDesc = description;
        artistRating = rating;
        artistCost = generatePrice();
        artistGenre = genre;
        artistOpinion = opinion;
        artistOpRate = opRate;

    }


    public decimal generatePrice()
    {
        var random = new System.Random();
      

        decimal randomValue = (decimal)random.NextDouble();

        // Calculate the range between 5000 and 1000000 based on the rating
        decimal minValue = 5000;
        decimal maxValue = 1000000;
        decimal range = maxValue - minValue;

        // Calculate the adjusted value based on the rating and random value
        decimal artistCost = minValue + (randomValue * range) * (decimal)artistRating / 10;


        // Adjust the random value based on the rating


        decimal roundedArtistCost = Math.Round(artistCost, 2);





        return roundedArtistCost;
    }





}

