using AutoMapper;
using Models;
using ProgramsNetCore.Models.Dto.DataCollisionDto;
using ProgramsNetCore.Models.Dto.DataMigration;
using ProgramsNetCore.Models.Dto.DataSecurity;
using ProgramsNetCore.Models.Dto.LoginDto;
using ProgramsNetCore.Models.Dto.RepairOrderDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common.AutoMapper
{
    public class CustomProfile:Profile
    {   /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<secretmode, ResultSecretModelDto>();

            CreateMap<datamigrationtaskinfo, DataMigrationInfoDto>();
            CreateMap<DataMigrationInfoDto, datamigrationtaskinfo>()
                .ForMember(desc=>desc.Department,opt=>opt.Ignore())
                .ForMember(desc => desc.Applicant, opt => opt.Ignore());
            CreateMap<ResultLoginInfoDto, LoginTokenInfoDto>();
            // 数据碰撞
            CreateMap<datamodel, DataModelDto>().ForMember(desc => desc.PrivateSheets, opt => opt.Ignore())
                .ForMember(desc => desc.PublicSheets, opt => opt.Ignore());
            CreateMap<DataModelDto, datamodel>();

            CreateMap<DataModelPrivateSheetDto, datamodel_privatesheet>();
            CreateMap<datamodel_privatesheet, DataModelPrivateSheetDto>();

            CreateMap<datamodel_publicsheet, DataModelPublicSheetDto>();
            CreateMap<DataModelPublicSheetDto, datamodel_publicsheet>();

            CreateMap<datatask, DataTaskDto>().ForMember(desc => desc.PrivateSheets, opt => opt.Ignore())
                .ForMember(desc => desc.PublicSheets, opt => opt.Ignore());
            CreateMap<DataTaskDto, datatask>();

            CreateMap<dataconditions, DataConditionDto>();
            CreateMap<DataConditionDto, dataconditions>();

            CreateMap<tabletotable, TableToTableDto>();
            CreateMap<TableToTableDto, tabletotable>();

            CreateMap<topology, TopologyDto>();
            CreateMap<TopologyDto, topology>();

            CreateMap<datamodel_examinelog, DataModelExamineLogDto>();
            CreateMap<DataModelExamineLogDto, datamodel_examinelog>();
            CreateMap<AddRepairOrderDto, repair_order>();
            CreateMap<repair_order, AddRepairOrderDto>();

        }

    }
}
