using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rosterScript : MonoBehaviour
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

        GameObject.Find("AcceptModal").transform.localScale = new Vector3(0, 0, 0);
        opinionMeter.fillAmount = 0.0f;

        populateList();

    }
    public void populateList(bool isFirst = true)
    {

        int artistIndex = 0;

        for (int i = 0; i < stateManager.artistList.Count; i++)
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
                textBox.text = stateManager.artistList[artistIndex].artistName;
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
            textDescription.text = "Rating: " + stateManager.artistList[index].artistRating + "  Genre: " + stateManager.artistList[index].artistGenre + "\n\n" + stateManager.artistList[index].artistDesc;
            textArtistName.text = stateManager.artistList[index].artistName;
            textArtistCost.text = "$" + stateManager.artistList[index].artistCost;
            textImpression.text = "What they think of you: \n\n" + stateManager.artistList[index].artistOpinion;
            float fillVal = Convert.ToSingle(stateManager.artistList[index].artistOpRate);

            opinionMeter.fillAmount = fillVal / 10;
        }

        if (button.name == "Button_v4")
        {

            GameObject.Find("AcceptModal").transform.localScale = new Vector3(1, 1, 1);
            acceptModalText.text = stateManager.artistList[index].artistName;

            currentArtistIndex = index;

        }

    }

    public void acceptModalClickCancel()

    {

        GameObject.Find("AcceptModal").transform.localScale = new Vector3(0, 0, 0);

    }

    public void acceptModalClickOK()

    {

        bool error = false;
        bool errorRefused = false;
        string errorMessage = "";

        if (stateManager.moneyOnHand >= decimal.Parse(stateManager.artistList[currentArtistIndex].artistCost))
        {

            if (opinionGoodEnough())
            {

                error = false;

            }
            else
            {

                error = true;
                errorMessage = stateManager.artistList[currentArtistIndex].artistName + " doesn't think highly enough of your label to sign";
                errorRefused = true;

            }

            if (checkRefused())
            {

                error = true;
                errorMessage = stateManager.artistList[currentArtistIndex].artistName + " Has already refused you, check back again when your reputation is higher";

            }

        }
        else
        {

            error = true;
            errorMessage = "You haven't got enough money to sign " + stateManager.artistList[currentArtistIndex].artistName;

        }

        if (error == true)
        {

            acceptModalText.text = errorMessage;

            if (errorRefused == true)
            {
                if (!checkRefused())
                {
                    stateManager.artistListRefused.Add(stateManager.artistList[currentArtistIndex]);

                }

            }

        }
        else
        {

            for (int i = 0; i < stateManager.artistList.Count; i++)
            {

                GameObject childObject = parentObject.transform.GetChild(i).gameObject;
                // Destroy the child GameObject
                Destroy(childObject);

            }

            stateManager.moneyOnHand = stateManager.moneyOnHand - decimal.Parse(stateManager.artistList[currentArtistIndex].artistCost);

            //Debug.Log(stateManager.moneyOnHand.ToString());
            stateManager.artistListOwned.Add(stateManager.artistList[currentArtistIndex]);
            stateManager.artistList.RemoveAt(currentArtistIndex);
            GameObject.Find("AcceptModal").transform.localScale = new Vector3(0, 0, 0);
        }

    }

    private bool checkRefused()
    {
        foreach (artist artistRefused in stateManager.artistListRefused)
        {

            if (artistRefused.artistName == stateManager.artistList[currentArtistIndex].artistName)
            {

                return true;

            }

        }

        return false;

    }

    private bool opinionGoodEnough()
    {
        var random = new System.Random();
        int artistOp = stateManager.artistList[currentArtistIndex].artistOpRate * 10;

        if (artistOp <= 50)
        {

            int randNum = random.Next(1, 101);

            if (randNum <= artistOp)
            {

                return true;

            }
            else
            {

                return false;

            }

        }
        else
        {

            return true;
        }

    }

    void Update()
    {

        if (parentObject.transform.childCount == 0)
        {

            populateList();

        }

    }
}