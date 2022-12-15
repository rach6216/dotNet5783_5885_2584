
using BlApi;


namespace BlImplementation;

sealed internal class Bl : IBl
{
    public ICart Cart => new Cart();

    public IProduct Product => new Product();

    public IOrder Order => new Order();
}
