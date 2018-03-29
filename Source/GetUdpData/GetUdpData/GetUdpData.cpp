// GetUdpData.cpp : �������̨Ӧ�ó������ڵ㡣
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
int chartoint(unsigned char, unsigned char *);
int strtodata(unsigned char *, unsigned char *, int, int);

_RecordsetPtr&GetRecordset(_bstr_t);

bool isStop = false;
bool isReset = false;
int readyCount = 0;
int resetCount = 0;
int mustCloseCount = 0;

bool needResetFlag = false;

HANDLE hcom;
BYTE SendData[8];

DWORD WINAPI RESET_CAR_CHECK_FLAG(LPVOID lpParamter)
{
	cout << "���ó��������ʶ�߳������ɹ�!" << endl;
	while (true)
	{
		try
		{
			if (!isStop)
			{
				if (readyCount > 20) //����޳��ر�����
				{
					///////////////////////////		
					isStop = true;
					readyCount = 0;
					mustCloseCount = 0;
					///////////////////////////
					cout << "���ó�������!" << endl;
					m_pRecordset->AddNew();
					m_pRecordset->PutCollect("ID", _variant_t(0));
					m_pRecordset->PutCollect("FLAG", _variant_t(-1));
					m_pRecordset->PutCollect("VALUE", _variant_t(0));
					m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
					m_pRecordset->Update();
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

DWORD WINAPI FORCE_RESET_CAR_CHECK_FLAG(LPVOID lpParamter)
{
	cout << "ǿ�����ó��������ʶ�߳������ɹ�!" << endl;
	while (true)
	{
		try
		{
			if (!isStop)
			{
				if (mustCloseCount > 300)
				{
					///////////////////////////		
					isStop = true;
					readyCount = 0;
					mustCloseCount = 0;
					///////////////////////////
					cout << "ǿ�����ó�������!" << endl;
					m_pRecordset->AddNew();
					m_pRecordset->PutCollect("ID", _variant_t(0));
					m_pRecordset->PutCollect("FLAG", _variant_t(-1));
					m_pRecordset->PutCollect("VALUE", _variant_t(0));
					m_pRecordset->PutCollect("SENSORTIME", _variant_t(-1));
					m_pRecordset->Update();
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

///����ʱ��
DWORD WINAPI RESET_TIME_BY_UDP(LPVOID lpParamter)
{
	cout << "�Զ�ͬ��������ʱ���߳������ɹ�!" << endl;
	while (true)
	{
		if (needResetFlag)
		{
			//�������Ĭ0.4s
			Sleep(400);
			try
			{
				unsigned char Data[13];
				char szData[40];
				BYTE DataLen = 0;
				char SendBuf[13];

				//SockDestCan
				int m_DevPort = 4000;
				SOCKADDR_IN SockDestCan;
				SockDestCan.sin_family = AF_INET;
				SockDestCan.sin_port = htons(m_DevPort);
				SockDestCan.sin_addr.S_un.S_addr = inet_addr("192.1.0.101");

				char addressStart[11] = "192.1.0.10";
				char addressEnd[2] = "0";
				char address[12] = "192.1.0.100";

				char h[3];

				char prefixSentData[13] = "08 00 00 00 ";
				char suffixSentData[25] = " f1 00 00 00 00 00 00 00";
				char sentData[39];

				//׼��13�ֽڵ�����
				strcpy_s(szData, "08 00 00 00 01 f1 00 00 00 00 00 00 00");
				strtodata((unsigned char*)szData, Data, 13, 1);
				memcpy(SendBuf, Data, sizeof(Data));

				sendto(SocketHost, SendBuf, 1 * sizeof(SendBuf), 0, (SOCKADDR*)&SockDestCan, sizeof(SOCKADDR));
				cout << "192.1.0.101" << endl;
				SockDestCan.sin_addr.S_un.S_addr = inet_addr("192.1.0.108");
				sendto(SocketHost, SendBuf, 1 * sizeof(SendBuf), 0, (SOCKADDR*)&SockDestCan, sizeof(SOCKADDR));
				cout << "192.1.0.108" << endl;

				for (int i = 34; i <= 231; i++)
				{
					_ltoa_s(i, h, 16);
					sprintf_s(sentData, "%s%02s%s", prefixSentData, h, suffixSentData);

					strcpy_s(szData, sentData);
					strtodata((unsigned char*)szData, Data, 13, 1);
					memcpy(SendBuf, Data, sizeof(Data));

					if (i % 33 == 0)
					{
						*addressEnd = char(i / 33 + 48);
					}
					else
					{
						*addressEnd = char(i / 33 + 49);
					}
					strcpy_s(address, addressStart);
					strcat_s(address, addressEnd);
					
					SockDestCan.sin_addr.S_un.S_addr = inet_addr(address);
					sendto(SocketHost, SendBuf, 1 * sizeof(SendBuf), 0, (SOCKADDR*)&SockDestCan, sizeof(SOCKADDR));
					cout << address << ":" << sentData << GetTickCount() << endl;
				}
			}
			catch (_com_error e)
			{
				cout << e.Description() << endl;
			}
			needResetFlag = false;
		}
		Sleep(100);
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	try
	{	
		//�������ݿ�
		//�����ⲿ����
		_RecordsetPtr m_pRecordset;
		string sql="select * from CAR_SOURCE";
		_bstr_t bstr_t(sql.c_str());
		m_pRecordset=GetRecordset(bstr_t);

		ConnectUdp();

		//�����ó��������ʶ�߳�
		try
		{
			HANDLE hThread = CreateThread(NULL, 0, RESET_CAR_CHECK_FLAG, NULL, 0, NULL);
			CloseHandle(hThread);
		}
		catch (_com_error e)
		{
			cout << "���ó��������ʶ�߳�����ʧ��!" << endl;
			throw e;
		}

		//��ǿ�����ó��������ʶ�߳�
		try
		{
			HANDLE hThread = CreateThread(NULL, 0, FORCE_RESET_CAR_CHECK_FLAG, NULL, 0, NULL);
			CloseHandle(hThread);
		}
		catch (_com_error e)
		{
			cout << "ǿ�����ó��������ʶ�߳�����ʧ��!" << endl;
			throw e;
		}

		//���Զ�ͬ��������ʱ���߳�
		try
		{
			HANDLE hThread = CreateThread(NULL, 0, RESET_TIME_BY_UDP, NULL, 0, NULL);
			CloseHandle(hThread);
		}
		catch (_com_error e)
		{
			cout << "�Զ�ͬ��������ʱ���߳�����ʧ��!" << endl;
			throw e;
		}

		BYTE RecBuf[2048];
		int i = 0;
		int Len = 0, Reslut = 0;
		cout << "��ʼ���ݽ��գ�����رոý���!" << endl;

		while (true)
		{
			needResetFlag = true;
			//UDP���� 
			Reslut = recvfrom(SocketHost, (char*)RecBuf, sizeof(RecBuf), 0, (SOCKADDR*)&SockFrom, &SockAddrlen);

			if (Reslut == SOCKET_ERROR)
			{
				cout << "Receive Error" << endl;
				WSACleanup();
				continue;
				//return 0;
			}

			if (isStop) { isStop = false; }
			readyCount = 0;

			INT id, flag, value, time;
			id = (INT)(RecBuf[3] * 256 + RecBuf[4]);
			flag = (INT)(RecBuf[5]);
			value = (INT)(RecBuf[6] * 256 + RecBuf[7]);
			time = (INT)(RecBuf[9] * 16777216 + RecBuf[10] * 65536 + RecBuf[11] * 256 + RecBuf[12]);

			m_pRecordset->AddNew();
			m_pRecordset->PutCollect("ID", _variant_t(id));
			m_pRecordset->PutCollect("FLAG", _variant_t(flag));
			m_pRecordset->PutCollect("VALUE", _variant_t(value));
			m_pRecordset->PutCollect("SENSORTIME", _variant_t(time));
			m_pRecordset->Update();

			if (flag == 6 && !needResetFlag)
			{
				needResetFlag = true;
			}
		}
		ExitConnect();
	}
	catch (_com_error e)
	{
		cout << e.Description() << endl;
		system("PAUSE");
	}	
}

///���Ӱ�UPD���ն˿�
void ConnectUdp()
{
	try
	{
		WORD wVersionRequested = MAKEWORD(1, 1);
		WSADATA wsaData;
		int  Result;
		int m_LocalPort = 4800;
		//CString m_DevIp = _T("192.168.1.102");
		//ȫip��ַ����
		CString m_DevIp = _T(".");
	
		//��ʼ��Winsock DLL
		if ((Result = WSAStartup(wVersionRequested, &wsaData)) != 0)
		{
			cout << "Socket Initial Error" << endl;
			WSACleanup();
			return;
		}

		//���Winsock �汾
		if (LOBYTE(wVersionRequested) < 1 || (LOBYTE(wVersionRequested) == 1 && HIBYTE(wVersionRequested)< 1))
		{
			WSACleanup();
			return;
		}

		//������IP �Ͷ˿�
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

		cout << "��UDP���ӳɹ�!" << endl;
	}
	catch(_com_error e)
	{
		cout << "��UDP����ʧ��!" << endl;
		cout << e.Description() << endl;
	}
}

//-----------------------------------------------------
//������
//str��Ҫת�����ַ���
//data������ת�����������ݴ�
//len:���ݳ���
//�������ܣ��ַ���ת��Ϊ���ݴ�
//-----------------------------------------------------
int strtodata(unsigned char *str, unsigned char *data, int len, int flag)
{
	unsigned char cTmp = 0;
	int i = 0;
	for (int j = 0; j<len; j++)
	{
		if (chartoint(str[i++], &cTmp))
			return 1;
		data[j] = cTmp;
		if (chartoint(str[i++], &cTmp))
			return 1;
		data[j] = (data[j] << 4) + cTmp;
		if (flag == 1)
			i++;
	}
	return 0;
}

//-----------------------------------------------------
//������
//chr��Ҫת�����ַ�
//cint������ת������������
//�������ܣ��ַ�ת��Ϊ����
//-----------------------------------------------------
int chartoint(unsigned char chr, unsigned char *cint)
{
	unsigned char cTmp;
	cTmp = chr - 48;
	if (cTmp >= 0 && cTmp <= 9)
	{
		*cint = cTmp;
		return 0;
	}
	cTmp = chr - 65;
	if (cTmp >= 0 && cTmp <= 5)
	{
		*cint = (cTmp + 10);
		return 0;
	}
	cTmp = chr - 97;
	if (cTmp >= 0 && cTmp <= 5)
	{
		*cint = (cTmp + 10);
		return 0;
	}
	return 1;
}

///�������ݿ�
void ConnectSql()
{
	try
	{
		::CoInitialize(NULL);//��ʼ��COM����
		m_pConnection.CreateInstance(__uuidof(Connection));//�������Ӷ���
		//m_pConnection->ConnectionString="Provider=SQLOLEDB; User ID=sa; Password=123456; Initial Catalog=VEHICLES_DATA; Data Source=."; //�뽫���ݿ���ӦID��Password����
		m_pConnection->ConnectionString = "Provider=SQLOLEDB; User ID=sa; Password=123456; Initial Catalog=VEHICLES_DATA; Data Source=10.211.55.24"; //�뽫���ݿ���ӦID��Password����

		//���ӷ����������ݿ�
		HRESULT hr=m_pConnection->Open("", "", "", 0);
		if(hr!=S_OK)
		{
			cout<<"�����ݿ�����ʧ��!"<<endl;
		}
		else
		{
			cout<<"�����ݿ����ӳɹ�!"<<endl;
		}
	}
	catch(_com_error e)
	{
		cout << e.Description() << endl;
	}
}

//�ر�����
void ExitConnect()
{
	try
	{
		if (m_pRecordset != NULL)
		{
			m_pRecordset->Close();
			m_pConnection->Close();
		}::CoUninitialize(); //�ͷŻ���
		cout << "�ر����ݿ�������ɹ�!" << endl;
		cout << "�ر����ݿ����ӳɹ�!" << endl;
	}
	catch (_com_error e)
	{
		cout << "�ر����ݿ�ʧ��!" << endl;
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
		cout<<"�����ݿ�������ɹ�!"<<endl;
	}
	catch(_com_error e)
	{
		cout<<e.Description()<<endl;
		m_pRecordset=NULL;
		//return m_pRecordset;
		cout<<"�����ݿ������ʧ��!"<<endl;
	}
	return m_pRecordset;
}
