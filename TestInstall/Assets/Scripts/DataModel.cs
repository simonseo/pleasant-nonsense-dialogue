using UnityEngine;


// singleton class
[System.Serializable]
public class DataModel {
    private static DataModel _current;
    public static DataModel current { 
        get {
            if (_current == null) {
                _current = new DataModel();
            }
            return _current;
        }
        set {
            _current = value;
        }
    }

    public int num1;
    public int num2;

}