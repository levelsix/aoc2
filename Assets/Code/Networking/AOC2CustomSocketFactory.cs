using System.Collections;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public static class AOC2CustomSocketFactory 
{

	public static System.Net.Sockets.TcpClient GetSocket(System.Net.Sockets.AddressFamily addressFamily)
	{
		System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(System.Net.Sockets.AddressFamily.InterNetwork);
		tcpClient.NoDelay = true;
		return tcpClient;
	}
}