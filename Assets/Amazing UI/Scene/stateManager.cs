using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System;

[System.Serializable]
public class labelRating
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public string opinion;
    public int rating;
  
 

}


[System.Serializable]
public class RatingList
{
    public labelRating[] opinions;
}

public class stateManager : MonoBehaviour
{

    public static artist objArtist;
    public static List<string> nounList { get; set; }
    public static List<string> adjList { get; set; }
    public static List<string> descList { get; set; }
    public static List<string> genList { get; set; }
    public static List<artist> artistList = new List<artist>();
    public static List<artist> artistListOwned = new List<artist>();
    public static decimal moneyOnHand;
    public static bool isNewGame;
    public TextAsset ratingJson;
    public static RatingList ratingList;
    public static int reputation;
    // Start is called before the first frame update
    void Start()
    {

      



    }



    public void startNewGame()
    {


        moneyOnHand = 500000m;

        reputation = 10;

        var nounFile = Resources.Load<TextAsset>("Wordlists/nouns");
        var nounContent = nounFile.text;
        var nounWords = nounContent.Split("\n");
        nounList = new List<string>(nounWords);

        var descFile = Resources.Load<TextAsset>("Wordlists/artistDesc");
        var descContent = descFile.text;
        var descWords = descContent.Split("\n");
        descList = new List<string>(descWords);

        var adjFile = Resources.Load<TextAsset>("Wordlists/adjectives");
        var adjContent = adjFile.text;
        var adjWords = adjContent.Split("\n");
        adjList = new List<string>(adjWords);

        var genFile = Resources.Load<TextAsset>("Wordlists/genreList");
        var genContent = genFile.text;
        var genWords = genContent.Split("\n");
        genList = new List<string>(genWords);

        var random = new System.Random();




       



        for (int i = 0; i < 20; i++)
        {

            int adjIndex = random.Next(adjList.Count);
            var adjective = adjList[adjIndex];



            int nounIndex = random.Next(nounList.Count);
            var noun = nounList[nounIndex];

            int artDescIndex = random.Next(descList.Count);
            var artDesc = descList[artDescIndex];

            int artGenIndex = random.Next(genList.Count);
            var artGen = genList[artGenIndex];

            adjective = char.ToUpper(adjective[0]) + adjective.Substring(1);
            noun = char.ToUpper(noun[0]) + noun.Substring(1);


           

            int artistRatingNum = random.Next(1, 11);



            var result = getArtistOpinion(artistRatingNum);
            string artistOpinion = result.Item2;
            int artistOpRate = result.Item1;




            objArtist = new artist(adjective + " " + noun, artDesc, artGen, artistOpinion, artistRatingNum, artistOpRate);

            artistList.Add(objArtist);
            // Debug.Log(adjective + " " + noun + " " + artDesc);



        }

    }




    public (int,string) getArtistOpinion(int artistRatingNum)
    {

        var random = new System.Random();
        ratingList = new RatingList();

        string fileContents = File.ReadAllText("Assets/Resources/Wordlists/labelOpinion.json");

    

        ratingList = JsonUtility.FromJson<RatingList>(fileContents);


        int yourRating = reputation; // Your rating (between 1 and 10)
        int otherPersonRating = artistRatingNum; // Other person's rating (between 1 and 10)


        int randomNumber = 0;

        int difference = 0;

        if (reputation >= artistRatingNum)
        {

            difference = reputation - artistRatingNum;

            difference = difference * 10;

            int randNum = random.Next(1, 101);

            if (randNum <= difference) { 

                randomNumber = random.Next(Math.Max(1, reputation), Math.Min(10, reputation + 3));

            }
            else
            {
                randomNumber = random.Next(Math.Max(1, reputation - 3), Math.Min(10, reputation));

            }
         }

        if (reputation < artistRatingNum)
        {

            difference = artistRatingNum-reputation;

            difference = difference * 10;

            int randNum = random.Next(1, 101);

            if (randNum  <= difference)
            {

                randomNumber = random.Next(Math.Max(1, reputation-3), Math.Min(10, reputation));
                Debug.Log(reputation+" "+ artistRatingNum + " "+ difference + " "+ randNum + " " + randomNumber);

            }
            else
            {
                randomNumber = random.Next(Math.Max(1, reputation), Math.Min(10, reputation+3));
                Debug.Log(reputation + " " + artistRatingNum + " " +  difference + " " + randNum + " " + randomNumber);

            }
        }

        /* if (reputation < artistRatingNum)
         {

             difference = artistRatingNum- reputation;


             randomNumber = random.Next(Math.Max(1, reputation - difference), Math.Min(10, reputation + 3));

             if (randomNumber < 1)
             {
                 randomNumber = 1;
             }

             if (randomNumber > 10)
             {

                 randomNumber = 10;
             }

         }*/







        int ratingIndex = random.Next(ratingList.opinions.Length);

        bool loopDone = false;
        string artistOp="";
        int artistRating = 0;

        while (loopDone == false) { 

             ratingIndex = random.Next(ratingList.opinions.Length);

             artistOp = ratingList.opinions[ratingIndex].opinion;
             artistRating = ratingList.opinions[ratingIndex].rating;

            /*if (artistRating >= lowerBound && artistRating <= upperBound)
            {
                //Debug.Log(artistRating + " " + artistOp);
                Debug.Log(artistRatingNum + " " + lowerBound.ToString() + " " + upperBound.ToString()+ " "+ artistRating);
                loopDone = true;
                

            }*/

            if (artistRating== randomNumber)
            {

                //Debug.Log(reputation.ToString()+ " "+artistRatingNum + " "  +artistRating);
                loopDone = true;
            }


        }


        return (artistRating,artistOp);

    }


    static double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }


    public void clickNew()
    {
        isNewGame = true;
        SceneManager.LoadScene("cityView");
        startNewGame();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
