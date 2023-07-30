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
   
    // Start is called before the first frame update
    void Start()
    {

        textField.GetComponent<InputField>().text = "HAI";

        
        //Instantiate(prefab, parentObject);

        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefab, randomPos, Quaternion.identity,parentObject);
           
         
         
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

            Debug.Log(x.ToString());

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
