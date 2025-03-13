using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;

using AventStack.ExtentReports.Reporter;  // Import the ExtentReports library

namespace RashmiProject.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private static string screenshotsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "Screenshots");
        private static string extentReportsFolderPath = Path.Combine(Environment.GetEnvironmentVariable("GITHUB_WORKSPACE"), "TestResults", "ExtentReports");

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot();
                GenerateExtentReport();
            }

            if (driver != null)
            {
                driver.Quit();
            }
        }

        private void TakeScreenshot()
        {
            try
            {
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                    Console.WriteLine("Created Screenshots directory at: " + screenshotsFolderPath);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

                if (File.Exists(screenshotFilePath))
                {
                    Console.WriteLine($"Screenshot file found: {screenshotFilePath}");
                }
                else
                {
                    Console.WriteLine("Screenshot file not found.");
                }

                var directoryContents = Directory.GetFiles(screenshotsFolderPath);
                Console.WriteLine("Files in the Screenshots directory:");
                foreach (var file in directoryContents)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
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
                var htmlReporter = new ExtentSparkReporter(reportFilePath);
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
