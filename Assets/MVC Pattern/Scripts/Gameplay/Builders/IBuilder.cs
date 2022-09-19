using VContainer;

namespace MVC.Builders
{
    public interface IBuilder
    {
        void Build(IContainerBuilder builder);
    }
}