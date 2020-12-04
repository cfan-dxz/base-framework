using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace ServerSide.Framework.Attributes
{
    /// <summary>
    /// HttpApi签名
    /// </summary>
    public class HttpApiSignAttribute : ApiActionFilterAttribute
    {
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            return base.OnBeginRequestAsync(context);
        }
    }
}
