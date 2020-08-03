using C.O.S.E.C.Infrastructure.Cors.Enums;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Cors.Attributes
{
    public sealed class CorsAttribute : EnableCorsAttribute
    {
        public CorsAttribute(CorsPolicyEnum policyEnum) : base(policyEnum.ToString())
        {

        }
    }
}
