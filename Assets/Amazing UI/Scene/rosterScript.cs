using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rosterScript : MonoBehaviour
{

    public InputField textField;
    public Transform parentObject;
    public GameObject prefab;
    public static artist objArtist;
    public static List<string> nounList { get; set; }
    public static List<string> adjList { get; set; }
    // Start is called before the first frame update
    void Start()
    {

        textField.GetComponent<InputField>().text = "HAI";

        var nounFile = Resources.Load<TextAsset>("Wordlists/nouns");
        var nounContent = nounFile.text;
        var nounWords = nounContent.Split("\n");
        nounList = new List<string>(nounWords);

        var adjFile = Resources.Load<TextAsset>("Wordlists/adjectives");
        var adjContent = adjFile.text;
        var adjWords = adjContent.Split("\n");
        adjList = new List<string>(adjWords);
        //Instantiate(prefab, parentObject);
        var random = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefab, randomPos, Quaternion.identity,parentObject);
            int adjIndex = random.Next(adjList.Count);
            var adjective = adjList[adjIndex];



            int nounIndex = random.Next(nounList.Count);
            var noun = nounList[nounIndex];
            objArtist = new artist(adjective+ " "+ noun);

            Debug.Log(objArtist.artistName);



        }

        Button[] buttons = parentObject.GetComponentsInChildren<Button>();

     

        int a = 0;
        int x = 0;
        foreach (Button button in buttons)
        {



           

            if (a == 2)
            {
                x++;
                a = 0;
            }

            ListenerHandler(button, x);

           

            a++;
        }


    }

    private void ListenerHandler(Button button, int index)
    {
        button.onClick.AddListener(() => { OnUIButtonClick(button, index); });
    }

    private void OnUIButtonClick(Button button, int index)
    {
        textField.GetComponent<InputField>().text = index.ToString() + " "+ button.name;

       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
