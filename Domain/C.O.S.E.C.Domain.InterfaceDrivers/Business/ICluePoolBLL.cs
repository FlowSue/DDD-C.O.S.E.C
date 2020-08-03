using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C.O.S.E.C.Domain.InterfaceDrivers.Business
{
    public interface ICluePoolBLL : IBaseBLL<CluePool>
    {
        bool ConversionBusiness(Guid keyValue);
        bool Range(List<CluePool> list);
        Task<bool> RangeAsync(List<CluePool> list);
    }
}
