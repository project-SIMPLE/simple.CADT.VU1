using System.Collections.Generic;

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (var item in items)
        {
            dict[item.key] = item.value;
        }

        return dict;
    }
}