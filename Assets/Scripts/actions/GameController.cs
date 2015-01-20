using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour {
	
	/**
	http://stackoverflow.com/questions/17286308/how-to-write-an-xml-file-in-c-sharp-in-unity

	 */
	
	void Awake(){
		/*
		XmlSerializer xmls = new XmlSerializer(typeof(Player));
		
		StringWriter sw = new StringWriter();
		xmls.Serialize(sw, new Player { Level = 5, Health = 500 });
		string xml = sw.ToString();
		
		Player player = xmls.Deserialize(new StringReader(xml)) as Player;
		Debug.Log (xml);
		*/
		
		XmlSerializer xmls = new XmlSerializer(typeof(Player));
		string path = Application.persistentDataPath + "/PlayerData.xml";
		using (var stream = File.OpenWrite(path)){
			xmls.Serialize(stream, new Player { Level = 5, Health = 500, Lista = new List<string>{"string1", "string2"} });
		}
		Debug.Log (path);
		
		// Reading data from Player
		Player player = null;
		using (var stream = File.OpenRead(path)){
			player = xmls.Deserialize(stream) as Player;
		}
		
		Debug.Log ("Health: " + player.Health + " Level: " + player.Level);
	}
}

[XmlRoot]
public class Player
{
	[XmlElement]
	public int Level { get; set; }
	
	[XmlElement]
	public int Health { get; set; }
	
	[XmlArray("Lista")]
	[XmlArrayItem("string")]
	public List<string> Lista;
}

