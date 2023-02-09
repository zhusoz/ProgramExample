using Models;
using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Reposity
{
    public class DataMapRep : BaseRep<datamap>, IDataMapRep
    {
        public SqlSugarScope db = BaseDB.Db;

        public Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag)
        {

            //已标记
            if (IsAlreadyTag)
            {
                return db.Queryable<datamap, cloud_user, layered, modified, attribution, cloud_org>((dataMapEntity, user, layer, modify, attr, org) => new JoinQueryInfos(JoinType.Left, dataMapEntity.UserId==(int)user.id, JoinType.Left, dataMapEntity.LayeredType==layer.Id, JoinType.Left, dataMapEntity.ModifierType==modify.Id, JoinType.Left, dataMapEntity.Attribution==attr.Id, JoinType.Left, user.description==org.Id))
                .Where(dataMapEntity => dataMapEntity.LayeredType > 0 && dataMapEntity.ModifierType > 0)
                .Select((dataMapEntity, user, layer, modify, attr, org) => new GetClassifyManageDto
                {
                    Id=dataMapEntity.Id,
                    Name=dataMapEntity.Name,
                    EnName=dataMapEntity.EnName,
                    AssociativeTable=dataMapEntity.AssociativeTable,
                    Title = org.Title,
                    Attribution=attr.Attribution,
                    LayerName = layer.Name,
                    ModifyName = modify.Name
                }).MergeTable().ToListAsync(); //在mergetale之前不能有排序 分页 skip take操作，之后可以
            }

            return db.Queryable<datamap, cloud_user, layered, modified, attribution, cloud_org>((dataMapEntity, user, layer, modify, attr, org) => new JoinQueryInfos(JoinType.Left, dataMapEntity.UserId==(int)user.id, JoinType.Left, dataMapEntity.LayeredType==layer.Id, JoinType.Left, dataMapEntity.ModifierType==modify.Id, JoinType.Left, dataMapEntity.Attribution==attr.Id, JoinType.Left, user.description==org.Id))
                .Where(dataMapEntity => dataMapEntity.LayeredType <= 0 || dataMapEntity.ModifierType <=0)
                .Select((dataMapEntity, user, layer, modify, attr, org) => new GetClassifyManageDto
                {
                    Id=dataMapEntity.Id,
                    Name=dataMapEntity.Name,
                    EnName=dataMapEntity.EnName,
                    AssociativeTable=dataMapEntity.AssociativeTable,
                    Title = org.Title,
                    Attribution=attr.Attribution,
                    LayerName = layer.Name,
                    ModifyName = modify.Name
                }).MergeTable().ToListAsync(); //在mergetale之前不能有排序 分页 skip take操作，之后可以


        }

        public Task<List<GetClassifyManageDto>> GetDatamaps(bool IsAlreadyTag, int pageIndex, int pageSize, RefAsync<int> total)
        {
            //已标记
            if (IsAlreadyTag)
            {
                return db.Queryable<datamap, cloud_user, layered, modified, attribution, cloud_org>((dataMapEntity, user, layer, modify, attr, org) => new JoinQueryInfos(JoinType.Left, dataMapEntity.UserId==(int)user.id, JoinType.Left, dataMapEntity.LayeredType==layer.Id, JoinType.Left, dataMapEntity.ModifierType==modify.Id, JoinType.Left, dataMapEntity.Attribution==attr.Id, JoinType.Left, user.description==org.Id))
                .Where(dataMapEntity => dataMapEntity.LayeredType > 0 && dataMapEntity.ModifierType > 0)
                .Select((dataMapEntity, user, layer, modify, attr, org) => new GetClassifyManageDto
                {
                    Id=dataMapEntity.Id,
                    Name=dataMapEntity.Name,
                    EnName=dataMapEntity.EnName,
                    AssociativeTable=dataMapEntity.AssociativeTable,
                    Title = org.Title,
                    Attribution=attr.Attribution,
                    LayerName = layer.Name,
                    ModifyName = modify.Name
                }).MergeTable().OrderBy(dataMapEntity => dataMapEntity.Id).ToPageListAsync(pageIndex, pageSize, total); //在mergetale之前不能有排序 分页 skip take操作，之后可以
            }

            return db.Queryable<datamap, cloud_user, layered, modified, attribution, cloud_org>((dataMapEntity, user, layer, modify, attr, org) => new JoinQueryInfos(JoinType.Left, dataMapEntity.UserId==(int)user.id, JoinType.Left, dataMapEntity.LayeredType==layer.Id, JoinType.Left, dataMapEntity.ModifierType==modify.Id, JoinType.Left, dataMapEntity.Attribution==attr.Id, JoinType.Left, user.description==org.Id))
                .Where(dataMapEntity => dataMapEntity.LayeredType <= 0 || dataMapEntity.ModifierType <=0)
                .Select((dataMapEntity, user, layer, modify, attr, org) => new GetClassifyManageDto
                {
                    Id=dataMapEntity.Id,
                    Name=dataMapEntity.Name,
                    EnName=dataMapEntity.EnName,
                    AssociativeTable=dataMapEntity.AssociativeTable,
                    Title = org.Title,
                    Attribution=attr.Attribution,
                    LayerName = layer.Name,
                    ModifyName = modify.Name
                }).MergeTable().OrderBy(dataMapEntity => dataMapEntity.Id).ToPageListAsync(pageIndex, pageSize, total); //在mergetale之前不能有排序 分页 skip take操作，之后可以
        }
    }
}
