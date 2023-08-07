using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signedRosterScript : MonoBehaviour
{

    public Text textArtistName;
    public Text textDescription;
    public Text textArtistCost;
    public Text acceptModalText;
    public Text textImpression;
    public InputField textField;
    public Transform parentObject;
    public GameObject prefab;
    public static artist objArtist;
    public Image opinionMeter;
    public int currentArtistIndex;
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Find("AcceptModalSigned").transform.localScale = new Vector3(0, 0, 0);
        opinionMeter.fillAmount = 0.0f;
        populateList();
    }

    public void populateList(bool isFirst = true)
    {

        int artistIndex = 0;

        for (int i = 0; i < stateManager.artistListOwned.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefab, randomPos, Quaternion.identity, parentObject);

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

    public void acceptModalClickCancel()

    {

        GameObject.Find("AcceptModalSigned").transform.localScale = new Vector3(0, 0, 0);

    }

    private void ListenerHandler(Button button, int index)
    {

        button.onClick.AddListener(() => {
            OnUIButtonClick(button, index);
        });
    }

    private void OnUIButtonClick(Button button, int index)
    {

        if (button.name == "itemTemplate")
        {
            textDescription.text = "Rating: " + stateManager.artistListOwned[index].artistRating + "  Genre: " + stateManager.artistListOwned[index].artistGenre + "\n\n" + stateManager.artistListOwned[index].artistDesc;
            textArtistName.text = stateManager.artistListOwned[index].artistName;
            textArtistCost.text = "$" + stateManager.artistListOwned[index].artistCost;
            textImpression.text = "What they think of you: \n\n" + stateManager.artistListOwned[index].artistOpinion;
            float fillVal = Convert.ToSingle(stateManager.artistListOwned[index].artistOpRate);

            opinionMeter.fillAmount = fillVal / 10;
        }

        if (button.name == "Button_v4")
        {

            GameObject.Find("AcceptModalSigned").transform.localScale = new Vector3(1, 1, 1);
            acceptModalText.text = stateManager.artistListOwned[index].artistName;

            currentArtistIndex = index;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
