using System;
using System.Threading;
using Gtk;


public class CustomAlertWindow : Window
{
    private Timer _timer;

    public CustomAlertWindow(string message, int xPosition, int yPosition, int durationMilliseconds) : base(WindowType.Popup)
    {


        // Load CSS provider
        var cssProvider = new CssProvider();
        cssProvider.LoadFromData(@"#customAlertWindow { color: #FF0000; border-radius: 10px; background-color: rgba(66, 76, 85, 1);box-shadow : none; font-size:14px; font-weight: bold; font-family: Poppins;} .label {border-radius: 10px;background-color: rgba(66, 76, 85, 0);}");
         // Set RGBA color for transparency

        // Apply CSS to the window
        StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

         // Set the opacity of the window
        Gdk.RGBA rgba = new Gdk.RGBA();
        rgba.Parse("rgba(66, 76, 85, 0.8)"); // Adjust the alpha (transparency) value here
        this.OverrideBackgroundColor(StateFlags.Normal, rgba);


        // Set the position of the window
        Move(xPosition, yPosition);

        // Create a label to display the message
        Label label = new Label(message);
        label.Name = "alertLabel";
        var labelcss = label.StyleContext;
        labelcss.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
        labelcss.AddClass("label");


        // Add the label to the window
        Add(label);

        // Set window properties
        SetDefaultSize(100, 50);

        SetPosition(WindowPosition.Center);

        // Load CSS provider
        Title = "Alert";
        Name = "customAlertWindow";




        // Show all widgets within the window
        ShowAll();

        // Create a timer to close the window after the specified duration
        _timer = new Timer(CloseWindow, null, durationMilliseconds, Timeout.Infinite);
    }

    private void CloseWindow(object state)
    {
        // Close the window from the UI thread
        Application.Invoke(delegate
        {
            Hide();
            Dispose();
        });
    }


    public static void showErrorAllert(string message)
    {
        // Create and show the window in a separate thread
        Thread alertThread = new Thread(() =>
        {
            CustomAlertWindow alertWindow = new CustomAlertWindow(message, 1247, 960, 3000);





            alertWindow.Show();
        });
        alertThread.Start();
    }

    public static void showSuccessAllert(string message)
    {
        // Create and show the window in a separate thread
        Thread alertThread = new Thread(() =>
        {
            CustomAlertWindow alertWindow = new CustomAlertWindow(message, 900, 900, 3000);
            alertWindow.Show();
        });
        alertThread.Start();
    }
}
