



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

        [AfterStep]
        public void AfterStep()
        {
            string stepText = ScenarioContext.Current.StepContext.StepInfo.Text;

            // Capture screenshot after each step
            string screenshotPath = TakeScreenshot();

            if (ScenarioContext.Current.TestError == null)
            {
                // Attach screenshot for passed steps
                if (screenshotPath != null)
                {
                    string relativeScreenshotPath = Path.Combine("TestResults", "Screenshots", Path.GetFileName(screenshotPath));
                    scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(relativeScreenshotPath).Build());
                }
                else
                {
                    scenario.Log(Status.Pass, stepText);
                }
            }
            else
            {
                // Attach screenshot for failed steps
                if (screenshotPath != null)
                {
                    string relativeScreenshotPath = Path.Combine("TestResults", "Screenshots", Path.GetFileName(screenshotPath));
                    scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(relativeScreenshotPath).Build());
                }
                else
                {
                    scenario.Log(Status.Fail, stepText);
                }

                // Log the actual error message
                scenario.Log(Status.Fail, ScenarioContext.Current.TestError.Message);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            // Flush the ExtentReport after all scenarios have finished
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
    }
}
*/

////////This part for screnshot attachment

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

        [AfterStep]
        public void AfterStep()
        {
            string stepText = ScenarioContext.Current.StepContext.StepInfo.Text;

            // Capture screenshot after each step
            var screenshotPath = TakeScreenshot();

            if (ScenarioContext.Current.TestError == null)
            {
                // Attach screenshot directly to the report for passed steps
                if (screenshotPath != null)
                {
                    // Directly attach the screenshot from memory
                    scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotPath).Build());
                }
                else
                {
                    scenario.Log(Status.Pass, stepText);
                }
            }
            else
            {
                // Attach screenshot directly to the report for failed steps
                if (screenshotPath != null)
                {
                    // Directly attach the screenshot from memory
                    scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotPath).Build());
                }
                else
                {
                    scenario.Log(Status.Fail, stepText);
                }

                // Log the actual error message
                scenario.Log(Status.Fail, ScenarioContext.Current.TestError.Message);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            // Flush the ExtentReport after all scenarios have finished
            extent.Flush();
        }

        private string TakeScreenshot()
        {
            try
            {
                if (driver == null)
                {
                    return null;
                }

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                byte[] screenshotBytes = screenshot.AsByteArray;

                // Return screenshot as base64 string (can be attached to the Extent report directly)
                return Convert.ToBase64String(screenshotBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
                return null;
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

//         [AfterStep]
//         public void AfterStep()
//         {
//             string stepText = ScenarioContext.Current.StepContext.StepInfo.Text;

//             // Capture screenshot after each step
//             var screenshotPath = TakeScreenshot();
//             TakeScreenshot1();
//             string imgTag = $"<img src='data:image/png;base64,{screenshotPath}' width='600px' />";
// string screenshotFilePath = ConvertBase64ToImage(screenshotPath);
//             if (ScenarioContext.Current.TestError == null)
//             {
                
//                 // Attach screenshot directly to the report for passed steps
//                 if (screenshotPath != null)
//                 {

//                     // Directly attach the screenshot from memory
//                     scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotPath).Build());
//                 }
//                 else
//                 {
//                     scenario.Log(Status.Pass, stepText);
//                 }
//             }
//             else
//             {
//                 TakeScreenshot1();
//                 // Attach screenshot directly to the report for failed steps
//                 if (screenshotPath != null)
//                 {
//                     // Directly attach the screenshot from memory
//                     scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotPath).Build());
//                 }
//                 else
//                 {
//                     scenario.Log(Status.Fail, stepText);
//                 }

//                 // Log the actual error message
//                 scenario.Log(Status.Fail, ScenarioContext.Current.TestError.Message);
//             }
//         }
        [AfterStep]
public void AfterStep()
{
    string stepText = ScenarioContext.Current.StepContext.StepInfo.Text;

    // Capture screenshot after each step
    var screenshotPath = TakeScreenshot();

    if (!string.IsNullOrEmpty(screenshotPath))
    {
        // Prepare the image tag to embed the base64 image inline
        string imgTag = $"<img src='data:image/png;base64,{screenshotPath}' width='400px' />";
        
        if (ScenarioContext.Current.TestError == null)
        {
            // Attach screenshot directly to the report for passed steps
            scenario.Log(Status.Pass, stepText + imgTag);  // Append the image tag to the step text
        }
        else
        {
            // Attach screenshot directly to the report for failed steps
            scenario.Log(Status.Fail, stepText + imgTag);  // Append the image tag to the step text

            // Log the actual error message if any
            scenario.Log(Status.Fail, ScenarioContext.Current.TestError.Message);
        }
    }
    else
    {
        // Handle case when screenshot was not captured (optional)
        scenario.Log(Status.Info, stepText);
    }
}


        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            // Flush the ExtentReport after all scenarios have finished
            extent.Flush();
        }

        private string TakeScreenshot()
        {
            try
            {
                if (driver == null)
                {
                    return null;
                }

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                byte[] screenshotBytes = screenshot.AsByteArray;

                // Return screenshot as base64 string (can be attached to the Extent report directly)
                return Convert.ToBase64String(screenshotBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
                return null;
            }
        }


        private void TakeScreenshot1()
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
        private string ConvertBase64ToImage(string base64String)
        {
            try
            {
                // Generate the file name based on current timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotFileName = $"Screenshot_{timestamp}.png";
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, screenshotFileName);

                // Convert base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Write the byte array to the file system as an image
                File.WriteAllBytes(screenshotFilePath, imageBytes);

                // Return the file path
                return screenshotFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while saving base64 to image: " + ex.Message);
                return null;
            }
        }


    }
}
