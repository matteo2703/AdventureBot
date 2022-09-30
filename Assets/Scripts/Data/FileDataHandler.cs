using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class FileDataHandler
{
    readonly private string dataDirPath = "";
    readonly private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GenericGameData Load(string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GenericGameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                //read data from the file
                using FileStream stream = new(fullPath, FileMode.Open);
                using StreamReader reader = new(stream);
                string dataToLoad = reader.ReadToEnd();

                //deserialize data from Json
                loadedData = JsonUtility.FromJson<GenericGameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.Log("Errore nel tentativo di caricare i dati da " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GenericGameData data, string profileId)
    {
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            //create save directory if doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize data into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //write data into the file
            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch(Exception e)
        {
            Debug.Log("Errore nel tentativo di salvare i dati in " + fullPath + "\n" + e);
        }
    }

    public void Delete(string profileId)
    {
        if (profileId == null)
            return;

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            if (File.Exists(fullPath))
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
        }
        catch(Exception e)
        {
            Debug.Log("Errore nel tentativo di eliminare i dati in " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GenericGameData> LoadAllProfiles()
    {
        Dictionary<string, GenericGameData> profileDictionary = new();

        //loop over all directory
        IEnumerable<DirectoryInfo> dirInfo = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dir in dirInfo)
        {
            string profileId = dir.Name;

            //check if the folder is a profile
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
                continue;

            //load the game data for this profile and put in the dictionary
            GenericGameData profileData = Load(profileId);
            //check if profile has data
            if (profileData != null)
                profileDictionary.Add(profileId, profileData);
        }

        return profileDictionary;
    }
}
