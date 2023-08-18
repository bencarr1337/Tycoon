using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class cityView : MonoBehaviour
{
    // Start is called before the first frame update
    public bool modalShowing;

   
    void Start()
    {
        
        GameObject.Find("FinishWorkWeekModal").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("FinishWorkWeekModalNext").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("FinishWorkWeekModalNews").transform.localScale = new Vector3(0, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {

    

    }

  

    public void finishWorkWeekClick()
    {
        stateManager.endWeekModalShowing = true;
   
     
     
       

    }

    public void cancelFinishWorkWeek()
    {
        stateManager.endWeekModalShowing = false;
        GameObject.Find("FinishWorkWeekModal").transform.localScale = new Vector3(0, 0, 0);
    }


    void OnMouseDown()
    {
        
      

        if (gameObject.name == "buildingApartment" && stateManager.endWeekModalShowing == false)

        {
            stateManager.endWeekModalShowing = true;
            GameObject.Find("FinishWorkWeekModal").transform.localScale = new Vector3(1, 1, 1);

            Debug.Log(modalShowing);

        }



        if (gameObject.name == "buildingOffice" && stateManager.endWeekModalShowing == false)
        {
            Debug.Log(modalShowing);

            SceneManager.LoadScene("officeScene");

        }

        if (gameObject.name == "buildingBank" && stateManager.endWeekModalShowing == false)
        {
            Debug.Log(gameObject.name);
            SceneManager.LoadScene("bankScene");


        }
    
        if (gameObject.name == "buildingStudio" && stateManager.endWeekModalShowing == false)
        {
           
            SceneManager.LoadScene("studioScene");


        }


    }
}
