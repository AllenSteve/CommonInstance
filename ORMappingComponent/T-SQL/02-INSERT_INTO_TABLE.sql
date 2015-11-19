--���� BY ��ʿ��
--���� 2015��11��19��9:48:56
--��� �����ݱ��в�������

  
DECLARE @COUNT AS INT
SET @COUNT = 1

--ѭ������User���Role��  
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
    
--����������ϵ���е�����    
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