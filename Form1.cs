using Gma.System.MouseKeyHook;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;


namespace TextToSpeech
{
    public partial class Form1 : Form
    {
        static public IKeyboardMouseEvents? m_Events;
        private IWebDriver driver;
        public string mainLink = "https://thescrayx.github.io/index.html?text=Her%20Þey%20Yolunda%20Gözüküyor.";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            driver.Navigate().GoToUrl("https://chat.openai.com/login");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Subscribe();

            EdgeOptions options = new EdgeOptions();
            driver = new EdgeDriver(options);
            driver.Navigate().GoToUrl("https://thescrayx.github.io/index.html?text=Her%20Þey%20Yolunda%20Gözüküyor.");
            timer1.Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unsubscribe();
            driver.Quit();
        }

        public void Subscribe()
        {
            m_Events = Hook.GlobalEvents();
            m_Events.KeyDown += OnKeyDown;
        }

        public void Unsubscribe()
        {
            m_Events.KeyDown -= OnKeyDown;
            m_Events.Dispose();
        }

        public void OnKeyDown(object? sender, KeyEventArgs e)
        {
            Console.WriteLine("Basýlan Tuþ: " + e.KeyCode);
            if (e.KeyCode == System.Windows.Forms.Keys.Subtract && button1.Enabled.ToString() == "False")
            {
                Console.WriteLine("Tuþ yakalandý");
                OpenAndListen();
            }
        }

        public void OpenAndListen()
        {
            if (driver.Url.ToString().Substring(0, 24) == "https://www.youtube.com/")
            {

            }
            else
            {
                driver.Navigate().GoToUrl("https://www.google.com.tr/");
            }
        }

        public void CopyAndRequestApi()
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(driver.Url.ToString().Substring(0, 24) == mainLink)
            {
                //do nothing
            } else
            {
                if (driver.Url.ToString().Substring(0, 25) == "https://www.youtube.com/r")
                {
                    Console.WriteLine(driver.Title);
                    CopyAndRequestApi();
                    mainLink = driver.Title.ToString();
                }
            }
        }
    }
}