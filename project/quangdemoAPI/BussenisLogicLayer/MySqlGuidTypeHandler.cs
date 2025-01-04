using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        if (value is Guid)
        {
            return (Guid)value;
        }
        else if (value is string)
        {
            return new Guid((string)value);
        }
        else
        {
            throw new DataException("Unexpected data type when parsing GUID.");
        }
    }
}
