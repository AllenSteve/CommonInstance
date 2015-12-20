using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMappingComponent.ISQLHelper;
using ComponentModels.MyDBModel.iTECERP;
using ComponentORM.SQLHelper;

namespace BaseFunction.Service.iTecService
{
    public class CodeTableService
    {

        private IRepository<CodeTable> codeTableRepo { get; set; }

        public CodeTableService()
        {
            this.codeTableRepo = new Repository<CodeTable>();
        }

        public CodeTableService(IRepository<CodeTable> codeTableRepo)
        {
            this.codeTableRepo = codeTableRepo;
        }

        public List<CodeTable> GetList(string sysCode)
        {
            return this.codeTableRepo.Filter(code => code.SysCode == sysCode).ToList();
        }

        public CodeTable Get(string sysCode,string sysNo)
        {
            return this.codeTableRepo.Find(code=>code.SysCode==sysCode && code.SysNo== sysNo);
        }
    }
}
