/*using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;  // Importing the ExtentReports library
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;  // For the ExtentSparkReporter

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");
        private static string extentReportsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "ExtentReports");

        private static ExtentReports extent;
        private static ExtentTest feature;
        private ExtentTest scenario;
        private static ExtentSparkReporter sparkReporter;

        [BeforeScenario]
        public void BeforeScenario()
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Setting up ExtentReports
            if (extent == null)
            {
                string reportPath = Path.Combine(extentReportsFolderPath, "ExtentReport.html");
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
                sparkReporter = new ExtentSparkReporter(reportPath);
                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }

            feature = extent.CreateTest("Feature Name");  // Use actual feature name
            scenario = feature.CreateNode("Scenario Name");  // Use actual scenario name
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                string screenshotPath = TakeScreenshot();
                if (screenshotPath != null)
                {
                    scenario.Log(Status.Fail, "Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                GenerateExtentReport();
            }

            if (driver != null)
            {
                driver.Quit();
            }

            extent.Flush();
        }

        private string TakeScreenshot()
        {
            try
            {
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                return screenshotFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
                return null;
            }
        }

        private void GenerateExtentReport()
        {
            try
            {
                if (!Directory.Exists(extentReportsFolderPath))
                {
                    Directory.CreateDirectory(extentReportsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string reportFilePath = Path.Combine(extentReportsFolderPath, $"extent_report_{timestamp}.html");

                // Add test details to the report (example)
                scenario.Log(Status.Info, "Test Completed");

                extent.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while generating extent report: " + ex.Message);
            }
        }
    }
}
*/
using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;  // Importing the ExtentReports library
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;  // For the ExtentSparkReporter

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");
        private static string extentReportsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "ExtentReports");

        private static ExtentReports extent;
        private static ExtentTest feature;
        private ExtentTest scenario;
        private static ExtentSparkReporter sparkReporter;

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Setting up ExtentReports
            if (extent == null)
            {
                string reportPath = Path.Combine(extentReportsFolderPath, "ExtentReport.html");
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
                sparkReporter = new ExtentSparkReporter(reportPath);
                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }

            feature = extent.CreateTest("Feature Name");  // Use actual feature name
            scenario = feature.CreateNode("Scenario Name");  // Use actual scenario name
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                string screenshotPath = TakeScreenshot();
                if (screenshotPath != null)
                {
                    // Attach screenshot using the absolute path
                    string relativeScreenshotPath = Path.Combine("TestResults", "Screenshots", Path.GetFileName(screenshotPath));
                    scenario.Log(Status.Fail, "Test Failed", MediaEntityBuilder.CreateScreenCaptureFromPath(relativeScreenshotPath).Build());
                }
                GenerateExtentReport();
            }

            if (driver != null)
            {
                driver.Quit();
            }

            extent.Flush();
        }

        private string TakeScreenshot()
        {
            try
            {
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                return screenshotFilePath;  // Return the absolute screenshot path
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
                return null;
            }
        }

        private void GenerateExtentReport()
        {
            try
            {
                if (!Directory.Exists(extentReportsFolderPath))
                {
                    Directory.CreateDirectory(extentReportsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string reportFilePath = Path.Combine(extentReportsFolderPath, $"extent_report_{timestamp}.html");

                // Log the test result in the report (example)
                if (ScenarioContext.Current.TestError != null)
                {
                    scenario.Log(Status.Fail, "Test failed due to error: " + ScenarioContext.Current.TestError.Message);
                }
                else
                {
                    scenario.Log(Status.Pass, "Test passed successfully");
                }

                extent.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while generating extent report: " + ex.Message);
            }
        }
    }
}
