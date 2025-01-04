using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer.Interface
{
    public interface IBaseService<TEntity>
    {
        public bool Insert(TEntity models);
        List<TEntity> GetAll();
        public bool DeleteAll();
        public bool DeleteByID(Guid id);
        public bool UpdateByID(TEntity data);
        public  List<TEntity> GetAllByID(Guid id);
        bool InsertList(List<TEntity> models);
        public List<TEntity> GetRecordByColumn(ColumnValueDTO param);
    }
}
