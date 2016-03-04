using EBSComponent.Enums;
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
        /// 数据库名称
        /// </summary>
        protected StringBuilder entityName { get; set; }

        /// <summary>
        /// 服务站点枚举
        /// </summary>
        public WebServerEnum webServer { get; set; }

        /// <summary>
        /// 运行环境枚举
        /// </summary>
        public OperationModeEnum operationMode { get; set; }

        public EntityBase()
        {
            this.entityName = new StringBuilder();
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="web">站点名称</param>
        /// <param name="operation">运行环境名称</param>
        public EntityBase(WebServerEnum web, OperationModeEnum operation)
        {
            this.webServer = web;
            this.operationMode = operation;
        }

        /// <summary>
        /// 获取数据库连接类型
        /// </summary>
        /// <param name="access">数据库访问权限</param>
        /// <returns>数据库连接名枚举</returns>
        public EntityTypeEnum GetEntityType(AccessModeEnum access = AccessModeEnum.READ)
        {
            if(this.webServer == WebServerEnum.LOCALHOST)
            {
                return EntityTypeEnum.LOCAL_DATABASE;
            }
            else
            {
                this.entityName.Append(this.webServer.ToString());
                this.entityName.Append("_");
                this.entityName.Append(access.ToString());

                if (operationMode == OperationModeEnum.TEST)
                {
                    this.entityName.Append("_");
                    this.entityName.Append(this.operationMode.ToString());
                }
                return (EntityTypeEnum)Enum.Parse(typeof(EntityTypeEnum), this.entityName.ToString());
            }
        }
    }
}
