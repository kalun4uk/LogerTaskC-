using System;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace LogLogger
{
    class InternalLoggers {
    /// <summary>
    /// Choosing the right type of file to add due to users app.config
    /// </summary>
    /// <param name="textMessage">Text of the logtype element</param>
    /// <param name="logLevelType">Level of log type - debug, warning, error</param>
    /// <param name="name">name of the method? which called this log</param>
    /// <param name="date">date of the logtype element happened</param>
    public void WriteMessage(string textMessage, LogLevel logLevelType, string name, DateTime date)
    {
        switch (DataConfig.filetype)
        {
            case Format.TEXT:
                AddText(textMessage, logLevelType, name, date);
                break;
            case Format.JSON:
                AddJson(textMessage, logLevelType, name, date);
                break;
            case Format.XML:
                AddXML(textMessage, logLevelType, name, date);
                break;
            default:
                throw new ArgumentException();
        }
    }

    void AddText(string message, LogLevel logLevel, string name, DateTime date)
    {
        if (!File.Exists(DataConfig.path))
        {
            using (StreamWriter sw = File.CreateText(DataConfig.path))
            {
                sw.Write($"\n{date}  {logLevel}  [{name}]  {message}");
            }
        }
        else
        {
        using (FileStream fs = new FileStream(DataConfig.path, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine($"\n{date}  {logLevel}  [{name}]  {message}");
        }
        }
    }

    void AddJson(string message, LogLevel logLevel, string name, DateTime date)
    {
        if (!File.Exists(DataConfig.path))
        {
            using (FileStream fs = new FileStream(DataConfig.path, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                    sw.Write(ToJObject(message, logLevel, name, date).ToString());
            }
        }
        else
        {
            using (FileStream fs = new FileStream(DataConfig.path, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(ToJObject(message, logLevel, name, date) + "\n");
                }
        }
    }

    void AddXML(string message, LogLevel logLevel, string name, DateTime date)
    {
            if (!File.Exists(DataConfig.path))
             {
                 XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                 xmlWriterSettings.Indent = true;
                 xmlWriterSettings.NewLineOnAttributes = true;
                 using (XmlWriter xmlWriter = XmlWriter.Create(DataConfig.path, xmlWriterSettings))
                 {
                     xmlWriter.WriteStartElement("Logs");
                     xmlWriter.WriteElementString("message", message);
                     xmlWriter.WriteElementString("loglevel", logLevel.ToString());
                     xmlWriter.WriteElementString("name", name);
                     xmlWriter.WriteElementString("date", date.ToString());
                     xmlWriter.WriteEndElement();
                     xmlWriter.WriteEndDocument();
                     xmlWriter.Flush();
                 }
             }
             else
             {
                 try
                 {
                     XDocument doc = XDocument.Load(DataConfig.path);
                     XElement root = new XElement("log");
                     root.Add(new XElement("message", message));
                     root.Add(new XElement("loglevel", logLevel.ToString()));
                     root.Add(new XElement("name", name));
                     root.Add(new XElement("date", date.ToString()));
                     doc.Element("Logs").Add(root);
                     doc.Save(DataConfig.path);
                 }
                 catch (NullReferenceException)
                 {
                    Console.WriteLine("Null refereence exception");
                    Console.ReadKey();
                 }
             }
        }

    public JObject ToJObject(string message, LogLevel logLevel, string name, DateTime date)
    {
        JObject jObject = new JObject
        {
            {"Message", message},
            {"LogLevel", logLevel.ToString()},
            {"Name", name},
            {"Date", date}
        };
        return jObject;
    }
    }
}