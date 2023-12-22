using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


namespace Backend
{
    public class SaveManager : MonoBehaviour
    {
        public SaveData SaveData;
        
        public bool DataIsLoaded;

        const string saveDataFileName = "userSaveData.json";

//        public event Action<bool> OnLoadingSuccess = delegate { };

        static SaveManager instance;

        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SaveManager>();
                }

                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            LoadSaveData();
        }


        // IEnumerator AutoSave()
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(3);
        // WriteSaveData(saveData, saveDataFileName);
        // }


        [Button]
        void NewGame()
        {
            SaveData.Clear();

            WriteSaveData(SaveData, saveDataFileName);
        }


        public void WriteSaveData(SaveData data, string fileName)
        {
            SaveData.LastSaveTimeStampString = DateTime.Now.ToString();

            string dataAsJson = JsonUtility.ToJson(data);
            FileManager.WriteToFile(fileName, dataAsJson);
        }


        public void LoadSaveData()
        {
            string dataAsJson = FileManager.ReadFile(saveDataFileName);

            //never saved before
            if (dataAsJson == null)
            {
            }
            else
            {
                JsonUtility.FromJsonOverwrite(dataAsJson, SaveData);
            }

            DataIsLoaded = true;
            //   SaveDataReady(Data);
        }
    }
}