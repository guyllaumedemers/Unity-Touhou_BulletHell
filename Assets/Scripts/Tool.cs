using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

public class Tool
{
    public static void XMLSerialization_Array(string path, IDictionary<int, Vector3[]> dict)
    {
        if (File.Exists(path)) File.Delete(path);

        using FileStream fstream = File.OpenWrite(path);
        if (fstream == null)
        {
            Debug.LogWarning("stream was not created properly");
            goto STREAM_FAIL;
        }
        int i = 0, j = 0;
        XElement xel = new XElement("root", dict.Select(kv => new XElement("item_" + ++j, new XAttribute("id", kv.Key), dict[kv.Key].Select(x => new XAttribute("pos_" + ++i, x)))));
        if (xel != null) xel.Save(fstream);
        else Debug.LogWarning("elements couldn't be serialized");
        STREAM_FAIL:
        fstream.Close();
    }

    public static IDictionary<int, Vector3[]> XMLDeserialization_Array(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("file doesn't exist");
            return null;
        }
        using FileStream fstream = File.OpenRead(path);
        if (fstream == null)
        {
            Debug.LogWarning("stream couldn't be open");
        }
        XElement rootElement = XElement.Parse("<root><item></item></root>");
        IDictionary<int, Vector3[]> dict = new Dictionary<int, Vector3[]>();

        List<Vector3> list = new List<Vector3>();
        foreach (var xel in rootElement.Elements())
        {
            if (xel.Name.LocalName.Split('_')[0].Equals("item"))
            {
                foreach (var attributes in xel.Attributes())        // attributes are null for some reason
                {
                    if (attributes.Name.LocalName.Split('_')[0].Equals("pos")) list.Add(Utilities.StringTovec3(attributes.Value));
                }
                dict.Add(int.Parse(xel.Attribute("id").Value), list.ToArray());
                list.Clear();
            }
        }
        fstream.Close();
        return dict;
    }
}
