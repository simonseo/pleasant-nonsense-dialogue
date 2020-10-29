using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class SentenceModel
{
    [Tooltip("Name of speaker")]
    public string speakerName;

    // TODO this should be replaced with a string so that it's easier to edit in a text editor.
    // couple that with a getter that gets the Sprite from the images folder by name. 
    public Sprite standingCg;

    [Tooltip("The sentence being spoken")]
    [TextArea(3,10)]
    public string speechSentence;

    [Tooltip("Actions that the player can select")]
    [TextArea(3,10)]
    public string[] selectionOptions;

    // if HasSelections, show both the sentence and the options but not "nextline" button
    // else show only nextline button
    public bool HasSelection {
        get {
            return selectionOptions.Length > 0;
        }
    }

    // names of condition flags. All of these need to be True for the sentence.
    // the actual flags are saved in DataModel.
    public string[] conditionFlags;
    public bool HasFlags {
        get => conditionFlags.Length > 0;
    }
    public bool AllFlagsTrue {
        get {
            foreach (string flag in conditionFlags)
            {
                if (!DataModel.current.GetPropertyValue<bool>(flag)) {
                    return false;
                }
            }
            return true;
        }
    }

    // names of eventlisteners that will fire
    public string[] eventTriggers;
    public bool HasTriggers {
        get => eventTriggers.Length > 0;
    }
    // should dialogue manager invoke the eventlisteners? yes.
    // where should the triggers be saved? "TriggerManager"?




    
}
