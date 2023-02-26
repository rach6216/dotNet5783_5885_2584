using BlApi;

namespace Simulator;
public static class Simulator
{
    private static BlApi.IBl? bl = BlApi.Factory.Get();
    public static string? prevState;
    public static string? nextState;
    volatile static bool stopThread;
    public static event EventHandler simulationCompleted;
    public static event EventHandler ProgressChange;
    public static void run()
    {
        stopThread = false;
        Thread simulatorTread = new Thread(new ThreadStart(getOrder));
        simulatorTread.Start();
        return;
    }
    public static void stop()
    {
        stopThread = true;
        if(simulationCompleted != null)
            simulationCompleted("",EventArgs.Empty);
    }
    public static void getOrder()
    {
        int? id;
        while (!stopThread)
        {
            id = bl.Order.GetRandomOrder();
            if (id != null)
            {
                BO.Order order = bl.Order.Read(x => x?.ID == id);
                Random rand = new Random();
                prevState = order.Status.ToString();
                int sec = rand.Next(1000, 5000);
                propChange prop = new propChange(order, sec);
                if (ProgressChange != null)
                {
                    ProgressChange(null, prop);
                }
                Thread.Sleep(sec);
                nextState = (prevState == "OrderIsConfirmed" ? bl.Order.ShipOrder((int)id) : bl.Order.DeliveryOrder((int)id)).Status.ToString();

            }
            
            else
                stop();
        }
        return;
    }
}

/// <summary>
/// Helper class for updating the changes in the display
/// </summary>
public class propChange : EventArgs
{
    public BO.Order order;
    public int sec;
    public propChange(BO.Order order, int sec)
    {
        this.order = order;
        this.sec = sec;
    }
}
