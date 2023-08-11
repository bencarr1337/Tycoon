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


[System.Serializable]
public class techResearch
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public string name;
    public string description;
    public int[] applicable_genres;
    public string category;



}




[System.Serializable]
public class techResearchList
{
    public techResearch[] techniques;
}



[System.Serializable]
public class genre
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public int id;
    public string name;



}

[System.Serializable]
public class genreList
{
    public genre[] genres;
}



[System.Serializable]
public class serializedGameState
{
    public List<artist> artistList = new List<artist>();
    public List<artist> artistListOwned = new List<artist>();
    public List<artist> artistListRefused = new List<artist>();

    public string moneyOnHand;
    public bool isNewGame;
    public int reputation;
    public string labelName;
}

[System.Serializable]

public class stateManager : MonoBehaviour
{
    [SerializeField] public serializedGameState serializedGame;
    [SerializeField] public serializedGameState serializedGameSave;
    public static artist objArtist;
    public static List<string> nounList
    {
        get;
        set;
    }
    public static List<string> adjList
    {
        get;
        set;
    }
    public static List<string> descList
    {
        get;
        set;
    }
    public static List<string> genList
    {
        get;
        set;
    }


 

    public static List<artist> artistList = new List<artist>();
    public static List<artist> artistListOwned = new List<artist>();
    public static List<artist> artistListRefused = new List<artist>();
    public static List<techResearchList> techListOwned = new List<techResearchList>();
    public static decimal moneyOnHand;
    public static bool isNewGame;
    public TextAsset ratingJson;
    public static RatingList ratingList;
    public static techResearchList techList;
    public static genreList GenreList;
    public static int reputation;
    public static string labelName;
    public static bool isLoadGame;
    public static bool endWeekModalShowing = false;

    public void exitToMainMenu()
    {

        SceneManager.LoadScene("mainMenu");

    }

    public void loadGame()
    {

  



        techList = new techResearchList();

        string fileContentsTech = File.ReadAllText("Assets/Resources/Wordlists/technology.json");

        techList = JsonUtility.FromJson<techResearchList>(fileContentsTech);

        GenreList = new genreList();

        string fileContentGenre = File.ReadAllText("Assets/Resources/Wordlists/genreList.json");

        GenreList = JsonUtility.FromJson<genreList>(fileContentGenre);

        genList = new List<string>();
        for (int a = 0; a < GenreList.genres.Length; a++)
        {


       

            genList.Add(GenreList.genres[a].name);

            Debug.Log(genList[a]);
        }



        serializedGame = new serializedGameState();

        string fileContents = File.ReadAllText("Assets/Resources/saveGame.json");

        serializedGame = JsonUtility.FromJson<serializedGameState>(fileContents);

       labelName=serializedGame.labelName;
       isNewGame = false;
       moneyOnHand  = decimal.Parse(serializedGame.moneyOnHand);
       reputation = serializedGame.reputation;

        int i;

        artistList.Clear();
        artistListOwned.Clear();
        artistListRefused.Clear();

        for (i = 0; i < serializedGame.artistList.Count; i++)
        {

            artistList.Add(serializedGame.artistList[i]);

        }

        for (i = 0; i < serializedGame.artistListOwned.Count; i++)
        {

           artistListOwned.Add(serializedGame.artistListOwned[i]);

        }

        for (i = 0; i < serializedGame.artistListRefused.Count; i++)
        {

          artistListRefused.Add(serializedGame.artistListRefused[i]);

        }

        isLoadGame = false;


    }

    public void saveGame()
    {

        serializedGameSave = new serializedGameState();

        int i;

        for (i = 0; i < artistList.Count; i++)
        {

            serializedGameSave.artistList.Add(artistList[i]);

        }

        for (i = 0; i < artistListOwned.Count; i++)
        {

            serializedGameSave.artistListOwned.Add(artistListOwned[i]);

        }

        for (i = 0; i < artistListRefused.Count; i++)
        {

            serializedGameSave.artistListRefused.Add(artistListRefused[i]);

        }

        serializedGameSave.isNewGame = false;
        serializedGameSave.moneyOnHand = moneyOnHand.ToString();
        serializedGameSave.reputation = reputation;
        serializedGameSave.labelName = labelName;

       
        

        string json = JsonUtility.ToJson(serializedGameSave, true);

        string path = "Assets/Resources/saveGame.json";

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Close();

    }




    void Start()
    {

     
        if (isNewGame == true)
        {

            startNewGame();
        }

        if (isLoadGame == true)
        {

            loadGame();
        }

    }

    public void startNewGame()
    {

        isNewGame = false;

        moneyOnHand = 500000.32m;

        reputation = 1;

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


        artistList.Clear();
        artistListOwned.Clear();
        artistListRefused.Clear();


        GenreList = new genreList();

        string fileContentGenre = File.ReadAllText("Assets/Resources/Wordlists/genreList.json");

        GenreList = JsonUtility.FromJson<genreList>(fileContentGenre);

        techList = new techResearchList();

        string fileContentsTech = File.ReadAllText("Assets/Resources/Wordlists/technology.json");

        techList = JsonUtility.FromJson<techResearchList>(fileContentsTech);

       


        genList = new List<string>();
        for (int a = 0; a < GenreList.genres.Length; a++)
        {




            genList.Add(GenreList.genres[a].name);

            Debug.Log(genList[a]);
        }


        for (int i = 0; i < GenreList.genres.Length; i++)
        {


        

            genList.Add(GenreList.genres[i].name);
        }


        var random = new System.Random();

        for (int i = 0; i < 20; i++)
        {

            int adjIndex = random.Next(adjList.Count);
            var adjective = adjList[adjIndex];

            int nounIndex = random.Next(nounList.Count);
            var noun = nounList[nounIndex];

            int artDescIndex = random.Next(descList.Count);
            var artDesc = descList[artDescIndex];

            int artGenIndex = random.Next(GenreList.genres.Length);
            var artGen = GenreList.genres[artGenIndex].name;
            var artGenID = GenreList.genres[artGenIndex].id;
           

            adjective = char.ToUpper(adjective[0]) + adjective.Substring(1);
            noun = char.ToUpper(noun[0]) + noun.Substring(1);

            int artistRatingNum = random.Next(1, 11);

            var result = getArtistOpinion(artistRatingNum);
            string artistOpinion = result.Item2;
            int artistOpRate = result.Item1;

            decimal artistCost = generatePrice(artistRatingNum);

            string strArtistCost = artistCost.ToString();

            objArtist = new artist(adjective + " " + noun, artDesc, artGen, artistOpinion, artistRatingNum, artistOpRate, strArtistCost, artGenID);

            artistList.Add(objArtist);
            // Debug.Log(adjective + " " + noun + " " + artDesc);

        }

    }



    public decimal generatePrice(int artistRating)
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

    public (int, string) getArtistOpinion(int artistRatingNum)
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

            if (randNum <= difference)
            {

                randomNumber = random.Next(Math.Max(1, reputation), Math.Min(10, reputation + 5));

            }
            else
            {
                randomNumber = random.Next(Math.Max(1, reputation - 3), Math.Min(10, reputation));

            }
        }

        if (reputation < artistRatingNum)
        {

            difference = artistRatingNum - reputation;

            difference = difference * 10;

            int randNum = random.Next(1, 101);

            if (randNum <= difference)
            {

                randomNumber = random.Next(Math.Max(1, reputation - 3), Math.Min(10, reputation));
                //Debug.Log(reputation+" "+ artistRatingNum + " "+ difference + " "+ randNum + " " + randomNumber);

            }
            else
            {
                randomNumber = random.Next(Math.Max(1, reputation), Math.Min(10, reputation + 5));
                // Debug.Log(reputation + " " + artistRatingNum + " " +  difference + " " + randNum + " " + randomNumber);

            }
        }

        int ratingIndex = random.Next(ratingList.opinions.Length);

        bool loopDone = false;
        string artistOp = "";
        int artistRating = 0;

        while (loopDone == false)
        {

            ratingIndex = random.Next(ratingList.opinions.Length);

            artistOp = ratingList.opinions[ratingIndex].opinion;
            artistRating = ratingList.opinions[ratingIndex].rating;

            if (artistRating == randomNumber)
            {

                loopDone = true;
            }

        }

        return (artistRating, artistOp);

    }

    static double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    public void clickNew()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}