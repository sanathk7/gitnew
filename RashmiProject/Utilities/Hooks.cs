/*using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public  static IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private static ExtentSparkReporter _sparkReporter;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ExtentReport.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Initializing WebDriver...");

            if (driver == null)
            {
                driver = new ChromeDriver();
            }

         
            _scenarioContext["WebDriver"] = driver;
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
        }
        [AfterStep]
        public void InsertReportingSteps()
        {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            string screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title, stepText);

            if (_scenarioContext.TestError == null)
            {
               
                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build()); //"MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build()");
                }
                else
                {
                    _scenario.Log(Status.Pass, stepText);
                }
            }
            else
            {

                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                else
                {
                    _scenario.Log(Status.Fail, stepText);
                }

            
                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
            }
        }


        [AfterScenario]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }

        
        private string CaptureScreenshot(string scenarioName, string stepName)
        {
            try
            {
                if (driver == null)
                {
                    TestContext.Progress.WriteLine("WebDriver is null. Cannot capture screenshot.");
                    return null;
                }

                if (driver.WindowHandles.Count == 0)
                {
                    TestContext.Progress.WriteLine("No active browser window. Skipping screenshot.");
                    return null;
                }

                
                Thread.Sleep(500);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

               
                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(screenshotPath);

                
                string sanitizedStepName = string.Join("_", stepName.Split(Path.GetInvalidFileNameChars()));
                string filePath = Path.Combine(screenshotPath, $"{scenarioName}_{sanitizedStepName}.png");

                
                screenshot.SaveAsFile(filePath);
                TestContext.Progress.WriteLine($"Screenshot saved at: {filePath}");

                return filePath;
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return null;
            }
        }
    }
}*/

/*using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");

        // Hook to initialize the browser before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Chrome options for headless mode
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        // Hook to close the browser after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Take a screenshot if the scenario fails
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot();
            }

            if (driver != null)
            {
                driver.Quit();  // Close and dispose the driver
            }
        }

        // Method to take a screenshot
        private void TakeScreenshot()
        {
            try
            {
                // Ensure the screenshots directory exists
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                // Capture the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

                // You can also upload the file as part of the artifacts, but sending it by email needs an extra step in GitHub Actions
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
            }
        }
    }
}
*/
/*using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");

        // Hook to initialize the browser before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Chrome options for headless mode
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        // Hook to close the browser after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Take a screenshot if the scenario fails
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot();
            }

            if (driver != null)
            {
                driver.Quit();  // Close and dispose the driver
            }
        }

        // Method to take a screenshot
        private void TakeScreenshot()
        {
            try
            {
                // Ensure the screenshots directory exists
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                // Capture the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

                // Check if the screenshot was captured correctly
                if (File.Exists(screenshotFilePath))
                {
                    Console.WriteLine($"Screenshot file found: {screenshotFilePath}");
                }
                else
                {
                    Console.WriteLine("Screenshot file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
            }
        }
    }
}
*/
/*
using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        // Updated to use the GITHUB_WORKSPACE environment variable for GitHub Actions
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");

        // Hook to initialize the browser before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Chrome options for headless mode
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        // Hook to close the browser after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Take a screenshot if the scenario fails
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot();
            }

            if (driver != null)
            {
                driver.Quit();  // Close and dispose the driver
            }
        }

        // Method to take a screenshot
        private void TakeScreenshot()
        {
            try
            {
                // Ensure the screenshots directory exists
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                // Capture the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                // Log the screenshot file path
                Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

                // Check if the screenshot was captured correctly
                if (File.Exists(screenshotFilePath))
                {
                    Console.WriteLine($"Screenshot file found: {screenshotFilePath}");
                }
                else
                {
                    Console.WriteLine("Screenshot file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
            }
        }
    }
}
*/
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace RashmiProject.Utilities
{
    public class Hooks
    {
        private static IWebDriver driver;
        private static ExtentReports extentReports;
        private static ExtentTest test;
        private static string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "ExtentReports");
        private static string screenshotsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");

        // Setup before tests run
        [SetUp]
        public void BeforeTest()
        {
            // Ensure the ExtentReports directory exists
            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }

            // Setup ExtentReports
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);

            // Chrome options for headless mode
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Start a new test for each execution
            test = extentReports.CreateTest("Test Execution");
        }

        // Cleanup after each test run
        [TearDown]
        public void AfterTest()
        {
            // Log whether the report was generated
            if (extentReports != null)
            {
                Console.WriteLine("Extent Report generated at: " + reportPath);
            }

            // Take screenshot if the test fails
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot();
                test.Fail("Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotsFolderPath).Build());
            }
            else
            {
                test.Pass("Test Passed");
            }

            // Close the driver and flush the Extent Report
            if (driver != null)
            {
                driver.Quit();
            }

            extentReports.Flush();
        }

        // Method to take a screenshot
        private void TakeScreenshot()
        {
            try
            {
                // Ensure the screenshots directory exists
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                // Capture the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                // Attach screenshot to ExtentReport
                test.AddScreenCaptureFromPath(screenshotFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
            }
        }
    }
}
