using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace Backend
{
    public class SaveManager : MonoBehaviour
    {
        SaveData saveData;

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


        public SaveData GetSaveData()
        {
            if(saveData == null)
            {
                saveData = new SaveData();
            }
            
            return saveData;
        }
        

        [Button]
        public void WriteSaveData()
        {
            if(saveData == null)
            {
                saveData = new SaveData();
            }
            
            saveData.LastSaveTimeStampString = DateTime.Now.ToString();

            string dataAsJson = JsonUtility.ToJson(saveData);
            FileManager.WriteToFile(saveDataFileName , dataAsJson);
            
           
        }


        [Button]
        public void LoadSaveData()
        {
            string dataAsJson = FileManager.ReadFile(saveDataFileName);

         
            if (dataAsJson == null)
            {
                
            }
            else
            {
                JsonUtility.FromJsonOverwrite(dataAsJson, saveData);
            }

            DataIsLoaded = true;
            
         
            //   SaveDataReady(Data);
        }


       
    }
}