using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Reposity;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        IBaseRep<UnitTest1> _baseRep = new BaseRep<UnitTest1>();
        [TestMethod]
        public void TestMethod1()
        {

            _baseRep.CreateEntity("ApprovalTask", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Authority", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("ClearData", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Cloud_Org", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Cloud_User", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Conn_Orirule_Rule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Datamap", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("DatamapStatus", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Files", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Frequency", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Hierarchy", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Layered", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Modified", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("OriginalRule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Process", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Rule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("Secret", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            _baseRep.CreateEntity("SecretMode", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");



            //_baseRep.CreateEntity("approvaltask", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("authority", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("cleardata", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("cloud_org", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("cloud_user", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("conn_orirule_rule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("datamap", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("datamapstatus", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("files", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("frequency", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("hierarchy", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("layered", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("modified", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("originalrule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("process", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("rule", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("secret", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");
            //_baseRep.CreateEntity("secretmode", @"D:\qKunvPrograms\SVN\data-sharing-api-qx\ProgramsNetCore.Models");

        }
    }
}