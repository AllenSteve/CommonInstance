using EBSComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Model.EntityType
{
    public class EntityOpenPlatform : EntityBase
    {
        /// <summary>
        /// 开放平台站点默认设置
        /// </summary>
        public EntityOpenPlatform()
            : base(WebServerEnum.OPENPLATFORM)
        {

        }
    }
}
