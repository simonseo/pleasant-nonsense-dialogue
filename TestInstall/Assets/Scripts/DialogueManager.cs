using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<SentenceModel> sentences;

    // TODO These should be the fields of the dialogue panel prefab
    GameObject DialogueModal = null; // Dialogue Modal
    Image StandingCG = null; // Image on screen
    Text NameText = null; // Name on screen
    Text SentenceText = null; // Sentence on screen
    Transform OptionPanel = null; // option panel on screen
    [SerializeField] GameObject OptionButton = null; // option button prefab
    
    // 
    // [SerializeField] // Options

    void Start()
    {
        sentences = new Queue<SentenceModel>();
        OptionPanel = GameObject.Find("Option Panel").transform;
        StandingCG = GameObject.Find("Image Panel").GetComponentInChildren<Image>();
        NameText = GameObject.Find("Name Textbox").GetComponent<Text>();
        SentenceText = GameObject.Find("Sentence Textbox").GetComponent<Text>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void StartDialogue(SentenceModel[] dialogue)
    {
        DialogueModal.SetActive(true);

        
        sentences.Clear();
        foreach (SentenceModel sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        Debug.Log("conversation with " + NameText.text + " started.");
        NextSentence();
        
    }

    public void NextSentence()
    {
        if (sentences.Count == 0) {
            Debug.Log("conversation with " + NameText.text + " ended.");
            return;
        }
        SetSentence(sentences.Dequeue());

    }

    private void SetSentence(SentenceModel sentence)
    {

        SetName(sentence.speakerName);
        SetCG(sentence.standingCg);
        SetSpeech(sentence.speechSentence);
        
    }

    private void SetName(string name)
    {
        NameText.text = name;
    }

    private void SetCG(Sprite cg) {
        StandingCG.sprite = cg;
    }

    private void SetSpeech(string speechText) {
        SentenceText.text = speechText;
    }
}
