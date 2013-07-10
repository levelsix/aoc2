using UnityEngine;
using System.Collections;
using RabbitMQ;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;
using RabbitMQ.Client;

public class AOC2NetworkTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Debug.Log("Testing Rabbit");
		ConnectionFactory factory = new ConnectionFactory();
		factory.HostName = "robot.lvl6.com";
		factory.UserName = "lvl6client";
		factory.Password = "devclient";
		factory.VirtualHost = "devageofchaos";
		
		IConnection connection = null;
		try{
			connection = factory.CreateConnection();
		}
		catch (Exception e)
		{
			Debug.LogError("Connection exception: " + e);
			gameObject.SetActive(false);
		}
		
		Debug.Log("connction created");
		
		IModel channel = null;
		try
		{
			channel = connection.CreateModel();
			Debug.Log("Channel");
		}
		catch (Exception e)
		{
			Debug.LogError("Channel error: " + e);
			gameObject.SetActive(false);
		}
		
		
	}
}
