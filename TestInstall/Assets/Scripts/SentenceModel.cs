using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class SentenceModel
{
    [Tooltip("Name of speaker")]
    public string name;

    public Sprite standingCg;

    [Tooltip("The sentence being spoken")]
    [TextArea(3,10)]
    public string speech;

    [Tooltip("Actions that the player can select")]
    [TextArea(3,10)]
    public string[] options;

    // if HasOptions, show both the sentence and the options    
    public bool HasOptions {
        get {
            return options.Length > 0;
        }
    }

    // names of condition flags. All of these need to be True for the sentence.
    // the actual flags are saved in DataModel.
    public string[] flags;
    public bool HasFlags {
        get => flags.Length > 0;
    }
    public bool AllFlagsTrue {
        get {
            foreach (string flag in flags)
            {
                if (!DataModel.current.GetPropertyValue<bool>(flag)) {
                    return false;
                }
            }
            return true;
        }
    }

    // names of eventlisteners that will fire
    public string[] triggers;
    public bool HasTriggers {
        get => triggers.Length > 0;
    }
    // should dialogue manager invoke the eventlisteners?
    // where should the triggers be saved?




    
}
