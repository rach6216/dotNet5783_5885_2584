using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Simulator;
using BlApi;
using System.Windows.Threading;


///// <summary>
///// Interaction logic for SimulatorWindow.xaml
///// </summary>
//public partial class SimulatorWindow : Window
//{
//    //disable the option of closing the window with x sign
//    private const int GWL_STYLE = -16;
//    private const int WS_SYSMENU = 0x80000;
//    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
//    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
//    [System.Runtime.InteropServices.DllImport("user32.dll")]
//    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


//    IBl bl;
//    string prevState;
//    string nextState;
//    BackgroundWorker worker;
//    private Stopwatch sw;
//    private bool isTimerRunning;
//    public SimulatorWindow(IBl bl)
//    {
//        InitializeComponent();
//        this.bl = bl;
//        Loaded += ToolWindow_Loaded;
//        TimerStart();
//    }
//    void StartTimer()
//    {
//        sw = new Stopwatch();
//        worker = new BackgroundWorker();
//        worker.DoWork += TimerDoWork;
//        worker.ProgressChanged += TimerProgressChanged;
//        worker.WorkerReportsProgress = true;
//        worker.WorkerSupportsCancellation = true;
//        sw.Restart();
//        isTimerRunning = true;
//        worker.RunWorkerAsync();
//    }
//    void Timer(int sec)
//    {
//        _time = TimeSpan.FromSeconds(sec);

//        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
//        {
//            tbTime.Text = _time.ToString("c");
//            if (_time == TimeSpan.Zero) _timer.Stop();
//            _time = _time.Add(TimeSpan.FromSeconds(-1));
//        }, Application.Current.Dispatcher);

//        _timer.Start();
//    }
//    private void Button_Click(object sender, RoutedEventArgs e)
//    {
//        if (isTimerRunning)
//        {
//            sw.Stop();
//            isTimerRunning = false;
//        }
//        Simulator.Simulator.stop();
//        this.Close();
//    }


namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    string nextStatus;
    string prevStatus;
    BackgroundWorker worker;
    Tuple<BO.Order, int, string, string> dcT;
    //====== disable the option of closing the window =======
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    //=====================================================
    private Stopwatch stopWatch;
    private bool isTimerRun;
    //=======progressBar variables
    Duration duration;
    DoubleAnimation doubleanimation;
    ProgressBar ProgressBar;
    //=======countdown timer variables
    DispatcherTimer _timer;
    TimeSpan _time;
    //=======
    public SimulatorWindow()
    {
        InitializeComponent();
        Loaded += ToolWindow_Loaded;
        TimerStart();
    }

    #region Timer

    //play timer
    void TimerStart()
    {
        stopWatch = new Stopwatch();
        worker = new BackgroundWorker();
        worker.DoWork += TimerDoWork!;
        worker.ProgressChanged += TimerProgressChanged!;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        //Simulator.Simulator.StartSimulator();
        stopWatch.Restart();
        isTimerRun = true;
        worker.RunWorkerAsync();
    }

    //logic of timer
    void countDownTimer(int sec)
    {
        _time = TimeSpan.FromSeconds(sec);

        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
        {
            tbTime.Text = _time.ToString("c");
            if (_time == TimeSpan.Zero) _timer.Stop();
            _time = _time.Add(TimeSpan.FromSeconds(-1));
        }, Application.Current.Dispatcher);

        _timer.Start();
    }
    void TimerDoWork(object sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.ProgressChange += changeOrder!;
        Simulator.Simulator.simulationCompleted += Stop!;
        Simulator.Simulator.run();
        while (isTimerRun)
        {
            worker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        SimulatorTXTB.Text = timerText;
    }
    #endregion

    //Progress bar animation
    void ProgressBarStart(int sec)
    {
        if (ProgressBar != null)
        {
            pBar.Items.Remove(ProgressBar);
        }
        ProgressBar = new ProgressBar();
        ProgressBar.IsIndeterminate = false;
        ProgressBar.Orientation = Orientation.Horizontal;
        ProgressBar.Width = 500;
        ProgressBar.Height = 30;
        duration = new Duration(TimeSpan.FromSeconds(sec * 2));
        doubleanimation = new DoubleAnimation(200.0, duration);
        ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        pBar.Items.Add(ProgressBar);
    }

    
    private void changeOrder(object sender, EventArgs e)
    {
        if (!(e is propChange))
            return;

        propChange? prop = e as propChange;
        this.prevStatus = (prop!.order.ShipDate == DateTime.MinValue) ? BO.OrderStatus.OrderIsConfirmed.ToString() : BO.OrderStatus.OrderIsShiped.ToString();
        this.nextStatus = (prop.order.ShipDate == DateTime.MinValue) ? BO.OrderStatus.OrderIsShiped.ToString() : BO.OrderStatus.OrderIsDelivered.ToString();
        dcT = new Tuple<BO.Order, int, string, string>(prop.order, prop.sec / 1000, prevStatus, nextStatus);
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(changeOrder, sender, e);
        }
        else
        {
            DataContext = dcT;
            countDownTimer(prop.sec / 1000);

            ProgressBarStart(prop.sec / 1000);
        }
    }
    
    void ToolWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Code to remove close box from window
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
    {
        if (isTimerRun)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
        Simulator.Simulator.stop();
        this.Close();
    }
    public void Stop(object sender, EventArgs e)
    {
        Simulator.Simulator.ProgressChange -= changeOrder!;
        Simulator.Simulator.simulationCompleted -= Stop!;
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(Stop, sender, e);
        }
        else
        {
            MessageBox.Show("no more orders to update");
            this.Close();
        }
    }
}