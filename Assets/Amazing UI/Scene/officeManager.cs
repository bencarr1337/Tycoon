using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class officeManager : MonoBehaviour
{

    public Text labelName;
    public Text reputation;
    public Transform moneyParentObject;
    public Transform xpParentObject;
    void Start()
    {
        Text moneyText = moneyParentObject.GetComponentInChildren<Text>();
        moneyText.text = stateManager.moneyOnHand.ToString();

        Text xpText = xpParentObject.GetComponentInChildren<Text>();
        xpText.text = stateManager.xp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Text moneyText = moneyParentObject.GetComponentInChildren<Text>();
        moneyText.text = stateManager.moneyOnHand.ToString();

        labelName.text = stateManager.labelName;
        reputation.text = "Rep: "+stateManager.reputation.ToString();

        Text xpText = xpParentObject.GetComponentInChildren<Text>();
        xpText.text = stateManager.xp.ToString();

    }

    public void exitToCity()
    {

        SceneManager.LoadScene("cityView");
    }
}
