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
        if (File.Exists(path)) AppendXML(path, childs);
        else
        {
            int i = 0;
            XElement xel = new XElement("root", dict.Select(kv => new XElement("waypoints", new XAttribute("id", kv.Key), dict[kv.Key].Select(x => new XAttribute("pos_" + ++i, x)))));
            if (xel != null) xel.Save(path, SaveOptions.None);
            else Debug.LogWarning("dictionary couldnt be parse to xml");
        }
    }

    private static void AppendXML(string path, params string[] childs)
    {
        var file = XDocument.Load(path);
        file.Root.Element(childs[0]).AddAfterSelf(new XElement(childs[1]));
        file.Save(path);
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
