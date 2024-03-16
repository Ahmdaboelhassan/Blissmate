using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blessmate.tests.Fakes
{
    internal class TrueIdentityResult : IdentityResult
    {
        public TrueIdentityResult() => Succeeded = true;
    }
}
