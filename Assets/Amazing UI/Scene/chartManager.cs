using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chartManager : MonoBehaviour
{
    public Transform parentObjectSingle;
    public GameObject prefabSingle;

    public Transform parentObjectAlbum;
    public GameObject prefabAlbum;
    void Start()
    {
        populateList();
    }


    private void populateList()
    {


        int singleList = 0;

        int albumList = 0;
        for (int i = 0; i < stateManager.singleList.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefabSingle, randomPos, Quaternion.identity, parentObjectSingle);

        }

        for (int i = 0; i < stateManager.albumList.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefabAlbum, randomPos, Quaternion.identity, parentObjectAlbum);

       

        }


        Text[] textSingles = parentObjectSingle.GetComponentsInChildren<Text>();

        foreach (Text textBox in textSingles)
        {
            if (textBox.name == "Text_ArtistSong")
            {

                //Debug.Log(stateManager.artistList[artistIndex].artistName);
                textBox.text = stateManager.singleList[singleList].songPosition + " "+stateManager.singleList[singleList].artistName + " - "+ stateManager.singleList[singleList].songName;
                singleList++;
            }

        }

        Text[] textAlbum = parentObjectAlbum.GetComponentsInChildren<Text>();

        foreach (Text textBox in textAlbum)
        {
            if (textBox.name == "Text_ArtistSong")
            {
                
                //Debug.Log(stateManager.artistList[artistIndex].artistName);
                textBox.text = stateManager.albumList[albumList].songPosition + " "+stateManager.albumList[albumList].artistName + " - " + stateManager.albumList[albumList].songName;
                albumList++;
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
