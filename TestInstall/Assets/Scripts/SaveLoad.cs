using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] Text AmountText1 = null;
    [SerializeField] Text AmountText2 = null;
    void Start()
    {
        Button saveButton = GameObject.Find("Save").GetComponent<Button>();
        Button loadButton = GameObject.Find("Load").GetComponent<Button>();

        loadButton.onClick.AddListener(OnLoadGame);
        saveButton.onClick.AddListener(OnSaveGame);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSaveGame() {
        SerializationManager.Save("testsave", DataModel.current);
    }
    public void OnLoadGame() {
        DataModel.current = (DataModel)SerializationManager.Load("testsave");
        UpdateUI();
    }

    public void UpdateUI() {
        AmountText1.text = DataModel.current.num1.ToString();
        AmountText2.text = DataModel.current.num2.ToString();
    }
}
