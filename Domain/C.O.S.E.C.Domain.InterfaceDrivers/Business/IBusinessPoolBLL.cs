using System;

namespace C.O.S.E.C.Domain.InterfaceDrivers.Business
{
    public interface IBusinessPoolBLL : IBaseBLL<BusinessPool>
    {
        bool ConversionCustomer(Guid keyValue);
        bool Invalid(Guid id);
    }
}