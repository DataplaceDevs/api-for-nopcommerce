﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Services
{
    public interface ICustomerDocumentsApiService
    {
        Task<IList<string>> GetCustomerDocumentTypeAsync();

    }
}