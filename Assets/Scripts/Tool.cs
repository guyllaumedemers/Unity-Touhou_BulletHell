using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

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

        XElement rootElement = XElement.Load(path);
        IDictionary<int, Vector3[]> dict = new Dictionary<int, Vector3[]>();

        foreach (var el in rootElement.Elements())
        {
            List<Vector3> vl = new List<Vector3>();
            foreach (var xa in el.Attributes())
            {
                if (!xa.Name.LocalName.Equals("id")) vl.Add(Utilities.StringTovec3(xa.Value));
            }
            dict.Add(Int32.Parse(el.Attribute("id").Value), vl.ToArray());
        }
        return dict;
    }
}
