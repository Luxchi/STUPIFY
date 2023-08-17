using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;


public class displayingdata : MonoBehaviour
{
    public Text nameText;
    public Text ageText;
    public Text birthText;
    public Text entryText;
    public Text weightText;
    public Text notesText;
    public Text breedText;
    public Text genderText;
    public Text obtainText;
    public Text stageText;
    public Text tagmotherText;
    public Text tagfatherText;

    private MyData myData;

    void Start()
    {
        //string jsonFilePath = "Assets/GoatInfo.json";
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "GoatInfo.json");

        // Read the JSON file
        string jsonString = File.ReadAllText(jsonFilePath);

        // Parse the JSON data
        myData = JsonUtility.FromJson<MyData>(jsonString);


        if (myData != null && myData.dataList != null)
        {
            int targetAge = PlayerPrefs.GetInt("AgePass");

            // Display the data based on the target age
            DisplayDataByAge(targetAge);
        }
        else
        {
            // If no data is available, display a message
            nameText.text = "No Data";
            ageText.text = "";
            birthText.text = "";
            entryText.text = "";
            weightText.text = "";
            notesText.text = "";
            breedText.text = "";
            genderText.text = "";
            obtainText.text = "";
            stageText.text = "";
            tagmotherText.text = "";
            tagfatherText.text = "";
        }
    }

    private void DisplayDataByAge(int targetAge)
{

    if (myData != null && myData.dataList != null)
    {
        // Find the data item with the target age
        DataArhive targetItem = myData.dataList.Find(item => item.age == targetAge);

        if (targetItem != null)
        {
            // Assign the data fields to text objects
            nameText.text = targetItem.name;
            ageText.text = targetItem.age.ToString();
            birthText.text = targetItem.birth;
            entryText.text = targetItem.entry;
            weightText.text = targetItem.weight;
            notesText.text = targetItem.notes;
            breedText.text = targetItem.breed;
            genderText.text = targetItem.gender;
            obtainText.text = targetItem.obtain;
            stageText.text =targetItem.stageG;
            tagmotherText.text =targetItem.tagmother;
            tagfatherText.text = targetItem.tagfather;
            
        }
        else
        {
            // If no data item with the target age is found, display a message
        
             nameText.text = "No Data";
            ageText.text = "";
            birthText.text = "";
            entryText.text = "";
            weightText.text = "";
            notesText.text = "";
            breedText.text = "";
            genderText.text = "";
            obtainText.text = "";
            stageText.text = "";
            tagmotherText.text = "";
            tagfatherText.text = "";
        }
    }
    else
    {
    
        // If no data is available, display a message
        nameText.text = "No Data";
            ageText.text = "";
            birthText.text = "";
            entryText.text = "";
            weightText.text = "";
            notesText.text = "";
            breedText.text = "";
            genderText.text = "";
            obtainText.text = "";
            stageText.text = "";
            tagmotherText.text = "";
            tagfatherText.text = "";
    }
}
public void EditScene(){
    PlayerPrefs.SetInt("AgePass", int.Parse(ageText.text));
    SceneManager.LoadScene("UpdateGoatF");
    }
}

[System.Serializable]
public class MyData
{
    public List<DataArhive> dataList;
}

[System.Serializable]
public class DataArhive
{
    public string name;
    public int age;
    public string birth;
    public string entry;
    public string weight;
    public string notes;
    public string breed;
    public string gender;
    public string obtain;
    public string stageG;
    public string tagfather;
    public string tagmother;
    // Add other fields to match your JSON structure
}