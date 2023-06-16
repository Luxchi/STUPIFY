using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerprefname : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField nameInputField;
    void Start()
    {
        nameInputField.text = PlayerPrefs.GetString("NamePass");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
