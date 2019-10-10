using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DapperRepository
{
    public interface IDapperFactory
    {
        DapperClient CreateClient(string name);
    }
}
