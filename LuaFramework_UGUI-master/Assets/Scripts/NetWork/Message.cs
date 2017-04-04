using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using SimpleJson;
using UnityEngine;
using LuaFramework;


namespace GlobalGame 
{
	public class Message
	{
		public ByteBuffer buf;
		public string cmdName;
		public JsonObject mJson;
		/**
	 * 构造方法
	*/
		public Message()
		{

		}

		/**
	 * 写入cmd
	*/
		public bool Write(short cmdType, string cmdStr)
		{
			JsonObject jobj = Global.StrToJson(cmdStr);
			if(jobj == null){
				return false;
			}

			ByteBuffer tmpbuf = ByteBuffer.Allocate(1024);
			buf = ByteBuffer.Allocate(1024);

			if(_WriteToBuffer(cmdType, jobj, tmpbuf))
			{
				int len = tmpbuf.ReadableBytes();
				buf.WriteShort((short)len);

				buf.WriteBytes(tmpbuf.ToArray());

				return true;
			}

			return false;
		}

		/**
	 * 写入cmd
	*/
		public bool Write(string cmdName, JsonObject jobj)
		{
			if(jobj == null){
				return false;
			}

			//ByteBuffer tmpbuf = ByteBuffer.Allocate(1024);
			buf = ByteBuffer.Allocate(1024);

			if(_WriteToBuffer(cmdName, jobj, buf))
			{
	//			int len = tmpbuf.ReadableBytes();
	//			buf.WriteShort((short)len);
	//
	//			buf.WriteBytes(tmpbuf.ToArray());

				return true;
			}

			return false;
		}

		/**
	 * 写入临时buffer
	*/
		static Dictionary<short, XmlElement> fieldsNodeCache_Short = new Dictionary<short, XmlElement>();
		private bool _WriteToBuffer(short cmdType, JsonObject jobj, ByteBuffer tmpbuf)
		{
			XmlElement fieldsNode;
			if (!fieldsNodeCache_Short.TryGetValue(cmdType, out fieldsNode)) {
				// 查找类型
				XmlElement typeNode = (XmlElement)LuaHelper.GetWebManager().root.SelectSingleNode("/vector/module/messageModels/message/messageType[text()='"+cmdType+"']");
				if(typeNode == null)
				{
					return false;
				}

				//tmpbuf.WriteShort(0);


				fieldsNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("fields");
				if(fieldsNode == null)
				{
					return true;
				}
				fieldsNodeCache_Short.Add(cmdType, fieldsNode);
			}
			ByteBuffer tmpbuf1 = ByteBuffer.Allocate(1024);
			XmlNodeList fieldsList = fieldsNode.ChildNodes;
			for(int i=0; i<fieldsList.Count; i++)
			{
				XmlElement fieldNode = (XmlElement)fieldsList.Item(i);
				if(fieldNode != null)
				{
					_WriteFeildToBuffer(fieldNode,jobj,tmpbuf1);
				}
			}
			int len = tmpbuf1.ReadableBytes();
			tmpbuf.WriteShort((short)len);
			tmpbuf.WriteShort(cmdType);
			tmpbuf.WriteBytes(tmpbuf1.ToArray());


			return true;
		}

		/**
	 * 写入临时buffer
	*/
		static Dictionary<string, KeyValuePair<short, XmlElement>> fieldsNodeCache_String = new Dictionary<string, KeyValuePair<short, XmlElement>>();
		private bool _WriteToBuffer(string cmdName, JsonObject jobj, ByteBuffer tmpbuf)
		{
			KeyValuePair<short, XmlElement> typeFieldsNode;
			XmlElement fieldsNode;
			short cmdType;
			if (!fieldsNodeCache_String.TryGetValue(cmdName, out typeFieldsNode)) {
				// 查找类型
//				XmlElement x = WebManager.Instance.root;
				XmlElement typeNode = (XmlElement)LuaHelper.GetWebManager().root.SelectSingleNode("/vector/module/messageModels/message/name[text()='"+cmdName+"']");
				if(typeNode == null)
				{
					return false;
				}

				XmlElement mtypeNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("messageType");
				if(mtypeNode == null)
				{
					return false;
				}

				//tmpbuf.WriteShort(0);
				//tmpbuf.WriteShort(Convert.ToInt16(mtypeNode.InnerText));

				fieldsNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("fields");
				if(fieldsNode == null)
				{
					return true;
				}
				cmdType = Convert.ToInt16(mtypeNode.InnerText);
				fieldsNodeCache_String.Add(cmdName, new KeyValuePair<short, XmlElement>(cmdType, fieldsNode));
			} else {
				cmdType = typeFieldsNode.Key;
				fieldsNode = typeFieldsNode.Value;
			}
			ByteBuffer tmpbuf1 = ByteBuffer.Allocate(1024);

			XmlNodeList fieldsList = fieldsNode.ChildNodes;
			for(int i=0; i<fieldsList.Count; i++)
			{
				XmlElement fieldNode = (XmlElement)fieldsList.Item(i);
				if(fieldNode != null)
				{
					_WriteFeildToBuffer(fieldNode,jobj,tmpbuf1);
				}
			}

			int len = tmpbuf1.ReadableBytes();
			tmpbuf.WriteShort((short)len);
			tmpbuf.WriteShort(cmdType);
			tmpbuf.WriteBytes(tmpbuf1.ToArray());

			return true;
		}
		/**
	 * feild写入临时buffer
	*/
		private void _WriteFeildToBuffer(XmlElement fieldNode, JsonObject jobj, ByteBuffer tmpbuf)
		{
			short bigType = Convert.ToInt16(((XmlElement)fieldNode.SelectSingleNode("bigType")).InnerText);
			string type = ((XmlElement)fieldNode.SelectSingleNode("javaType")).InnerText;
			string name = ((XmlElement)fieldNode.SelectSingleNode("name")).InnerText;

			object value = jobj.TryGet(name);
			if(value == null)
			{
				return;
			}
			switch(bigType)
			{
			case 0:
				_WriteTypeToBuffer(type,value,tmpbuf);
				break;
			case 1:
				List<object> values = value as List<object>;
				tmpbuf.WriteShort(Convert.ToInt16(values.Count));
				for(int i=0; i<values.Count; i++)
				{
					_WriteTypeToBuffer(type,values[i],tmpbuf);
				}
				break;
			case 2:
				JsonObject tmpjson = jobj.TryGet(name) as JsonObject;
				_WriteToBuffer(type,tmpjson, tmpbuf);
				break;
			case 3:
				List<object> msgs = value as List<object>;
				tmpbuf.WriteShort(Convert.ToInt16(msgs.Count));
				for(int i=0; i<msgs.Count; i++)
				{
					JsonObject msgjson = msgs[i] as JsonObject;

					_WriteToBuffer(type, msgjson, tmpbuf);
				}
				break;
			}
		}
		/**
	 * type写入临时buffer
	*/
		private void _WriteTypeToBuffer(string type, object value, ByteBuffer tmpbuf)
		{
			if(type == "byte")
			{
				tmpbuf.WriteByte(Convert.ToByte(value));
			}
			else if(type == "String")
			{
				tmpbuf.WriteString(Convert.ToString(value));
			}
			else if(type == "short")
			{
				tmpbuf.WriteShort(Convert.ToInt16(value));
			}
			else if(type == "int")
			{
				tmpbuf.WriteInt(Convert.ToInt32(value));
			}
			else if(type == "long")
			{
				tmpbuf.WriteLong(Convert.ToInt64(value));
			}
			else if(type == "float")
			{
				tmpbuf.WriteFloat(Convert.ToSingle(value));
			}
			else if(type == "double")
			{
				tmpbuf.WriteDouble(Convert.ToDouble(value));
			}
		}
		/**
	 * 读取cmd
	*/
		public JsonObject Read(byte[] bytes)
		{
			buf = ByteBuffer.Allocate(bytes);
			short len = buf.ReadShort();
			short type = buf.ReadShort();
			XmlElement typeNode = (XmlElement)LuaHelper.GetWebManager().root.SelectSingleNode("/vector/module/messageModels/message/messageType[text()='"+type+"']");
			if(typeNode == null)
			{
				return null;
			}

			XmlElement mtypeNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("name");
			if(mtypeNode == null)
			{
				return null;
			}

			cmdName = mtypeNode.InnerText;
			mJson = new JsonObject();

			if(_ReadFromBuffer(type, ref mJson))
			{	
				return mJson;
			}

			return null;
		}

		/**
	 * 从buffer读取
	*/
		private bool _ReadFromBuffer(short cmdType, ref JsonObject jobj)
		{
			// 查找类型
			XmlElement typeNode = (XmlElement)LuaHelper.GetWebManager().root.SelectSingleNode("/vector/module/messageModels/message/messageType[text()='"+cmdType+"']");
			if(typeNode == null)
			{
				return false;
			}

			XmlElement fieldsNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("fields");
			if(fieldsNode == null)
			{
				return true;
			}

			XmlNodeList fieldsList = fieldsNode.ChildNodes;
			for(int i=0; i<fieldsList.Count; i++)
			{
				XmlElement fieldNode = (XmlElement)fieldsList.Item(i);
				if(fieldNode != null)
				{
					_ReadFeildFromBuffer(fieldNode, ref mJson);
				}
			}

			return true;
		}

		/**
	 * 从buffer读取
	*/
		private bool _ReadFromBuffer(string cmdName, ref JsonObject jobj)
		{
			// 查找类型
			XmlElement typeNode = (XmlElement)LuaHelper.GetWebManager().root.SelectSingleNode("/vector/module/messageModels/message/name[text()='"+cmdName+"']");
			if(typeNode == null)
			{
				return false;
			}

			XmlElement mtypeNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("messageType");
			if(mtypeNode == null)
			{
				return false;
			}

			short len = buf.ReadShort();
			short type = buf.ReadShort();
	//		if(type == 0)
	//		{
	//			return false;
	//		}

			XmlElement fieldsNode = (XmlElement)typeNode.ParentNode.SelectSingleNode("fields");
			if(fieldsNode == null)
			{
				return true;
			}

			XmlNodeList fieldsList = fieldsNode.ChildNodes;
			for(int i=0; i<fieldsList.Count; i++)
			{
				XmlElement fieldNode = (XmlElement)fieldsList.Item(i);
				if(fieldNode != null)
				{
					_ReadFeildFromBuffer(fieldNode, ref jobj);
				}
			}

			return true;
		}

		/**
	 * 从buffer读取feild
	*/
		private void _ReadFeildFromBuffer(XmlElement fieldNode, ref JsonObject mJson)
		{
			short bigType = Convert.ToInt16(((XmlElement)fieldNode.SelectSingleNode("bigType")).InnerText);
			string type = ((XmlElement)fieldNode.SelectSingleNode("javaType")).InnerText;
			string name = ((XmlElement)fieldNode.SelectSingleNode("name")).InnerText;

			switch(bigType)
			{
			case 0:
				mJson.TrySet(name, _ReadTypeFromBuffer(type));
				break;
			case 1:
				short count = buf.ReadShort();
				List<object> arr = new List<object>();
				for(short i=0; i<count; i++)
				{
					arr.Add(_ReadTypeFromBuffer(type));
				}
				mJson.TrySet(name, arr);
				break;
			case 2:
				JsonObject obj = new JsonObject();
				_ReadFromBuffer(type,ref obj);
				mJson.TrySet(name,obj);
				break;
			case 3:
				short cnt = buf.ReadShort();
				List<JsonObject> msgarr = new List<JsonObject>();
				for(short i=0; i<cnt; i++)
				{
					JsonObject tmpobj = new JsonObject();
					_ReadFromBuffer(type, ref tmpobj);
					msgarr.Add(tmpobj);
				}
				mJson.TrySet(name, msgarr);
				break;
			}
		}

		/**
	 * 从buffer读取type
	*/
		private object _ReadTypeFromBuffer(string type)
		{
			if(type == "byte")
			{
				return buf.ReadByte();
			}
			else if(type == "String")
			{
				return buf.ReadString();
			}
			else if(type == "short")
			{
				return buf.ReadShort();
			}
			else if(type == "int")
			{
				return buf.ReadInt();
			}
			else if(type == "long")
			{
				return buf.ReadLong();
			}
			else if(type == "float")
			{
				return buf.ReadFloat();
			}
			else if(type == "double")
			{
				return buf.ReadDouble();
			}
			return null;
		}
	}
}

