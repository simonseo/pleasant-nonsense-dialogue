using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<SentenceModel> sentences;

    // TODO These should be the fields of the dialogue panel prefab
    [SerializeField] GameObject DialogueModal = null; // Dialogue Modal
    [SerializeField] Image StandingCG = null; // Image
    [SerializeField] Text NameText = null; // Name
    [SerializeField] Text SentenceText = null; // Sentence
    
    // 
    // [SerializeField] // Options

    void Start()
    {
        sentences = new Queue<SentenceModel>();
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

        SetName(sentence.name);
        SetCG(sentence.standingCg);
        SetSpeech(sentence.speech);
        
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
