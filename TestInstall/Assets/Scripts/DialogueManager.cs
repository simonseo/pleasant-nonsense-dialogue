using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{

    private Queue<SentenceModel> Sentences;
    private DialogueModel[] Dialogues;

    // TODO These should be the fields of the dialogue panel prefab
    GameObject DialogueModal = null; // Dialogue Modal
    Image StandingCG = null; // Image on screen
    Text NameText = null; // Name on screen
    Text SentenceText = null; // Sentence on screen
    Button NextLineButton = null;
    Transform OptionPanel = null; // option panel on screen
    [SerializeField] GameObject OptionButton = null; // option button prefab

    // 
    // [SerializeField] // Options

    void Start()
    {
        Sentences = new Queue<SentenceModel>();
        Transform canvas = GameObject.Find("Canvas").transform;
        DialogueModal = Utils.RecursiveFind(canvas, "Dialogue Panel").gameObject;

        OptionPanel = Utils.RecursiveFind(canvas, "Option Panel");
        StandingCG = Utils.RecursiveFind(canvas, "Image").GetComponent<Image>();
        NameText = Utils.RecursiveFind(canvas, "Name Textbox").GetComponent<Text>();
        SentenceText = Utils.RecursiveFind(canvas, "Sentence Textbox").GetComponent<Text>();
        NextLineButton = Utils.RecursiveFind(canvas, "Next Line Button").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void RegisterDialogues(DialogueModel[] newDialogues)
    {
        Debug.Assert(newDialogues.Length > 0, "Dialogues is empty");
        Dialogues = newDialogues;
        StartDialogue(0);
    }

    internal void StartDialogue(int dialogueIndex)
    {
        DialogueModal.SetActive(true);
        Sentences.Clear();
        foreach (SentenceModel sentence in Dialogues[dialogueIndex].dialogue)
        {
            Sentences.Enqueue(sentence);
        }

        Debug.Log("conversation with " + NameText.text + " started.");
        NextSentence();
    }


    public void NextSentence()
    {
        if (Sentences.Count == 0)
        {
            Debug.Log("conversation with " + NameText.text + " ended.");
            DialogueModal.SetActive(false);
            return;
        }
        // TODO check condition for sentence here?
        SetSentence(Sentences.Dequeue());

    }


    private void SetSentence(SentenceModel sentence)
    {
        NextLineButton.onClick.RemoveAllListeners();
        Utils.DestroyChildren(OptionPanel);
        if (sentence.HasSelection)
        {
            SetOptions(sentence.selectionOptions, sentence.eventTriggers);
            // TODO deactivate next line button here
        }
        else
        {
            SetTriggers(sentence.eventTriggers);
            // TODO activate next line button here
        }
        SetName(sentence.speakerName);
        SetCG(sentence.standingCg);
        SetSpeech(sentence.speechSentence);

    }

    private void SetTriggers(string[] eventTriggers)
    {
        foreach (string triggerName in eventTriggers)
        {
            UnityAction listener = DialogueEvents.CreateCallback(triggerName);
            NextLineButton.onClick.AddListener(listener);
        }
    }

    private void SetOptions(string[] selectionOptions, string[] eventTriggers)
    {
        // add listeners for all N options x M triggers
        foreach (string option in selectionOptions)
        {
            GameObject optionButton = Instantiate(OptionButton, OptionPanel);
            optionButton.GetComponentInChildren<Text>().text = option;
            // optionButton.GetComponent<Button>().onClick.AddListener(NextSentence);

            foreach (string triggerName in eventTriggers)
            {
                UnityAction listener = DialogueEvents.CreateCallback(triggerName, option);
                optionButton.GetComponent<Button>().onClick.AddListener(listener);
            }

        }

    }

    private void SetName(string name)
    {
        NameText.text = name;
    }

    private void SetCG(Sprite cg)
    {
        StandingCG.sprite = cg;
    }

    private void SetSpeech(string speechText)
    {
        SentenceText.text = speechText;
    }
}
