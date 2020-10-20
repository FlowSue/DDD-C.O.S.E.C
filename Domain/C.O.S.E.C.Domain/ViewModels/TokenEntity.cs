using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.ViewModels
{
    public class TokenEntity
    {
        public TokenEntity(string token)
        {
            this.Token = token;
        }
        /// <summary>
        /// token令牌
        /// </summary>
        public string Token { get; private set; }
    }
}
