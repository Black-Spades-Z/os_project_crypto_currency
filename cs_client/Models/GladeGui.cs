using System;
using System.Linq;
using Gtk;


namespace GladeFunctions
{


    public class CCTPSApp
    {

        //Global variables

        private string[] currencyName =   {"Bitcoin"};
        private string[] currencyPrice =  {"1800$"};
        private string[] currencyVolume = {"1k BTC"};
        private string[] currencyRank =   {"#1"};
        private string[] currencyIcons = {"/icons/Bitcoin.png"};

        // Main Window
        private Window main_window;
        private Frame card;
        private Box market_values_box;
        private Button exchange_button_main_window;



        // Window 1
        private Window login_window1;
        private Entry email_entry_login_window1;
        private Entry password_entry_login_window1;
        private Button login_button_login_window1;
        private Button authorization_button_login_window1;

        // Window 2
        private Window login_window2;

        private Entry name_entry_login_window2;
        private Entry email_entry_login_window2;
        private Entry password_entry_login_window2;
        private Entry password_confirm_entry_login_window2;
        private Button back_button_login_window2;
        private Button submit_button_login_window2;
        private CheckButton agreement_login_window2;

        // Window 3
        private Window login_window3;
        private Entry phone_number_entry_login_window3;
        private Entry dob_entry_login_window3;
        private Entry address_entry_login_window3;
        private Entry combo_box_entry_login_window3;
        private Button submit_button_login_window3;
        private Button back_button_login_window3;

        // main Window 1
        // private Button dashboard_button_gtkwindow1;
        // private Button p2p_button_gtkwindow1;
        // private Button portfolio_button_gtkwindow1;
        // private Button settings_button_gtkwindow1;
        // private Button help_button_gtkwindow1;




        public CCTPSApp()
        {


            Application.Init();
            Builder builder = new Builder();
            builder.AddFromFile("GUI/Glade/CCTPS.glade");


            // CSS Declaration

            var cssProvider = new CssProvider();
            cssProvider.LoadFromPath("GUI/CSS/styles.css");

            main_window = (Window)builder.GetObject("main_window");
            main_window.DefaultSize = new Gdk.Size(1440, 968);
            login_window1 = (Window)builder.GetObject("login_window1");
            login_window1.DefaultSize = new Gdk.Size(1440, 968);
            login_window2 = (Window)builder.GetObject("login_window2");
            login_window2.DefaultSize = new Gdk.Size(1440, 968);
            login_window3 = (Window)builder.GetObject("login_window3");
            login_window3.DefaultSize = new Gdk.Size(1440, 968);

            // Retrieve objects from Glade for login_window1
            card = (Frame)builder.GetObject("card");
            market_values_box = (Box)builder.GetObject("market_values_box");
            exchange_button_main_window = (Button)builder.GetObject("exchange_button_main_window");



            // Retrieve objects from Glade for login_window1
            email_entry_login_window1 = (Entry)builder.GetObject("email_entry_login_window1");
            password_entry_login_window1 = (Entry)builder.GetObject("password_entry_login_window1");
            login_button_login_window1 = (Button)builder.GetObject("login_button_login_window1");
            authorization_button_login_window1 = (Button)builder.GetObject("authorization_button_login_window1");

            // Retrieve objects from Glade for login_window2
            name_entry_login_window2 = (Entry)builder.GetObject("name_entry_login_window2");
            email_entry_login_window2 = (Entry)builder.GetObject("email_entry_login_window2");
            password_entry_login_window2 = (Entry)builder.GetObject("password_entry_login_window2");
            password_confirm_entry_login_window2 = (Entry)builder.GetObject("password_confirm_entry_login_window2");
            agreement_login_window2 = (CheckButton)builder.GetObject("agreement_login_window2");
            submit_button_login_window2 = (Button)builder.GetObject("submit_button_login_window2");
            back_button_login_window2 = (Button)builder.GetObject("back_button_login_window2");

            // Window 3
            phone_number_entry_login_window3 = (Entry)builder.GetObject("phone_number_entry_login_window3");
            dob_entry_login_window3 = (Entry)builder.GetObject("dob_entry_login_window3");
            combo_box_entry_login_window3 = (Entry)builder.GetObject("combo_box_entry_login_window3");
            address_entry_login_window3 = (Entry)builder.GetObject("address_entry_login_window3");
            submit_button_login_window3 = (Button)builder.GetObject("submit_button_login_window3");
            back_button_login_window3 = (Button)builder.GetObject("back_button_login_window3");

            // Main Window 1






            // Connect button click events for login_window1
            login_button_login_window1.Clicked += login_button_login_window1_clicked;
            authorization_button_login_window1.Clicked += authorization_button_login_window1_clicked;

            // Connect button click events for login_window2
            submit_button_login_window2.Clicked += submit_button_login_window2_clicked;
            back_button_login_window2.Clicked += back_button_login_window2_clicked;

            // Connect toggle event for agreement CheckButton
            agreement_login_window2.Toggled += agreement_login_window2_toggled;

            //Window 3
            back_button_login_window3.Clicked += back_button_login_window3_clicked;


            // CSS Button

            var submit_button_login_window2_css = submit_button_login_window2.StyleContext;
            submit_button_login_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            submit_button_login_window2_css.AddClass("submit-button-login-window2");


            // CSS Entries

            var name_entry_login_window2_css = name_entry_login_window2.StyleContext;
            name_entry_login_window2_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            name_entry_login_window2_css.AddClass("name-entry-login-window2");

            var email_entry_login_window2_css = email_entry_login_window2.StyleContext;
            email_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            email_entry_login_window2_css.AddClass("email-entry-login-window2");

            var password_entry_login_window2_css = password_entry_login_window2.StyleContext;
            password_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            password_entry_login_window2_css.AddClass("password-entry-login-window2");

            var password_confirm_entry_login_window2_css = password_confirm_entry_login_window2.StyleContext;
            password_confirm_entry_login_window2_css.AddProvider(cssProvider, 		   Gtk.StyleProviderPriority.Application);
            password_confirm_entry_login_window2_css.AddClass("password-confirm-entry-login-window2");


            // CSS Card

            var card_css = card.StyleContext;
            card_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            card_css.AddClass("card");

            var main_window_css = main_window.StyleContext;
            main_window_css.AddProvider(cssProvider, Gtk.StyleProviderPriority.Application);
            main_window_css.AddClass("main-window");


            login_window1.DeleteEvent += delegate { Application.Quit(); };
            login_window2.DeleteEvent += delegate { Application.Quit(); };
            AddFrameToMarketValuesMainWindow(0);


            main_window.Hide();
            login_window1.ShowAll();
            login_window2.Hide();
            login_window3.Hide();

            Application.Run();
        }

        private void exchange_button_main_window_clicked(object sender, EventArgs e){


            AddFrameToMarketValuesMainWindow(0);
        }

        private void AddFrameToMarketValuesMainWindow(int index)
        {




            // Create a new frame
            Frame currencyFrame = new Frame("");

            // Create the frame
            currencyFrame.Visible = true;
            currencyFrame.CanFocus = false;
            currencyFrame.MarginTop = 10;
            currencyFrame.MarginBottom = 10;
            currencyFrame.LabelXalign = 0;
            currencyFrame.ShadowType = ShadowType.Out;

            // Create the alignment
            Alignment alignment = new Alignment(0, 0, 0, 0);
            alignment.Visible = true;
            alignment.CanFocus = false;
            alignment.LeftPadding = 12;

            // Create the inner grid
            Grid innerGrid = new Grid();
            innerGrid.Visible = true;
            innerGrid.CanFocus = false;
            innerGrid.RowSpacing = 0;
            innerGrid.ColumnSpacing = 10;
            innerGrid.RowHomogeneous = true;
            innerGrid.ColumnHomogeneous = true;

            // Create the inner grid
            Grid currencyNameGrid = new Grid();
            currencyNameGrid.MarginBottom = 9;
            currencyNameGrid.MarginLeft = 30;
            currencyNameGrid.Visible = true;
            currencyNameGrid.CanFocus = false;
            currencyNameGrid.RowSpacing = 0;
            currencyNameGrid.ColumnSpacing = 0;
            currencyNameGrid.RowHomogeneous = true;
            currencyNameGrid.ColumnHomogeneous = true;


            // Add child widgets to the inner grid (similar to your provided XML structure)
            // Here, you'd create and add GtkImage, GtkLabel, GtkButton, etc., to the innerGrid


            // Icon Image

            Image currencyIconImage = new Image("icons/Bitcoin.png");
            currencyIconImage.Visible = true;
            currencyIconImage.CanFocus = false;
            currencyIconImage.MarginLeft = 40;
            currencyNameGrid.Attach(currencyIconImage, 0, 0, 1, 1);

            // Name Label

            Label currencyNameLabel = new Label(currencyName[index]);
            currencyNameLabel.Visible = true;
            currencyNameLabel.CanFocus = false;
            currencyNameLabel.MarginRight = 30;
            currencyNameGrid.Attach(currencyNameLabel, 1, 0, 1, 1);




            innerGrid.Attach(currencyNameGrid, 0, 0, 1, 1);



            // Price Label

            Label currencyPriceLabel = new Label(currencyPrice[index]);
            currencyPriceLabel.Visible = true;
            currencyPriceLabel.CanFocus = false;
            currencyPriceLabel.MarginBottom = 9;
            currencyPriceLabel.Halign = Align.End;
            innerGrid.Attach(currencyPriceLabel, 1, 0, 1, 1);

            // Volume Label

            Label currencyVolumeLabel = new Label(currencyVolume[index]);
            currencyVolumeLabel.Visible = true;
            currencyVolumeLabel.CanFocus = false;
            currencyVolumeLabel.MarginBottom = 9;
            currencyVolumeLabel.Halign = Align.End;
            innerGrid.Attach(currencyVolumeLabel, 2, 0, 1, 1);

            // Rank Label

            Label currencyRankLabel = new Label(currencyRank[index]);
            currencyRankLabel.Visible = true;
            currencyRankLabel.CanFocus = false;
            currencyRankLabel.MarginBottom = 9;
            innerGrid.Attach(currencyRankLabel, 3, 0, 1, 1);

            // Exchange Button
            Button exchangeButton = new Button("Exchange");
            exchangeButton.MarginBottom = 9;
            exchangeButton.MarginRight = 10;
            innerGrid.Attach(exchangeButton, 4, 0, 1, 1);

            // Connect button click events for main_window
            exchangeButton.Clicked += exchange_button_main_window_clicked;



            // Add the inner grid to the alignment
            alignment.Add(innerGrid);

            // Add the alignment to the frame
            currencyFrame.Add(alignment);



            // Add the frame to the market_values_box
            market_values_box.Add(currencyFrame);
            market_values_box.ShowAll();
        }

        private void login_button_login_window1_clicked(object sender, EventArgs e)
        {
            string email = email_entry_login_window1.Text;
            string password = password_entry_login_window1.Text;

            if (IsValidEmail(email))
            {
                Console.WriteLine($"Login successful. Email: {email}, Password: {password}");
            }
            else
            {
                Console.WriteLine("Invalid email address.");
            }
        }

        private void authorization_button_login_window1_clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Submit button clicked.");

            // Close login_window1
            login_window1.Hide();

            // Show login_window2
            login_window2.ShowAll();
        }

        private void submit_button_login_window2_clicked(object sender, EventArgs e)
        {
            // Check if the agreement CheckButton is checked
            if (agreement_login_window2.Active)
            {
                string name = name_entry_login_window2.Text;
                string email = email_entry_login_window2.Text;
                string password = password_entry_login_window2.Text;
    // Validate email
                if (!IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email address.");
                    // Optionally, you can show an error message or take other actions
                    return;
                }

                // Validate password
                if (password.Length < 7 || !PasswordContainsDigitAndUppercase(password))
                {
                    Console.WriteLine("Password must be at least 7 characters long and contain at least one digit and one uppercase character.");
                    // Optionally, you can show an error message or take other actions
                    return;
                }

                // Optionally, you can close login_window2 or perform other actions
                login_window2.Hide();
                login_window3.ShowAll();
            }
            else
            {
                Console.WriteLine("Please agree to the terms before submitting.");
                // Optionally, you can show an error message or take other actions
            }
        }

        private bool IsValidEmail(string email)
        {
            // You can implement your email validation logic here
            return email.Contains('@');
        }

        private bool PasswordContainsDigitAndUppercase(string password)
        {
            // Check if the password contains at least one digit and one uppercase character
            return password.Any(char.IsDigit) && password.Any(char.IsUpper);
        }

        private void back_button_login_window2_clicked(object sender, EventArgs e)
        {
            // Add your logic for going back from login_window2 to login_window1

            // Optionally, you can close login_window2 or perform other actions
            login_window2.Hide();
            login_window1.ShowAll();
        }

        private void agreement_login_window2_toggled(object sender, EventArgs e)
        {
            // Add your logic for handling the agreement CheckButton state change
            bool isChecked = agreement_login_window2.Active;

            // Optionally, you can perform actions based on whether the CheckButton is checked or unchecked
            Console.WriteLine($"Agreement CheckButton state changed: {isChecked}");
        }
    // Window 3
            private void submit_button_login_window3_clicked(object sender, EventArgs e)
        {
            string phoneNumber = phone_number_entry_login_window3.Text;
            string dob = dob_entry_login_window3.Text;
            string address = address_entry_login_window3.Text;
            string comboBoxValue = combo_box_entry_login_window3.Text;

            // Validate phone number (only digits allowed)
            if (!IsValidPhoneNumber(phoneNumber))
            {
                Console.WriteLine("Invalid phone number. Please enter only digits.");
                // Optionally, you can show an error message or take other actions
                return;
            }

            // Validate date of birth (in the format 00/00/0000)
            if (!IsValidDateOfBirth(dob))
            {
                Console.WriteLine("Invalid date of birth. Please enter the date in the format 00/00/0000.");
                // Optionally, you can show an error message or take other actions
                return;
            }

            // Optionally, you can perform other actions, for example, submit the data or close the window
            Console.WriteLine($"Submitted data: Phone Number: {phoneNumber}, Date of Birth: {dob}, Address: {address}, ComboBox Value: {comboBoxValue}");
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Check if the phone number contains only digits
            return phoneNumber.All(char.IsDigit);
        }

        private bool IsValidDateOfBirth(string dob)
        {
            // Validate date of birth format (00/00/0000)
            if (DateTime.TryParseExact(dob, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                return true;
            }
            return false;
        }

        private void back_button_login_window3_clicked(object sender, EventArgs e)
        {
            GoBackToWindow2();
        }

        private void GoBackToWindow2()
        {
            // Optionally, you can perform other actions before switching to login_window2
            // For example, you might want to clear the entries in login_window3.

            // Close login_window3
            login_window3.Hide();

            // Show login_window2
            login_window2.ShowAll();
        }

    }
}
