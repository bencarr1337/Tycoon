using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class researchManager : MonoBehaviour
{
    public Transform parentObjectTechnology;
    public GameObject prefabTechnology;
    public GameObject prefabTechnique;
    public Text textTechName;
    public Text textTechDescription;
    public Text textTechCost;

    void Start()
    {

        int artistIndex = 0;

        for (int i = 0; i < stateManager.techList.techniques.Length; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);

            Debug.Log(stateManager.techList.techniques[i].name + " "+ stateManager.techList.techniques[i].category);

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

                //Debug.Log(stateManager.artistList[artistIndex].artistName);
                textBox.text = stateManager.techList.techniques[artistIndex].name;
                artistIndex++;
            }

        }

        int x = 0;
        foreach (Button button in buttons)
        {

       

            ListenerHandler(button, x);

            x++;
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
            textTechName.text = stateManager.techList.techniques[index].name;
            textTechDescription.text = stateManager.techList.techniques[index].description;

        }

    


    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
