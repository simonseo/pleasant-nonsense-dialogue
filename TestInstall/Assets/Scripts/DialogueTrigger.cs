using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;


// at this point is equal to what DialogueModel should be doing.
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] SentenceModel[] dialogue = null;
    InputField DialogueName = null;

    string JsonStr {
        get {
           return JsonUtility.ToJson(this);
        }
    }

    private void Start() {
        DialogueName = GameObject.Find("DialogueNameInputField").GetComponent<InputField>();
    }

    public void LoadDialogue() {
        string fileName = "SampleDialogue";
        if (DialogueName.text.Length > 0) {
            fileName = DialogueName.text;
        }
        LoadDialogue(fileName);
    }


    public void LoadDialogue(string fileName) {
        string filePath = $"Assets/Resources/{fileName}.json";
        StreamReader reader = new StreamReader(filePath); 
        string json = reader.ReadToEnd();
        reader.Close();
        Debug.Log(json);
        JsonUtility.FromJsonOverwrite(json, this);
    }


    public void SaveDialogue() {
        string fileName = "SampleDialogue";
        if (DialogueName.text.Length > 0) {
            fileName = DialogueName.text;
        }
        SaveDialogue(fileName);
    }

    public void SaveDialogue(string fileName) {
        string filePath = $"Assets/Resources/{fileName}.json";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(filePath, false); // append=false
        writer.WriteLine(JsonStr);
        writer.Close();

        // below is for debugging purposes in UnityEditor

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(filePath);  // works only in unityeditor
        TextAsset asset = Resources.Load<TextAsset>(fileName);

        //Print the text from the file
        Debug.Log(asset.text);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            // if mouse is over UI
            return;
        }
        TriggerDialogue();
    }

    void TriggerDialogue()
    {
        DialogueManager manager = GameObject.FindObjectOfType<DialogueManager>();
        
        // Debug.Log(JsonUtility.ToJson(this)); // jsonifies serializable fields of DialogueTrigger
        // Debug.Log(JsonUtility.ToJson(dialogue)); // doesn't print anything?? cause it's an array not an object?
        // Debug.Log(JsonUtility.ToJson(dialogue[0])); // jsonifies first SentenceModel of dialogue
        manager.StartDialogue(dialogue);
    }
}
