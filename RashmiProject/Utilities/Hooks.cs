using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using WebDriverManager;

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private static ExtentSparkReporter _sparkReporter;
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");
        private static string extentReportsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "ExtentReports");

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ExtentReport.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);

            Console.WriteLine("Extent report initialized.");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine("Initializing WebDriver...");
            
            if (driver == null)
            {
                driver = new ChromeDriver();
            }

            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep()
        {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            string screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title, stepText);

            if (_scenarioContext.TestError == null)
            {
                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
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
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }

            Console.WriteLine("Test finished, WebDriver closed.");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
            Console.WriteLine("Extent report finalized.");
        }

        // Capture Screenshot
        private string CaptureScreenshot(string scenarioName, string stepName)
        {
            try
            {
                if (driver == null || driver.WindowHandles.Count == 0)
                {
                    Console.WriteLine("WebDriver is null or no active window.");
                    return null;
                }

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(screenshotPath);

                string sanitizedStepName = string.Join("_", stepName.Split(Path.GetInvalidFileNameChars()));
                string filePath = Path.Combine(screenshotPath, $"{scenarioName}_{sanitizedStepName}.png");

                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved at: {filePath}");

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
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
                    Console.WriteLine("Created ExtentReports directory at: " + extentReportsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string reportFilePath = Path.Combine(extentReportsFolderPath, $"extent_report_{timestamp}.html");

                // Create and configure the ExtentReports instance
                var extent = new ExtentReports();
                var htmlReporter = new ExtentHtmlReporter(reportFilePath);
                extent.AttachReporter(htmlReporter);

                // Add test details to the report (example)
                var test = extent.CreateTest("Test Name", "Test Description");
                test.Log(Status.Info, "Test started");

                // Finish the report after the test is completed
                extent.Flush();

                Console.WriteLine($"Extent report saved to: {reportFilePath}");

                if (File.Exists(reportFilePath))
                {
                    Console.WriteLine($"Extent report file found: {reportFilePath}");
                }
                else
                {
                    Console.WriteLine("Extent report file not found.");
                }

                var directoryContents = Directory.GetFiles(extentReportsFolderPath);
                Console.WriteLine("Files in the ExtentReports directory:");
                foreach (var file in directoryContents)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while generating extent report: " + ex.Message);
            }
        }
    }
}
