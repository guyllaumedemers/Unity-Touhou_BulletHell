using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

public class Tool
{
    public static void XMLSerialization_KVParray(string path, string child, IDictionary<int, Vector3[]> dict)
    {
        if (File.Exists(path)) AppendXML(path, child, LastEntry(path, child), dict);
        else
        {
            XElement xel = new XElement("root", dict.Select(kv => new XElement("waypoints", new XAttribute("id", kv.Key), dict[kv.Key].Select(x => new XElement("position", x)))));
            if (xel != null) xel.Save(path, SaveOptions.None);
            else Debug.LogWarning("dictionary couldnt be parse to xml");
        }
    }

    private static void AppendXML(string path, string child, XElement lastXel, IDictionary<int, Vector3[]> dict)
    {
        int id = Int32.Parse(lastXel.Attribute("id").Value);
        var file = XDocument.Load(path);
        foreach (var key in dict.Keys.Where(key => key > Int32.Parse(lastXel.Attribute("id").Value)))
        {
            XElement xel = new XElement(child, new XAttribute("id", key), dict[key].Select(x => new XElement("position", x)));
            file.Root.Element(child).AddAfterSelf(xel);
        }
        file.Save(path);
    }

    private static XElement LastEntry(string path, string child)
    {
        return XDocument.Load(path).Root.Elements(child).Last();
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

        foreach (var el in rootElement.Elements("waypoints"))
        {
            List<Vector3> vl = (el.Elements("position").Select(xa => Utilities.StringParseToVector3(xa.Value))).ToList();
            dict.Add(Int32.Parse(el.Attribute("id").Value), vl.ToArray());
        }
        return dict;
    }
}
