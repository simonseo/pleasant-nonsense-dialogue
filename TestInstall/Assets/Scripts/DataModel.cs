using UnityEngine;


// singleton class that holds data that records progress of the game using flags and values

[System.Serializable]
public class DataModel
{
    private static DataModel _current;
    public static DataModel current
    {
        get
        {
            if (_current == null)
            {
                _current = new DataModel();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public static T GetPropertyValue<T>(object obj, string propName)
    {
        return (T)obj.GetType().GetProperty(propName).GetValue(obj, null);
    }

    public T GetPropertyValue<T>(string propName)
    {
        return (T)this.GetType().GetProperty(propName).GetValue(this, null);
    }

    
    public int num1;
    public bool FlagNum1GreaterThan10
    {
        get
        {
            return num1 > 10;
        }
    }
    public int num2;
    public bool FlagNum2EqualTo5
    {
        get
        {
            return num2 == 5;
        }
    }
}