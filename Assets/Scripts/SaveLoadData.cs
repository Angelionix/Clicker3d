using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    public void SaveRecords(List<PlayerRecord> records)
    {
        foreach (var record in records)
        {
            string place = record.Place.ToString();
            string values = $"{record.Name}_{record.Score}";
            PlayerPrefs.SetString(place, values);
        }
    }

    public List<PlayerRecord> LoadRecord(List<PlayerRecord> records)
    {
        if (PlayerPrefs.HasKey("0"))
        {
            for (int i = 0; i < 10; i++)
            {
                PlayerRecord rec = new PlayerRecord();
                string[] temp = (PlayerPrefs.GetString((i + 1).ToString()).Split('_'));
                rec.Name = temp[0];
                rec.Score = int.Parse(temp[1]);
                rec.Place = i + 1;
                records.Add(rec);
            }
            return records;
        }
        else
        {
            SaveRecords(records);
            return records;
        }
     }
}
