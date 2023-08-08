using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class studioManager : MonoBehaviour
{
    public Transform parentObject;
    public GameObject prefab;
    void Start()
    {

        int artistIndex = 0;

        for (int i = 0; i < stateManager.artistListOwned.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefab, randomPos, Quaternion.identity, parentObject);

            Debug.Log(parentObject.name);

        }

        Button[] buttons = parentObject.GetComponentsInChildren<Button>();
        Text[] textBoxes = parentObject.GetComponentsInChildren<Text>();

        foreach (Text textBox in textBoxes)
        {
            if (textBox.name == "Text_ArtistName")
            {

                //Debug.Log(stateManager.artistList[artistIndex].artistName);
                textBox.text = stateManager.artistListOwned[artistIndex].artistName;
                artistIndex++;
            }



        }

    }

        // Update is called once per frame
        void Update()
    {
        
    }


    public void exitToCity()
    {

        SceneManager.LoadScene("cityView");
    }

}
