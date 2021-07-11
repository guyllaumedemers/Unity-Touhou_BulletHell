using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

public class Tool
{
    public static void XMLSerialization_KVParray<T, K>(string path, IDictionary<T, K[]> dict, params string[] childs) where T : struct where K : struct
    {
        using FileStream fstream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        if (fstream.Length <= 0)
        {
            if (fstream == null)
            {
                Debug.LogWarning("stream failed");
                return;
            }
            int i = 0;
            XElement xel = new XElement("root", dict.Select(kv => new XElement("waypoints", new XAttribute("id", kv.Key), dict[kv.Key].Select(x => new XAttribute("pos_" + ++i, x)))));
            if (xel != null) xel.Save(fstream);
            else Debug.LogWarning("dictionary couldnt be parse to xml");
        }
        else AppendXML(fstream, path, childs);
        fstream.Close();
    }

    private static void AppendXML(Stream stream, string path, params string[] childs)
    {
        var file = XDocument.Load(stream);
        file.Element(childs[0]).Add(new XElement(childs[1]));
        file.Save(stream);
    }

    public static IDictionary<int, Vector3[]> XMLDeserialization_KVParray(string path)
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
            List<Vector3> vl = (el.Attributes().Where(xa => !xa.Name.LocalName.Equals("id")).Select(xa => Utilities.StringParseToVector3(xa.Value))).ToList();
            dict.Add(Int32.Parse(el.Attribute("id").Value), vl.ToArray());
        }
        return dict;
    }
}
