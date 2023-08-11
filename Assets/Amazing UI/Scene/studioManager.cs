using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class studioManager : MonoBehaviour
{
    public Transform parentObject;
    public Transform parentObjectA;
    public GameObject prefab;
    public GameObject prefabArtist;
    public Text textArtistNameStudio;
    public Text textDescriptionStudio;
    public Dropdown genreDropDown;
    public Button albumToggle;
    public Button singleToggle;
    public string recordType;
    public Slider sliderSong;
    public Slider sliderProduction;
    public Slider sliderMastering;
    public Image xpMeter;

    public float totalXP;
    public float remainingXP;
    float maxValue;

    public Slider[] Sliders;

    public float MaximumTotal;


    public bool ReduceOthers;

    void Reset()
    {
        MaximumTotal = 1.0f;
    }

    float LastMaximumTotal;
    float[] LastSliderValues;

    void SetSliderGuarded(Slider slider, float value)
    {
        if (value < slider.minValue) value = slider.minValue;
        if (value > slider.maxValue) value = slider.maxValue;
        slider.value = value;
    }


    void Start()
    {

        int artistIndex = 0;
        MaximumTotal = 2.0f;



        for (int i = 0; i < stateManager.artistListOwned.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefabArtist, randomPos, Quaternion.identity, parentObject);

            

        }


        for (int i = 0; i < stateManager.artistListOwned.Count; i++)
        {
            Vector3 randomPos = new Vector3(0, 0, 0f);
            Instantiate(prefab, randomPos, Quaternion.identity, parentObjectA);

            

        }

        Debug.Log("asdas"+ stateManager.genList[4]);
        genreDropDown.ClearOptions();
        if (stateManager.genList is not null)
        {
            genreDropDown.AddOptions(stateManager.genList);

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
              //  x++;
             //   a = 0;
            }

            ListenerHandler(button, x);

            x++;
        }




        ReduceOthers = true;
        AddSliderToArray(sliderSong);
        AddSliderToArray(sliderMastering);
        AddSliderToArray(sliderProduction);

        sliderSong.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        sliderMastering.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        sliderProduction.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

    }

    private void OnSliderValueChanged()
    {

        float sliderSum = sliderSong.value + sliderMastering.value + sliderProduction.value;

        // Update the progress bar value
        xpMeter.fillAmount = MaximumTotal - sliderSum;

    }

    private void AddSliderToArray(Slider slider)
    {
        // Create a new array with increased size to accommodate the new Slider
        Slider[] newArray = new Slider[Sliders.Length + 1];

        // Copy the existing Sliders to the new array
        for (int i = 0; i < Sliders.Length; i++)
        {
            newArray[i] = Sliders[i];
        }

        // Add the new Slider to the last position in the new array
        newArray[newArray.Length - 1] = slider;

        // Replace the original array with the new array
        Sliders = newArray;
    }


    public void singleValueChanged()
     {

         Image[] singleImage = singleToggle.GetComponents<Image>();

         foreach (Image image in singleImage)
         {
             Debug.Log(image.name);
             image.color = new Color32(142, 208, 255,255);

         }

         Image[] albumImage = albumToggle.GetComponents<Image>();

         foreach (Image image in albumImage)
         {
             Debug.Log(image.name);
             image.color = new Color32(32, 32, 32, 255);

         }

         recordType = "Single";

         // singleImage.color = new Color(142, 207, 253);
         //Debug.Log("Single");

     }

     public void albumValueChanged()
     {


         Image[] singleImage = singleToggle.GetComponents<Image>();

         foreach (Image image in singleImage)
         {
             Debug.Log(image.name);
             image.color = new Color32(32, 32, 32, 255);

         }

         Image[] albumImage = albumToggle.GetComponents<Image>();

         foreach (Image image in albumImage)
         {
             Debug.Log(image.name);

             image.color = new Color32(142, 208, 255, 255);
         }

         recordType = "Album";

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
            textDescriptionStudio.text = "Rating: " + stateManager.artistListOwned[index].artistRating + "  Genre: " + stateManager.artistListOwned[index].artistGenre + "\n\n" + stateManager.artistListOwned[index].artistDesc;
            textArtistNameStudio.text = stateManager.artistListOwned[index].artistName;
         
        }

    }

        // Update is called once per frame
        void Update()
    {

 
        if (MaximumTotal != LastMaximumTotal)
        {
            LastSliderValues = null;
            LastMaximumTotal = MaximumTotal;
        }

        // is this our first time or did the count of sliders change?
        if (LastSliderValues == null || LastSliderValues.Length != Sliders.Length)
        {
            LastSliderValues = new float[Sliders.Length];

            float currentTotal = 0.0f;
            for (int i = 0; i < Sliders.Length; i++)
            {
                LastSliderValues[i] = Sliders[i].value;
                currentTotal += Sliders[i].value;
            }

          

            if (currentTotal > MaximumTotal)
            {
                Debug.LogWarning("Total already greater than max at start!");

                if (ReduceOthers)
                {
                    for (int i = 0; i < Sliders.Length; i++)
                    {
                        float reducedValue = (Sliders[i].value * MaximumTotal) / currentTotal;
                        SetSliderGuarded(Sliders[i], reducedValue);
                    }
                }
            }
        }

        // check and limit
        {
            bool adjusted = false;
            for (int i = 0; i < Sliders.Length; i++)
            {
                if (Sliders[i].value != LastSliderValues[i])
                {
                    if (!adjusted)
                    {
                        // tally others
                        float othersTotal = 0.0f;
                        for (int j = 0; j < Sliders.Length; j++)
                        {
                            if (i != j)
                            {
                                othersTotal += Sliders[j].value;
                            }
                        }

                        // limit?
                        if (othersTotal + Sliders[i].value > MaximumTotal)
                        {
                            adjusted = true;        // an adjustment has happend, don't do any other this frame

                            bool doLimiting = true;

                            if (ReduceOthers)
                            {
                                // we will bring all the others down to fit, if possible
                                float overAmount = (othersTotal + Sliders[i].value) - MaximumTotal;

                                // we need to reduce the others by overAmount... can we?
                                if (overAmount < othersTotal)
                                {
                                    doLimiting = false;

                                    // proportionally reduce the others
                                    for (int j = 0; j < Sliders.Length; j++)
                                    {
                                        if (i != j)
                                        {
                                            float reducedValue = Sliders[j].value - (Sliders[j].value * overAmount) / othersTotal;
                                            SetSliderGuarded(Sliders[j], reducedValue);
                                        }
                                    }
                                }
                                else
                                {
                                    // we cannot... therefore we must limit this one
                                    doLimiting = true;
                                    // meanwhile drive all the others to their minvaule and retotal
                                    othersTotal = 0.0f;
                                    for (int j = 0; j < Sliders.Length; j++)
                                    {
                                        if (i != j)
                                        {
                                            Sliders[j].value = Sliders[j].minValue;
                                            othersTotal += Sliders[j].value;
                                        }
                                    }
                                }
                            }

                            // we either ar not reducing, OR we were unable to reduce, so fall back to limit
                            if (doLimiting)
                            {
                                // we will prevent this slider from causing the total to exceed
                                float limited = MaximumTotal - othersTotal;
                                SetSliderGuarded(Sliders[i], limited);
                            }
                        }
                    }

                          
                    LastSliderValues[i] = Sliders[i].value;
                }
            }
        }

     

    }





    public void exitToCity()
    {

        SceneManager.LoadScene("cityView");
    }

}
