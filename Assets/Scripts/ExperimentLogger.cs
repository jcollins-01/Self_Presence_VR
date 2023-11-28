using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentLogger : MonoBehaviour
{
    private string filePath;
    private bool startWriting;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        startWriting = false;
        filePath = GetFilePath();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    string GetFilePath()
    {
        string path = Application.persistentDataPath;
        string fileName = System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_Log.csv";
        string fullPath = System.IO.Path.Combine(path, fileName);
        return fullPath;
    }

    public void RecordKey(string key, string keyPurpose)
    {
        System.DateTime timeReal = System.DateTime.Now;
        float timeApp = Time.time;
        RecordKey(timeReal, timeApp, key, keyPurpose);
    }
    
    void RecordKey(System.DateTime timeReal, float timeApp, string key, string keyPurpose)
    {
        string line = counter + "," + timeReal.ToString() + "," + timeApp.ToString() + "," + key + "," + keyPurpose + System.Environment.NewLine;
        if(!startWriting)
        {
            // Record header line
            string header = "Index,Real-time,App-time,Key,Key-Purpose" + System.Environment.NewLine;
            System.IO.File.WriteAllText(filePath, header);
            startWriting = true;
        }
        System.IO.File.AppendAllText(filePath, line);

        counter++;
    }
}
