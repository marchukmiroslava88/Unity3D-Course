using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using KnightInBorderlands.Scripts.Data;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public static class SaveSystem
    {
        public static void SavePlayer()
        {
            var bf = new BinaryFormatter(); 
            var file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
            var data = new PlayerData();
            bf.Serialize(file, data);
            file.Close();
            
            Debug.Log($"Game data saved! {data._position}, {data._hp}, {data._inventoryItem}");
        }

        public static PlayerData LoadPlayer()
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
                var data = (PlayerData)bf.Deserialize(file);
                file.Close();
           
                Debug.Log($"Game data loaded!, {data}");
                return data;
            }

            Debug.LogError("There is no save data!");
            return null;
        }
    }
}