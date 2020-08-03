using System;
using System.Collections.Generic;

namespace C.O.S.E.C.Domain.InterfaceDrivers.Business
{
    public interface ICustomerBLL : IBaseBLL<Customer>
    {
        bool RangeDelete(List<Customer> customers);
        bool Invalid(Guid keyValue);
    }
}
