using Products.Api.Events;

namespace Products.Api.Notifications
{
    public interface INotifier
    {
        void Notify(ProductChangeEvent productChangeEvent);
    }
}
