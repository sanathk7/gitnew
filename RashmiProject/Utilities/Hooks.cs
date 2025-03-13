using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Threading;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");
        private static string extentReportsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "ExtentReports");

        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private static ExtentSparkReporter _sparkReporter;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Setup WebDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Initialize ExtentReports
            string reportPath = Path.Combine(extentReportsFolderPath, $"ExtentReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.html");
            Directory.CreateDirectory(extentReportsFolderPath);
            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);
            _feature = _extent.CreateTest("Feature Name"); // You can change the feature name as needed.
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                // If the test fails, capture a screenshot and log it in the Extent report.
                string screenshotPath = TakeScreenshot();
                _feature.Log(AventStack.ExtentReports.Status.Fail, "Scenario failed", 
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                _feature.Log(AventStack.ExtentReports.Status.Fail, ScenarioContext.Current.TestError.Message);
            }
            else
            {
                // If the test passes, log success in Extent report.
                string screenshotPath = TakeScreenshot();
                _feature.Log(AventStack.ExtentReports.Status.Pass, "Scenario passed", 
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }

            // Finalize Extent Report for this scenario.
            _extent.Flush();

            // Close the WebDriver if it exists.
            if (driver != null)
            {
                driver.Quit();
            }
        }

        private string TakeScreenshot()
        {
            try
            {
                if (driver == null)
                {
                    Console.WriteLine("WebDriver is null. Cannot capture screenshot.");
                    return null;
                }

                if (driver.WindowHandles.Count == 0)
                {
                    Console.WriteLine("No active browser window. Skipping screenshot.");
                    return null;
                }

                // Introduce a small wait before capturing screenshot
                Thread.Sleep(500);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");

                // Ensure Screenshots Folder Exists
                Directory.CreateDirectory(screenshotsFolderPath);

                // Save Screenshot
                screenshot.SaveAsFile(screenshotFilePath);

                Console.WriteLine($"Screenshot saved at: {screenshotFilePath}");

                return screenshotFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return null;
            }
        }
    }
}
