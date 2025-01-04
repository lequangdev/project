using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussenisLogicLayer.Interface;
using Common.DTO;
using Common.Model;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Mysqlx.Crud;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer
{
    public class HistorydeliveryService : BaseService<HistorydeliveryEntity>, IHistorydeliveryService
    {
        IHistorydeliveryRepo _repo;
        IStaffRepo _staffRepo;
        public HistorydeliveryService(IHistorydeliveryRepo repo, IStaffRepo staffRepo) : base(repo)
        {
            _repo = repo;
            _staffRepo = staffRepo;

        }
        public override bool Insert(HistorydeliveryEntity models)
        {
            models.historydelivery_id = Guid.NewGuid();
            models.staff_id = _staffRepo.GetByUserID(models.staff_id).staff_id;
            return _repo.Insert(models);
        }
        public List<HistoryDeliveryDTO> GetAllByUserID(Guid id)
        {
            return _repo.GetAllByUserID(id);
        }
        public List<HistoryDeliveryDTO> getStaffHistory(Guid? staff_id)
        {
            staff_id = _staffRepo.GetByUserID(staff_id).staff_id;
            return _repo.getStaffHistory(staff_id);
        }
        public List<RevenuDTO> GetAllByTime(PeripodDTO date)
        {
            List<RevenuDTO> result = _repo.GetAllByTime(date);
            result = result.Where(x => x.status == "1").OrderBy(x => x.delivery_date).ToList();
            return result;
        }

    }
}
