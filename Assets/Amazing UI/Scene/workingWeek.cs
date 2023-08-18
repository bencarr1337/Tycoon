using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class workingWeek : MonoBehaviour
{

    private float fillSpeed = 0.001f;
    public Image workingWeekBar;
    public bool startProgress;
    public Text status;
    private int frameIterator;

    public  List<string> weekList
    {
        get;
        set;
    }

    void Start()
    {
     
        startProgress = false;
        var weekFile = Resources.Load<TextAsset>("Wordlists/workingWeek");
        var weekContent = weekFile.text;
        var weekWords = weekContent.Split("\n");
        weekList = new List<string>(weekWords);
        frameIterator = 100;
       



    }

    public void clickWorkingWeekEnd()
    {
        startProgress = true;
        workingWeekBar.fillAmount = 0.0f;

        GameObject.Find("FinishWorkWeekModal").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("FinishWorkWeekModalNext").transform.localScale = new Vector3(1, 1, 1);

    }

    public void ClickFinal()
    {
        GameObject.Find("FinishWorkWeekModalNews").transform.localScale = new Vector3(0, 0, 0);
        stateManager.endWeekModalShowing = false;

        incrementWeek();
    }


    public void incrementWeek()
    {
        stateManager.week++;

        //calculate new single/album (ie last weeks) reviews / star ratings
        //Inject single/album into chart at relevant position based on ratings
        //calculate back catalogue royalties 
        //calculate single release royalties 
        //calculate album release royalties 
        //calculate XP Increase 
        



    }


    // Update is called once per frame
    void Update()
    {
        if (startProgress)
        {

            if (workingWeekBar.fillAmount < 1.0f)
            {
          
                workingWeekBar.fillAmount += fillSpeed;
                if (frameIterator == 200)
                {
                    var random = new System.Random();
                    int weekIndex = random.Next(weekList.Count);
                    var weekFinal = weekList[weekIndex];
                    status.text = weekFinal;
                    frameIterator = 0;

                }
               
                frameIterator++;

            }
            else
            {


                GameObject.Find("FinishWorkWeekModalNext").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find("FinishWorkWeekModalNews").transform.localScale = new Vector3(1, 1, 1);
                startProgress = false;
              

            }
       
            

        }

    }
}
