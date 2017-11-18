// GetUdpData.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "atlstr.h"
#include "iostream"
#include "string"
#include <winsock.h>
#include <windows.h>
#include <direct.h>
#import "c:\program files\common files\system\ado\msado15.dll" no_namespace rename("EOF","adoEOF") 

using namespace std;

#pragma comment(lib,"ws2_32.lib")

SOCKET SocketHost;
SOCKADDR_IN SockFrom;
SOCKADDR_IN SockSrc;

int SockAddrlen = sizeof(SOCKADDR);

static _ConnectionPtr m_pConnection;
static _RecordsetPtr m_pRecordset;

void ConnectUdp(void);
void ConnectSql(void);
void ExitConnect(void);

_RecordsetPtr&GetRecordset(_bstr_t);

bool isStop = false;
bool isReset = false;
int readyCount = 0;
int resetCount = 0;
int mustCloseCount = 0;

bool isGetPath = false;
string pathStr;

HANDLE hcom;
BYTE SendData[8];
//BYTE SendData[8] = { 0xAA, 0x06, 0x00, 0x01, 0x00, 0x03, 0x81, 0xD0 };

DWORD WINAPI SEND_SIGNAL(LPVOID lpParamter)
{
	try
	{
		cout << "信号发送线程启动成功!" << endl;

		char path[_MAX_PATH];
		//bool isGetPath = false;
		//string pathStr;
		if (_getcwd(path, _MAX_PATH) == NULL)
		{
			cout << "地址获取错误!" << endl;
		}
		else
		{
			pathStr.assign(path);
			pathStr = pathStr + "\\SendToPort485.exe";
			//cout << pathStr << endl;
			isGetPath = true;
		}
	}
	catch (_com_error e)
	{
		cout << "信号发送线程启动失败!" << endl;
		cout << e.Description() << endl;
	}
	while(true)
	{
		try
		{
			if (!isStop)
			{
				if (readyCount > 20) //多久无车关闭秒数
				{
					///////////////////////////		
					//isReset = true; //暂时没用
					isStop = true;
					readyCount = 0;
					mustCloseCount = 0;
					//////C++发送信号逻辑
					DWORD dwWrittenLen = 0;
					if (!WriteFile(hcom, SendData, 8, &dwWrittenLen, NULL))
					{
						DWORD err = GetLastError();
						if (err != ERROR_IO_PENDING)
						{
							cout << "发送串口数据失败!" << endl;
							system("SHUTDOWN -R -T 0");
						}
						else
						{
							///////////////////////////
							cout << "关闭传感器指令进入队列!" << endl;
							m_pRecordset->AddNew();
							m_pRecordset->PutCollect("ID", _variant_t(0));
							m_pRecordset->PutCollect("FLAG", _variant_t(-1));
							m_pRecordset->PutCollect("VALUE", _variant_t(0));
							m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
							m_pRecordset->Update();
						}
					}
					else
					{
						///////////////////////////
						cout << "关闭传感器系统!" << endl;
						m_pRecordset->AddNew();
						m_pRecordset->PutCollect("ID", _variant_t(0));
						m_pRecordset->PutCollect("FLAG", _variant_t(-1));
						m_pRecordset->PutCollect("VALUE", _variant_t(0));
						m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
						m_pRecordset->Update();
					}
					///////////////////////////
					//发送信号逻辑
					//if (isGetPath)
					//{
					//	WinExec(pathStr.c_str(), SWP_HIDEWINDOW);
					//	///////////////////////////		
					//	//isReset = true; //暂时没用
					//	isStop = true;
					//	readyCount = 0;
					//	mustCloseCount = 0;
					//	///////////////////////////
					//	cout << "关闭传感器系统!" << endl;
					//	m_pRecordset->AddNew();
					//	m_pRecordset->PutCollect("ID", _variant_t(0));
					//	m_pRecordset->PutCollect("FLAG", _variant_t(-1));
					//	m_pRecordset->PutCollect("VALUE", _variant_t(0));
					//	m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
					//	m_pRecordset->Update();
					//}
					//else
					//{
					//	cout << "未获取到关停插件地址!" << endl;
					//}
				}
				else
				{
					readyCount++;
				}
			}
		}
		catch (_com_error e)
		{
			cout << e.Description() << endl;
		}
		Sleep(1000);
	}
	return 0;
}

DWORD WINAPI RESET_UDP(LPVOID lpParamter)
{
	while (true)
	{
		try
		{
			if (!isStop)
			{
				if (mustCloseCount > 300)
				{
					///////////////////////////		
					//isReset = true; //暂时没用
					isStop = true;
					readyCount = 0;
					mustCloseCount = 0;
					////////C++发送信号逻辑
					DWORD dwWrittenLen = 0;
					if (!WriteFile(hcom, SendData, 8, &dwWrittenLen, NULL))
					{
						DWORD err = GetLastError();
						if (err != ERROR_IO_PENDING)
						{
							cout << "发送串口数据失败!" << endl;
							system("SHUTDOWN -R -T 0");
						}
						else
						{
							///////////////////////////
							cout << "强制关闭传感器指令进入队列!" << endl;
							m_pRecordset->AddNew();
							m_pRecordset->PutCollect("ID", _variant_t(0));
							m_pRecordset->PutCollect("FLAG", _variant_t(-1));
							m_pRecordset->PutCollect("VALUE", _variant_t(0));
							m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
							m_pRecordset->Update();
						}
					}
					else
					{
						///////////////////////////
						cout << "强制关闭传感器系统!" << endl;
						m_pRecordset->AddNew();
						m_pRecordset->PutCollect("ID", _variant_t(0));
						m_pRecordset->PutCollect("FLAG", _variant_t(-1));
						m_pRecordset->PutCollect("VALUE", _variant_t(0));
						m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
						m_pRecordset->Update();
					}
					///////////////////////////
					//发送信号逻辑
					//if (isGetPath)
					//{
					//	WinExec(pathStr.c_str(), SWP_HIDEWINDOW);
					//	isStop = true;
					//	///////////////////////////
					//	readyCount = 0;
					//	mustCloseCount = 0;
					//	///////////////////////////
					//	cout << "强制关闭传感器系统!" << endl;
					//	m_pRecordset->AddNew();
					//	m_pRecordset->PutCollect("ID", _variant_t(0));
					//	m_pRecordset->PutCollect("FLAG", _variant_t(-1));
					//	m_pRecordset->PutCollect("VALUE", _variant_t(0));
					//	m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
					//	m_pRecordset->Update();
					//}
					//else
					//{
					//	cout << "未获取到关停插件地址!" << endl;
					//}
				}
				else
				{
					mustCloseCount++;
				}
			}
			else
			{
				mustCloseCount = 0;
			}
		}
		catch (_com_error e)
		{
			cout << e.Description() << endl;
		}
		Sleep(1000);
	}

	return 0;
}


int _tmain(int argc, _TCHAR* argv[])
{
	try
	{	
		//连接数据库
		//覆盖外部声明
		_RecordsetPtr m_pRecordset;
		string sql="select * from CAR_SOURCE";
		_bstr_t bstr_t(sql.c_str());
		m_pRecordset=GetRecordset(bstr_t);

		////打开串口端口
		hcom = CreateFile(L"COM6", GENERIC_WRITE, 0, NULL, OPEN_EXISTING
			, FILE_ATTRIBUTE_NORMAL, NULL);
		if (hcom == INVALID_HANDLE_VALUE)
		{
			cout << "打开串口失败!" << endl;
			//exit(0);
		}
		else
		{
			cout << "打开串口成功!" << endl;
		}
		DCB dcb;
		SetupComm(hcom, 4096, 2048);
		GetCommState(hcom, &dcb);
		dcb.BaudRate = 9600;
		dcb.ByteSize = 8;
		dcb.Parity = 0;
		dcb.StopBits = 0;   //0为1;1为1.5;2为2
		SetCommState(hcom, &dcb);

		//给信号数据赋值
		SendData[0] = 0xAA; 
		SendData[1] = 0x06;
		SendData[2] = 0x00;
		SendData[3] = 0x01; 
		SendData[4] = 0x00; 
		SendData[5] = 0x03;
		SendData[6] = 0x81;
		SendData[7] = 0xD0;

		//打开发送关闭信号线程
		try
		{
			HANDLE hThread = CreateThread(NULL, 0, SEND_SIGNAL, NULL, 0, NULL);
			CloseHandle(hThread);			
		}
		catch (_com_error e)
		{
			cout << "信号发送线程启动失败!" << endl;
			throw e;
		}

		ConnectUdp();

		//打开传感器重置线程
		try
		{
			HANDLE hThread = CreateThread(NULL, 0, RESET_UDP, NULL, 0, NULL);
			CloseHandle(hThread);
		}
		catch (_com_error e)
		{
			cout << "传感器重置线程启动失败!" << endl;
			throw e;
		}

		BYTE RecBuf[2048];
		int i = 0;
		int Len = 0, Reslut;
		cout << "开始数据接收，请勿关闭该界面!" << endl;
		while (true)
		{			
			//CString str, tmpstr;
			i = 0;
			Len = 0;
			Reslut = 0;

			//UDP接收 
			Reslut = recvfrom(SocketHost, (char*)RecBuf, sizeof(RecBuf), 0, (SOCKADDR*)&SockFrom, &SockAddrlen);

			if (Reslut == SOCKET_ERROR)
			{
				cout << "Receive Error" << endl;
				WSACleanup();
				return 0;
			}

			Len = Reslut / 13;

			//收到数据且UDP包完整
			if (Len > 0 && Reslut % 13 == 0)
			{
				for (i = 0; i < Len; i++)
				{
					INT id, flag, value, topdata, time;
					id=(INT)(RecBuf[i * 13 + 3]*256 +RecBuf[i * 13 + 4]);
					flag=(INT)(RecBuf[i * 13 + 5]); 
					value=(INT)(RecBuf[i * 13 + 6]*256 + RecBuf[i * 13 + 7]);
					topdata = (INT)RecBuf[i * 13 + 8] % 64;
					time = (INT)(topdata * 4294967296 + RecBuf[i * 13 + 9] * 16777216 + RecBuf[i * 13 + 10] * 65536 + RecBuf[i * 13 + 11] * 256 + RecBuf[i * 13 + 12]);

					////传感器测量到的高度大于2.1米即定义为有卡车通过(总高度大约为5.8米)
					if (value < 3700)
					{
						readyCount = 0;
						isStop = false;
						//cout << "重新开始接收..." << endl;
					}

					m_pRecordset->AddNew();
					m_pRecordset->PutCollect("ID", _variant_t(id));
					m_pRecordset->PutCollect("FLAG", _variant_t(flag));
					m_pRecordset->PutCollect("VALUE", _variant_t(value));
					m_pRecordset->PutCollect("SENSORTIME", _variant_t(time));
					m_pRecordset->Update();
				}
			}
			//Sleep(1000);
		}
		ExitConnect();
	}
	catch (_com_error e)
	{
		cout << e.Description() << endl;
		system("PAUSE");
	}	
}

///连接绑定UPD接收端口
void ConnectUdp()
{
	try
	{
		WORD wVersionRequested = MAKEWORD(1, 1);
		WSADATA wsaData;
		int  Result;
		int m_LocalPort = 4800;
		//CString m_DevIp = _T("192.168.1.102");
		//全ip地址接收
		CString m_DevIp = _T(".");
	
		//初始化Winsock DLL
		if ((Result = WSAStartup(wVersionRequested, &wsaData)) != 0)
		{
			cout << "Socket Initial Error" << endl;
			WSACleanup();
			return;
		}

		//检查Winsock 版本
		if (LOBYTE(wVersionRequested) < 1 || (LOBYTE(wVersionRequested) == 1 && HIBYTE(wVersionRequested)< 1))
		{
			WSACleanup();
			return;
		}

		//绑定主机IP 和端口
		SocketHost = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
		if (SocketHost == INVALID_SOCKET)
		{
			cout << "Socket Open failed" << endl;
			WSACleanup();
			return;
		}

		SockSrc.sin_family = AF_INET;
		//////////////
		SockSrc.sin_port = htons(m_LocalPort);
		SockSrc.sin_addr.S_un.S_addr = htonl(INADDR_ANY);

		Result = bind(SocketHost, (SOCKADDR *)&SockSrc, sizeof(SOCKADDR));
		if (Result == SOCKET_ERROR)
		{
			cout << "Bind Error" << endl;
			WSACleanup();
			return;
		}

		cout << "打开UDP连接成功!" << endl;
	}
	catch(_com_error e)
	{
		cout << "打开UDP连接失败!" << endl;
		cout << e.Description() << endl;
	}
}

///连接数据库
void ConnectSql()
{
	try
	{
		::CoInitialize(NULL);//初始化COM环境
		m_pConnection.CreateInstance(__uuidof(Connection));//创建连接对象
		m_pConnection->ConnectionString="Provider=SQLOLEDB; User ID=sa; Password=123456; Initial Catalog=VEHICLES_DATA; Data Source=."; //请将数据库相应ID与Password更改
		
		//连接服务器和数据库
		HRESULT hr=m_pConnection->Open("", "", "", 0);
		if(hr!=S_OK)
		{
			cout<<"打开数据库连接失败!"<<endl;
		}
		else
		{
			cout<<"打开数据库连接成功!"<<endl;
		}
	}
	catch(_com_error e)
	{
		cout << e.Description() << endl;
	}
}

//关闭连接
void ExitConnect()
{
	try
	{
		if (m_pRecordset != NULL)
		{
			m_pRecordset->Close();
			m_pConnection->Close();
		}::CoUninitialize(); //释放环境
		cout << "关闭数据库操作集成功!" << endl;
		cout << "关闭数据库连接成功!" << endl;
	}
	catch (_com_error e)
	{
		cout << "关闭数据库失败!" << endl;
		cout << e.Description() << endl;
	}
}

_RecordsetPtr&GetRecordset(_bstr_t SQL)

{
	m_pRecordset=NULL;
	try
	{
		if(m_pConnection==NULL)
		{
			ConnectSql();
		}
		m_pRecordset.CreateInstance(__uuidof(Recordset));
		m_pRecordset->Open((_bstr_t)SQL, m_pConnection.GetInterfacePtr(), adOpenDynamic, adLockOptimistic, adCmdText);
		cout<<"打开数据库操作集成功!"<<endl;
	}
	catch(_com_error e)
	{
		cout<<e.Description()<<endl;
		m_pRecordset=NULL;
		//return m_pRecordset;
		cout<<"打开数据库操作集失败!"<<endl;
	}
	return m_pRecordset;
}
