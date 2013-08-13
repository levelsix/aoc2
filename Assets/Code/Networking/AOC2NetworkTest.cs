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
	
		ConnectionFactory factory = new ConnectionFactory();
		factory.HostName = "robot.lvl6.com";
		factory.UserName = "lvl6client";
		factory.Password = "devclient";
		factory.VirtualHost = "devageofchaos";
		
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

		
		IModel channel = null;
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
		
		AOC2EventManager.Popup.CreatePopup("Connected");
		
	}
}
