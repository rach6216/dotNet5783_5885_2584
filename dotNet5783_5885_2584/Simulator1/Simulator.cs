using BlApi;

namespace Simulator;
//המתודות והמחלקה צריכות להיות פרטיות לפי ההוראות
public static class Simulator
{
    private static string? previousState;
    private static string? nextState;
    volatile static bool stopThread;
    public static event EventHandler simulationCompleted;
    public static event EventHandler ProgressChange;
    public static void run()
    {
        
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
        IBl? bl = Factory.Get();
        int? id;
        while (!stopThread)
        {
            id = bl.Order.GetRandomOrder();
            if (id != null)
            {
                BO.Order order = bl.Order.Read(x => x?.ID == id);
                Random rand = new Random();
                previousState = order.Status.ToString();
                int sec = rand.Next(1000, 5000);
                propChange prop = new propChange(order, sec);
                if (ProgressChange != null)
                {
                    ProgressChange(null, prop);
                }
                Thread.Sleep(sec);
                nextState = (previousState == "ConfirmOrder" ? bl.Order.ShipOrder((int)id) : bl.Order.DeliveryOrder((int)id)).Status.ToString();

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
