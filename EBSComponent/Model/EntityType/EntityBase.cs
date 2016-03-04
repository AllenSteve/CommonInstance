﻿using EBSComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Model.EntityType
{
    public class EntityBase
    {
        /// <summary>
        /// 服务站点枚举
        /// </summary>
        public WebServerEnum webServer { get; set; }

        /// <summary>
        /// 访问权限枚举
        /// </summary>
        public AccessModeEnum accessMode { get; set; }

        /// <summary>
        /// 运行环境枚举
        /// </summary>
        public OperationModeEnum operationMode { get; set; }

        /// <summary>
        /// 获取数据库连接类型
        /// </summary>
        public EntityTypeEnum GetEntityType()
        {
            return EntityTypeEnum.LOCAL_DATABASE;
        }
    }
}