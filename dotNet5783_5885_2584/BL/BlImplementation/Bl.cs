
using BlApi;


namespace BlImplementation;

sealed internal class Bl : IBl
{
    public ICart Cart { get; } = new Cart();

    public IProduct Product { get; } = new Product();

    public IOrder Order { get; } = new Order();
    public IUser User { get; } = new User();

}
