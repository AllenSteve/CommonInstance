#region
/*************************************************/
/***** Create by zoujiliang 2015/11/24 11:41:38****/
/*************************************************/
using System;
using System.Collections.Generic;
using System.Text;
namespace EOPComponent.Model.Entity
{
    public partial class Partner_Company
    {
        /// <summary>
        /// 合作伙伴公司编码
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 城市编码
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态，0停用，1启用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除，0否，1是
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 公司的母公司
        /// </summary>
        public int ParentCompanyID { get; set; }
        /// <summary>
        /// 公司类型，0搜房直销，1合作伙伴
        /// </summary>
        public int CompanyType { get; set; }
        /// <summary>
        /// 资质认证图片Url
        /// </summary>
        public string CertUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyLevels { get; set; }
        /// <summary>
        /// 对接销售名称
        /// </summary>
        public string SalerName { get; set; }
        /// <summary>
        /// 公司层级
        /// </summary>
        public int CompanyLevel { get; set; }
        /// <summary>
        /// 公司类型,有角色0,无角色1
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNO { get; set; }
        /// <summary>
        /// 录入人真实姓名
        /// </summary>
        public string EntryName { get; set; }
        /// <summary>
        /// 录入人搜房ID
        /// </summary>
        public long EntrySoufunID { get; set; }
        /// <summary>
        /// 录入人搜房名称
        /// </summary>
        public string EntrySoufunName { get; set; }
        /// <summary>
        /// 合作伙伴收款人卡号
        /// </summary>
        public string CardNO { get; set; }
        /// <summary>
        /// 对接销售Id，对应Admin_UserInfo表Id
        /// </summary>
        public int SalerId { get; set; }
        /// <summary>
        /// 服务人员姓名
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务人员编码（Admin_UserInfo.ID)
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// 公司名称拼音缩写
        /// </summary>
        public string ChineseCap { get; set; }
        /// <summary>
        /// 企业开户行行号
        /// </summary>
        public string BankNO { get; set; }
        /// <summary>
        /// 企业开户行省级编码
        /// </summary>
        public string BankProvinceNO { get; set; }
        /// <summary>
        /// 企业开户行市级编码
        /// </summary>
        public string BankCityNO { get; set; }
        /// <summary>
        /// 账户类型，０对私账户，１对公账户
        /// </summary>
        public int AccountType { get; set; }
        /// <summary>
        /// 纬度PosY
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 经度PosX
        /// </summary>
        public string Lng { get; set; }
        /// <summary>
        /// 企业联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 楼层门牌号
        /// </summary>
        public string RoomNO { get; set; }
        /// <summary>
        /// 乘车路线
        /// </summary>
        public string BusRoute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal LastDayScore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 认证图片地址
        /// </summary>
        public string AuthImageUrl { get; set; }
        /// <summary>
        /// 开户名
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 打款账号
        /// </summary>
        public string PayAccount { get; set; }
        /// <summary>
        /// 协议照，最多可以出现9张，中间以","逗号分隔
        /// </summary>
        public string AgreementPic { get; set; }
        /// <summary>
        /// --PayAccount字段通过控股接口验证之后返回的打款账号SouFunId
        /// </summary>
        public long ManagerSoufunId { get; set; }
    }
}
#endregion
