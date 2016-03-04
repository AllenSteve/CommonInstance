using EBSComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        protected WebServerEnum webServer { get; set; }

        private OperationModeEnum operationMode { get; set; }

        /// <summary>
        /// 运行环境枚举
        /// </summary>
        protected OperationModeEnum OperationMode
        {
            get
            {
                if(this.operationMode==null)
                {
                    this.operationMode = this.IsDebugMode() ? OperationModeEnum.TEST : OperationModeEnum.PRODUCTION;
                }
                return this.operationMode;
            }
        }

        /// <summary>
        /// 解析HTTP上下文
        /// </summary>
        /// <param name="context">HTTP上下文对象</param>
        /// <returns>解析字符串</returns>
        protected string ParseHttpContext(HttpContext context, string key)
        {
            if (context == null || context.Items[key] == null)
            {
                return string.Empty;
            }
            else
            {
                return context.Items[key].ToString();
            }
        }

        /// <summary>
        /// 判断当前是否为调试模式
        /// </summary>
        /// <returns>当前模式</returns>
        protected bool IsDebugMode()
        {
            return this.ParseHttpContext(HttpContext.Current, "IsTestUser").Equals("1");
        }


        public EntityBase()
        {
            this.entityName = new StringBuilder();
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="web">站点名称</param>
        /// <param name="operation">运行环境名称</param>
        public EntityBase(WebServerEnum web)
        {
            this.webServer = web;
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

                if (this.OperationMode == OperationModeEnum.TEST)
                {
                    this.entityName.Append("_");
                    this.entityName.Append(this.operationMode.ToString());
                }
                return (EntityTypeEnum)Enum.Parse(typeof(EntityTypeEnum), this.entityName.ToString());
            }
        }


    }
}
