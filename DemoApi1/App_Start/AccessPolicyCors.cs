using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Net.Http;
using System.Threading;

namespace webServer.App_Start
{
    public class AccessPolicyCors:Attribute,ICorsPolicyProvider
    {
        public async Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corsRequestContext = request.GetCorsRequestContext();
            var originRequested = corsRequestContext.Origin;

            if (await IsOriginFromCustomer(originRequested))
            {
                var policy = new CorsPolicy
                {
                    AllowAnyHeader = true,
                    AllowAnyMethod = true
                    
                };

                policy.Origins.Add(originRequested);

                //IP ESPECIFICA
                //policy.Origins.Add("http://localhost:56360");
                policy.AllowAnyOrigin = true;
                return policy;
            }
            return null;
        }

        private async Task<bool> IsOriginFromCustomer(string originRequested)
        {
            return true;
        }
    }
}