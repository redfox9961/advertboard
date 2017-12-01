using Castle.MicroKernel.Lifestyle;

namespace Common
{
    public class HybridPerWebRequestScopedScopeAccessor : HybridPerWebRequestScopeAccessor
    {
        public HybridPerWebRequestScopedScopeAccessor() : base(new LifetimeScopeAccessor())
        {
        }
    }
}