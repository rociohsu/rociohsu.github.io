DROP TABLE CA20_FORM
CREATE TABLE CA20_FORM   /*線上訂購*/
(
	SNO			INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
	LANG		NVARCHAR(2)			NULL,					/*JP,TW*/
	NAM			NVARCHAR(50)		NULL,					/*平假名*/
	CNAM		NVARCHAR(50)		NULL,					/*片假名*/	
	ADR_CODE	NVARCHAR(50)		NULL,					/*區碼*/	
	CTY			NVARCHAR(50)		NULL,					/*縣市*/	
	ARA			NVARCHAR(50)		NULL,					/*地區*/	
	ADR			NVARCHAR(200)		NULL,					/*地址*/	
	TEL			NVARCHAR(100)		NULL,					/*電話*/	
	FAX			NVARCHAR(100)		NULL,					/*傳真*/	
	EMAIL		NVARCHAR(150)		NULL,					/*信箱*/	
	TYP			INT					NULL DEFAULT(0),		/*處理狀態*/	
	MEM			NVARCHAR(MAX)		NULL,					/*訂購內容*/
	STS			INT					NULL DEFAULT(0),		/*處理狀態*/
	RTN_TYP		NVARCHAR(50)		NULL,					/*回覆方式電話,傳真,信箱*/	
	RTN_MEM		NVARCHAR(MAX)		NULL,					/*回覆內容*/
	CRT_DAT		DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
	CRT_USR		NVARCHAR(50)        NULL,                   /*建檔人*/
	CRT_UNO		INT                 NULL DEFAULT(0),        /*建檔人*/  
	UPD_DAT		DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
	UPD_USR		NVARCHAR(50)        NULL,                   /*異動人*/
	UPD_UNO		INT                 NULL DEFAULT(0)        /*異動人*/
)
CREATE NONCLUSTERED INDEX CA20_FORM_1 ON CA20_FORM(LANG,TYP)
CREATE NONCLUSTERED INDEX CA20_FORM_2 ON CA20_FORM(STS)

DROP TABLE CA20_FORM_TYP
CREATE TABLE CA20_FORM_TYP /*課程*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  LANG		NVARCHAR(2)			NULL,					/*JP,TW*/
  NAM		NVARCHAR(50)		NULL,					/*標題*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0)        /*異動人*/
)


CREATE NONCLUSTERED INDEX CA20_NEWS_TYP_1 ON CA20_NEWS_TYP(LANG)
insert into ca20_news_typ (lang,nam) values ('CH',N'媒體報導')
insert into ca20_news_typ (lang,nam) values ('CH',N'活動訊息')
insert into ca20_news_typ (lang,nam) values ('CH',N'日常公告')

DROP TABLE CA20_NEWS
CREATE TABLE CA20_NEWS /*課程*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  LANG		NVARCHAR(2)			NULL,					/*JP,TW*/
  TPC		NVARCHAR(50)		NULL,					/*標題*/
  TYP		INT					NULL DEFAULT(0),		/*媒體報導,活動訊息,日常公告*/
  ONF		INT					NULL DEFAULT(0),		/*0=下,1=上架*/
  SDT		DATETIME			NULL,
  EDT		DATETIME			NULL,
  IMG		NVARCHAR(250)		NULL,                    /*連結*/
  LNK		NVARCHAR(250)		NULL,                    /*連結*/
  LNK_TGT	NVARCHAR(20)		NULL,                    /*連結*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  MEM		NVARCHAR(max)		NULL					/*課程內容介紹*/
)
CREATE NONCLUSTERED INDEX CA20_NEWS_1 ON CA20_NEWS(ONF,SDT,EDT)
CREATE NONCLUSTERED INDEX CA20_NEWS_2 ON CA20_NEWS(TYP)

DROP TABLE CA20_NEWS_TYP
CREATE TABLE CA20_NEWS_TYP /*課程*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  LANG		NVARCHAR(2)			NULL,					/*JP,TW*/
  NAM		NVARCHAR(50)		NULL,					/*標題*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0)        /*異動人*/
  
)
CREATE NONCLUSTERED INDEX CA20_NEWS_TYP_1 ON CA20_NEWS_TYP(LANG)
insert into ca20_news_typ (lang,nam) values ('CH',N'媒體報導')
insert into ca20_news_typ (lang,nam) values ('CH',N'活動訊息')
insert into ca20_news_typ (lang,nam) values ('CH',N'日常公告')