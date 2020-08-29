using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    Button TargetButton = null;
    [SerializeField] Text AmountText = null;
    [SerializeField] int IncrementAmount = 0;
    [SerializeField] int AmountModel = 0;


    void Start()
    {
        if (TargetButton == null) {
            TargetButton = gameObject.GetComponent<Button>();
        }
        // Text text = GameObject.Find("Text").GetComponentInChildren<Text>();
        // text.text = "Hello world";  
        Debug.Log("hello world");

        if (AmountModel == 1)
            TargetButton.onClick.AddListener(() => UpdateAmount(ref DataModel.current.num1));
        else
            TargetButton.onClick.AddListener(() => UpdateAmount(ref DataModel.current.num2));
    }

    void UpdateAmount(ref int amountModel) {
        amountModel += IncrementAmount;
        //update ui
        AmountText.text = amountModel.ToString();
    }
    


}
