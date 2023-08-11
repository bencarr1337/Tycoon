using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class mainMenu : MonoBehaviour
{
    public Text textLabelName;

    void Start()
    {

        GameObject.Find("newGameModal").transform.localScale = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void clickNew()
    {


        GameObject.Find("newGameModal").transform.localScale = new Vector3(1, 1, 1);
        

    }

    public void loadGame()
    {

        stateManager.isNewGame = false;
        stateManager.isLoadGame = true;
        SceneManager.LoadScene("cityView");

    }


    public void acceptNewGame()
    {

        if (textLabelName.text == "")
        {


        }
        else
        {
            stateManager.isLoadGame = false;
            stateManager.labelName = textLabelName.text;
            stateManager.isNewGame = true;
            SceneManager.LoadScene("cityView");

        }

    }


    public void clickCancel()
    {

        GameObject.Find("newGameModal").transform.localScale = new Vector3(0, 0, 0);
    }
}
