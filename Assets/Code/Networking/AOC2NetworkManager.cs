using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RabbitMQ;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;
using RabbitMQ.Client;
using proto;
using ProtoBuf;
using System.IO;

public class AOC2NetworkManager : MonoBehaviour {
	
	MySerializer ser = new MySerializer();
	
	//Dictionary<string, Type> classDict = new Dictionary<string, Type>();
	
	string exchangeName = "gamemessages";
	
	string queueName = "_queue";
	
	int sessionID;
	
	int tagNum = 1;
	
	IModel channel = null;
	
	const int HEADER_SIZE = 12;
	
	void Awake()
	{
		AOC2ManagerReferences.networkManager = this;
	}
	
	// Use this for initialization
	void Start () {
	
		sessionID = UnityEngine.Random.Range(0, int.MaxValue);
		
		queueName = "_" + sessionID + "_queue";
		
		ConnectionFactory factory = new ConnectionFactory();
		
		factory.HostName = "robot.lvl6.com";
		factory.UserName = "lvl6client";
		factory.Password = "devclient";
		factory.VirtualHost = "devaoc2";
		
		
		IConnection connection = null;
		try{
			connection = factory.CreateConnection();
			gameObject.SetActive(true);
		}
		catch (Exception e)
		{
			Debug.LogError("Connection exception: " + e);
			gameObject.SetActive(false);
		}

		
		try
		{
			channel = connection.CreateModel();
			gameObject.SetActive(true);
		}
		catch (Exception e)
		{
			Debug.LogError("Channel error: " + e);
			gameObject.SetActive(false);
		}
		
		Debug.Log("Connected");
		
		channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
		channel.QueueDeclare(queueName, true, false, false, null);
		channel.QueueBind(queueName, exchangeName, "");
		
		Debug.Log("Bounded");
		
		QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
		string consumerTag = channel.BasicConsume(queueName, false, consumer);
		
		StartCoroutine(OperateConnection(channel, consumerTag));
		
	}
	
	public void SendRequest(System.Object request, int type)
	{
		
		MemoryStream stream = new MemoryStream();
		ser.Serialize(stream, request);
		
		int size = (int)stream.Length;
		byte[] header = new byte[HEADER_SIZE];
		
		header[3] = (byte)(type & 0xFF);
		header[2] = (byte)((type & 0xFF00) >> 8);
		header[1] = (byte)((type & 0xFF0000) >> 16);
		header[0] = (byte)((type & 0xFF000000) >> 24);
		
		header[7] = (byte)(tagNum & 0xFF);
		header[6] = (byte)((tagNum & 0xFF00) >> 8);
		header[5] = (byte)((tagNum & 0xFF0000) >> 16);
		header[4] = (byte)((tagNum & 0xFF000000) >> 24);
  
		header[11] = (byte)(size & 0xFF);
		header[10] = (byte)((size & 0xFF00) >> 8);
		header[9] = (byte)((size & 0xFF0000) >> 16);
		header[8] = (byte)((size & 0xFF000000) >> 24);
		
		stream.Write (header, 0, HEADER_SIZE);
		
		byte[] body = stream.GetBuffer();
		
		IBasicProperties properties = channel.CreateBasicProperties();
		properties.SetPersistent(true);
		channel.BasicPublish(exchangeName, "messagesFromPlayers", properties, body);
		
		Debug.Log("Message sent");
	}
	
	IEnumerator OperateConnection(IModel model, string queue)
	{
		BasicGetResult getResult = null;
		while(model.IsOpen)
		{
			//Loop through each result we can get from the queue
			for(;getResult != null; getResult = model.BasicGet(queue, true))
			{
				MemoryStream bytes = new MemoryStream(getResult.Body);
				
				Debug.Log(bytes);
				
				ReceiveResponse(ser.Deserialize(bytes, null, Type.GetType(getResult.BasicProperties.Type)));
			}
			yield return null;
		}
	}
	
	void ReceiveResponse(System.Object response)
	{
		Debug.Log("Received response!\nType: " + response.GetType());
	}
}
