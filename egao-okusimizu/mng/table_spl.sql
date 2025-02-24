
DROP TABLE CABASE_USER
CREATE TABLE CABASE_USER /*人員檔*/
(
  UNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  UID		NVARCHAR(30)        NULL,       /*帳號*/
  PWD		NVARCHAR(100)        NULL,       /*密碼*/
  NAM		NVARCHAR(30)        NULL,       /*姓名*/
  UNT		NVARCHAR(100)       NULL,       /*部門單位*/
  UNT_MNG	INT                 NULL DEFAULT(0),        /*0部門人員,1部門主管,2跨部門人員*/
  TEL_A		NVARCHAR(10)        NULL,       /*人才廠商tel區碼*/
  TEL		NVARCHAR(100)       NULL,       /*人才廠商tel*/
  FAX_A		NVARCHAR(10)        NULL,       /*人才廠商傳真區碼*/
  FAX		NVARCHAR(100)       NULL,       /*人才廠商傳真*/
  EMAIL		NVARCHAR(100)       NULL,       /*廠商聯絡人EMAIL,人才EMAIL*/
  ADM		INT                 NULL DEFAULT(0),        /*1一般user,2管理者*/
  GRS		INT                 NULL DEFAULT(0),        /*屬0部份群組,1全部*/
  AGN1		INT                 NULL DEFAULT(0),        /*代理人1,=CABASE_user.uno*/
  AGN1_ONF	INT                 NULL DEFAULT(0),        /*代理人1啟用*/
  AGN2		INT                 NULL DEFAULT(0),        /*代理人2,=CABASE_user.uno*/
  AGN2_ONF	INT                 NULL DEFAULT(0),        /*代理人2啟用*/
  ONF		INT                 NULL DEFAULT(0),        /*0帳號停用,1正常*/

  LOGIN_TIME	DATETIME        NULL,                   /*最後登入日期*/
  LOGIN_IP	NVARCHAR(100)       NULL,                   /*最後登入IP*/
  LOGIN_CNT	INT                 NULL DEFAULT(0),        /*登入總次數*/
  LOGIN_FAIL	INT                 NULL DEFAULT(0),        /*登入總次數*/
  CHG_PWD	DATETIME            NULL DEFAULT(GETUTCDATE()),	/*最後更改密碼的時間*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)

insert into CABASE_user (uid,pwd,nam,adm,grs,onf,crt_uno) values ('admin','sampleadm','admin',2,1,1,1)
CREATE UNIQUE NONCLUSTERED INDEX CABASE_USER_1 ON CABASE_USER(UID,PWD)
CREATE NONCLUSTERED INDEX CABASE_USER_2 ON CABASE_USER(GRS)



DROP TABLE CABASE_USER_GROUP
CREATE TABLE CABASE_USER_GROUP /*人員群組*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,  /*系統編號*/
  UNO		INT                 NULL DEFAULT(0),        /*標題key*/
  GRNO		INT                 NULL DEFAULT(0),        /*GNO或TNO領域*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_USER_GROUP_1 ON CABASE_USER_GROUP(UNO)
CREATE NONCLUSTERED INDEX CABASE_USER_GROUP_2 ON CABASE_USER_GROUP(GRNO)

DROP TABLE CABASE_USER_ITO
CREATE TABLE CABASE_USER_ITO /*人員單元權限*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  UNO		INT                 NULL DEFAULT(0),        /*標題key*/
  ITO		INT                 NULL DEFAULT(0),        /*GNO或TNO領域*/
  ADE       NVARCHAR(50)        NULL,       /*A新增,D刪除,E修改,B瀏覽,C類別*/  
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_USER_ITO_1 ON CABASE_USER_ITO(UNO)
CREATE NONCLUSTERED INDEX CABASE_USER_ITO_2 ON CABASE_USER_ITO(ITO)


DROP TABLE CABASE_ITO
CREATE TABLE CABASE_ITO /*後端單元*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  NAM		NVARCHAR(100)       NULL,                   /*名稱*/
  NAM_E		NVARCHAR(100)       NULL,                   /*名稱*/
  URL		NVARCHAR(500)       NULL,                   /*後端*/
  URL2		NVARCHAR(500)       NULL,                   /*前端*/
  FSNO		INT                 NULL DEFAULT(0),        /*上層*/
  CNT		INT                 NULL DEFAULT(0),        /*子層數*/
  SOT		INT                 NULL DEFAULT(1),        /*排列*/
  UNT_LOC	INT                 NULL DEFAULT(0),        /*0不限,1只限看自己資料,但該單位主管等級不限,職務代理人也不限*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_ITO_1 ON CABASE_ITO(FSNO)




INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'系統管理','',0,5,7,1)  /*1*/ 
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'會員管理','',0,5,1,1)  /*1*/ 
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'會員資料','JKZ_MEMBER.ASPX',2,0,1,1)

/*INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES ('流量監控','',0,3,3,1)*/
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'系統設定','MLZ_SETUP.ASPX',1,0,1,1)
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'人員資料','MLZ_USER.ASPX',1,0,2,1)
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'群組管理','MLZ_GROUPS.ASPX',1,0,3,1)
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'系統記錄','MLZ_SYSLOG.ASPX',1,0,4,1)
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'後台選單','MLZ_ITO.ASPX',1,0,5,1)
INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'人事出勤','',0,5,7,1)  /*2*/ 

INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'人事出勤','',0,1,7,1)  /*2*/ 
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'部門層級維護','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'請假簽核層級維護','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'請假單','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'假別維護','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'國定假日維護','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'加班單','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'審核作業','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'加班單統計表','JKZ_WORKFLOW.ASPX',2,0,1,1)
--INSERT INTO CABASE_ITO (NAM,URL,FSNO,CNT,SOT,CRT_UNO) VALUES (N'人事出勤統計表','JKZ_WORKFLOW.ASPX',2,0,1,1)



DROP TABLE CABASE_GROUP
CREATE TABLE CABASE_GROUP /*群組*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  NAM		NVARCHAR(100)		NULL,       /*名稱*/
  NAM_E		NVARCHAR(100)		NULL,       /*名稱*/
  URL		NVARCHAR(500)		NULL DEFAULT(0),        /*領域分類*/
  FSNO		INT                 NULL DEFAULT(0),        /*上層*/
  CNT		INT                 NULL DEFAULT(0),        /*子層數*/
  SOT		INT                 NULL DEFAULT(1),        /*排列*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_GROUP_1 ON CABASE_GROUP(FSNO)


DROP TABLE CABASE_GROUP_ITO
CREATE TABLE CABASE_GROUP_ITO /*群組單元權限*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  GNO		INT                 NULL DEFAULT(0),        /*標題key*/
  ITO		INT                 NULL DEFAULT(0),        /*GNO或TNO領域*/
  ADE		NVARCHAR(50)		NULL,       /*A新增,D刪除,E修改,B瀏覽,C類別*/  
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_GROUP_ITO_1 ON CABASE_GROUP_ITO(GNO)
CREATE NONCLUSTERED INDEX CABASE_GROUP_ITO_2 ON CABASE_GROUP_ITO(ITO)


DROP TABLE CABASE_AREA_CODE
CREATE TABLE CABASE_AREA_CODE /*地區國別*/
(
  SNO		INT			IDENTITY(1,1) PRIMARY KEY,	/*系統編號*/
  CTY		NVARCHAR(50)		NULL,		/*縣市名*/

  ARB		NVARCHAR(5)			NULL,		/*北中南東*/
  SOT		INT					NULL DEFAULT(1),		/*排列*/
  ARA		NVARCHAR(50)		NULL,		/*區名*/
  CTY_CODE	NVARCHAR(2)		NULL DEFAULT(1),			/*縣市代碼1~24*/
  A_CODE	NVARCHAR(10)		NULL DEFAULT(1),		/*區碼*/
  P_CODE	NVARCHAR(10)		NULL DEFAULT(1),		/*電話區碼*/	
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)

CREATE  NONCLUSTERED INDEX CABASE_AREA_CODE_1 ON CABASE_AREA_CODE(A_CODE)
CREATE  NONCLUSTERED INDEX CABASE_AREA_CODE_2 ON CABASE_AREA_CODE(P_CODE)
CREATE  NONCLUSTERED INDEX CABASE_AREA_CODE_3 ON CABASE_AREA_CODE(CTY)
CREATE  NONCLUSTERED INDEX CABASE_AREA_CODE_4 ON CABASE_AREA_CODE(SOT)
CREATE  NONCLUSTERED INDEX CABASE_AREA_CODE_5 ON CABASE_AREA_CODE(ARA)


DROP TABLE CABASE_SETUP
CREATE TABLE CABASE_SETUP /*系統設定*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  NAM		NVARCHAR(100)       NULL,       /*名稱*/
  NAM_E		NVARCHAR(100)       NULL,       /*名稱*/
  URL		NVARCHAR(500)       NULL DEFAULT(0),        /*後端*/
  URL2		NVARCHAR(500)       NULL DEFAULT(0),        /*前端*/
  FSNO		INT                 NULL DEFAULT(0),        /*上層*/
  CNT		INT                 NULL DEFAULT(0),        /*子層數*/
  SOT		INT                 NULL DEFAULT(1),        /*排列*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
)
CREATE NONCLUSTERED INDEX CABASE_SETUP_1 ON CABASE_SETUP(FSNO)


INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理標題',N'後台管理系統',1,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理版權','Copyright 2019',2,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報發信E-mail',N'系統管理員<liang.jack1974@gmail.com>',3,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報回覆E-mail',N'系統管理員<liang.jack1974@gmail.com>',4,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報網域','@gmail.com',5,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報發信帳號','',6,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報發信密碼','',7,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報驗證0或1','0',8,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報UTF8格式','NO',9,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理電子報E-mail主機IP','127.0.0.1',10,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理目錄名稱','MNG',11,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理主選單格式',N'左',12,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理IP','*,127.0.0.1',13,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理HTTP網址','HTTP://',13,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理啟用子網站功能','N',14,1)
INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理程式人員E-mail','liang.jack1974@gmail.com',15,1)


INSERT INTO CABASE_SETUP (NAM,URL,SOT,CRT_UNO) VALUES (N'後台管理程式人員E-mail','liang.jack1974@gmail.com',15,1)





insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',1,N'中正區',100,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',2,N'大同區',103,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',3,N'中山區',104,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',4,N'松山區',105,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',5,N'大安區',106,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',6,N'萬華區',108,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',7,N'信義區',110,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',8,N'士林區',111,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',9,N'北投區',112,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',10,N'內湖區',114,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',11,N'南港區',115,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺北市',12,N'文山區',116,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',13,N'仁愛區',200,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',14,N'信義區',201,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',15,N'中正區',202,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',16,N'中山區',203,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',17,N'安樂區',204,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',18,N'暖暖區',205,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'基隆市',19,N'七堵區',206,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',20,N'萬里區',207,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',21,N'金山區',208,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',22,N'板橋區',220,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',23,N'汐止區',221,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',24,N'深坑區',222,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',25,N'石碇區',223,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',26,N'瑞芳區',224,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',27,N'平溪區',226,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',28,N'雙溪區',227,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',29,N'貢寮區',228,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',30,N'新店區',231,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',31,N'坪林區',232,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',32,N'烏來區',233,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',33,N'永和區',234,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',34,N'中和區',235,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',35,N'土城區',236,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',36,N'三峽區',237,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',37,N'樹林區',238,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',38,N'鶯歌區',239,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',39,N'三重區',241,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',40,N'新莊區',242,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',41,N'泰山區',243,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',42,N'林口區',244,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',43,N'蘆洲區',247,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',44,N'五股區',248,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',45,N'八里區',249,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',46,N'淡水區',251,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',47,N'三芝區',252,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新北市',48,N'石門區',253,2)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',49,N'宜蘭市',260,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',50,N'頭城鎮',261,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',51,N'礁溪鄉',262,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',52,N'壯圍鄉',263,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',53,N'員山鄉',264,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',54,N'羅東鎮',265,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',55,N'三星鄉',266,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',56,N'大同鄉',267,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',57,N'五結鄉',268,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',58,N'冬山鄉',269,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',59,N'蘇澳鎮',270,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'宜蘭縣',60,N'南澳鄉',272,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹市',61,N'新竹市',300,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',62,N'竹北市',302,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',63,N'湖口鄉',303,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',64,N'新豐鄉',304,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',65,N'新埔鎮',305,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',66,N'關西鎮',306,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',67,N'芎林鄉',307,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',68,N'寶山鄉',308,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',69,N'竹東鎮',310,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',70,N'五峰鄉',311,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',71,N'橫山鄉',312,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',72,N'尖石鄉',313,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',73,N'北埔鄉',314,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'新竹縣',74,N'峨眉鄉',315,35)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',75,N'中壢區',320,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',76,N'平鎮區',324,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',77,N'龍潭區',325,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',78,N'楊梅區',326,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',79,N'新屋區',327,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',80,N'觀音區',328,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',81,N'桃園區',330,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',82,N'龜山區',333,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',83,N'八德區',334,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',84,N'大溪區',335,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',85,N'復興區',336,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',86,N'大園區',337,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'桃園市',87,N'蘆竹區',338,33)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',88,N'竹南鎮',350,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',89,N'頭份鎮',351,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',90,N'三灣鄉',352,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',91,N'南庄鄉',353,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',92,N'獅潭鄉',354,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',93,N'後龍鎮',356,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',94,N'通宵鎮',357,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',95,N'苑裡鎮',358,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',96,N'苗栗市',360,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',97,N'造橋鄉',361,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',98,N'頭屋鄉',362,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',99,N'公館鄉',363,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',100,N'大湖鄉',364,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',101,N'泰安鄉',365,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',102,N'銅鑼鄉',366,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',103,N'三義鄉',367,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',104,N'西湖鄉',368,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'苗栗縣',105,N'卓蘭鎮',369,37)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',106,N'中區',400,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',107,N'東區',401,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',108,N'南區',402,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',109,N'西區',403,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',110,N'北區',404,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',111,N'北屯區',406,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',112,N'西屯區',407,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',113,N'南屯區',408,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',114,N'太平區',411,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',115,N'大里區',412,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',116,N'霧峰區',413,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',117,N'烏日區',414,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',118,N'豐原區',420,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',119,N'后里區',421,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',120,N'石岡區',422,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',121,N'東勢區',423,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',122,N'和平區',424,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',123,N'新社區',426,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',124,N'潭子區',427,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',125,N'大雅區',428,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',126,N'神岡區',429,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',127,N'大肚區',432,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',128,N'沙鹿區',433,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',129,N'龍井區',434,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',130,N'梧棲區',435,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',131,N'清水區',436,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',132,N'大甲區',437,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',133,N'外埔區',438,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺中市',134,N'大安區',439,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',135,N'彰化市',500,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',136,N'芬園鄉',502,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',137,N'花壇鄉',503,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',138,N'秀水鄉',504,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',139,N'鹿港鎮',505,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',140,N'福興鄉',506,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',141,N'線西鄉',507,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',142,N'和美鎮',508,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',143,N'伸港鄉',509,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',144,N'員林鎮',510,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',145,N'社頭鄉',511,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',146,N'永靖鄉',512,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',147,N'埔心鄉',513,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',148,N'溪湖鎮',514,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',149,N'大村鄉',515,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',150,N'埔鹽鄉',516,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',151,N'田中鎮',520,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',152,N'北斗鎮',521,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',153,N'田尾鄉',522,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',154,N'埤頭鄉',523,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',155,N'溪州鄉',524,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',156,N'竹塘鄉',525,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',157,N'二林鎮',526,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',158,N'大城鄉',527,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',159,N'芳苑鄉',528,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'彰化縣',160,N'二水鄉',530,4)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',161,N'南投市',540,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',162,N'中寮鄉',541,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',163,N'草屯鎮',542,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',164,N'國姓鄉',544,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',165,N'埔里鎮',545,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',166,N'仁愛鄉',546,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',167,N'名間鄉',551,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',168,N'集集鎮',552,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',169,N'水里鄉',553,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',170,N'魚池鄉',555,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',171,N'信義鄉',556,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',172,N'竹山鎮',557,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南投縣',173,N'鹿谷鄉',558,49)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義市',174,N'嘉義市',600,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',175,N'番路鄉',602,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',176,N'梅山鄉',603,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',177,N'竹崎鄉',604,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',178,N'阿里山',605,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',179,N'中埔鄉',606,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',180,N'大埔鄉',607,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',181,N'水上鄉',608,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',182,N'鹿草鄉',611,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',183,N'太保市',612,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',184,N'朴子市',613,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',185,N'東石鄉',614,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',186,N'六腳鄉',615,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',187,N'新港鄉',616,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',188,N'民雄鄉',621,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',189,N'大林鎮',622,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',190,N'溪口鄉',623,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',191,N'義竹鄉',624,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'嘉義縣',192,N'布袋鎮',625,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',193,N'斗南鎮',630,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',194,N'大埤鄉',631,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',195,N'虎尾鎮',632,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',196,N'土庫鎮',633,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',197,N'褒忠鄉',634,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',198,N'東勢鄉',635,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',199,N'臺西鄉',636,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',200,N'崙背鄉',637,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',201,N'麥寮鄉',638,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',202,N'斗六市',640,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',203,N'林內鄉',643,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',204,N'古坑鄉',646,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',205,N'莿桐鄉',647,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',206,N'西螺鎮',648,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',207,N'二崙鄉',649,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',208,N'北港鎮',651,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',209,N'水林鄉',652,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',210,N'口湖鄉',653,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',211,N'四湖鄉',654,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'雲林縣',212,N'元長鄉',655,5)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',213,N'中西區',700,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',214,N'東區',701,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',215,N'南區',702,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',217,N'北區',704,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',218,N'安平區',708,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',219,N'安南區',709,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',220,N'永康區',710,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',221,N'歸仁區',711,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',222,N'新化區',712,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',223,N'左鎮區',713,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',224,N'玉井區',714,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',225,N'楠西區',715,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',226,N'南化區',716,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',227,N'仁德區',717,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',228,N'關廟區',718,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',229,N'龍崎區',719,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',230,N'官田區',720,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',231,N'麻豆區',721,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',232,N'佳里區',722,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',233,N'西港區',723,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',234,N'七股區',724,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',235,N'將軍區',725,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',236,N'學甲區',726,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',237,N'北門區',727,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',238,N'新營區',730,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',239,N'後壁區',731,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',240,N'白河區',732,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',241,N'東山區',733,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',242,N'六甲區',734,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',243,N'下營區',735,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',244,N'柳營區',736,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',245,N'鹽水區',737,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',246,N'善化區',741,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',247,N'大內區',742,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',248,N'山上區',743,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',249,N'新市區',744,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺南市',250,N'安定區',745,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',251,N'新興區',800,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',252,N'前金區',801,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',253,N'苓雅區',802,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',254,N'鹽埕區',803,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',255,N'鼓山區',804,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',256,N'旗津區',805,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',257,N'前鎮區',806,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',258,N'三民區',807,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',259,N'楠梓區',811,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',260,N'小港區',812,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',261,N'左營區',813,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',262,N'仁武區',814,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',263,N'大社區',815,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',264,N'岡山區',820,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',265,N'路竹區',821,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',266,N'阿蓮區',822,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',267,N'田寮區',823,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',268,N'燕巢區',824,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',269,N'橋頭區',825,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',270,N'梓官區',826,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',271,N'彌陀區',827,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',272,N'永安區',828,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',273,N'湖內區',829,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',274,N'鳳山區',830,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',275,N'大寮區',831,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',276,N'林園區',832,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',277,N'鳥松區',833,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',278,N'大樹區',840,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',279,N'旗山區',842,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',280,N'美濃區',843,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',281,N'六龜區',844,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',282,N'內門區',845,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',283,N'杉林區',846,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',284,N'甲仙區',847,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',285,N'桃源區',848,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',286,N'三民區',849,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',287,N'茂林區',851,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'高雄市',288,N'茄定區',852,7)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',289,N'馬公市',880,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',290,N'西嶼鄉',881,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',291,N'望安鄉',882,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',292,N'七美鄉',883,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',293,N'白沙鄉',884,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'澎湖縣',294,N'湖西鄉',885,6)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',295,N'屏東市',900,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',296,N'三地門鄉',901,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',297,N'霧臺鄉',902,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',298,N'瑪家鄉',903,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',299,N'九如鄉',904,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',300,N'里港鄉',905,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',301,N'高樹鄉',906,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',302,N'鹽埔鄉',907,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',303,N'長治鄉',908,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',304,N'麟洛鄉',909,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',305,N'竹田鄉',911,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',306,N'內埔鄉',912,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',307,N'萬丹鄉',913,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',308,N'潮州鎮',920,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',309,N'泰武鄉',921,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',310,N'來義鄉',922,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',311,N'萬巒鄉',923,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',312,N'崁頂鄉',924,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',313,N'新埤鄉',925,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',314,N'南州鄉',926,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',315,N'林邊鄉',927,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',316,N'東港鎮',928,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',317,N'琉球鄉',929,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',318,N'佳冬鄉',931,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',319,N'新園鄉',932,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',320,N'枋寮鄉',940,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',321,N'枋山鄉',941,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',322,N'春日鄉',942,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',323,N'獅子鄉',943,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',324,N'車城鄉',944,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',325,N'牡丹鄉',945,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',326,N'恆春鎮',946,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'屏東縣',327,N'滿州鄉',947,8)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',328,N'臺東市',950,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',329,N'綠島鄉',951,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',330,N'蘭嶼鄉',952,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',331,N'延平鄉',953,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',332,N'卑南鄉',954,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',333,N'鹿野鄉',955,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',334,N'關山鎮',956,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',335,N'海瑞鄉',957,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',336,N'池上鄉',958,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',337,N'東河鄉',959,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',338,N'成功鎮',961,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',339,N'長濱鄉',962,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',340,N'太麻里鄉',963,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',341,N'金峰鄉',964,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',342,N'大武鄉',965,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'臺東縣',343,N'達仁鄉',966,89)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',344,N'花蓮市',970,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',345,N'新城鄉',971,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',346,N'秀林鄉',972,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',347,N'吉安鄉',973,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',348,N'壽豐鄉',974,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',349,N'鳳林鄉',975,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',350,N'光復鄉',976,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',351,N'豐濱鄉',977,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',352,N'瑞穗鄉',978,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',353,N'萬榮鄉',979,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',354,N'玉里鎮',981,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',355,N'卓溪鄉',982,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'花蓮縣',356,N'富里鄉',983,3)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',357,N'金沙鎮',890,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',358,N'金湖鎮',891,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',359,N'金寧鄉',892,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',360,N'金城鎮',893,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',361,N'烈嶼鄉',894,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'金門縣',362,N'烏坵鄉',896,82)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'連江縣',363,N'南竿鄉',209,836)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'連江縣',364,N'北竿鄉',210,836)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'連江縣',365,N'莒光鄉',211,836)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'連江縣',366,N'東引鄉',212,836)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南海諸島',367,N'東沙群島',817,827)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'南海諸島',368,N'南沙群島',819,827)
insert into CABASE_area_code (cty,sot,ara,a_code,p_code) values  (N'釣魚台列嶼',369,N'釣魚台列嶼',290,2)

update CABASE_area_code set arb=N'北部' where cty in (N'基隆市',N'臺北市',N'新北市',N'桃園市',N'新竹市',N'新竹縣',N'宜蘭縣')
update CABASE_area_code set arb=N'中部' where cty in (N'苗栗縣',N'臺中市',N'彰化縣',N'雲林縣',N'南投縣')
update CABASE_area_code set arb=N'南部' where cty in (N'嘉義縣',N'嘉義市',N'臺南市',N'高雄市',N'屏東縣')
update CABASE_area_code set arb=N'東部' where cty in (N'花蓮縣',N'臺東縣')
update CABASE_area_code set arb=N'離島' where cty in (N'金門縣',N'連江縣',N'南海諸島',N'釣魚台列嶼',N'澎湖縣')


update cabase_area_code set cty_code='01' where cty=N'基隆市'
update cabase_area_code set cty_code='02' where cty=N'臺北市'
update cabase_area_code set cty_code='03' where cty=N'新北市'
update cabase_area_code set cty_code='04' where cty=N'桃園市'
update cabase_area_code set cty_code='05' where cty=N'新竹市'
update cabase_area_code set cty_code='06' where cty=N'新竹縣'
update cabase_area_code set cty_code='07' where cty=N'苗栗縣'
update cabase_area_code set cty_code='08' where cty=N'臺中市'
update cabase_area_code set cty_code='09' where cty=N'彰化縣'
update cabase_area_code set cty_code='10' where cty=N'南投縣'
update cabase_area_code set cty_code='11' where cty=N'雲林縣'
update cabase_area_code set cty_code='12' where cty=N'嘉義市'
update cabase_area_code set cty_code='13' where cty=N'嘉義縣'
update cabase_area_code set cty_code='14' where cty=N'臺南市'
update cabase_area_code set cty_code='15' where cty=N'高雄市'
update cabase_area_code set cty_code='16' where cty=N'屏東縣'
update cabase_area_code set cty_code='17' where cty=N'臺東縣'
update cabase_area_code set cty_code='18' where cty=N'花蓮縣'
update cabase_area_code set cty_code='19' where cty=N'宜蘭縣'
update cabase_area_code set cty_code='20' where cty=N'澎湖縣'
update cabase_area_code set cty_code='21' where cty=N'金門縣'
update cabase_area_code set cty_code='22' where cty=N'連江縣'
update cabase_area_code set cty_code='23' where cty=N'南海諸島'
update cabase_area_code set cty_code='24' where cty=N'釣魚台列嶼'


DROP TABLE CABASE_SYS_LOG
CREATE TABLE CABASE_SYS_LOG /*網站流量*/
( SNO		INT			IDENTITY(1,1) PRIMARY KEY,	    /*系統編號*/ 
  APPNAM	NVARCHAR(100)        NULL,                   /*單元*/
  UNT       NVARCHAR(100)        NULL,                   /*單元*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔ip*/
  CRT_IP	NVARCHAR(50)        NULL,                   /*建檔ip*/
  MEM		NTEXT				null
)
CREATE NONCLUSTERED INDEX CABASE_SYS_LOG_1 ON CABASE_SYS_LOG(UNT)




DROP TABLE MLBASE_CMSDTL
CREATE TABLE MLBASE_CMSDTL /*文章檔*/
(
  SNO		INT         IDENTITY(1,1) PRIMARY KEY,      /*系統編號*/
  UNTA		NVARCHAR(100)		NULL,                   /*父單元用*/
  UNTB		NVARCHAR(100)		NULL,                   /*子單元用*/
  SNOA		INT					NULL DEFAULT(0),		/*父SNO*/
  SNOB		INT					NULL DEFAULT(0),		/*字SNO*/
  FSNO		INT					NULL DEFAULT(0),		/*typ=6時,會有fsno*/
  TYP		NVARCHAR(2)		NULL ,		/*1txt,2HTML,3圖+文,4檔案,5連結,6多圖片*/
  TPC		NVARCHAR(100)		NULL,					/*標題*/
  IMG		NVARCHAR(250)		NULL,					/*圖片或檔案路徑*/
  ALIGN		NVARCHAR(10)		NULL,					/*圖片置左,中,右*/
  ALT		NVARCHAR(250)		NULL,					/*圖說*/
  TGT		NVARCHAR(10)		NULL,					/*連結時,另開視窗*/
  LNK		NVARCHAR(250)		NULL,					/*圖說*/
  WDH		INT					NULL DEFAULT(0),		/*圖寬*/
  HGH		INT					NULL DEFAULT(0),		/*圖高*/
  SIZ		INT					NULL DEFAULT(0),		/*圖尺寸*/
  SOT		INT					NULL DEFAULT(0),		/*排序*/
  MEM		NVARCHAR(MAX)		NULL,
  CRT_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*建檔日*/
  CRT_USR	NVARCHAR(50)        NULL,                   /*建檔人*/
  CRT_UNO	INT                 NULL DEFAULT(0),        /*建檔人*/  
  UPD_DAT	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT2	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR2	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO2	INT                 NULL DEFAULT(0),        /*異動人*/
  UPD_DAT3	DATETIME            NULL DEFAULT(GETUTCDATE()),/*異動日*/
  UPD_USR3	NVARCHAR(50)        NULL,                   /*異動人*/
  UPD_UNO3	INT                 NULL DEFAULT(0)         /*異動人*/
  
)

CREATE NONCLUSTERED INDEX MLBASE_CMSDTL_1 ON MLBASE_CMSDTL(FSNO)
CREATE NONCLUSTERED INDEX MLBASE_CMSDTL_2 ON MLBASE_CMSDTL(TYP)
CREATE NONCLUSTERED INDEX MLBASE_CMSDTL_3 ON MLBASE_CMSDTL(UNTA,UNTB,SNOA,SNOB)