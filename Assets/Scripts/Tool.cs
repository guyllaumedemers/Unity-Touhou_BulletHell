using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

public class Tool
{

    // I dont like the redundancy of this class

#if VEC3
#endif

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

#if TUPLE
#endif

    public static void XMLSerialization_KVPTuple(string path, string child, IDictionary<int, Queue<(string, int)>> dict)
    {
        if (File.Exists(path)) AppendXML_Tuple(path, child, LastEntry(path, child), dict);
        else
        {
            Debug.Log(dict);
            XElement xel = new XElement("root", dict.Select(kv => new XElement("levels", new XAttribute("id", kv.Key), dict[kv.Key].Select(x => new XElement("wave", new XAttribute("unit_type", x.Item1), new XAttribute("count", x.Item2))))));
            if (xel != null) xel.Save(path, SaveOptions.None);
            else Debug.LogWarning("dictionary couldnt be parse to xml");
        }
    }

    private static void AppendXML_Tuple(string path, string child, XElement lastXel, IDictionary<int, Queue<(string, int)>> dict)
    {
        int id = Int32.Parse(lastXel.Attribute("id").Value);
        var file = XDocument.Load(path);
        foreach (var key in dict.Keys.Where(key => key > Int32.Parse(lastXel.Attribute("id").Value)))
        {
            XElement xel = new XElement(child, new XAttribute("id", key), dict[key].Select(x => new XElement("wave", new XAttribute("unit_type", x.Item1), new XAttribute("count", x.Item2))));
            file.Root.Element(child).AddAfterSelf(xel);
        }
        file.Save(path);
    }

    public static IDictionary<int, Queue<(string, int)>> XMLDeserialization_KVPTuple(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("file doesn't exist");
            return null;
        }

        XElement rootElement = XElement.Load(path);
        IDictionary<int, Queue<(string, int)>> dict = new Dictionary<int, Queue<(string, int)>>();

        foreach (var el in rootElement.Elements("levels"))
        {
            Queue<(string, int)> q_t = new Queue<(string, int)>();
            foreach (var wave in el.Elements())
            {
                q_t.Enqueue((wave.Attribute("unit_type").Value, Int32.Parse(wave.Attribute("count").Value)));
            }
            dict.Add(Int32.Parse(el.Attribute("id").Value), q_t);
        }
        return dict;
    }
}
