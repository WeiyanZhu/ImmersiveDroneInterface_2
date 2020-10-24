﻿using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.interface_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib
{

	public class TopicTypesMsg : ROSBridgeLib
	{

		private string _name;
		private string _type;

		public TopicTypesMsg(JSONNode msg)
		{
			_name = msg["_name"].ToString();
			_type = msg["_type"].ToString();
		}

		public static string getMessageType()
		{
			return "issacs_server/topic_types";
		}

		public string getName()
        {
			return _name;
        }

		public string getType()
        {
			return _type;
        }
	}
}