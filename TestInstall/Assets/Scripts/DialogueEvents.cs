using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEvents
{
    private static DialogueEvents _current;
    public static DialogueEvents current
    {
        get
        {
            if (_current == null)
            {
                _current = new DialogueEvents();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    // create a UnityAction callback from provided method name.
    // to be used by DialogueManager
    public static UnityAction CreateCallback(string methodName, string arg) {
        List<object> args = new List<object> {arg};
        return () => current.InvokeMethod(methodName, args);
    }
    public static UnityAction CreateCallback(string methodName) {
        List<object> args = new List<object> {};
        return () => current.InvokeMethod(methodName, args);
    }
    
    // Invokes method in this class by name
    public void InvokeMethod(string methodName, List<object> args)
    {
        System.Type type = GetType();
        Debug.Assert(type != null);
        System.Reflection.MethodInfo method = type.GetMethod(methodName);
        Debug.Assert(method != null, $"method {methodName} could not be found in {type.Name}");
        method.Invoke(this, args.ToArray());
    }   
    

    // list methods that will be fired when user selects an option
    public void branchShowSelection()
    {
        // "branching based on internal state. either leads to dialogues[1] or dialogues[4]"
        DialogueManager manager = GameObject.FindObjectOfType<DialogueManager>();
        if (DataModel.current.FlagSilverCoinAtLeast1) {
            manager.StartDialogue(1);
        }
        else {
            manager.StartDialogue(4);
        }
    }

    // requires that index is sent
    public void branchPlayGame(string choice)
    {
        // "branching based on user choice. either leads to dialogues[2] or dialogues[3]"
        // deduct 1 silver
        DialogueManager manager = GameObject.FindObjectOfType<DialogueManager>();
        if (choice == "게임을 한다") {
            DataModel.current.silverCoin -= 1;
            manager.StartDialogue(2);
        }
        else {
            manager.StartDialogue(3);
        }

    }

    public void gambling1(string choice)
    {
        DataModel.current.gamblingChoices[0] = int.Parse(choice);
    }

    public void gambling2(string choice)
    {
        DataModel.current.gamblingChoices[1] = int.Parse(choice);
    }

    public void gambling3(string choice)
    {
        DataModel.current.gamblingChoices[2] = int.Parse(choice);
    }

    public void branchGamblingConclusion() {
        // "merge branches. leads to dialogues[5]"
        DialogueManager manager = GameObject.FindObjectOfType<DialogueManager>();
        manager.StartDialogue(5);
    }
}
