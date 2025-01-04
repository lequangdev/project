using BussenisLogicLayer.Interface;
using Common.DTO;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLogicLayer
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepo<TEntity> _repo;

        public BaseService(IBaseRepo<TEntity> repo)
        {
            _repo = repo;
        }

        public virtual bool Insert(TEntity models)
        {
            return _repo.Insert(models);
        }
        public virtual List<TEntity> GetAll()
        {

            return _repo.GetAll();

        }
        public virtual bool DeleteAll()
        {
            return _repo.DeleteAll();
        }
        public virtual bool DeleteByID(Guid id)
        {
            return _repo.DeleteByID(id);
        }
        
        public virtual bool UpdateByID(TEntity data)
        {
            return _repo.UpdateByID(data);
        }
        public virtual List<TEntity> GetAllByID(Guid id)
        {
            return _repo.GetAllByID(id);
        }
        public virtual bool InsertList(List<TEntity> models)
        {
            return _repo.InsertList(models);
        }
        public List<TEntity> GetRecordByColumn(ColumnValueDTO param)
        {
            return _repo.GetRecordByColumn(param);
        }
    }
}
