--创建 BY 仇士龙
--日期 2015年11月19日9:48:56
--简介 在数据表中插入数据

  
DECLARE @COUNT AS INT
SET @COUNT = 1

--循环插入User表和Role表  
WHILE @COUNT < 100
    BEGIN
        INSERT  INTO dbo.CICUser
                ( Username ,
                  PasswordHash ,
                  Email ,
                  PhoneNumber ,
                  IsFirstTimeLogin ,
                  AccessFailedCount ,
                  CreationDate ,
                  IsActive
                )
        VALUES  ( N'User-' + CONVERT(CHAR(2), @COUNT) , -- Username - nvarchar(256)
                  N'PWDHASH-' + CONVERT(CHAR(2), @COUNT) , -- PasswordHash - nvarchar(500)
                  N'EMAIL-' + CONVERT(CHAR(2), @COUNT) , -- Email - nvarchar(256)
                  N'PHONE-' + CONVERT(CHAR(2), @COUNT) , -- PhoneNumber - nvarchar(30)
                  0 , -- IsFirstTimeLogin - bit
                  0 , -- AccessFailedCount - int
                  GETDATE() , -- CreationDate - datetime
                  0  -- IsActive - bit
                )
         
        INSERT  INTO dbo.CICRole
                ( RoleName )
        VALUES  ( N'ROLE-' + CONVERT(CHAR(2), @COUNT)  -- RoleName - nvarchar(256)
                  )
  
        SET @COUNT += 1
    END
    
--创建关联关系表中的数据    
INSERT  INTO dbo.CICUserRole
        ( UserId ,
          RoleId
        )
        SELECT  T.UserId ,
                T.RoleId
        FROM    ( SELECT    U.UserId AS UserId ,
                            R.RoleId AS RoleId
                  FROM      dbo.CICUser AS U
                            CROSS JOIN dbo.CICRole AS R
                ) AS T