using EBSComponent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSComponent.Model.EntityType
{
    public class EntityEBS : EntityBase
    {
        /// <summary>
        /// EBS站点默认设置
        /// </summary>
        public EntityEBS()
            : base(WebServerEnum.EBS)
        {
        }
    }
}
