using Common.DTO;
using DataAccessLayer.Interface;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IBaseRepo<TEntity> 
    {
        bool Insert(TEntity models);
        List<TEntity> GetAll();
        bool DeleteAll();
        bool DeleteByID(Guid id);
        bool UpdateByID(TEntity data);
        public List<TEntity> GetAllByID(Guid? id);
        bool InsertList(List<TEntity> models);
        bool DeleteByTable(Guid? id, string table);
        public List<TEntity> GetRecordByColumn(ColumnValueDTO param);
    }
}

