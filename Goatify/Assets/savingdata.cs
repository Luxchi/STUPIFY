using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class savingdata : MonoBehaviour
{
    public InputField nameInputField;
    public InputField ageInputField;

    //public Text jsonText; // Reference to the Text component to display JSON data
    //public Text nameText; // Reference to the Text component for displaying name
    //public Text ageText; // Reference to the Text component for displaying age
    public GameObject dataTextPrefab; // Reference to the Text prefab
    public Transform dataTextContainer; // Reference to the parent Transform for the created Text objects
    private string dataFilePath;
    private List<Data> dataList;

    private void Awake()
    {
        dataFilePath = Path.Combine(Application.dataPath, "GoatInfo.json");
    }

    private void Start()
    {
        LoadData();
        ExampleUsage();
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(new DataListWrapper(dataList));
        File.WriteAllText(dataFilePath, json);
    }

    private void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);
            DataListWrapper dataListWrapper = JsonUtility.FromJson<DataListWrapper>(json);
            dataList = dataListWrapper.dataList;
        }
        else
        {
            dataList = new List<Data>();
        }
    }

    public void AddData()
    {
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);

        Data newData = new Data(name, age);
        dataList.Add(newData);

        SaveData();
    }

    // Example usage
    private void ExampleUsage()
    {
        // Display the JSON data
        //string json = JsonUtility.ToJson(new DataListWrapper(dataList), true);
        //Debug.Log(json);

        // Display the JSON data in the Text object
        //string json = JsonUtility.ToJson(new DataListWrapper(dataList), true);
        //jsonText.text = json;

        //string nameData = "";
        //string ageData = "";

        //foreach (Data data in dataList)
        //{
        //    nameData += data.name + "\n";
        //    ageData += data.age.ToString() + "\n";
        //}

        //nameText.text = nameData;
        //ageText.text = ageData;

        
        // Clear the existing Text objects
    foreach (Transform child in dataTextContainer)
        {
            Destroy(child.gameObject);
        }

    float yOffset = 0f; // Vertical offset between Text objects

    // Create Text objects for each data entry
    foreach (Data data in dataList)
        {
            // Instantiate the dataTextPrefab
            GameObject dataTextObject = Instantiate(dataTextPrefab, dataTextContainer);
            Text dataText = dataTextObject.GetComponent<Text>();
            dataText.text = "Name: " + data.name +" "+ "Age: " + data.age;

            // Position the Text object
            RectTransform textTransform = dataTextObject.GetComponent<RectTransform>();
            textTransform.anchoredPosition = new Vector2(0f, yOffset);

            // Create a button as a child of the Text object
            GameObject buttonObject = new GameObject("Button");
            buttonObject.transform.SetParent(dataTextObject.transform, false);

            // Add the button component and set its properties
            Button button = buttonObject.AddComponent<Button>();
            button.onClick.AddListener(() => OnButtonClick(data.name));

            // Add a Text component to the button and set its properties
            Text buttonText = buttonObject.AddComponent<Text>();
            buttonText.text = "Click";
            buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontSize = 40; // Set the text size here
            

             // Position and size the button within the Text object
                 RectTransform buttonTransform = buttonObject.GetComponent<RectTransform>();
                buttonTransform.anchorMin = new Vector2(0f, 0f); // Align top-left
                buttonTransform.anchorMax = new Vector2(0f, 0f); // Align top-left
                buttonTransform.pivot = new Vector2(0f, 0f); // Set pivot to top-left
                buttonTransform.anchoredPosition = new Vector2(700f, 75f); // Set the position of the button

                buttonTransform.sizeDelta = new Vector2(150f, 80f); // Set the button size here

            // Increase the vertical offset
            yOffset -= 100f; // Adjust this value to control the spacing between Text objects
        }
    
    }

    
    private void OnButtonClick(string name)
    {
        PlayerPrefs.SetString("NamePass", name);
        SceneManager.LoadScene("UpdateGoat");

        // inputField.text = PlayerPrefs.GetString("INamePassnput");
        
    }
    //udpdating data 
    public int GetIndexByName(string name)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].name == name)
            {
                return i; // Return the index of the matching entry
            }
        }
        
        return -1; // Return -1 if the entry with the specified name is not found
    }
    public Button updateButton;
    public void SearchAndDisplayData()
    {   
        
        string name = nameInputField.text;
        int index = GetIndexByName(name);
        
        if (index >= 0)
        {
            Data data = dataList[index];
            //nameText.text = "Name: " + data.name;
            //ageText.text = "Age: " + data.age.ToString();
            
            // Set the input field values to the current data
            nameInputField.text = data.name;
            ageInputField.text = data.age.ToString();
            
            // Set the update button to update the data at the current index
            updateButton.onClick.RemoveAllListeners();
            updateButton.onClick.AddListener(() => UpdateData(index));
        }
        else
        {
            //nameText.text = "Name: Not Found";
            //ageText.text = "Age: Not Found";
            
            // Clear the input field values
            nameInputField.text = "";
            ageInputField.text = "";
            
            // Set the update button to do nothing (no data to update)
            updateButton.onClick.RemoveAllListeners();
        }
    }

    public void UpdateData(int index)
    {
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);

        // Update the data at the specified index
        dataList[index].name = name;
        dataList[index].age = age;

        SaveData();
    }
}

[System.Serializable]
public class Data
{
    public string name;
    public int age;

    public Data(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

[System.Serializable]
public class DataListWrapper
{
    public List<Data> dataList;

    public DataListWrapper(List<Data> dataList)
    {
        this.dataList = dataList;
    }
}
