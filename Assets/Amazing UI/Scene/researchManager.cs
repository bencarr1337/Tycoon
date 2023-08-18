using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class researchManager : MonoBehaviour
{
    public Transform parentObjectTechnology;
    public Transform parentObjectTechnologyOwned;
    public GameObject prefabTechnology;
    public GameObject prefabTechnique;
    public Text textTechName;
    public Text textTechDescription;
    public Text textTechCost;
    public Text acceptModalResearchName;
    public int techIndex;
    public Button techBuyButton;
    void Start()
    {

      

            GameObject.Find("AcceptModalResearch").transform.localScale = new Vector3(0, 0, 0);

        techIndex = 0;




    }


    private void  populateList()
    {

        if (stateManager.techList is not null)
        {
            for (int i = 0; i < stateManager.techList.techniques.Length; i++)
            {
                Vector3 randomPos = new Vector3(0, 0, 0f);





                if (stateManager.techList.techniques[i].category == "technology")
                {

                    Instantiate(prefabTechnology, randomPos, Quaternion.identity, parentObjectTechnology);


                }

                if (stateManager.techList.techniques[i].category == "technique")
                {

                    Instantiate(prefabTechnique, randomPos, Quaternion.identity, parentObjectTechnology);

                }

            }




            Button[] buttons = parentObjectTechnology.GetComponentsInChildren<Button>();
            Text[] textBoxes = parentObjectTechnology.GetComponentsInChildren<Text>();

            foreach (Text textBox in textBoxes)
            {
                if (textBox.name == "Text_Name")
                {

                    if (!checkIfOwned(stateManager.techList.techniques[techIndex].id))
                    {

                        //Debug.Log(stateManager.artistList[artistIndex].artistName);
                        textBox.text = stateManager.techList.techniques[techIndex].name;

                    }
                    else
                    {

                        textBox.color = new Color32(7, 157, 0, 255);
                        textBox.text = stateManager.techList.techniques[techIndex].name;

                    }
                    techIndex++;
                }



            }

            int x = 0;
            foreach (Button button in buttons)
            {



                ListenerHandler(button, x);



                x++;
            }

        }

    }

    private bool checkIfOwned(int id)
    {

        for (int i = 0; i < stateManager.techListOwned.Count; i++)
        {

            if (id== stateManager.techListOwned[i])
            {

                return true;
            }
        }

        return false;

    }

    public void clickBuyResearch()
    {

        GameObject.Find("AcceptModalResearch").transform.localScale = new Vector3(1, 1, 1);

        acceptModalResearchName.text = stateManager.techList.techniques[techIndex].name;



    }


    public void researchAcceptModalClickYes()
    {

        bool error = false;
   
        string errorMessage = "";

        if (stateManager.techList.techniques[techIndex].category == "technology")
        {

            if (stateManager.moneyOnHand >= decimal.Parse(stateManager.techList.techniques[techIndex].cost))
            {
                stateManager.techListOwned.Add(stateManager.techList.techniques[techIndex].id);

                stateManager.moneyOnHand = stateManager.moneyOnHand - decimal.Parse(stateManager.techList.techniques[techIndex].cost);

            }
            else
            {

                error = true;
                errorMessage = "You cannot afford to do this";

            }

        }


        if (stateManager.techList.techniques[techIndex].category == "technique")
        {

            if (stateManager.xp >= decimal.Parse(stateManager.techList.techniques[techIndex].cost))
            {
                stateManager.techListOwned.Add(stateManager.techList.techniques[techIndex].id);

                stateManager.xp = stateManager.xp - decimal.Parse(stateManager.techList.techniques[techIndex].cost);

            }
            else
            {

                error = true;
                errorMessage = "You cannot afford to do this";

            }

        }


        if (error == false)
        {


            for (int i = 0; i < stateManager.techList.techniques.Length; i++)
            {

                GameObject childObject = parentObjectTechnology.transform.GetChild(i).gameObject;
                // Destroy the child GameObject
                Destroy(childObject);

            }

            GameObject.Find("AcceptModalResearch").transform.localScale = new Vector3(0, 0, 0);

            techIndex = 0;

        }
        else
        {


            acceptModalResearchName.text = stateManager.techList.techniques[techIndex].name + " \n\n" + errorMessage;


        }




    }

    public void researchAcceptModalClickCancel()
    {
        GameObject.Find("AcceptModalResearch").transform.localScale = new Vector3(0, 0, 0);

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

            if (checkIfOwned(stateManager.techList.techniques[index].id))
            {
                textTechName.text ="[Owned] "+ stateManager.techList.techniques[index].name;
            }
            else
            {

                textTechName.text = stateManager.techList.techniques[index].name;
            }
           
            textTechDescription.text = stateManager.techList.techniques[index].description;
            string typeOfCost = "";

            if (stateManager.techList.techniques[index].category == "technology")
            {
                   textTechCost.text= typeOfCost + " $ "+stateManager.techList.techniques[index].cost;
               textTechCost.color= new Color32(195, 157, 10, 255);
            }
            else
            {
                textTechCost.text = typeOfCost + " XP " + stateManager.techList.techniques[index].cost;
                textTechCost.color = new Color32(0, 149, 255, 255);
            }

            if (checkIfOwned(stateManager.techList.techniques[index].id))
            {

                techBuyButton.interactable = false;


            }
            else
            {
                techBuyButton.interactable = true;

            }

                techIndex = index;

        }

    


    }

        // Update is called once per frame
        void Update()
    {

        if (parentObjectTechnology.transform.childCount == 0)
        {

            populateList();

        }

    }
}
