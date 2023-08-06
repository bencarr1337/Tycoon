using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class officeManager : MonoBehaviour
{


    public Transform moneyParentObject;
    
    void Start()
    {
        Text moneyText = moneyParentObject.GetComponentInChildren<Text>();
        moneyText.text = stateManager.moneyOnHand.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        Text moneyText = moneyParentObject.GetComponentInChildren<Text>();
        moneyText.text = stateManager.moneyOnHand.ToString();

    }

    public void exitToCity()
    {

        SceneManager.LoadScene("cityView");
    }
}
